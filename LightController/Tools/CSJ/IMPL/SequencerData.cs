using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
	[Serializable]
	public class SequencerData
    {
        private const int RELAY_SWITCH_NAME_COUNT = 7;
        private const int RELAY_DELAY_TIME_COUNT = 6;

        public String[] RelaySwitchNames { get; set; }
        public int[] RelaySwitchDelayTimes { get; set; }
        public bool IsOpenSequencer { get; set; }

        public byte[] GetData()
        {
            try
            {
                List<byte> buff = new List<byte>();
                for (int relayIndex = 0; relayIndex < RELAY_SWITCH_NAME_COUNT; relayIndex++)
                {
                    byte[] nameBuff = Encoding.Default.GetBytes(this.RelaySwitchNames[relayIndex]);
                    for (int nameIndex = 0; nameIndex < 32; nameIndex++)
                    {
                        if (nameIndex >= nameBuff.Length)
                        {
                            buff.Add(Convert.ToByte(0x00));
                        }
                        else
                        {
                            buff.Add(nameBuff[nameIndex]);
                        }
                    }
                }
                for (int relayDelayTimeIndex = 0; relayDelayTimeIndex < RELAY_DELAY_TIME_COUNT; relayDelayTimeIndex++)
                {
                    buff.Add(Convert.ToByte(RelaySwitchDelayTimes[relayDelayTimeIndex]));
                }
                return buff.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public static SequencerData Build(byte[] data)
        {
            try
            {
                if (data.Length < 60) return null;
                int point = 80;
                SequencerData sequencer = new SequencerData
                {
                    RelaySwitchNames = new string[RELAY_SWITCH_NAME_COUNT],
                    RelaySwitchDelayTimes = new int[RELAY_DELAY_TIME_COUNT],
                    IsOpenSequencer = data[60] == 0x01
                };
                List<byte> buff = new List<byte>();
                for (int relayIndex = 0; relayIndex < RELAY_SWITCH_NAME_COUNT; relayIndex++)
                {
                    for (int index = 0; index < 32; index++)
                    {
                        if (data[point + relayIndex * 32 + index] != Convert.ToByte(0x00))
                        {
                            if (data[point + relayIndex * 32 + index] != Convert.ToByte(0xFF)) buff.Add(data[point + relayIndex * 32 + index]);
                        }
                    }
                    sequencer.RelaySwitchNames[relayIndex] = "";
                    if(buff.Count > 0) sequencer.RelaySwitchNames[relayIndex] = Encoding.Default.GetString(buff.ToArray());
                    buff.Clear();
                }
                for (int relayDelayTimeIndex = 0; relayDelayTimeIndex < RELAY_DELAY_TIME_COUNT; relayDelayTimeIndex++)
                {
                    int value = Convert.ToInt32(data[point + 7 * 32 + relayDelayTimeIndex]);
                    if (value > 15) value = 15;
                    else if (value < 1) value = 1;
                    sequencer.RelaySwitchDelayTimes[relayDelayTimeIndex] = value;
                }
                return sequencer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
