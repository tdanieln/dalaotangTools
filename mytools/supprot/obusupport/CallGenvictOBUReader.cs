using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class CallGenvictOBUReader : OBUReaderTransparent
    {
        public bool RSU_Open(int mode, string dev, int port, ref long fd,ref string errInfo)
        {
            bool operationOk = false;

            //int operateResult = (int)OperationResult.oprFault;

            try
            {
                fd = (int)GenvictOBUReaderDll.RSU_Open(mode, dev, port);

                if (fd < (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("金溢OBU发行器读系统信息失败，返回码：" + fd.ToString());
                    throw new Exception();
                }
                operationOk = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                operationOk = false;
            }
            return operationOk;
        }
    }
}
