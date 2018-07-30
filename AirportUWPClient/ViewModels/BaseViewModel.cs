using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected INavigationService _navigationService;
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowFlightsCommand = new RelayCommand(ShowFlights);
            ShowTicketsCommand = new RelayCommand(ShowTickets);
            ShowDeparturesCommand = new RelayCommand(ShowDepartures, () => false);
            ShowCrewsCommand = new RelayCommand(ShowCrews, () => false);
            ShowPilotsCommand = new RelayCommand(ShowPilots);
            ShowStewardessesCommand = new RelayCommand(ShowStewardesses);
            ShowPlanesCommand = new RelayCommand(ShowPlanes);
            ShowPlaneTypesCommand = new RelayCommand(ShowPlaneTypes);
        }
        
        public ICommand ShowFlightsCommand { get; set; }
        public void ShowFlights() =>
            _navigationService.NavigateTo(nameof(FlightsViewModel));

        public ICommand ShowTicketsCommand { get; set; }
        public void ShowTickets() =>
            _navigationService.NavigateTo(nameof(TicketsViewModel));

        public ICommand ShowDeparturesCommand { get; set; }
        public void ShowDepartures() =>
            _navigationService.NavigateTo(nameof(FlightsViewModel));

        public ICommand ShowCrewsCommand { get; set; }
        public void ShowCrews() =>
            _navigationService.NavigateTo(nameof(FlightsViewModel));

        public ICommand ShowPilotsCommand { get; set; }
        public void ShowPilots() =>
            _navigationService.NavigateTo(nameof(PilotsViewModel));

        public ICommand ShowStewardessesCommand { get; set; }
        public void ShowStewardesses() =>
            _navigationService.NavigateTo(nameof(StewardessesViewModel));

        public ICommand ShowPlanesCommand { get; set; }
        public void ShowPlanes() =>
            _navigationService.NavigateTo(nameof(PlanesViewModel));

        public ICommand ShowPlaneTypesCommand { get; set; }
        public void ShowPlaneTypes() =>
            _navigationService.NavigateTo(nameof(PlaneTypesViewModel));


    }
}
