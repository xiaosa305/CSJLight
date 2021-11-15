﻿using LightController.Ast;
using LightController.Entity;
using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Xiaosa.Preview
{
    public class Channel
    {
        private const int BASIC_MODE = 0;
        private const int MUSIC_MODE = 1;
        private MainFormInterface MainFormInterface;
        private int CurrentChannelNo;
        private int CurrentMode;
        private IList<TongdaoWrapper> StepValues;
        private int StepPoint;
        private int StartValue;
        private DB_FineTune FineTune;
        private Queue<byte> DmxDataQueue;
        public Channel(int currentChannelNo,int mode,MainFormInterface mainFormInterface)
        {
            DmxDataQueue = new Queue<byte>();
            CurrentChannelNo = currentChannelNo;
            MainFormInterface = mainFormInterface;
            CurrentMode = mode;
            StepPoint = 0;
            foreach (var item in MainFormInterface.GetFineTunes())
            {
                if (item.FineTuneIndex == currentChannelNo)
                {
                    item.MaxValue = item.MaxValue == 0 ? 255 : item.MaxValue;
                    FineTune = item;
                    break;
                }
            }
            StepValues = new List<TongdaoWrapper>();
            switch (CurrentMode)
            {
                case BASIC_MODE:
                    foreach (var item in mainFormInterface.GetPreviewChannelData(CurrentChannelNo, CurrentMode))
                    {
                        if (item.ChangeMode != 2)
                        {
                            StepValues.Add(item);
                        }
                    }
                    break;
                case MUSIC_MODE:
                    foreach (var item in mainFormInterface.GetPreviewChannelData(CurrentChannelNo, CurrentMode))
                    {
                        if (item.ChangeMode == 1)
                        {
                            StepValues.Add(item);
                        }
                    }
                    break;
            }
            if (CurrentMode == BASIC_MODE)
            {
                if (StepValues.Count > 0)
                {
                    StartValue = StepValues[0].ScrollValue;
                    StepPoint++;
                    if (StepPoint == StepValues.Count)
                    {
                        StepPoint = 0;
                    }
                    LoadStepDataIntoQueue();
                }
            }
            else
            {
                if (StepValues.Count > 0) LoadStepDataIntoQueue();
            }
            
        }
        public byte TakeDmxData()
        {
            byte result = 0x00;
            if (DmxDataQueue != null && DmxDataQueue.Count > 0)
            {
                result = DmxDataQueue.Dequeue();
              
                if (DmxDataQueue.Count == 0)
                {
                    LoadStepDataIntoQueue();
                }
            }
            return result;
        }
        public bool IsNoEmpty()
        {
            return StepValues.Count > 0;
        }
        private void LoadStepDataIntoQueue()
        {
            switch (CurrentMode)
            {
                case BASIC_MODE:
                    LoadBasicStepData();
                    break;
                case MUSIC_MODE:
                    LoadMusicStepData();
                    break;
            }
        }
        private void LoadBasicStepData()
        {
            try
            {
                TongdaoWrapper stepValue = StepValues[StepPoint];
                if (stepValue.ChangeMode == 1)
                {
                    float inc = (stepValue.ScrollValue - StartValue) / (float)stepValue.StepTime;
                    for (int i = 0; i < stepValue.StepTime; i++)
                    {
                        var value = StartValue + (i + 1) * inc;
                        value = value < 0 ? 0 : value;
                        value = value > 255 ? 255 : value;
                        int intValue = (int)Math.Floor(value * 256);
                        DmxDataQueue.Enqueue(Convert.ToByte(FineTune == null ? (intValue >> 8) & 0xFF : (intValue & 0xFF) / (255 / FineTune.MaxValue)));
                    }
                }
                else if (stepValue.ChangeMode == 0)
                {
                    for (int i = 0; i < stepValue.StepTime; i++)
                    {
                        DmxDataQueue.Enqueue(Convert.ToByte(FineTune == null ? stepValue.ScrollValue : 0));
                    }
                }
                StepPoint++;
               
                if (StepPoint == StepValues.Count)
                {
                    StepPoint = 0;
                }
                StartValue = stepValue.ScrollValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadMusicStepData()
        {
            TongdaoWrapper stepValue = StepValues[StepPoint];
           
            DmxDataQueue.Enqueue(Convert.ToByte(stepValue.ScrollValue));
            StepPoint++;
            if (StepPoint == StepValues.Count)
            {
                StepPoint = 0;
            }
        }
    }
}