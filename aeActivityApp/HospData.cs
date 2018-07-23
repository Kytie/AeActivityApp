using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aeActivityApp
{
    //This class stores the data on a hospital's name and code so data bases can be interigated and names displayed in ComboBoxes.
    public class HospName
    {
        string _name;
        string _code;

        public HospName()
        {
        }

        public HospName(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    _code = value;
                }
            }
        }
    }

    //This class holds the A&E attendee data that will be brought back from a web service.
    public class QuarterData
    {
        int _record;

        public int Record
        {
            get
            {
                return _record;
            }
            set
            {
                if (_record != value)
                {
                    _record = value;
                }
            }
        }
    }
}
