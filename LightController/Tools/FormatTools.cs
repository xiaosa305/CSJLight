using DMX512;
using LightController.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace LightController.Tools
{ 
    public class FormatTools
    {
        private IList<DB_Light> DB_Lights { get; set; }
        private IList<DB_StepCount> DB_StepCounts { get; set; }
        private IList<DB_Value> DB_Values { get; set; }
        private int Mode { get; set; }
        private IList<int> SenceArray { get; set; }

        private IList<DMX_C_File> c_files { get; set; }
        private IList<DMX_M_File> m_files { get; set; }

        public FormatTools(IList<DB_Light> lights, IList<DB_StepCount> stepCounts, IList<DB_Value> values)
        {
            this.DB_Lights = lights;
            this.DB_StepCounts = stepCounts;
            this.DB_Values = values;
            this.GetSenceArray();
        }


        //Test
        public void Test()
        {
            //DMXTools toolsC = new DMXTools(GetC_SenceDatas(), Constant.MODE_C);
            //DMXTools toolsM = new DMXTools(GetM_SenceDatas(), Constant.MODE_M);
            //c_files = toolsC.Get_C_Files();
            //m_files = toolsM.Get_M_Files();
            Thread SerialThread = new Thread(new ThreadStart(SerialPortTest));
            //SerialThread.Start();
            SocketTest();
            int i = 0;
            while (true)
            {
                Console.WriteLine(i++ + " : ");
                Thread.Sleep(5000);
            }
        }
        
        //Test
        private void SocketTest()
        {
            SockTools tools = SockTools.GetInstance();
            IPAddress iPAddress = IPAddress.Parse("192.168.31.235");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2333);
            IPEndPoint udpIPEnd = new IPEndPoint(IPAddress.Any,2333);
            //tools.Start(iPEndPoint);
            tools.Start(udpIPEnd);
            int test = 0;
            while (true)
            {
                tools.UdpTest("UDPtest :" + test);
                //tools.Test("192.168.31.113");
                test++;
                Thread.Sleep(2000);
            }
           
        }

        //Test
        private void SerialPortTest()
        {
            SerialPortTools serialPort = new SerialPortTools();
            serialPort.Test(c_files[0]);
        }

        private void GetSenceArray()
        {
            SenceArray = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                foreach (DB_StepCount item in DB_StepCounts)
                {
                    if (i == item.PK.Frame)
                    {
                        SenceArray.Add(i);
                        break;
                    }
                }
            }
        }

        public IList<SceneData> GetC_SenceDatas()
        {
            this.Mode = Constant.MODE_C;
            IList<SceneData> senceDatas = new List<SceneData>();
            foreach (int item in SenceArray)
            {
                SceneData data = GetC_SenceData(SenceArray[item]);
                senceDatas.Add(data);
            }
            return senceDatas;
        }

        public IList<SceneData> GetM_SenceDatas()
        {
            this.Mode = Constant.MODE_M;
            IList<SceneData> senceDatas = new List<SceneData>();
            foreach (int item in SenceArray)
            {
                SceneData data = GetC_SenceData(SenceArray[item]);
                senceDatas.Add(data);
            }
            return senceDatas;
        }

        private SceneData GetC_SenceData(int senceNo)
        {
            SceneData senceData = new SceneData
            {
                SceneNo = senceNo
            };
            int chanelCount = GetChanelCount(senceNo);
            senceData.ChanelCount = chanelCount;
            IList<ChanelData> chanelDatas = new List<ChanelData>();
            IList<DB_Light> lights = GetLights(senceNo);
            foreach (DB_Light light in lights)
            {
                for (int chanel = 0; chanel < light.Count; chanel++)
                {
                    int chanelNo = light.StartID + chanel;
                    ChanelData chanelData = new ChanelData
                    {
                        ChanelNo = chanelNo,
                        StepCount = GetStepCount(light.LightNo, senceNo).StepCount
                    };
                    IList<int> IsGradualChange = new List<int>();
                    IList<int> StepTimes = new List<int>();
                    IList<int> StepValues = new List<int>();
                    for (int stepNo = 0; stepNo < GetStepCount(light.LightNo, senceNo).StepCount; stepNo++)
                    {
                        DB_Value value = GetValue(light.LightNo, senceNo, stepNo + 1, chanelData.ChanelNo);
                        IsGradualChange.Add(value.ChangeMode);
                        StepTimes.Add(value.StepTime);
                        StepValues.Add(value.ScrollValue);
                    }
                    chanelData.IsGradualChange = IsGradualChange;
                    chanelData.StepTimes = StepTimes;
                    chanelData.StepValues = StepValues;
                    if (Mode == Constant.MODE_C)
                    {
                        chanelDatas.Add(chanelData);
                    }
                    else
                    {
                        if (chanelData.IsGradualChange[0] == 1)
                        {
                            chanelDatas.Add(chanelData);
                        }
                    }
                }
            }
            senceData.ChanelDatas = chanelDatas;
            return senceData;
        }

        private int GetChanelCount(int senceNo)
        {
            int chanelCount = 0;
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (stepCount.PK.Mode == Mode && stepCount.PK.Frame == senceNo)
                {
                    chanelCount += GetLight(stepCount.PK.LightIndex).Count;
                }
            }
            return chanelCount;
        }

        private IList<DB_Light> GetLights(int senceNo)
        {
            IList<DB_Light> lights = new List<DB_Light>(); 
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (stepCount.PK.Frame == senceNo && stepCount.PK.Mode == Mode)
                {
                    lights.Add(GetLight(stepCount.PK.LightIndex));
                }
            }
            return lights;
        }

        private DB_Light GetLight(int lightIndex)
        {
            foreach (DB_Light light in DB_Lights)
            {
                if (light.LightNo == lightIndex)
                {
                    return light;
                }
            }
            return null;
        }

        private DB_StepCount GetStepCount(int lightIndex,int senceNo)
        {
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (lightIndex == stepCount.PK.LightIndex && senceNo == stepCount.PK.Frame && Mode == stepCount.PK.Mode)
                {
                    return stepCount;
                }
            }
            return null;
        }

        private DB_Value GetValue(int lightIndex,int senceNo,int step,int lightID)
        {
            foreach (DB_Value value in DB_Values)
            {
                if (value.PK.LightIndex == lightIndex && value.PK.Mode == Mode && value.PK.Step == step && value.PK.Frame == senceNo && value.PK.LightID == lightID)
                {
                    return value;
                }
            }
            return null;
        }

    }
}
