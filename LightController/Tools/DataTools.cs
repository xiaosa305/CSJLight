using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DataTools
    {
        private static DataTools Instance { get; set; }

        private DataTools()
        {

        }

        public static DataTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataTools();
            }
            return Instance;
        }

        /// <summary>
        /// 获取单个常规程序文件信息
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <returns></returns>
        public DMX_C_File GetC_File(DBWrapper dBWrapper,int senceNo)
        {
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            foreach (DMX_C_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    return value;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取单个常规程序数据
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <returns></returns>
        public DMX_C_Data GetC_Data(DBWrapper dBWrapper,int senceNo)
        {
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            foreach (DMX_C_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    return value.Data;
                }
            }
            return null;
        }
        /// <summary>
        /// 将单个常规程序数据写到文件
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <param name="path"></param>
        public void WriteC_DataToFile(DBWrapper dBWrapper,int senceNo,string path)
        {
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            foreach (DMX_C_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    value.WriteFile(path);
                }
            }
        }
        /// <summary>
        /// 获取单个音频程序文件信息
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <returns></returns>
        public DMX_M_File GetM_File(DBWrapper dBWrapper, int senceNo)
        {
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            foreach (DMX_M_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    return value;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取单个音频程序数据
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <returns></returns>
        public DMX_M_Data GetM_Data(DBWrapper dBWrapper,int senceNo)
        {
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            foreach (DMX_M_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    return value.Data;
                }
            }
            return null;
        }
        /// <summary>
        /// 将单个音频程序数据写到文件
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="senceNo"></param>
        /// <param name="path"></param>
        public void WriteM_DataToFile(DBWrapper dBWrapper,int senceNo,string path)
        {
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            foreach (DMX_M_File value in files)
            {
                if (value.SenceNo == senceNo)
                {
                    value.WriteFile(path);
                }
            }
        }
        /// <summary>
        /// 获取项目下所有常规程序文件信息
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <returns></returns>
        public IList<DMX_C_File> GetC_Files(DBWrapper dBWrapper)
        {
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            return files;
        }
        /// <summary>
        /// 获取所有常规程序数据
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <returns></returns>
        public IList<DMX_C_Data> GetC_Datas(DBWrapper dBWrapper)
        {
            IList<DMX_C_Data> c_Datas = new List<DMX_C_Data>();
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            foreach (DMX_C_File file in files)
            {
                c_Datas.Add(file.Data);
            }
            if (c_Datas.Count > 0)
            {
                return c_Datas;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 将所有常规程序数据写到文件
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="path"></param>
        public void WriteAllC_DataToFile(DBWrapper dBWrapper,string path)
        {
            IList<DMX_C_File> files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            foreach (DMX_C_File file in files)
            {
                file.WriteFile(path);
            }
        }
        /// <summary>
        /// 获取项目下所有音频程序文件信息
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <returns></returns>
        public IList<DMX_M_File> GetM_Files(DBWrapper dBWrapper)
        {
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            return files;
        }
        /// <summary>
        /// 获取项目下所有音频程序数据
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <returns></returns>
        public IList<DMX_M_Data> GetM_Datas(DBWrapper dBWrapper)
        {
            IList<DMX_M_Data> m_Datas = new List<DMX_M_Data>();
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            foreach (DMX_M_File file in files)
            {
                m_Datas.Add(file.Data);
            }
            if (m_Datas.Count > 0)
            {
                return m_Datas;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 将所有音频程序数据写到文件
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="path"></param>
        public void WriteAllM_DataToFile(DBWrapper dBWrapper,string path)
        {
            IList<DMX_M_File> files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            foreach (DMX_M_File file in files)
            {
                file.WriteFile(path);
            }
        }
        /// <summary>
        /// 获取全局配置数据
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <returns></returns>
        public DMXConfigData GetConfigData(DBWrapper dBWrapper,string filePath)
        {
            DMXConfigData configData = new DMXConfigData(dBWrapper, filePath);
            return configData;
        }
        /// <summary>
        /// 将全局配置数据写入文件
        /// </summary>
        /// <param name="dBWrapper"></param>
        /// <param name="path"></param>
        public void WriteConfigToFile(DBWrapper dBWrapper,string path)
        {
            DMXConfigData configData = new DMXConfigData(dBWrapper,path);
            configData.WriteToFile(path);
        }
    }
}
