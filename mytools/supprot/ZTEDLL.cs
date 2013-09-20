using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class ZTEDLL
    {
        //开启读卡器
        [DllImport(@".\dll\RSUComm.dll")]
        public static extern IntPtr CT_open(
            string name,
            uint param1,
            uint param2);


        //关闭读卡器
        [DllImport(@".\dll\RSUComm.dll")]
        public static extern IntPtr CT_close(
            IntPtr fd);


        //选择卡片类型
        [DllImport(@".\dll\RSUComm.dll")]
        public static extern void ICC_set_NAD(
            IntPtr fd,
            byte nad);


        //向IC卡发送命令
        [DllImport(@".\dll\RSUComm.dll")]
        public static extern int ICC_tsi_api(
            IntPtr fd,
            int len,
            byte[] comm,
            ref byte lenr,
            byte[] resp);

    }

}