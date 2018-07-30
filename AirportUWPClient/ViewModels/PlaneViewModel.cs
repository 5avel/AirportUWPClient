using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class PlaneViewModel : BaseViewModel
    {
        private Plane _model;
        private IPlanesService _service;
        public PlaneViewModel(INavigationService navigationService, IPlanesService service) : base(navigationService)
        {
            _model = new Plane();
            _service = service;

            GoBackCommand = new RelayCommand(goBack);


            SaveItemCommand = new RelayCommand(SaveItem);

            MessengerInstance.Register<Plane>(this, entity =>
            {
                _model = entity;
            });
        }

        public DateTimeOffset dpDateTime
        {
            get { return new DateTimeOffset(_model.ReleaseDate); }
            set
            {
                _model.ReleaseDate = value.DateTime;
                RaisePropertyChanged();
            }
        }

        public Plane Plane
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
            MessengerInstance.Send<Plane, PlanesViewModel>(_model);
            goBack();

        }

        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
