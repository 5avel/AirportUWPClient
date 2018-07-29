using AirportUWPClient.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportUWPClient.ViewModels
{
    public class PilotsViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public PilotsViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }

        public ObservableCollection<Pilot> Pilots { get; private set; }
        

        private Pilot _selectedPilot;
        public Pilot SelectedPilot
        {
            get => _selectedPilot;
            set
            {
                _selectedPilot = value;
                if(_selectedPilot != null)
                {
                    MessengerInstance.Send(_selectedPilot);
                    _navigationService.NavigateTo(nameof(PilotViewModel));
                }
                RaisePropertyChanged(() => SelectedPilot);
            }
        }

    }
}
