/*#include <string>

#include "stdafx.h"
#include "TVSIEPGPlugin.h"
#include "DVBLogicPluginGateway.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

using namespace DVBLogicPlugin;

DVBLogicPluginGateway::DVBLogicPluginGateway() { }

DVBLogicPluginGateway::~DVBLogicPluginGateway() { }

bool __stdcall DVBLogicPluginGateway::Init(const wchar_t* working_dir)
{
	String ^nameString = gcnew String("C:\\Program Files\\Geekzone\\EPG Collector\\DVBLogicPlugin.dll");
	
	Assembly ^assembly = Assembly::LoadFrom(nameString);
	Type ^type = assembly->GetType("DVBLogicPlugin.PluginController");
		
	PropertyInfo ^propertyInfo = type->GetProperty("Instance");
	Object^ pluginController = propertyInfo->GetValue(nullptr, nullptr); 
	
	MethodInfo ^method = type->GetMethod("Init");
	String ^dirString = gcnew String(working_dir);
	Object^ reply = method->Invoke(pluginController, gcnew array<Object^>(1){dirString});

	return(reply == true);
}

bool __stdcall DVBLogicPluginGateway::StartScan(ITVSEPGPluginHost* host_control, LPVOID data, char* scan_info)
{
	IntPtr ^bufferAddress = gcnew IntPtr((LPVOID)data);

    PluginController pluginController = PluginController::Instance;
	return(pluginController.StartScan(bufferAddress->ToInt32(), (System::IntPtr)scan_info));
}

bool __stdcall DVBLogicPluginGateway::StopScan()
{
    PluginController pluginController = PluginController::Instance;
	return(pluginController.StopScan());
}

EEPGPLuginScanStatus __stdcall DVBLogicPluginGateway::GetScanStatus()
{
    PluginController pluginController = PluginController::Instance;
	int status = pluginController.GetScanStatus();

	switch (status)
	{
	case 0:
		return(EEPSS_UNKNOWN);
	case 1:
		return(EEPSS_IN_PROGRESS);
	case 2:
		return(EEPSS_FINISHED_ERROR);
	case 3:
		return(EEPSS_FINISHED_SUCCESS);
	case 4:
		return(EEPSS_FINISHED_ABORTED);
	}

	return(EEPSS_UNKNOWN);	
}

bool __stdcall DVBLogicPluginGateway::GetEPGData(char* epg_buf, long& epg_buf_size)
{
    PluginController pluginController = PluginController::Instance;
	return(pluginController.GetEPGData((System::IntPtr)epg_buf, (int)&epg_buf_size));
}*/
