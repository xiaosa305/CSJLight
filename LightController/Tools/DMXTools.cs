using LightController.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMXTools
    {
        private static DMXTools Instance { get; set; }
        private IList<SenceData> SenceDatas { get; set; }
        private int Mode { get; set; }

        private DMXTools()
        {
            
        }

        public static DMXTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DMXTools();
            }
            return Instance;
        }
       
        public IList<DMX_C_File> Get_C_Files(IList<SenceData> senceDatas, int mode)
        {
            this.SenceDatas = senceDatas;
            this.Mode = mode;
            IList<DMX_C_File> c_Datas = GetDMX_C_Files();
            return c_Datas;
        }

        public IList<DMX_M_File> Get_M_Files(IList<SenceData> senceDatas, int mode)
        {
            this.SenceDatas = senceDatas;
            this.Mode = mode;
            IList<DMX_M_File> m_Datas = GetDMX_M_Files();
            return m_Datas;
        }

        private IList<DMX_C_File> GetDMX_C_Files()
        {
            IList<DMX_C_File> c_Files = new List<DMX_C_File>();
            foreach (SenceData senceData in SenceDatas)
            {
                int senceNo = senceData.SenceNo;
                int chanelCount = senceData.ChanelCount;
                IList<ChanelData> chanelDatas = senceData.ChanelDatas;
                DMX_C_Data dMX_C_Data = GetDMX_C_Data(senceNo,chanelCount,chanelDatas);
                DMX_C_File c_File = new DMX_C_File
                {
                    SenceNo = senceNo,
                    Data = dMX_C_Data
                };
                c_Files.Add(c_File);
            }
            return c_Files;
        }

        private IList<DMX_M_File> GetDMX_M_Files()
        {
            IList<DMX_M_File> m_Files = new List<DMX_M_File>();
            foreach (SenceData senceData in SenceDatas)
            {
                int senceNo = senceData.SenceNo;
                int chanelCount = senceData.ChanelCount;
                IList<ChanelData> chanelDatas = senceData.ChanelDatas;
                DMX_M_Data dMX_M_Data = GetDMX_M_Data(senceNo, chanelCount, chanelDatas);
                DMX_M_File m_File = new DMX_M_File
                {
                    SenceNo = senceNo,
                    data = dMX_M_Data
                };
                m_Files.Add(m_File);
            }
            return m_Files;
        }

        private DMX_C_Data GetDMX_C_Data(int shenceNo,int chanelCount,IList<ChanelData> chanelDatas)
        {
            DMX_C_Data dMX_C_Data = new DMX_C_Data
            {
                HeadData = GetC_Head(chanelCount),
                Datas = GetC_Datas(chanelDatas)
            };
            foreach (C_Data data in dMX_C_Data.Datas)
            {
                dMX_C_Data.HeadData.FileSize += data.DataSize;
            }

            return dMX_C_Data;
        }

        private DMX_M_Data GetDMX_M_Data(int senceNo,int chanelCount,IList<ChanelData> chanelDatas)
        {
            DMX_M_Data dMX_M_Data = new DMX_M_Data
            {
                Datas = GetM_Datas(chanelDatas),
                HeadData = GetM_Heads(chanelCount)

            };
            foreach (M_Data data in dMX_M_Data.Datas)
            {
                dMX_M_Data.HeadData.FileSize += data.DataSize;
            }
            return dMX_M_Data;
        }

        private C_Head GetC_Head(int chanelCount)
        {
            C_Head head = new C_Head
            {
                MusicControl = 0,
                MICSensor = 0,
                SenseFreq = 0,
                RunTime = 0,
                ChanelCount = chanelCount,
                FileSize = 0
            };
            return head;
        }

        private M_Head GetM_Heads(int chanelCount)
        {
            M_Head head = new M_Head
            {
                ChanelCount = chanelCount,
                FileSize = 0,
                StepTimes = 0,
                FreameTime = 0
            };
            return head;
        }

        private IList<C_Data> GetC_Datas(IList<ChanelData> chanelDatas)
        {
            IList<C_Data> c_Datas = new List<C_Data>();
            foreach (ChanelData chanelData in chanelDatas)
            {
                c_Datas.Add(GetC_Data(chanelData));
            }
            return c_Datas;
        }

        private IList<M_Data> GetM_Datas(IList<ChanelData> chanelDatas)
        {
            IList<M_Data> m_Datas = new List<M_Data>();
            foreach (ChanelData chanelData in chanelDatas)
            {
                m_Datas.Add(GetM_Data(chanelData));
            }
            return m_Datas;
        }

        private C_Data GetC_Data(ChanelData chanelData)
        {
            int startValue = chanelData.StepValues[0];
            int stepCount = chanelData.StepCount;
            IList<int> datas = new List<int>();
            datas.Add(startValue);
            C_Data c_Data = new C_Data
            {
                ChanelNo = chanelData.ChanelNo
            };
            if (stepCount < 2)
            {
                for (int stepTime = 0; stepTime < chanelData.StepTimes[0] - 1; stepTime++)
                {
                    datas.Add(startValue);
                }
            }
            else
            {
                for (int step = 1; step < stepCount + 1; step++)
                {
                    int stepTime;
                    int stepValue;
                    int isGradualChange;
                    if (step == stepCount)
                    {
                        stepValue = chanelData.StepValues[0];
                        stepTime = chanelData.StepTimes[0];
                        isGradualChange = chanelData.IsGradualChange[0];
                    }
                    else
                    {
                        stepTime = chanelData.StepTimes[step];
                        stepValue = chanelData.StepValues[step];
                        isGradualChange = chanelData.IsGradualChange[step];
                    }
                    if (isGradualChange == Constant.MODE_GRADUAL)
                    {
                        double Inc = (stepValue - startValue) / (stepTime * 1.0);
                        for (int stepTimeNum = 0; stepTimeNum < stepTime; stepTimeNum++)
                        {
                            if (step == stepCount && stepTimeNum == stepTime - 1)
                            {
                                break;
                            }
                            datas.Add((int)Math.Floor(startValue + Inc * (stepTimeNum + 1)));
                        }
                        startValue = stepValue;
                    }
                    else
                    {
                        for (int stepNum = 0; stepNum < stepTime; stepNum++)
                        {
                            if (step == stepCount && stepNum == stepTime - 1)
                            {
                                break;
                            }
                            datas.Add(stepValue);
                        }
                        startValue = stepValue;
                    }
                }
            }
            c_Data.Datas = datas;
            c_Data.DataSize = datas.Count;
            return c_Data;
        }

        private M_Data GetM_Data(ChanelData chanelData)
        {
            int chanelNo = chanelData.ChanelNo;
            IList<int> stepValues = chanelData.StepValues;
            M_Data m_Data = new M_Data
            {
                ChanelNo = chanelNo,
                Datas = stepValues,
                DataSize = stepValues.Count
            };
            return m_Data;
        }
    }
}
