////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright (C) 2005-2016 nzsjb                                           //
//                                                                              //
//  This Program is free software; you can redistribute it and/or modify        //
//  it under the terms of the GNU General Public License as published by        //
//  the Free Software Foundation; either version 2, or (at your option)         //
//  any later version.                                                          //
//                                                                              //
//  This Program is distributed in the hope that it will be useful,             //
//  but WITHOUT ANY WARRANTY; without even the implied warranty of              //
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                //
//  GNU General Public License for more details.                                //
//                                                                              //
//  You should have received a copy of the GNU General Public License           //
//  along with GNU Make; see the file COPYING.  If not, write to                //
//  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.       //
//  http://www.gnu.org/copyleft/gpl.html                                        //
//                                                                              //  
//////////////////////////////////////////////////////////////////////////////////

#include <string>
#include <stdio.h>
#include <tchar.h>
#include <sys/stat.h>

#include "direct.h"
#include "stdafx.h"
#include "TVSIEPGPlugin.h"
#include "DVBLogicPluginInterface.h"
#include "DVBLogicPluginGateway.h"
#include "SharedMem.h"

static DVBLogicPluginInterface g_EPGScanner;

ITVSEPGPlugin* __stdcall TVSGetEPGPluginIf()
{
    return &g_EPGScanner;
}

void __stdcall TVSReleaseEPGPluginIf(ITVSEPGPlugin* plugin_if)
{
}

ITVSEPGPluginHost* hostControl;
int lastClearCount = 0;
HANDLE m_hFile = INVALID_HANDLE_VALUE;
const wchar_t* workingDir;
HANDLE mutex = INVALID_HANDLE_VALUE;
int monitorReference;
int sampleCount = 0;
int maxSampleSize = 0;
IDLLogging* logger = NULL;
bool scanFailed = false;

DVBLogicPluginInterface::DVBLogicPluginInterface()
{
	/*m_hFile = CreateFileA("DVBLogic Plugin.log",	// The filename
		GENERIC_WRITE,				// File access
        FILE_SHARE_READ,			// Share access
        NULL,						// Security
        CREATE_ALWAYS,				// Open flags
        (DWORD) 0,					// More flags
        NULL);						// Template*/

	LogString("\r\nConstructor returning");
}

DVBLogicPluginInterface::~DVBLogicPluginInterface()
{
	if(m_hFile != INVALID_HANDLE_VALUE)
	{
		CloseHandle(m_hFile);
		m_hFile = INVALID_HANDLE_VALUE;
	}
}

bool __stdcall DVBLogicPluginInterface::Init(SEPGPluginHostInfo* host_info, const wchar_t* working_dir)
{
	LogString("\r\nInit called");

	logger = host_info->log_interface;
	logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Init called");

	SetCurrentDirectory(working_dir);

	if (mutex == INVALID_HANDLE_VALUE)
		mutex = CreateMutexA(NULL, false, "DVBLogic Plugin");
	
	workingDir = working_dir;

	logger = host_info->log_interface;
	logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Init returning");

	LogString("\r\nInit returning");

	return(true);
}

bool __stdcall DVBLogicPluginInterface::Term()
{
	LogString("\r\nTerm called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Term called");

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Term returning");
	LogString("\r\nTerm returning");
	return(true);
}

bool __stdcall DVBLogicPluginInterface::GetPluginInfo(char* info_buf, long& info_buf_size)
{
	LogString("\r\nGetPluginInfo called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: GetPluginInfo called");

    bool ret_val = false;

    char* szPLuginInfo = 
        "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" \
        "<PluginInfo>" \
            "<name>EPG Collector for DVBLogic</name>" \
            "<version>4.3.0</version>" \
            "<info>Multi-protocol EPG downloader</info>" \
            "<isconfigurable>no</isconfigurable>" \
            "<owntunercontrol>no</owntunercontrol>" \
        "</PluginInfo>";

    if (info_buf_size >= (long)strlen(szPLuginInfo)+1)
    {
        strcpy_s(info_buf, info_buf_size, szPLuginInfo);
        ret_val = true;
    }

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: GetPluginInfo returning");
	LogString("\r\nGetPluginInfo returning");	
    
	return ret_val;
}

