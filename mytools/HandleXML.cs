using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Data;

namespace mytools
{
    /// <summary>
    /// HandleXml类放置处理XML文件的内容
    /// </summary>
    class HandleXML
    {
        public void readXML(XmlNode xmlNode)
        {
            //如果有子节点并且第一个子节点的类型不是文本类型，那么就去读子节点
            if (xmlNode.HasChildNodes && (xmlNode.FirstChild.NodeType != XmlNodeType.Text))
                readXML(xmlNode.FirstChild);
            //如果第一个子节点是文本类型，说明当前节点已经没有子节点了
            else
            {
                //如果当前节点有兄弟节点，那么去读兄弟节点
                if (xmlNode.NextSibling != null)
                    readXML(xmlNode.NextSibling);
                else
                    //如果当前节点没有兄弟节点了，那么去看父节点有没有兄弟节点
                    if (xmlNode.ParentNode.NextSibling != null)
                        readXML(xmlNode.ParentNode.NextSibling);
            }
        }


    }


}

