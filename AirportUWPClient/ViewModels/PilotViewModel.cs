using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace AirportUWPClient.ViewModels
{
    public class PilotViewModel : BaseViewModel
    {
        private Pilot _model;
        private IPilotsService _service;
        public PilotViewModel(INavigationService navigationService, IPilotsService service) : base(navigationService)
        {
            _model = new Pilot();
            _service = service;

            GoBackCommand = new RelayCommand(goBack);


            SaveItemCommand = new RelayCommand(SaveItem);

            MessengerInstance.Register<Pilot>(this, entity =>
            {
                _model = entity;
               
            });
           
        }

        public Pilot Pilot
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
            //MessengerInstance.Send<Pilot, PilotViewModel>(new Pilot());
            //_navigationService.NavigateTo(nameof(PilotViewModel));
        }

        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
