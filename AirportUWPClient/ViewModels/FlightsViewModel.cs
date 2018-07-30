using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class FlightsViewModel : BaseViewModel
    {
        private IFlightsService _service;

        public FlightsViewModel(INavigationService navigationService, IFlightsService service)
             : base(navigationService)
        {
            Title = "Flights";
            _service = service;
            AddNewItemCommand = new RelayCommand(AddNewItem);
            EditSelectedItemCommand = new RelayCommand(EditSelectedItem);
            DeleteSelectedItemCommand = new RelayCommand(DeleteSelectedItem);
            SearchCommand = new RelayCommand(SearchAsync);


            UpdateDataAsync().GetAwaiter();

            MessengerInstance.Register<Flight>(this, entity =>
            {
                if (entity != null)
                    UpdateDataAsync().GetAwaiter();
            });
        }

        private async Task UpdateDataAsync()
        {
            var _flights = await _service.GetAll();
            this.Flights = new ObservableCollection<Flight>(_flights);
            RaisePropertyChanged(nameof(Flights));
        }

        public ObservableCollection<Flight> Flights { get; private set; }

        private string _searchFilter;
        public string SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                _searchFilter = value;
                RaisePropertyChanged(() => SearchFilter);
            }
        }

        public ICommand SearchCommand { get; set; }
        protected void SearchAsync()
        {
            List<Flight> temp = Flights.ToList();
            Flights.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                Flights = new ObservableCollection<Flight>(temp.Where(s => s.FlightNumber.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)
                                            || s.DeparturePoint.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)
                                            || s.DeparturePoint.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(Flights));
        }

        private Flight _selectedFlight;
        public Flight SelectedFlight
        {
            get => _selectedFlight;
            set
            {
                _selectedFlight = value;
                RaisePropertyChanged(() => SelectedFlight);
            }
        }

        public ICommand AddNewItemCommand { get; set; }
        public void AddNewItem()
        {
            MessengerInstance.Send<Flight, FlightViewModel>(new Flight());
            _navigationService.NavigateTo(nameof(FlightViewModel));
        }

        public ICommand EditSelectedItemCommand { get; set; }
        public void EditSelectedItem()
        {
            if (_selectedFlight != null)
            {
                MessengerInstance.Send<Flight, FlightViewModel>(_selectedFlight);
                _navigationService.NavigateTo(nameof(FlightViewModel));
            }
        }

        public ICommand DeleteSelectedItemCommand { get; set; }
        public void DeleteSelectedItem()
        {
            if (_selectedFlight != null)
            {
                _service.Delete(_selectedFlight.Id);
                UpdateDataAsync();
            }
        }

    }
}
