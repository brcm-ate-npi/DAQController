using ModularZT64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DAQController
{
    public class ZTM
    {
        public ModularZT64.USB_ZT MyZT = new USB_ZT();
        public string SN = "";
        public string MN = "";
        public string retStr = "";
        public string Command;
        private int i;
        public bool bSwitchState = false;
        public bool bAvaliable = false;

        private List<string> lastStatus = new List<string>();
        private bool IsRequiredtoSetPath = false;
        private Regex regex = new Regex(@"SP6T:(?<swnum>\d):State:(?<state>\d)", RegexOptions.IgnoreCase);

        public ZTM(string SerialNumber = null)
        {
            SN = SerialNumber;

            Initialize();
        }

        #region iSwitch Members

        public int Initialize()
        {
            try
            {
                string availableSN = "";

                i = MyZT.Get_Available_SN_List(ref availableSN);
                var lstAvailableSNs = availableSN.Split(' ').Where(t => t.Trim().Length > 0).Select(s => s.Trim()).ToList();

                if (i == 1 && lstAvailableSNs.Count() > 0)
                {
                    if (!lstAvailableSNs.Any(s => s.Equals(SN)))
                        SN = lstAvailableSNs.First();

                    i = MyZT.Connect(ref (SN));
                    MyZT.Read_ModelName(ref MN);

                    if (MN.ToUpper().Equals("ZT-325"))
                    {
                        Command = ":SP6T:ALL:STATE?";
                        MyZT.Send_SCPI(ref Command, ref retStr);

                        for (int i = 0; i < 6; i++)
                        {
                            lastStatus.Add(retStr[i].ToString());
                        }
                    }

                    if (i == 1)
                        bSwitchState = true;
                    else
                        bSwitchState = false;
                }

                bAvaliable = true;

                return 1;
            }
            catch (Exception ex)
            {
                bAvaliable = false;
                MessageBox.Show("ZTM : Fail Initialize -> " + ex.Message);
                return -1;
            }
        }

        public bool ActivatePath(string val)
        {
            if (!bAvaliable) return false;

            var swmatchlist = regex.Matches(val);
            foreach (Match mt in swmatchlist)
            {
                if (lastStatus[int.Parse(mt.Groups["swnum"].Value) - 1] != mt.Groups["state"].Value)
                {
                    IsRequiredtoSetPath = true;
                    lastStatus[int.Parse(mt.Groups["swnum"].Value) - 1] = mt.Groups["state"].Value;
                }
            }

            if (IsRequiredtoSetPath == true)
            {
                SetPath(val);
            }

            IsRequiredtoSetPath = false;
            return true;
        }

        public void SetPath(string val)
        {
            if (bSwitchState)
            {
                //Command = val;
                Command = $":SP6T:All:State:{string.Join("", lastStatus)}";
                MyZT.Send_SCPI(ref (Command), ref (retStr));
            }
        }

        #endregion iSwitch Members
    }
}