using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    /// <summary>
    /// OBU发行器透明通道模式
    /// </summary>
    public interface OBUReaderTransparent
    {
        /// <summary>
        /// 打开RSU设备
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dev"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        bool RSU_Open(int mode, string dev, int port,ref long fd, ref string errInfo);
    }
}
