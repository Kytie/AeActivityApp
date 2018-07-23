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

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace aeActivityApp
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class Welcome : aeActivityApp.Common.LayoutAwarePage
    {
        //This is located in the WelcomeView class file. This is used for creating the clickable grid items to naviage to other pages.
        RealViewModel ViewModel;

        public Welcome()
        {
            this.InitializeComponent();
            ViewModel = new RealViewModel();
            this.DataContext = ViewModel;
        }

        private void ItemGridView_ItemClicked(object sender, ItemClickEventArgs e)
        {
            //This code handles navigation between my content pages, the items on the Welcome page act as hyperlinks.
            WelcomeView page = (WelcomeView)e.ClickedItem;
            if (page.Title.Equals("Hospital Data"))
            {
                this.Frame.Navigate(typeof(Specific));
            }
            if (page.Title.Equals("Hospital Rankings"))
            {
                this.Frame.Navigate(typeof(Rankings));
            }
            if (page.Title.Equals("Hospital Comparison"))
            {
                this.Frame.Navigate(typeof(Comparison));
            }
        }
    }
}
