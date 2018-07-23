using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aeActivityApp
{
    /*This class holds all the data that the VisiFire charts will display. I tried to find a more sophisticated method of adding data to the chart but I couldn't find one
    /so I had to just keep track of each data item individually and add them to the graph individually.*/ 
    public class ChartItems
    {
                // this is the name of the first hospital the user has selected.
        string hospName1;
        // this is the name of the second hospital the user has selected, if they have selected a second one as the Specific page only uses one hospital.
        string hospName2;

        //This is th data for the first hospital chosen.
        int value_Q12010 = 0;
        int value_Q22010 = 0;
        int value_Q32010 = 0;
        int value_Q42010 = 0;
        int value_Q12009 = 0;
        int value_Q22009 = 0;
        int value_Q32009 = 0;
        int value_Q42009 = 0;
        int value_Q12008 = 0;
        int value_Q22008 = 0;
        int value_Q32008 = 0;
        int value_Q42008 = 0;

        //This is the data for the second hospital chosen if one has been chosen.
        int value_Q12010_2 = 0;
        int value_Q22010_2 = 0;
        int value_Q32010_2 = 0;
        int value_Q42010_2 = 0;
        int value_Q12009_2 = 0;
        int value_Q22009_2 = 0;
        int value_Q32009_2 = 0;
        int value_Q42009_2 = 0;
        int value_Q12008_2 = 0;
        int value_Q22008_2 = 0;
        int value_Q32008_2 = 0;
        int value_Q42008_2 = 0;

        //These are data for the VisiFire graphs to use so they now the minimum and maximum Y values the graph should display.
        double _yMax;
        double _yMin;

        public ChartItems()
        {
        }

        public ChartItems(List<QuarterData> data, string name)
        {
            Name1 = name;

            Q12010 = data[0].Record;
            Q22010 = data[1].Record;
            Q32010 = data[2].Record;
            Q42010 = data[3].Record;
            Q12009 = data[4].Record;
            Q22009 = data[5].Record;
            Q32009 = data[6].Record;
            Q42009 = data[7].Record;
            Q12008 = data[8].Record;
            Q22008 = data[9].Record;
            Q32008 = data[10].Record;
            Q42008 = data[11].Record;

            SetYMax(data, null);
            SetYMin(data, null);
        }

        public ChartItems(List<QuarterData> data, List<QuarterData> data2, string name1, string name2)
        {
            Name1 = name1;
            Name2 = name2;

            Q12010 = data[0].Record;
            Q22010 = data[1].Record;
            Q32010 = data[2].Record;
            Q42010 = data[3].Record;
            Q12009 = data[4].Record;
            Q22009 = data[5].Record;
            Q32009 = data[6].Record;
            Q42009 = data[7].Record;
            Q12008 = data[8].Record;
            Q22008 = data[9].Record;
            Q32008 = data[10].Record;
            Q42008 = data[11].Record;

            Q12010_2 = data2[0].Record;
            Q22010_2 = data2[1].Record;
            Q32010_2 = data2[2].Record;
            Q42010_2 = data2[3].Record;
            Q12009_2 = data2[4].Record;
            Q22009_2 = data2[5].Record;
            Q32009_2 = data2[6].Record;
            Q42009_2 = data2[7].Record;
            Q12008_2 = data2[8].Record;
            Q22008_2 = data2[9].Record;
            Q32008_2 = data2[10].Record;
            Q42008_2 = data2[11].Record;

            SetYMax(data, data2);
            SetYMin(data, data2);
        }

        public string Name1
        {
            get
            {
                return hospName1;
            }
            set
            {
                if (hospName1 != value)
                {
                    hospName1 = value;
                }
            }
        }

        public string Name2
        {
            get
            {
                return hospName2;
            }
            set
            {
                if (hospName2 != value)
                {
                    hospName2 = value;
                }
            }
        }

        public int Q12010
        {
            get
            {
                return value_Q12010;
            }
            set
            {
                if (value_Q12010 != value)
                {
                    value_Q12010 = value;
                }
            }
        }

        public int Q22010
        {
            get
            {
                return value_Q22010;
            }
            set
            {
                if (value_Q22010 != value)
                {
                    value_Q22010 = value;
                }
            }
        }

        public int Q32010
        {
            get
            {
                return value_Q32010;
            }
            set
            {
                if (value_Q32010 != value)
                {
                    value_Q32010 = value;
                }
            }
        }

        public int Q42010
        {
            get
            {
                return value_Q42010;
            }
            set
            {
                if (value_Q42010 != value)
                {
                    value_Q42010 = value;
                }
            }
        }

        public int Q12009
        {
            get
            {
                return value_Q12009;
            }
            set
            {
                if (value_Q12009 != value)
                {
                    value_Q12009 = value;
                }
            }
        }

        public int Q22009
        {
            get
            {
                return value_Q22009;
            }
            set
            {
                if (value_Q22009 != value)
                {
                    value_Q22009 = value;
                }
            }
        }

        public int Q32009
        {
            get
            {
                return value_Q32009;
            }
            set
            {
                if (value_Q32009 != value)
                {
                    value_Q32009 = value;
                }
            }
        }

        public int Q42009
        {
            get
            {
                return value_Q42009;
            }
            set
            {
                if (value_Q42009 != value)
                {
                    value_Q42009 = value;
                }
            }
        }

        public int Q12008
        {
            get
            {
                return value_Q12008;
            }
            set
            {
                if (value_Q12008 != value)
                {
                    value_Q12008 = value;
                }
            }
        }

        public int Q22008
        {
            get
            {
                return value_Q22008;
            }
            set
            {
                if (value_Q22008 != value)
                {
                    value_Q22008 = value;
                }
            }
        }

        public int Q32008
        {
            get
            {
                return value_Q32008;
            }
            set
            {
                if (value_Q32008 != value)
                {
                    value_Q32008 = value;
                }
            }
        }

        public int Q42008
        {
            get
            {
                return value_Q42008;
            }
            set
            {
                if (value_Q42008 != value)
                {
                    value_Q42008 = value;
                }
            }
        }

        public int Q12010_2
        {
            get
            {
                return value_Q12010_2;
            }
            set
            {
                if (value_Q12010_2 != value)
                {
                    value_Q12010_2 = value;
                }
            }
        }

        public int Q22010_2
        {
            get
            {
                return value_Q22010_2;
            }
            set
            {
                if (value_Q22010_2 != value)
                {
                    value_Q22010_2 = value;
                }
            }
        }

        public int Q32010_2
        {
            get
            {
                return value_Q32010_2;
            }
            set
            {
                if (value_Q32010_2 != value)
                {
                    value_Q32010_2 = value;
                }
            }
        }

        public int Q42010_2
        {
            get
            {
                return value_Q42010_2;
            }
            set
            {
                if (value_Q42010_2 != value)
                {
                    value_Q42010_2 = value;
                }
            }
        }

        public int Q12009_2
        {
            get
            {
                return value_Q12009_2;
            }
            set
            {
                if (value_Q12009_2 != value)
                {
                    value_Q12009_2 = value;
                }
            }
        }

        public int Q22009_2
        {
            get
            {
                return value_Q22009_2;
            }
            set
            {
                if (value_Q22009_2 != value)
                {
                    value_Q22009_2 = value;
                }
            }
        }

        public int Q32009_2
        {
            get
            {
                return value_Q32009_2;
            }
            set
            {
                if (value_Q32009_2 != value)
                {
                    value_Q32009_2 = value;
                }
            }
        }

        public int Q42009_2
        {
            get
            {
                return value_Q42009_2;
            }
            set
            {
                if (value_Q42009_2 != value)
                {
                    value_Q42009_2 = value;
                }
            }
        }

        public int Q12008_2
        {
            get
            {
                return value_Q12008_2;
            }
            set
            {
                if (value_Q12008_2 != value)
                {
                    value_Q12008_2 = value;
                }
            }
        }

        public int Q22008_2
        {
            get
            {
                return value_Q22008_2;
            }
            set
            {
                if (value_Q22008_2 != value)
                {
                    value_Q22008_2 = value;
                }
            }
        }

        public int Q32008_2
        {
            get
            {
                return value_Q32008_2;
            }
            set
            {
                if (value_Q32008_2 != value)
                {
                    value_Q32008_2 = value;
                }
            }
        }

        public int Q42008_2
        {
            get
            {
                return value_Q42008_2;
            }
            set
            {
                if (value_Q42008_2 != value)
                {
                    value_Q42008_2 = value;
                }
            }
        }

        public double YMax
        {
            get
            {
                return _yMax;
            }

            set
            {
                if (_yMax != value)
                {
                    _yMax = value;
                }
            }
        }

        public double YMin
        {
            get
            {
                return _yMin;
            }

            set
            {
                if (_yMin != value)
                {
                    _yMin = value;
                }
            }
        }

        private void SetYMax(List<QuarterData> data, List<QuarterData> data2)
        {
            double max = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Record > max)
                {
                    max = data[i].Record;
                }
            }

            if (data2 != null)
            {
                for (int k = 0; k < data2.Count; k++)
                {
                    if (data2[k].Record > max)
                    {
                        max = data2[k].Record;
                    }
                }
            }

            //By this point miax should equal the value of the data item that has the Highest value.
            max = RoundToNearest(max, 1000, "up");
            YMax = max;
        }

        private void SetYMin(List<QuarterData> data, List<QuarterData> data2)
        {
            double min = YMax;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Record < min)
                {
                    min = data[i].Record;
                }
            }

            if (data2 != null)
            {
                for (int k = 0; k < data2.Count; k++)
                {
                    if (data2[k].Record < min)
                    {
                        min = data2[k].Record;
                    }
                }
            }

            //By this point min should equal the value of the data item that has the lowest value.
            min = RoundToNearest(min, YMax, "down");
            YMin = min;
        }

        /*This method simply takes a starting number and adds/subtracts 100 away from it until it reaches a number that is higher/lower than the amount that was passed
        by the max/min variables above. This allows the VisiCharts to display a number equal to the nearset thousand instead of starting at 6233 and ending on 63555 it will
        start on 6000 and end on 64000 which looks better when displayed on a graph.*/
        public static double RoundToNearest(double Amount, double startNum, string method)
        {
            bool rounded = false;

            if (method == "up")
            {
                do
                {
                    if (Amount > startNum)
                    {
                        startNum = startNum + 1000;
                    }
                    else
                    {
                        rounded = true;
                    }
                }
                while (rounded == false);

            }

            if (method == "down")
            {
                do
                {
                    if (Amount < startNum)
                    {
                        startNum = startNum - 1000;
                    }
                    else
                    {
                        rounded = true;
                    }
                }
                while (rounded == false);
            }

            return startNum;
        }
    }
}