bool __stdcall DVBLogicPluginInterface::Configure(HWND parent_wnd)
{
    LogString("\r\nConfigure called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Configure called");

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Configure returning");
	LogString("\r\nConfigure returning");
	return(true);	
}

bool __stdcall DVBLogicPluginInterface::InitScanner(ITVSEPGPluginHost* host_control)
{
	LogString("\r\nInitScanner called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: InitScanner called");

	hostControl = host_control;

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: InitScanner returning");
	LogString("\r\nInitScanner returning");
	return(true);
}

bool __stdcall DVBLogicPluginInterface::TermScanner()
{
    LogString("\r\nTermScanner called\r\n");
	if (logger != NULL)
	{
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: TermScanner called");
	
		wchar_t* logMessage = new wchar_t[128];
		swprintf(logMessage, 128, L"EPG Collector Plugin: Sample count = %d max sample size = %d", sampleCount, maxSampleSize);
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, logMessage);
	}

	char* logMessage = new char[128];
	sprintf_s(logMessage, 128, "Sample count = %d Max sample size = %d", sampleCount, maxSampleSize);
	LogString(logMessage);

	if (mutex != INVALID_HANDLE_VALUE)
	{
		LogString("\r\nTermScanner waiting for shared memory access");
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: TermScanner waiting for shared memory access");

		WaitForSingleObject(mutex, 5000);
		closeSharedMemory(m_hFile);
		ReleaseMutex(mutex);

		LogString("\r\nTermScanner closed shared memory");
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: TermScanner closed shared memory");
	}

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: TermScanner returning");
	LogString("\r\nTermScanner returning");
	return(true);
}

bool __stdcall DVBLogicPluginInterface::StartScan(char* scan_info)
{
	LogString("\r\nStartScan called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan called");

	scanFailed = false;

	DVBLogicPluginGateway* gateway = DVBLogicPluginGateway::GetInstance();

	LogString("\r\nStartScan calling gateway init");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: Initializing gateway");

	monitorReference = gateway->Init(workingDir);
	if (monitorReference < 1)
	{
		if (logger != NULL)
		{
			wchar_t* logMessage = new wchar_t[256];
			swprintf(logMessage, 256, L"EPG Collector Plugin: Gateway initialisation failed error code %d", monitorReference);
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, logMessage);

			char* lastError = gateway->GetLastError();
			char* fullMessage = new char[512];
			sprintf_s(fullMessage, 512, "EPG Collector Plugin: %s", lastError);
			wchar_t* errorMessage = new wchar_t[strlen(fullMessage) + 1];
			mbstowcs_s(NULL, errorMessage, strlen(fullMessage) + 1, fullMessage, strlen(fullMessage));
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, errorMessage);
		}

		char* logMessage = new char[128];
		sprintf_s(logMessage, 128, "\r\nGateway initialisation failed: error code %d", monitorReference);
		LogString(logMessage);
		
		char* logMessage1 = new char[512];
		sprintf_s(logMessage, 512, gateway->GetLastError());
		LogString(logMessage);

		scanFailed = true;

		LogString("\r\nStartScan returning false");
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan returning false");	

		return(false);
	}
	else
	{
		if (logger != NULL)
		{
			wchar_t* logMessage = new wchar_t[128];
			swprintf(logMessage, 128, L"EPG Collector Plugin: Monitor reference is %d", monitorReference);
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, logMessage);
		}

		char* logMessage = new char[128];
		sprintf_s(logMessage, 128, "\r\nMonitor reference is %d", monitorReference);
		LogString(logMessage);
	}

	if (mutex == INVALID_HANDLE_VALUE)
	{
		LogString("\r\nStartScan creating mutex");
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan creating mutex");

		TCHAR* fullIdentityString = new TCHAR[64];
		_stprintf_s(fullIdentityString, 64, _T("DVBLogic Plugin Resource Mutex %d-%d"), gateway->GetProcessID(), monitorReference);
		
		mutex = CreateMutexA(NULL, false, "DVBLogic Plugin");
	}

	LogString("\r\nStartScan calling create shared memory");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan creating shared memory");

	createSharedMemory(gateway->GetProcessID(), monitorReference, m_hFile);
	SHARED_DATA *data  = getSharedMemory(m_hFile);
	if (data == NULL)
	{
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan did not create shared memory");
		LogString("\r\nStartScan did not create shared memory");
	}
	else
	{
		if (logger != NULL)
			logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan created shared memory");
		LogString("\r\nStartScan did create shared memory");
	}

	lastClearCount = 0;

	sampleCount = 0;
	maxSampleSize = 0;
	
	LogString("\r\nStartScan calling gateway start scan");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan calling gateway to start collection");
	bool reply = gateway->StartScan(monitorReference, scan_info);

	scanFailed = !reply;

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StartScan returning");
	LogString("\r\nStartScan returning");
	return(reply);
}

