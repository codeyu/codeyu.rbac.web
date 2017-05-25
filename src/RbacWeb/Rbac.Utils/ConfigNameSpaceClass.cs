using Murphy.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Murphy.Utils
{
    public class ConfigNameSpaceClass
    {
        public ConfigNameSpaceClass()
        {
            XmlFileExists();
        }

        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\NameSpaceClass.xml",PublicMethod.GetAppPath());

        /// <summary>
        /// 检查配置文件是否存在，没有则创建
        /// </summary>
        private void XmlFileExists()
        {
            FileInfo fiXML = new FileInfo(XmlFile);
            if (!(fiXML.Exists))
            {
                XDocument xelLog = new XDocument(
                    new XDeclaration("1.0", "utf-8", string.Empty),
                    new XElement("root")
                 );
                xelLog.Save(XmlFile);
            }
        }

        /// <summary>
        /// 得到所有类命名空间
        /// </summary>
        /// <returns></returns>
        public List<DataBaseNameSpaceClass> GetAll()
        {
            List<DataBaseNameSpaceClass> list = new List<DataBaseNameSpaceClass>();
            XElement xelem = XElement.Load(XmlFile);
            var queryXML = from xele in xelem.Elements("namespaceclass")
                           select new
                           {
                               entity = xele.Element("entity").Value,
                               dal = xele.Element("dal").Value,
                               business = xele.Element("business").Value,
                               idal = xele.Element("idal").Value
                           };
            foreach (var q in queryXML)
            {
                list.Add(new DataBaseNameSpaceClass()
                {
                    Entity = q.entity,
                    Dal = q.dal,
                    Business = q.business,
                    IDal = q.idal
                });
            }
            return list;
        }

        /// <summary>
        /// 添加一个类命名空间
        /// </summary>
        /// <param name="cns"></param>
        public bool Add(DataBaseNameSpaceClass cns)
        {
            //先删除
            Delete(cns.Entity);
            XElement xelem = XElement.Load(XmlFile);
            XElement newLog = new XElement("namespaceclass",
                                  new XElement("entity", cns.Entity),
                                  new XElement("dal", cns.Dal),
                                  new XElement("business", cns.Business),
                                  new XElement("idal", cns.IDal)
                              );
            xelem.Add(newLog);
            xelem.Save(XmlFile);
            return true;
        }

        /// <summary>
        /// 删除一个命名空间
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(string entity)
        {
            XElement xelem = XElement.Load(XmlFile);
            var queryXML = from xele in xelem.Elements("namespaceclass")
                           where xele.Element("entity").Value == entity
                           select xele;
            queryXML.Remove();
            xelem.Save(XmlFile);
            return true;
        }

        /// <summary>
        /// 保存类命名空间
        /// </summary>
        /// <param name="cns"></param>
        /// <returns></returns>
        public bool Save(DataBaseNameSpaceClass cns, string oldmodel = "")
        {
            if (!oldmodel.IsNullOrEmpty())
                Delete(oldmodel);
            return Add(cns);
        }

        /// <summary>
        /// 查询一个命名空间
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataBaseNameSpaceClass Get(string entity)
        {
           XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespaceclass")
                               where xele.Element("entity").Value == entity
                               select new
                               {
                                   entity = xele.Element("entity").Value,
                                   dal = xele.Element("dal").Value,
                                   business = xele.Element("business").Value,
                                   idal = xele.Element("idal").Value
                               };
                DataBaseNameSpaceClass cns = new DataBaseNameSpaceClass();
                if (queryXML.Count() > 0)
                {
                    cns.Entity = queryXML.First().entity;
                    cns.Dal = queryXML.First().dal;
                    cns.Business = queryXML.First().business;
                    cns.IDal = queryXML.First().idal;
                }
                return cns;
        }

        /// <summary>
        /// 查询默认命名空间
        /// </summary>
        /// <returns></returns>
        public DataBaseNameSpaceClass GetDefault()
        {
            var list = GetAll();
            if (list.Count == 0)
            {
                return new DataBaseNameSpaceClass()
                {
                    Entity = "Entity",
                    Dal = "Data.SQLServer",
                    Business = "Business",
                    IDal = "IData"
                };
            }
            else
            {
                return list.Last();
            }
        }
    }
}
