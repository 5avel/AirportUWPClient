using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
    
        public List<MenuItem> MenuItems { get; set; }
        public MainPageViewModel(INavigationService navigationService)
            : base (navigationService)
        {
            


            MenuItems = new List<MenuItem>
            {
                new MenuItem{MenuItemName = "Flights"},
                new MenuItem{MenuItemName = "Tickets"},
                new MenuItem{MenuItemName = "Departure"},
                new MenuItem{MenuItemName = "Crews"},
            };
        }


    }



    public class MenuItem
    {
        public string MenuItemName { get; set; }
    }
}