bool __stdcall DVBLogicPluginInterface::StopScan()
{
	LogString("\r\nStopScan called\r\n");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StopScan called");

	if (logger != NULL)
	{
		wchar_t* logMessage = new wchar_t[128];
		swprintf(logMessage, 128, L"EPG Collector Plugin: Sample count = %d max sample size = %d", sampleCount, maxSampleSize);
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, logMessage);
	}

	char* logMessage = new char[128];
	sprintf_s(logMessage, 128, "Sample count = %d Max sample size = %d", sampleCount, maxSampleSize);
	LogString(logMessage);

	DVBLogicPluginGateway* gateway = DVBLogicPluginGateway::GetInstance();
	bool reply = gateway->StopScan(monitorReference);

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: StopScan returning");
	LogString("\r\nStopScan returning");
    return(reply);
}

EEPGPLuginScanStatus __stdcall DVBLogicPluginInterface::GetScanStatus()
{
	LogString("\r\nGetScanStatus called");

	if (scanFailed)
	{
		LogString("\r\nGetScanStatus returning finished in error");
		return(EEPSS_FINISHED_ERROR);
	}

	if (isOpen(m_hFile))
	{		
		LogString("\r\nMemory buffer is open");

		SHARED_DATA *data  = getSharedMemory(m_hFile);

		if (data != NULL)
			LogString("\r\nMemory buffer has an address");
		else
			LogString("\r\nMemory buffer has a null address");

		if (data->clearCount != lastClearCount)
		{
			LogString("\r\nSetting new pids");

			bool done = false;

			for (int index = 0; index < 32 && !done; index++)
			{
				if (data->pids[index] != -1)
					hostControl->AddPid(data->pids[index]);
				else
					done = true;
			}

			LogString("\r\nClearing current pointer");

			data->currentPointer = 0;
			lastClearCount = data->clearCount;
			LogString("\r\nPids changed");
		}
	}
	else
		LogString("\r\nMemory buffer is not open");

	DVBLogicPluginGateway* gateway = DVBLogicPluginGateway::GetInstance();
	EEPGPLuginScanStatus status = gateway->GetScanStatus(monitorReference);

    LogString("\r\nGetScanStatus returning");
	return(status);
}

bool __stdcall DVBLogicPluginInterface::GetEPGData(char* epg_buf, long& epg_buf_size)
{
    LogString("\r\nGetEPGData called");
	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: GetEPGData called");

	if (logger != NULL)
	{
		wchar_t* logMessage = new wchar_t[128];
		swprintf(logMessage, 128, L"EPG Collector Plugin: Sample count = %d max sample size = %d", sampleCount, maxSampleSize);
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, logMessage);
	}

	char* logMessage = new char[128];
	sprintf_s(logMessage, 128, "Sample count = %d Max sample size = %d", sampleCount, maxSampleSize);
	LogString(logMessage);

	/*const char[] fileName = "\\DVBLogic Plugin Output.xml"; 

	struct stat fileInfo;

	size_t origSize = wcslen(workingDir) + 1;
	const size_t newSize = 262;
	size_t convertedChars = 0;
	char nstring[newSize];
	wcstombs_s(&convertedChars, nstring, origSize, workingDir, _TRUNCATE);

	int reply = stat(nstring, &fileInfo);

	if (epg_buf_size == 0)
	{
		if (reply == 0)
			epg_buf_size = fileInfo.st_size + 1;
		return (true);
	}

	HANDLE dataFile = CreateFile(workingDir,	// The filename
		GENERIC_READ,				// File access
        FILE_SHARE_READ,			// Share access
        NULL,						// Security
        OPEN_EXISTING,				// Open flags
        (DWORD) 0,					// More flags
        NULL);						// Template
	if (dataFile == INVALID_HANDLE_VALUE)
	{
		epg_buf_size = 0;
		return(true);
	}

	DWORD bytesRead;
	BOOL readReply = ReadFile(dataFile, epg_buf, fileInfo.st_size, &bytesRead, NULL);

	CloseHandle(dataFile);
	DeleteFile(workingDir);

	if (!readReply)
	{
		epg_buf_size = 0;
		return(true);
	}
	else
	{
		epg_buf[fileInfo.st_size] = 0x00;
		epg_buf_size = fileInfo.st_size + 1;
		return(true);
	}*/

	DVBLogicPluginGateway* gateway = DVBLogicPluginGateway::GetInstance();
	bool reply = gateway->GetEPGData(monitorReference, epg_buf, epg_buf_size);

	if (logger != NULL)
		logger->LogMessage(MBGE_LL_EXTENDED_INFO, L"EPG Collector Plugin: GetEPGData returning");
	LogString("\r\nGetEPGData returning");
	
	return(reply);
}

