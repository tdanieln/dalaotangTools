using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mytools
{
    /// <summary>
    /// 日志类
    /// </summary>
    class Log
    {
        public static string LogPath = "";

        static Log()
        {

        }

        /// <summary>
        /// 写日志信息文件
        /// </summary>
        /// <param name="logInfo"></param>
        private static void SaveLog(string logInfo)
        {
            LogPath = MytoolsIniConstant.MytoolsConfigPath + "\\log\\";

            //如果不存在log文件夹就创建file文件夹
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            //如果不存在log文件夹下的以年月命名的文件夹就创建file文件夹
            string fileName = LogPath +"MytoolsLog"+ DateTime.Now.ToString("yyyyMMdd") +".txt";

            StreamWriter sWriter = null;

            try
            {
                sWriter = new StreamWriter(fileName, true, System.Text.Encoding.GetEncoding("GBK"));

                sWriter.Write(logInfo);
            }
            catch(Exception err)
            {
                Console.WriteLine("写日志信息失败" + err.Message);
            }
            finally
            {
                //如果流不为空,关闭它
                if (sWriter != null)
                    sWriter.Close();
            }
        }

        /// <summary>
        /// 写log并显示在控制台上
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            info = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\t") + info + "\r\n";
            Console.WriteLine(info);
            SaveLog(info);
        }

    }
}
