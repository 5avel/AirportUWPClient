using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class PlaneTypeViewModel : BaseViewModel
    {
        private PlaneType _model;
        private IPlaneTypesService _service;
        public PlaneTypeViewModel(INavigationService navigationService, IPlaneTypesService service) : base(navigationService)
        {
            _model = new PlaneType();
            _service = service;

            GoBackCommand = new RelayCommand(goBack);


            SaveItemCommand = new RelayCommand(SaveItem);

            MessengerInstance.Register<PlaneType>(this, entity =>
            {
                _model = entity;
            });
        }
        public int ServiceLifeDays
        {
            get { return _model.ServiceLife.Days; }
            set
            {
                _model.ServiceLife = new TimeSpan(value, 0, 0, 0);
                RaisePropertyChanged();
            }
        }

        public PlaneType PlaneType
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
            MessengerInstance.Send<PlaneType, PlaneTypesViewModel>(_model);
            goBack();

        }

        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
