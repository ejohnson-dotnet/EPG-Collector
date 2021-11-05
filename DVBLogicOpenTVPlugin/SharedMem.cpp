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

#include <tchar.h>
#include <stdio.h>

#include "SharedMem.h"

static HANDLE shared_handle = INVALID_HANDLE_VALUE;
static SHARED_DATA *shared_mem = NULL;
static int processIdentity = -1;
static int instanceIdentity = -1;
static int memorySize = 0;

int createSharedMemory(int processID, int identity, HANDLE m_hFile)
{
	/*if (shared_mem != NULL)
		return(0);

	shared_mem = (SHARED_DATA*) malloc(shareDataSize);
	if (shared_mem == NULL)
		return(-1);

	memset(shared_mem, 0, sizeof(SHARED_DATA));
	
	for (int index = 0; index < 32; index++)
		shared_mem->pids[index] = -1;

	return (0);*/

	/*SECURITY_ATTRIBUTES SA_ShMem;
	PSECURITY_DESCRIPTOR pSD_ShMem;

	pSD_ShMem = (PSECURITY_DESCRIPTOR)LocalAlloc(LPTR, SECURITY_DESCRIPTOR_MIN_LENGTH);

	if (pSD_ShMem == NULL)
		return -1;
	if (!InitializeSecurityDescriptor(pSD_ShMem, SECURITY_DESCRIPTOR_REVISION))
		return -2;
	if (!SetSecurityDescriptorDacl(pSD_ShMem, TRUE, (PACL)NULL, FALSE))
		return -3;

	SA_ShMem.nLength = sizeof(SA_ShMem);
	SA_ShMem.lpSecurityDescriptor = pSD_ShMem;
	SA_ShMem.bInheritHandle = TRUE;*/

	logString(m_hFile, "\r\nCreate shared memory called");

	processIdentity = processID;
	instanceIdentity = identity;

	TCHAR* fullIdentityString = new TCHAR[64];
	_stprintf_s(fullIdentityString, 64, _T("DVBLogic Plugin Shared Memory %d-%d"), processID, identity);

	/*TCHAR szName[] = TEXT("DVBLogic Plugin Shared Memory");
	shared_handle = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, sizeof(SHARED_DATA), fullIdentityString);*/

	logString(m_hFile, "\r\nOpening file mapping");
	shared_handle = OpenFileMapping(FILE_MAP_WRITE, false, fullIdentityString);

	if (shared_handle == NULL)
	{
		logString(m_hFile, "\r\nReturning error -4");
		return -4;
	}

	logString(m_hFile, "\r\nMapping view of file");
	shared_mem = (SHARED_DATA*)MapViewOfFile(shared_handle, FILE_MAP_WRITE, 0, 0, 0);

	if (shared_mem == NULL)
	{
		logString(m_hFile, "\r\nReturning error -5");
		CloseHandle(shared_handle);
		return -5;
	}

	logString(m_hFile, "\r\nGetting shared memory size");

	MEMORY_BASIC_INFORMATION basicInfo;

	SIZE_T size = VirtualQuery(shared_mem, &basicInfo, sizeof(basicInfo));
	memorySize = basicInfo.RegionSize;

	char* logMessage = new char[64];
	logString(m_hFile, "\r\n");
	sprintf_s(logMessage, 64, "Memory buffer is %d bytes", memorySize);
	logString(m_hFile, logMessage);
	
	memset(shared_mem, 0, memorySize);

	return 0;
}

void closeSharedMemory(HANDLE m_hFile)
{
	/*if (shared_mem != NULL)
		free(shared_mem);

	shared_mem = NULL;*/

	if (shared_mem != NULL)
	{
		UnmapViewOfFile(shared_mem);
	}

	if (shared_handle != NULL)
	{
		CloseHandle(shared_handle);
	}

	shared_mem = NULL;
	shared_handle = INVALID_HANDLE_VALUE;
}

SHARED_DATA* getSharedMemory(HANDLE m_hFile)
{
	return (shared_mem);
}

void setSharedMemory(LPVOID memoryBuffer, HANDLE m_hFile)
{
	shared_mem = (SHARED_DATA*)memoryBuffer;
	memset(shared_mem, 0, 72);
}

BOOL isOpen(HANDLE m_hFile)
{
	if (shared_mem == NULL && processIdentity != -1)
	{
		logString(m_hFile, "\r\nisOpen creating shared memory");
		int reply = createSharedMemory(processIdentity, instanceIdentity, m_hFile);
		if (reply != 0)
		{
			char* logMessage = new char[64];
			logString(m_hFile, "\r\n");
			sprintf_s(logMessage, 64, "isOpen creating shared memory failed %d", reply);
			logString(m_hFile, logMessage);
			return(false);
		}
	}

	return (shared_mem != NULL);
}

void logString(HANDLE m_hFile, char *sz)
{
	if (m_hFile == INVALID_HANDLE_VALUE)
		return;

	DWORD dwWritten;
	WriteFile(m_hFile, sz, (DWORD)strlen(sz), &dwWritten, NULL);
}

int sharedDataSize(HANDLE m_hFile)
{
	return(memorySize);
}

int reservedDataSize(HANDLE m_hfile)
{
	if (shared_mem == NULL)
		return(0);

	return(sizeof(shared_mem->currentPointer) + sizeof(shared_mem->clearCount) + sizeof(shared_mem->pids));
}
