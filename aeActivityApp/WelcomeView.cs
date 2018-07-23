using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aeActivityApp
{
    //This class holds the data nessesary to populate the items on the Welcome page.
    public class WelcomeView
    {
        string _title;
        string _description;

        public WelcomeView()
        {
        }

        public WelcomeView(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                }
            }
        }
    }

    public class BaseViewModel
    {
        ObservableCollection<WelcomeView> _items;

        public BaseViewModel()
        {
            _items = new ObservableCollection<WelcomeView>();
        }

        public ObservableCollection<WelcomeView> NaviButton
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }
    }

    public class RealViewModel : BaseViewModel
    {
        public RealViewModel()
        {
            NaviButton.Add(new WelcomeView("Hospital Data", "Find all the attendance data for a hospital of your choice."));
            NaviButton.Add(new WelcomeView("Hospital Rankings", "Find out the rankings of hospitals based on thier attendies."));
            NaviButton.Add(new WelcomeView("Hospital Comparison", "Compare one hospital's data with another's."));
        }
    }

    public class DesignTimeViewModel : BaseViewModel
    {
        public DesignTimeViewModel()
        {

            NaviButton.Add(new WelcomeView("Hospital Data", "Find all the attendance data for a hospital of your choice."));
            NaviButton.Add(new WelcomeView("Hospital Rankings", "Find out the rankings of hospitals based on thier attendies."));
            NaviButton.Add(new WelcomeView("Hospital Comparison", "Compare one hospital's data with another's."));

        }
    }
}
