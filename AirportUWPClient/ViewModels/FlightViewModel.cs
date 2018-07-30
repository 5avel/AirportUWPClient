using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class FlightViewModel : BaseViewModel
    {
        private Flight _model;
        private IFlightsService _service;
        public FlightViewModel(INavigationService navigationService, IFlightsService service) : base(navigationService)
        {
            _model = new Flight();
            _service = service;
            GoBackCommand = new RelayCommand(goBack);

            SaveItemCommand = new RelayCommand(SaveItem);

            MessengerInstance.Register<Flight>(this, entity =>
            {
                _model = entity;
            });
        }

        public DateTimeOffset DepartureTime
        {
            get { return new DateTimeOffset(_model.DepartureTime); }
            set
            {
                _model.DepartureTime = value.DateTime;
                RaisePropertyChanged();
            }
        }

        public DateTimeOffset ArrivalTime
        {
            get { return new DateTimeOffset(_model.ArrivalTime); }
            set
            {
                _model.ArrivalTime = value.DateTime;
                RaisePropertyChanged();
            }
        }

        public Flight Flight
        {
            get => _model;
            set => _model = value;
        }

        public ICommand SaveItemCommand { get; set; }
        public void SaveItem()
        {
            if (_model.Id == 0)
            {
                var res = _service.Add(_model);
            }
            else
            {
                var res = _service.Update(_model);
            }
            MessengerInstance.Send<Flight, FlightsViewModel>(_model);
            goBack();

        }

        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
