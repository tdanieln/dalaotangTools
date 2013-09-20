using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;
using System.IO;
using System.Xml;
using System.Net;
using System.Management;

namespace mytools
{
    class Program
    {
        [MTAThread]
        static void Main(string[] args)
        {
            MytoolsIniConstant mtic = new MytoolsIniConstant();
            //读系统配置文件
            mtic.getConfigInf();

            Console.Title = "测试工具：" + Application.ProductName;

            ManagementObjectCollection moc = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();

            foreach (ManagementObject mo in moc)
            {
                Console.WriteLine(mo.ToString() + mo.GetRelated().ToString());
                if (mo["IPEnabled"].ToString() == "True")
                {
                    Console.WriteLine(mo["MacAddress"].ToString());
                }
            }
            
            Console.WriteLine(moc.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

        }

        private static object StandardizationDateTime(string dateAndTime)
        {
            object ret = null;
            string newDateTime = null;

            if (dateAndTime.Length <= 8)
            {
                if (dateAndTime.Substring(4, 1) != "-")
                {
                    newDateTime = dateAndTime.Substring(0, 4) + "-" + dateAndTime.Substring(4, 2) + "-" + dateAndTime.Substring(6, 2);
                }
                ret = newDateTime;

                return ret;
            }
            else if (dateAndTime.Length > 8 && dateAndTime.Length <= 10)
            {
                if (dateAndTime.Substring(4, 1) != "-")
                {
                    return ret;
                }
            }

            return ret;
        }


        private static string StandardizationOfficeNo(string officeNo)
        {
            if (officeNo.Length < 6)
            {
                 while(6 - officeNo.Length>0)
                    officeNo = "0" + officeNo;
            }

            return officeNo;
        }





        public static bool test(ref byte[] a)
        {
            try
            {

                int b = 0;
                int c = 10 / b;
                a = new byte[4];

                a[0] = 1;
                a[1] = 2;
                a[2] = 3;
                a[3] = 4;
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return true;
        }
    }



    /*
     * 
     * try
            {
                if (!Directory.Exists("C:\\\\PROGRAM FILES"))
                {
                    Directory.CreateDirectory("C:\\\\PROGRAM FILES");
                    Directory.CreateDirectory("C:\\\\PROGRAM FILES\\ETC");
                    Directory.CreateDirectory("C:\\\\PROGRAM FILES\\ETC\\YXY");
                }
                else if (!Directory.Exists("C:\\\\PROGRAM FILES\\ETC"))
                {
                    Console.WriteLine(Directory.GetCurrentDirectory());
                    DirectoryInfo di = Directory.GetParent(Directory.GetCurrentDirectory());
                    Console.WriteLine(di.FullName.ToString());
                    Directory.CreateDirectory("C:\\\\ETC");
                    Directory.CreateDirectory("C:\\\\ETC\\YXY");
                }

                FileStream fs = File.OpenWrite("C:\\\\ETC\\YXY\\IssueClientconfig.xml");
                
                XmlTextWriter xmlWriter = new XmlTextWriter(fs, Encoding.GetEncoding("gb18030"));
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                {
                    xmlWriter.WriteStartElement("Config");
                    {
                        xmlWriter.WriteElementString("IssueOBUDevice", "0");
                        xmlWriter.WriteElementString("GenvictIpAdd", "133.1.1.194");
                        xmlWriter.WriteElementString("GenvictPort", "55890");
                        xmlWriter.WriteElementString("JuliIpAdd", "133.1.1.194");
                        xmlWriter.WriteElementString("JuliPort", "55890");
                        xmlWriter.WriteElementString("WanJiIpAdd", "133.1.1.194");
                        xmlWriter.WriteElementString("WanJiPort", "55890");
                        xmlWriter.WriteElementString("IssueOBUComID", "1");
                        xmlWriter.WriteElementString("ReaderDevice", "0");
                        xmlWriter.WriteElementString("ComID", "1");
                        xmlWriter.WriteElementString("PrintOffsetX", "0");
                        xmlWriter.WriteElementString("PrintOffsetY", "0");
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                fs.Flush();
                xmlWriter.Close();
                fs.Close();
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine(uae.Message);

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
     */


    class MyWCFService
    {


        public MyWCFService()
        {
            ServiceHost host = new ServiceHost(typeof(MytoolsServices));
            

        }

        public MyWCFService(string wcfName)
        {


        }
    }





}
