using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    
    public class StewardessViewModel : BaseViewModel
    {
        private Stewardess _model;
        private IStewardessesService _service;

        public StewardessViewModel(INavigationService navigationService, IStewardessesService service) : base(navigationService)
        {
            _model = new Stewardess();
            _service = service;
            GoBackCommand = new RelayCommand(goBack);

            SaveItemCommand = new RelayCommand(SaveItem);

            MessengerInstance.Register<Stewardess>(this, entity =>
            {
                _model = entity;
            });
        }

        public DateTimeOffset dpDateTime
        {
            get { return new DateTimeOffset(_model.Birthday); }
            set
            {
                _model.Birthday = value.DateTime;
                RaisePropertyChanged();
            }
        }

        public Stewardess Stewardess
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
            MessengerInstance.Send<Stewardess, StewardessesViewModel>(_model);
            goBack();

        }

        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
