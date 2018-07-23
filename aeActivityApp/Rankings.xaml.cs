using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Xml;
using System.Xml.Serialization;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace aeActivityApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Rankings : aeActivityApp.Common.LayoutAwarePage
    {
        //These classes are made from the RankingData class which hold data specific for the ranking page.
        List<RankingData> hospitalDetails = null;
        //This is a second list which will only hold 10 results which will be displayed in the grid/list on this page.
        List<RankingData> data = null;

        string quarter = null;
        string year = null;
        int start = 0;
        int end = 0;
        //These are variables that hold the selected indexes for the ComboBox.
        int quarterIndex = 0;
        int yearIndex = 0;
        //This boolean is used for the checking of whether the table has data in it.
        bool hasData = false;

        public Rankings()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //The if statements below check for various session states.
            if (pageState != null)
            {
                if (pageState.ContainsKey("Quarter"))
                {
                    object value = null;
                    if (pageState.TryGetValue("Quarter", out value))
                    {
                        if (value != null)
                        {
                            quarter = value.ToString();
                        }
                    }
                }

                if (pageState.ContainsKey("Year"))
                {
                    object value = null;
                    if (pageState.TryGetValue("Year", out value))
                    {
                        if (value != null)
                        {
                            year = value.ToString();
                        }
                    }
                }

                if (pageState.ContainsKey("Start"))
                {
                    object value = null;
                    if (pageState.TryGetValue("Start", out value))
                    {
                        if (value != null)
                        {
                            start = Convert.ToInt32(value);
                        }
                    }
                }

                if (pageState.ContainsKey("End"))
                {
                    object value = null;
                    if (pageState.TryGetValue("End", out value))
                    {
                        if (value != null)
                        {
                            end = Convert.ToInt32(value);
                        }
                    }
                }

                if (pageState.ContainsKey("QuarterIndex"))
                {
                    object value = null;
                    if (pageState.TryGetValue("QuarterIndex", out value))
                    {
                        if (value != null)
                        {
                            quarterIndex = Convert.ToInt32(value);
                        }
                    }
                }

                if (pageState.ContainsKey("YearIndex"))
                {
                    object value = null;
                    if (pageState.TryGetValue("YearIndex", out value))
                    {
                        if (value != null)
                        {
                            yearIndex = Convert.ToInt32(value);
                        }
                    }
                }

                if (pageState.ContainsKey("HasData"))
                {
                    object value = null;
                    if (pageState.TryGetValue("HasData", out value))
                    {
                        if (value != null)
                        {
                            hasData = Convert.ToBoolean(value);
                        }
                    }
                }
            }
            PopulateComboBoxes();
            //Data Fetch is run whenever data is needed to be fetched from the web service.
            DataFetch();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            if (!String.IsNullOrEmpty(quarter))
            {
                pageState["Quarter"] = quarter;
            }
            if (!String.IsNullOrEmpty(year))
            {
                pageState["Year"] = year;
            }
            pageState["Start"] = start;
            pageState["End"] = end;
            pageState["QuarterIndex"] = quarterIndex;
            pageState["YearIndex"] = yearIndex;
            pageState["HasData"] = hasData;
        }

        private void PopulateComboBoxes()
        {
            Quarter.Items.Add("Quarter");
            Quarter.Items.Add("Quarter 1");
            Quarter.Items.Add("Quarter 2");
            Quarter.Items.Add("Quarter 3");
            Quarter.Items.Add("Quarter 4");

            //Quarter is the name of the Quarter Combobox.
            Quarter.SelectedIndex = quarterIndex;

            Year.Items.Add("Year");
            Year.Items.Add("2010");
            Year.Items.Add("2009");
            Year.Items.Add("2008");

            //Year is the Name of the Year ComboBox.
            Year.SelectedIndex = yearIndex;
        }

        //This is the handler for when the SubmitSelection button is clicked.
        private void SubmitSelection_Click(object sender, RoutedEventArgs e)
        {
            Error.Text = "";
            quarter = (string)Quarter.SelectedValue;
            year = (string)Year.SelectedValue;
            start = 0;
            end = 10;
            quarterIndex = Quarter.SelectedIndex;
            yearIndex = Year.SelectedIndex;

            //This checks to see if the app is in snapped view, if it is it unsnappes it.
            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped ||
                        Windows.UI.ViewManagement.ApplicationView.TryUnsnap() == true)
            {

            }

            //If the quarter and year ComboBox items selected are thier values which just describe to the user which ComboBox they are, display an error message.
            if (quarter == "Quarter" && year == "Year")
            {
                Error.Text = "Please select a year and a quarter to query or alternatively just a year.";
            }
            //if quarter has data but year doesn't then display an error message as a quarter must have a year selected with it. 
            else if (quarter != "Quarter" && year == "Year")
            {
                Error.Text = "All quarters must have an accompanying year.";
            }
            else
            {
                //If the user has made a valid selection then the table will contain data and hasData wil be true.
                DataFetch();
                hasData = true;
            }
                        
        }

        //Next_Click uses the hasData boolean to know whether to run its code or not as if there is no data in the table then next can't be pressed.
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (hasData)
            {
                if (end + 10 > hospitalDetails.Count)
                {
                    //224 is the maximum record number you recieve back on any query.
                    end = 224;
                    start = 220;
                }
                else
                {
                    start += 10;
                    end += 10;
                }
                RankingsSelection();
            }
            else
            {
                Error.Text = "Please ensure the table has data before clicking next/previous.";
            }
        }

        //Previous_Click uses the hasData boolean to know whether to run its code or not as if there is no data in the table then perevious can't be pressed.
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (hasData)
            {
                if (start - 10 < 0)
                {
                    end = 10;
                    start = 0;
                }
                else
                {
                    //224 is the maximum record number you recieve back on any query.
                    if (end == 224)
                    {
                        end = 220;
                        start = 210;
                    }
                    else
                    {
                        start -= 10;
                        end -= 10;
                    }
                }
                RankingsSelection();
            }
            else
            {
                Error.Text = "Please ensure the table has data before clicking next/previous.";
            }
        }

        private async void DataFetch()
        {
            if (quarter != null && year != null)
            {
                switch (quarter)
                {
                    case "Quarter 1":
                        quarter = "q1_activity";
                        break;
                    case "Quarter 2":
                        quarter = "q2_activity";
                        break;
                    case "Quarter 3":
                        quarter = "q3_activity";
                        break;
                    case "Quarter 4":
                        quarter = "q4_activity";
                        break;
                }

                switch (year)
                {
                    case "2010":
                        year = "hospital_activity_2010";
                        break;
                    case "2009":
                        year = "hospital_activity_2009";
                        break;
                    case "2008":
                        year = "hospital_activity_2008";
                        break;
                }

                //If the user has selected both a quarter and a year.
                if (quarter != "Quarter" & year != "Year")
                {
                    try
                    {
                        // Get the list of Names from the web service and deserialise it into a List of Names and codes
                        HttpClient client = new HttpClient();
                        HttpResponseMessage response = await client.GetAsync("http://aeactivityapp.azurewebsites.net/get_data.php?section=ranking&table=" + year + "&row=" + quarter);
                        response.EnsureSuccessStatusCode();
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<RankingData>), new XmlRootAttribute("ArrayOfData"));
                            hospitalDetails = (List<RankingData>)serializer.Deserialize(stream);
                        }
                        RankingsSelection();
                    }
                    catch
                    {
                        Error.Text = "Failed to connect to database, please close application and try again later.";
                    }
                }
                else
                {
                    //If the user has selected just a year.
                    if (quarter == "Quarter" && year != "Year")
                    {
                        try
                        {
                            // Get the list of Names from the web service and deserialise it into a List of Names and codes
                            HttpClient client = new HttpClient();
                            HttpResponseMessage response = await client.GetAsync("http://aeactivityapp.azurewebsites.net/get_data.php?section=ranking&byYear=yearOnly&table=" + year);
                            response.EnsureSuccessStatusCode();
                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(List<RankingData>), new XmlRootAttribute("ArrayOfData"));
                                hospitalDetails = (List<RankingData>)serializer.Deserialize(stream);
                            }
                            RankingsSelection();
                        }
                        catch
                        {
                            Error.Text = "Failed to connect to database, please close application and try again later.";
                        }
                    }
                }
            }
        }

        private void RankingsSelection()
        {
            data = new List<RankingData>();
            for (int i = start; i < end; i++)
            {
                data.Add(hospitalDetails[i]);
            }
            //If end is equal to the end of the data list then create a new RankingData variable and make it blank, this way you can populate the rest of the table's rows
            //and avoid a crash.
            if (end == 224)
            {
                RankingData rData = new RankingData("","","","");
                for (int k = 0; k < 6; k++)
                {
                    data.Add(rData);
                }
            }

            PopulateRankingList(data);
        }

        private void PopulateRankingList(List<RankingData> data)
        {
            //Ranking Number
            Rank1.Text = data[0].RankNumber;
            Rank2.Text = data[1].RankNumber;
            Rank3.Text = data[2].RankNumber;
            Rank4.Text = data[3].RankNumber;
            Rank5.Text = data[4].RankNumber;
            Rank6.Text = data[5].RankNumber;
            Rank7.Text = data[6].RankNumber;
            Rank8.Text = data[7].RankNumber;
            Rank9.Text = data[8].RankNumber;
            Rank10.Text = data[9].RankNumber;

            //Hospital Code
            Code1.Text = data[0].HospitalCode;
            Code2.Text = data[1].HospitalCode;
            Code3.Text = data[2].HospitalCode;
            Code4.Text = data[3].HospitalCode;
            Code5.Text = data[4].HospitalCode;
            Code6.Text = data[5].HospitalCode;
            Code7.Text = data[6].HospitalCode;
            Code8.Text = data[7].HospitalCode;
            Code9.Text = data[8].HospitalCode;
            Code10.Text = data[9].HospitalCode;

            //Hospital Name;
            Name1.Text = data[0].HospitalName;
            Name2.Text = data[1].HospitalName;
            Name3.Text = data[2].HospitalName;
            Name4.Text = data[3].HospitalName;
            Name5.Text = data[4].HospitalName;
            Name6.Text = data[5].HospitalName;
            Name7.Text = data[6].HospitalName;
            Name8.Text = data[7].HospitalName;
            Name9.Text = data[8].HospitalName;
            Name10.Text = data[9].HospitalName;

            //Hospital Attendee Amount
            Num1.Text = data[0].Data;
            Num2.Text = data[1].Data;
            Num3.Text = data[2].Data;
            Num4.Text = data[3].Data;
            Num5.Text = data[4].Data;
            Num6.Text = data[5].Data;
            Num7.Text = data[6].Data;
            Num8.Text = data[7].Data;
            Num9.Text = data[8].Data;
            Num10.Text = data[9].Data;
        }

        //Below are the handlers for the app bar items, these act as navigation controls.
        private void SpecificNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Specific));
            }

        }

        private void ComparisonNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Comparison));
            }

        }

        private void HomeNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Welcome));
            }
        }
    }
}
