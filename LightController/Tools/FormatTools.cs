﻿using DMX512;
using LightController.Ast;
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
        private static FormatTools Instance { get; set; }
        private IList<DB_Light> DB_Lights { get; set; }
        private IList<DB_StepCount> DB_StepCounts { get; set; }
        private IList<DB_Value> DB_Values { get; set; }
        private int Mode { get; set; }
        private IList<int> SceneArray { get; set; }
        private IList<DMX_C_File> c_files { get; set; }
        private IList<DMX_M_File> m_files { get; set; }

        public FormatTools(IList<DB_Light> lights, IList<DB_StepCount> stepCounts, IList<DB_Value> values)
        {
            this.DB_Lights = lights;
            this.DB_StepCounts = stepCounts;
            this.DB_Values = values;
            this.GetSceneArray();
        }

        public FormatTools(DBWrapper dBWrapper)
        {
            this.DB_Lights = dBWrapper.lightList;
            this.DB_StepCounts = dBWrapper.stepCountList;
            this.DB_Values = dBWrapper.valueList;
            this.GetSceneArray();
        }

        private FormatTools()
        {

        }

        public static FormatTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FormatTools();
            }
            return Instance;
        }

        private void GetSceneArray()
        {
            SceneArray = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                foreach (DB_StepCount item in DB_StepCounts)
                {
                    if (i == item.PK.Frame)
                    {
                        SceneArray.Add(i);
                        break;
                    }
                }
            }
        }

        public IList<SceneData> GetC_SceneDatas(DBWrapper dBWrapper)
        {
            this.Mode = Constant.MODE_C;
            this.DB_Lights = dBWrapper.lightList;
            this.DB_StepCounts = dBWrapper.stepCountList;
            this.DB_Values = dBWrapper.valueList;
            this.GetSceneArray();
            IList<SceneData> sceneDatas = new List<SceneData>();
            foreach (int item in SceneArray)
            {
                SceneData data = GetC_SceneData(item);
                sceneDatas.Add(data);
            }
            return sceneDatas;
        }

        public IList<SceneData> GetM_SceneDatas(DBWrapper dBWrapper)
        {
            this.Mode = Constant.MODE_M;
            this.DB_Lights = dBWrapper.lightList;
            this.DB_StepCounts = dBWrapper.stepCountList;
            this.DB_Values = dBWrapper.valueList;
            this.GetSceneArray();
            IList<SceneData> sceneDatas = new List<SceneData>();
            foreach (int item in SceneArray)
            {
                SceneData data = GetC_SceneData(item);
                sceneDatas.Add(data);
            }
            return sceneDatas;
        }

        private SceneData GetC_SceneData(int sceneNo)
        {
            SceneData sceneData = new SceneData
            {
                SceneNo = sceneNo
            };
            int chanelCount = GetChanelCount(sceneNo);
            sceneData.ChanelCount = chanelCount;
            IList<ChanelData> chanelDatas = new List<ChanelData>();
            IList<DB_Light> lights = GetLights(sceneNo);
            foreach (DB_Light light in lights)
            {
                for (int chanel = 0; chanel < light.Count; chanel++)
                {
                    int chanelNo = light.StartID + chanel;
                    ChanelData chanelData = new ChanelData
                    {
                        ChanelNo = chanelNo,
                        StepCount = GetStepCount(light.LightNo, sceneNo).StepCount
                    };
                    IList<int> IsGradualChange = new List<int>();
                    IList<int> StepTimes = new List<int>();
                    IList<int> StepValues = new List<int>();
                    for (int stepNo = 0; stepNo < GetStepCount(light.LightNo, sceneNo).StepCount; stepNo++)
                    {
                        DB_Value value = GetValue(light.LightNo, sceneNo, stepNo + 1, chanelData.ChanelNo);
                        if (value != null)
                        {
                            IsGradualChange.Add(value.ChangeMode);
                            StepTimes.Add(value.StepTime);
                            StepValues.Add(value.ScrollValue);
                        }
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
            sceneData.ChanelDatas = chanelDatas;
            return sceneData;
        }

        private int GetChanelCount(int sceneNo)
        {
            int chanelCount = 0;
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (stepCount.PK.Mode == Mode && stepCount.PK.Frame == sceneNo)
                {
                    chanelCount += GetLight(stepCount.PK.LightIndex).Count;
                }
            }
            return chanelCount;
        }

        private IList<DB_Light> GetLights(int sceneNo)
        {
            IList<DB_Light> lights = new List<DB_Light>(); 
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (stepCount.PK.Frame == sceneNo && stepCount.PK.Mode == Mode)
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

        private DB_StepCount GetStepCount(int lightIndex,int sceneNo)
        {
            foreach (DB_StepCount stepCount in DB_StepCounts)
            {
                if (lightIndex == stepCount.PK.LightIndex && sceneNo == stepCount.PK.Frame && Mode == stepCount.PK.Mode)
                {
                    return stepCount;
                }
            }
            return null;
        }

        private DB_Value GetValue(int lightIndex,int sceneNo,int step,int lightID)
        {
            foreach (DB_Value value in DB_Values)
            {
                if (value.PK.LightIndex == lightIndex && value.PK.Mode == Mode && value.PK.Step == step && value.PK.Frame == sceneNo && value.PK.LightID == lightID)
                {
                    //if (Mode == Constant.MODE_M)
                    //{
                    //    if (value.ChangeMode == 1)
                    //    {
                    //        return value;
                    //    }
                    //}
                    //else
                    //{
                        return value;
                    //}
                }
            }
            return null;
        }

    }
}