bool __stdcall DVBLogicPluginInterface::WriteStream(BYTE* pbData, int lDataLength)
{
	if (m_hFile != INVALID_HANDLE_VALUE)
	{
		char* logMessage = new char[64];
		LogString("\r\n");
		sprintf_s(logMessage, 64, "WriteStream called sample size = %d", lDataLength);
		LogString(logMessage);
	}

	sampleCount++;
	if (lDataLength > maxSampleSize)
		maxSampleSize = lDataLength;

	int offset = 0;

	WaitForSingleObject(mutex, 5000);

	while (offset < lDataLength)
	{
		if (pbData[offset] != 0x47)
		{
			ReleaseMutex(mutex);
			LogString("\r\nWriteStream returning (b)");
			return(true);
		}

		if (!isOpen(m_hFile))
		{
			LogString(" ignored (memory buffer closed)");
			ReleaseMutex(mutex);
			LogString("\r\nWriteStream returning (a)");
			return(true);
		}

		SHARED_DATA *data  = getSharedMemory(m_hFile);
		if (data == NULL)
		{
			LogString(" ignored (memory buffer not available)");
			ReleaseMutex(mutex);
			LogString("\r\nWriteStream returning (e)");
			return(true);
		}

		int pid = ((pbData[offset + 1] & 0x1f) * 256) + pbData[offset + 2];

		/*char *pidChars = new char[10];
		memset(pidChars, '\0', 10);
		_itoa_s(pid, pidChars, 10, 10);
		LogString("\r\nPID ");
		LogString(pidChars);*/		

		int found = 0;

		for (int index = 0; index < 32 && found == 0; index++)
		{
			if (data->pids[index] == pid)
				found = 1;
			else
			{
				if (data->pids[index] == -1)
					found = -1;
			}
		}

		if (found != 1)
			LogString(" ignored");
		else
		{		
			if((data->currentPointer + 188 + reservedDataSize(m_hFile)) < sharedDataSize(m_hFile))
			{
				memcpy((void*)(data->data + data->currentPointer), pbData + offset, 188);
				
				/*char* logMessage = new char[64];
				sprintf_s(logMessage, 64, " stored at %d", data->currentPointer);
				LogString(logMessage);*/

				data->currentPointer += 188;
			}
		}

		offset+= 188;
	}

	ReleaseMutex(mutex);	
	
	LogString("\r\nWriteStream returning (d)");
	return(true);
}

void __stdcall DVBLogicPluginInterface::LogString(char *sz)
{
	if (m_hFile == INVALID_HANDLE_VALUE)
		return;

	DWORD dwWritten;
	WriteFile(m_hFile, sz, (DWORD)strlen(sz), &dwWritten, NULL);
}

int __stdcall DVBLogicPluginInterface::GetMaxSampleSize()
{
	LogString("\r\nGetMaxSampleSize called");

	LogString("\r\nGetMaxSampleSize returning");
	return(maxSampleSize);
}
