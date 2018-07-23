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
    public sealed partial class Comparison : aeActivityApp.Common.LayoutAwarePage
    {
        List<HospName> hospitalDetails = null;
        List<QuarterData> quarterData = null;
        List<QuarterData> quarterData2 = null;
        ChartItems items;
        string code;
        //Names of hospitals selected.
        string hospSelected1;
        string hospSelected2;
        //Index numbers of selected Combobox Items.
        int userSelection1;
        int userSelection2;

        public Comparison()
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
            //This code retrives various session state information.
            if (pageState != null && pageState.ContainsKey("UserSelection1"))
            {
                object value = null;
                if (pageState.TryGetValue("UserSelection1", out value))
                {
                    if (value != null)
                    {
                        userSelection1 = Convert.ToInt32(value);
                    }
                }
            }
            if (pageState != null && pageState.ContainsKey("UserSelection2"))
            {
                object value = null;
                if (pageState.TryGetValue("UserSelection2", out value))
                {
                    if (value != null)
                    {
                        userSelection2 = Convert.ToInt32(value);
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
            //This code saves the user selections into session state holders.
            if (userSelection1 != 0)
            {
                pageState["UserSelection1"] = userSelection1;
            }
            if (userSelection2 != 0)
            {
                pageState["UserSelection2"] = userSelection2;
            }
        }

        //This method populates the 2 ComboBoxes with the names of the hospitals available for selection.
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
                //Orders the list by the name column and then the code column, codes stay matched up with thier names.
                hospitalDetails = hospitalDetails.OrderBy(x => x.Name).ThenBy(x => x.Code).ToList();
                selection1.Items.Add("Please select an item from the list.");
                selection2.Items.Add("Please select an item from the list.");

                foreach (HospName hospName in hospitalDetails)
                {
                    selection1.Items.Add(hospName.Name);
                    selection2.Items.Add(hospName.Name);
                }

                if (userSelection1 != 0)
                {
                    selection1.SelectedIndex = userSelection1;
                }
                else
                {
                    selection1.SelectedIndex = 0;
                }

                if (userSelection2 != 0)
                {
                    selection2.SelectedIndex = userSelection2;
                }
                else
                {
                    selection2.SelectedIndex = 0;
                }
            }
        }

        //This is the handler for when the selected index is changed in the selection1 ComboBox.
        private async void Selection1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selection1.SelectedIndex != 0)
            {
                //selection1  is the name of the first ComboBox.
                hospSelected1 = (string)selection1.SelectedItem;
                userSelection1 = selection1.SelectedIndex;

                for (int i = 0; i < hospitalDetails.Count; i++)
                {
                    if (hospSelected1 == hospitalDetails[i].Name)
                    {
                        code = hospitalDetails[i].Code;
                        break;
                    }
                }

                try
                {
                    // Get the list of quarter data from the service manager.
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
                    TryToPopulate();
                }
            }
        }

        //This is the handler for when the selected index is changed in the selection2 ComboBox.
        private async void Selection2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selection2.SelectedIndex != 0)
            {
                //selection2  is the name of the first ComboBox.
                hospSelected2 = (string)selection2.SelectedItem;
                userSelection2 = selection2.SelectedIndex;

                for (int i = 0; i < hospitalDetails.Count; i++)
                {
                    if (hospSelected2 == hospitalDetails[i].Name)
                    {
                        code = hospitalDetails[i].Code;
                        break;
                    }
                }

                try
                {
                    // Get the list of quarter data from the service manager.
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("http://aeactivityapp.azurewebsites.net/get_data.php?section=hospData&code=" + code);
                    response.EnsureSuccessStatusCode();
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<QuarterData>), new XmlRootAttribute("ArrayOfData"));
                        quarterData2 = (List<QuarterData>)serializer.Deserialize(stream);
                    }
                }
                catch
                {
                    Error.Text = "Failed to connect to database, please close application and try again later.";
                }

                //If the text block Error's text contains nothing, there hasn't been an error.
                if (Error.Text == "")
                {
                    TryToPopulate();
                }
            }
        }

        //This method populates the VisiFire Chart.
        private void TryToPopulate()
        {
            if (quarterData != null && quarterData2 != null)
            {
                /*The reson for these 2 code statements is so that if a user changes one of the ComboBoxes to the default discriptive text (which is the "Please select ect")
                and then selects a different item in the next ComboBox, this code will ensure that both ComboBoxes contain the items selected so the user can be sure of what
                they have selected.*/
                selection1.SelectedIndex = userSelection1;
                selection2.SelectedIndex = userSelection2;

                items = new ChartItems(quarterData, quarterData2, hospSelected1, hospSelected2);

                VisiChart.DataContext = items;

                //If the app is in snapped view this code unsnappes it.
                if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped ||
                 Windows.UI.ViewManagement.ApplicationView.TryUnsnap() == true)
                {

                }
            }
        }

        //Below are handlers for the app bar items, these act as navigation controls.
        private void RankingNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Rankings));
            }

        }

        private void SpecificNav_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Specific));
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
