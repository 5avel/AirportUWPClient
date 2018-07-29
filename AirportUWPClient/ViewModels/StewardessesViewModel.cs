using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportUWPClient.ViewModels
{
    public class StewardessesViewModel : BaseViewModel
    {
        public StewardessesViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Stewardesses";
        }
    }
}
