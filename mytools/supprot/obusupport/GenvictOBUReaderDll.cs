using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class GenvictOBUReaderDll
    {
        const string DLL_PATH_GENVICT_OBU = @".\dll\genvict_handyrsu_obu_api.dll";

        [DllImport(DLL_PATH_GENVICT_OBU, EntryPoint = "RSU_Open")]
        public unsafe static extern long RSU_Open(int mode, string dev, int port);


        [DllImport(DLL_PATH_GENVICT_OBU, EntryPoint = "RSU_Close")]
        public unsafe static extern long RSU_Close();
    }
}
