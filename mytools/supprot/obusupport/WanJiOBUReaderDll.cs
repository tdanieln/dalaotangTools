using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class WanJiOBUReaderDll
    {
        const string DLL_PATH_WANJI_OBU = @".\dll\wjOBUAPI_log.dll";

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_DevInit")]
        public unsafe static extern int OBU_Prog_DevInit(int mode, string deviceNo, int portNo);

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_Write_SysInfo")]
        public unsafe static extern int OBUProg_Write_SysInfo(ref SysInfoType struSystemInfo);

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_Read_SysInfo")]
        public unsafe static extern int OBUProg_Read_SysInfo(ref SysInfoType struSystemInfo);

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_Write_ETCVehicleInfo")]
        public unsafe static extern int OBUProg_Write_ETCVehicleInfo(ref ETCVehicleInfoType struETCVehicleInfoType);

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_Read_ETCVehicleInfo")]
        public unsafe static extern int OBUProg_Read_ETCVehicleInfo(ref ETCVehicleInfoType struETCVehicleInfoType);

        [DllImport(DLL_PATH_WANJI_OBU, EntryPoint = "OBUProg_DevRelease")]
        public unsafe static extern int OBUProg_DevRelease();
    }
}
