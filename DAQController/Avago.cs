using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAQController
{
    public partial class Avago
    {
        private string VisaAlias { get; set; }

        private Task digitalWriteTaskP00;
        private Task digitalWriteTaskP01;
        private Task digitalWriteTaskP02;
        private Task digitalWriteTaskP03;
        private Task digitalWriteTaskP04;
        private Task digitalWriteTaskP05;
        private Task digitalWriteTaskP06;
        private Task digitalWriteTaskP07;
        private Task digitalWriteTaskP08;
        private Task digitalWriteTaskP09;
        private Task digitalWriteTaskP10;
        private Task digitalWriteTaskP11;

        private DigitalSingleChannelWriter writerP00;
        private DigitalSingleChannelWriter writerP01;
        private DigitalSingleChannelWriter writerP02;
        private DigitalSingleChannelWriter writerP03;
        private DigitalSingleChannelWriter writerP04;
        private DigitalSingleChannelWriter writerP05;
        private DigitalSingleChannelWriter writerP06;
        private DigitalSingleChannelWriter writerP07;
        private DigitalSingleChannelWriter writerP08;
        private DigitalSingleChannelWriter writerP09;
        private DigitalSingleChannelWriter writerP10;
        private DigitalSingleChannelWriter writerP11;

        private int[] ChannelValue = new int[12];

        private List<KeyValuePair<bool, KeyValuePair<string, int>>> SwConfigBefore = new List<KeyValuePair<bool, KeyValuePair<string, int>>>();

        public Avago(string VisaAlias)
        {
            this.VisaAlias = VisaAlias;
            Initialize();
        }

        private int Initialize()
        {
            try
            {
                digitalWriteTaskP00 = new Task();
                digitalWriteTaskP01 = new Task();
                digitalWriteTaskP02 = new Task();
                digitalWriteTaskP03 = new Task();
                digitalWriteTaskP04 = new Task();
                digitalWriteTaskP05 = new Task();
                digitalWriteTaskP06 = new Task();
                digitalWriteTaskP07 = new Task();
                digitalWriteTaskP08 = new Task();
                digitalWriteTaskP09 = new Task();
                digitalWriteTaskP10 = new Task();
                digitalWriteTaskP11 = new Task();

                digitalWriteTaskP00.DOChannels.CreateChannel(VisaAlias + "/port0", "port0", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP01.DOChannels.CreateChannel(VisaAlias + "/port1", "port1", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP02.DOChannels.CreateChannel(VisaAlias + "/port2", "port2", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP03.DOChannels.CreateChannel(VisaAlias + "/port3", "port3", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP04.DOChannels.CreateChannel(VisaAlias + "/port4", "port4", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP05.DOChannels.CreateChannel(VisaAlias + "/port5", "port5", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP06.DOChannels.CreateChannel(VisaAlias + "/port6", "port6", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP07.DOChannels.CreateChannel(VisaAlias + "/port7", "port7", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP08.DOChannels.CreateChannel(VisaAlias + "/port8", "port8", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP09.DOChannels.CreateChannel(VisaAlias + "/port9", "port9", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP10.DOChannels.CreateChannel(VisaAlias + "/port10", "port10", ChannelLineGrouping.OneChannelForAllLines);
                digitalWriteTaskP11.DOChannels.CreateChannel(VisaAlias + "/port11", "port11", ChannelLineGrouping.OneChannelForAllLines);

                writerP00 = new DigitalSingleChannelWriter(digitalWriteTaskP00.Stream);
                writerP01 = new DigitalSingleChannelWriter(digitalWriteTaskP01.Stream);
                writerP02 = new DigitalSingleChannelWriter(digitalWriteTaskP02.Stream);
                writerP03 = new DigitalSingleChannelWriter(digitalWriteTaskP03.Stream);
                writerP04 = new DigitalSingleChannelWriter(digitalWriteTaskP04.Stream);
                writerP05 = new DigitalSingleChannelWriter(digitalWriteTaskP05.Stream);
                writerP06 = new DigitalSingleChannelWriter(digitalWriteTaskP06.Stream);
                writerP07 = new DigitalSingleChannelWriter(digitalWriteTaskP07.Stream);
                writerP08 = new DigitalSingleChannelWriter(digitalWriteTaskP08.Stream);
                writerP09 = new DigitalSingleChannelWriter(digitalWriteTaskP09.Stream);
                writerP10 = new DigitalSingleChannelWriter(digitalWriteTaskP10.Stream);
                writerP11 = new DigitalSingleChannelWriter(digitalWriteTaskP11.Stream);

                writerP00.WriteSingleSamplePort(true, 0);
                writerP01.WriteSingleSamplePort(true, 0);
                writerP02.WriteSingleSamplePort(true, 0);
                writerP03.WriteSingleSamplePort(true, 0);
                writerP04.WriteSingleSamplePort(true, 0);
                writerP05.WriteSingleSamplePort(true, 0);
                writerP06.WriteSingleSamplePort(true, 0);
                writerP07.WriteSingleSamplePort(true, 0);
                writerP08.WriteSingleSamplePort(true, 0);
                writerP09.WriteSingleSamplePort(true, 0);
                writerP10.WriteSingleSamplePort(true, 0);
                writerP11.WriteSingleSamplePort(true, 0);

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Initialize");
                return -1;
            }
        }

        public int ActivatePath(string SwitchPath, bool Force_Assign_Bits = false)
        {
            try
            {
                string[] SwitchNoAndStatus = SwitchPath.Split(' ');

                Dictionary<int, int> SwitchSettings = new Dictionary<int, int>();

                // Generate dictionary for switch no and switch on/off status
                for (int i = 0; i < SwitchNoAndStatus.Length; i++)
                {
                    string[] tempSwitchNoAndStatus = SwitchNoAndStatus[i].Split(':');
                    SwitchSettings[Convert.ToInt32(tempSwitchNoAndStatus[0])] = Convert.ToInt32(tempSwitchNoAndStatus[1]);
                }

                // Clear the channel write need status
                bool[] ChannelWriteNeeded = new bool[12];

#if false
                /// O(k+m)
                int[] desiredChannelValues = new int[12];

                foreach (KeyValuePair<int, int> switchSetting in SwitchPathSettingsDict[site][SwitchPath.dutPort, SwitchPath.instrPort])
                {
                    int switchNo = switchSetting.Key;
                    int switchStatus = switchSetting.Value;

                    int portNo = (switchNo == 48) ? 9 : (int)(switchNo / 8);
                    int chNo = switchNo % 8;

                    // Update the desired state directly
                    if (switchStatus == 1)
                    {
                        desiredChannelValues[portNo] |= (1 << chNo); // Set the bit
                    }
                    else
                    {
                        desiredChannelValues[portNo] &= ~(1 << chNo); // Clear the bit
                    }
                }

                // Compare and update the actual channel values
                for (int portNo = 0; portNo < desiredChannelValues.Length; portNo++)
                {
                    if (ChannelValue[portNo] != desiredChannelValues[portNo])
                    {
                        ChannelValue[portNo] = desiredChannelValues[portNo];
                        ChannelWriteNeeded[portNo] = true; // Mark for writing if a change is detected
                    }
                }
#endif

                /// O(k)
                // Iterate and compute the final channel values directly
                foreach (KeyValuePair<int, int> switchSetting in SwitchSettings)
                {
                    int switchNo = switchSetting.Key;
                    int switchStatus = switchSetting.Value;

                    int portNo = (switchNo == 48) ? 9 : (int)(switchNo / 8);
                    int chNo = switchNo % 8;

                    int channelBitValue = 1 << chNo; // Bit value for the current channel

                    // Compute the desired state and compare with the current state
                    if (switchStatus == 1) // Enable the channel
                    {
                        if ((ChannelValue[portNo] & channelBitValue) == 0) // Bit not set
                        {
                            ChannelValue[portNo] |= channelBitValue; // Set the bit
                            ChannelWriteNeeded[portNo] = true; // Mark for writing
                        }
                    }
                    else // Disable the channel
                    {
                        if ((ChannelValue[portNo] & channelBitValue) != 0) // Bit is set
                        {
                            ChannelValue[portNo] &= ~channelBitValue; // Clear the bit
                            ChannelWriteNeeded[portNo] = true; // Mark for writing
                        }
                    }
                }

                // Channel Write
                if (ChannelWriteNeeded[0])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP00.WriteSingleSamplePort(true, ChannelValue[0]);
                if (ChannelWriteNeeded[1])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP01.WriteSingleSamplePort(true, ChannelValue[1]);
                if (ChannelWriteNeeded[2])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP02.WriteSingleSamplePort(true, ChannelValue[2]);
                if (ChannelWriteNeeded[3])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP03.WriteSingleSamplePort(true, ChannelValue[3]);
                if (ChannelWriteNeeded[4])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP04.WriteSingleSamplePort(true, ChannelValue[4]);
                if (ChannelWriteNeeded[5])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP05.WriteSingleSamplePort(true, ChannelValue[5]);

                if (ChannelWriteNeeded[8])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP08.WriteSingleSamplePort(true, ChannelValue[8]);
                if (ChannelWriteNeeded[9])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP09.WriteSingleSamplePort(true, ChannelValue[9]);
                if (ChannelWriteNeeded[10])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP10.WriteSingleSamplePort(true, ChannelValue[10]);
                if (ChannelWriteNeeded[11])
                    // writerP10.WriteSingleSamplePort(true, portValue);
                    writerP11.WriteSingleSamplePort(true, ChannelValue[11]);

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "SetPortPath");
                return -1;
            }
        }

        
    }
}