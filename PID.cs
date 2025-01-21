using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMievCan
{
    internal class PIDFactory
    {
        private static int currentID = 0;
        public static PIDData Create(string rawData)
        {
            var data = rawData.Split(' ');
            if (data.Length == 9)
            {
                var newPidData = new PIDData()
                {
                    ID = currentID,
                    PID = data[0],
                    Bit_1 = data[1],
                    Bit_2 = data[2],
                    Bit_3 = data[3],
                    Bit_4 = data[4],
                    Bit_5 = data[5],
                    Bit_6 = data[6],
                    Bit_7 = data[7],
                    Bit_8 = data[8],

                };
                currentID++;
                return newPidData;
           
            }
            else return null;
        }
    }
    internal class PIDData
    {
        public int ID;
        public string PID;
        public string Bit_1;
        public string Bit_2;
        public string Bit_3;
        public string Bit_4;            
        public string Bit_5;
        public string Bit_6;
        public string Bit_7;
        public string Bit_8;
        public string Formula="";
        public string Value="";
        public string LastUpdated = "";
    }
}
