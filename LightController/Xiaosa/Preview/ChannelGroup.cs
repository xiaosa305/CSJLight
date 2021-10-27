using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Xiaosa.Preview
{
    public class ChannelGroup
    {
        private Dictionary<int, Channel> BasicChannels;
        private Dictionary<int, Channel> MusicChannels;
        private bool MusicControlState;
        private MainFormInterface MainFormInterface;
        private List<int> MusicStepList;

        public ChannelGroup(MainFormInterface mainFormInterface)
        {
            MainFormInterface = mainFormInterface;
        }
        private void Init()
        {
            BasicChannels = new Dictionary<int, Channel>();
            MusicChannels = new Dictionary<int, Channel>();
            MusicStepList = new List<int>();
            MusicControlState = false;
        }
        private void BuildChannels()
        {

        }
        public bool MusicControl()
        {
            return false;
        }
    }
}
