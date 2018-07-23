using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aeActivityApp
{
    //This class holds all the data nessesary to populate the table on the Rankings page.
    public class RankingData
    {
        //Ranking number 1 - x;
        public string rankNum;
        //Hospital's unique identifying code.
        public string hospitalCode;
        //Hospital's name.
        public string hospitalName;
        //The number of A&E Attendees.
        public string data = "";

        public RankingData()
        {
        }

        public RankingData(string rankNum, string hCode, string hName, string data)
        {
            RankNumber = rankNum;
            HospitalCode = hCode;
            HospitalName = hName;
            Data = data;
        }

        public string RankNumber
        {
            get
            {
                return rankNum;
            }
            set
            {
                if (rankNum != value)
                {
                    rankNum = value;
                }
            }
        }

        public string HospitalCode
        {
            get
            {
                return hospitalCode;
            }
            set
            {
                if (hospitalCode != value)
                {
                    hospitalCode = value;
                }
            }
        }

        public string HospitalName
        {
            get
            {
                return hospitalName;
            }
            set
            {
                if (hospitalName != value)
                {
                    hospitalName = value;
                }
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                if (data != value)
                {
                    data = value;
                }
            }
        }
    }
}
