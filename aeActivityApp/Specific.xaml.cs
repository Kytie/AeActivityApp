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
    public sealed partial class Specific : aeActivityApp.Common.LayoutAwarePage
    {
        //Located in HospData, this class allows me to gather a list of general hospital details as well as the 
        //data for the number of attendees per quarter.
        List<HospName> hospitalDetails = null;
        List<QuarterData> quarterData = null;
        string code;
        //This contains the index of the ComboBox item the user has selected.
        private int userSelection; 

        public Specific()
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
            //If this page has been loaded before and it contains a session data state called "userSelection" then run subsequent code.
            if (pageState != null && pageState.ContainsKey("userSelection"))
            {
                object value = null;
                if (pageState.TryGetValue("userSelection", out value))
                {
                    if (value != null)
                    {
                        userSelection = Convert.ToInt32(value);
                    }
                }
            }
            PopulateComboBox();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //Save the userSelection variable data into a session state called userSelection.
            if (userSelection !=0)
            {
                pageState["userSelection"] = userSelection;
            }
        }

        private async void PopulateComboBox()
        {
            try
            {
                // Get the list of Names from the web service and deserialise it into a List of Names and codes
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://aeactivityapp.azurewebsites.net/get_hosp_names.php");
                response.EnsureSuccessStatusCode();
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<HospName>), new XmlRootAttribute("ArrayOfName"));
                    hospitalDetails = (List<HospName>)serializer.Deserialize(stream);
                }
            }
            catch
            {
                Error.Text = "Failed to connect to database, please close application and try again later.";
            }

            //If the text block Error's text contains nothing, there hasn't been an error.
            if (Error.Text == "")
            {
                //This orders the list by the name column and then the code column, codes stay matched up with thier names.
                hospitalDetails = hospitalDetails.OrderBy(x => x.Name).ThenBy(x => x.Code).ToList();
                HospName.Items.Add("Please select an item from the list.");

                foreach (HospName hospName in hospitalDetails)
                {
                    HospName.Items.Add(hospName.Name);
                }

                if (userSelection != 0)
                {
                    HospName.SelectedIndex = userSelection;
                }
                else
                {
                    HospName.SelectedIndex = 0;
                }
            }
            
            
        }

        private async void HospName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //This code runs whenever the ComboBoxes selected index is changed.
            if (HospName.SelectedIndex != 0)
            {
                //This code checks to see if the app is in snapped view, if it is it unsnappes it.
                if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped ||
                Windows.UI.ViewManagement.ApplicationView.TryUnsnap() == true)
                {

                }

                //HospName is the name of the ComboBox.
                string hospSelected = (string)HospName.SelectedItem;
                userSelection = HospName.SelectedIndex;
                for (int i = 0; i < hospitalDetails.Count; i++)
                {
                    if (hospSelected == hospitalDetails[i].Name)
                    {
                        code = hospitalDetails[i].Code;
                        break;
                    }
                }

                try
                {
                    // Get the list of quarter data from the web service.
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("http://aeactivityapp.azurewebsites.net/get_data.php?section=hospData&code=" + code);
                    response.EnsureSuccessStatusCode();
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<QuarterData>), new XmlRootAttribute("ArrayOfData"));
                        quarterData = (List<QuarterData>)serializer.Deserialize(stream);
                    }
                }
                catch
                {
                    Error.Text = "Failed to connect to database, please close application and try again later.";
                }

                //If the text block Error's text contains nothing, there hasn't been an error.
                if (Error.Text == "")
                {
                    //ChartItems is a class that organises the quarter data so it can be used in the VisiFire graph.
                    ChartItems items = new ChartItems(quarterData, hospSelected);
                    VisiChart.DataContext = items;
                }
            }
        }

        //Below are handlers for the items on the appbar, my app bar items simply act as hyperlinks.
        private void RankingNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Rankings));
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
