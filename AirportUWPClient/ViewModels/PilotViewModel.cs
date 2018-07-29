using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirportUWPClient.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace AirportUWPClient.ViewModels
{
    public class PilotViewModel : BaseViewModel
    {
        private Pilot _model;
        public PilotViewModel(INavigationService navigationService) : base(navigationService)
        {
            _model = new Pilot();

            GoBackCommand = new RelayCommand(goBack);

            MessengerInstance.Register<Pilot>(this, entity =>
            {
                _model = entity;
                RaisePropertyChanged(() => Id);
                RaisePropertyChanged(() => FirstName);
                RaisePropertyChanged(() => LastName);
                RaisePropertyChanged(() => BirthDate);
                RaisePropertyChanged(() => Experience);
                RaisePropertyChanged(() => CrewId);
            });
        }

        public int Id => _model.Id;
        public string FirstName => _model.FirstName;
        public string LastName => _model.LastName;
        public DateTime BirthDate => _model.Birthday;
        public int Experience => _model.Experience;
        public int CrewId => _model.CrewId;



        public ICommand GoBackCommand { get; set; }

        private void goBack()
        {
            _navigationService.GoBack();
        }
    }
}
