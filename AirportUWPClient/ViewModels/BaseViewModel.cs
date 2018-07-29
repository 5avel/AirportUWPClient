﻿using GalaSoft.MvvmLight;
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
            
            ShowPilotsCommand = new RelayCommand(ShowPilots);
            ShowPilotCommand = new RelayCommand(ShowPilot);
            ShowMainCommand = new RelayCommand(ShowMain);

            

        }
        
        public ICommand ShowMainCommand { get; set; }
        public void ShowMain() =>
            _navigationService.NavigateTo(nameof(MainPageViewModel));

        public ICommand ShowPilotsCommand { get; set; }
        public void ShowPilots() =>
            _navigationService.NavigateTo(nameof(PilotsViewModel));
        public ICommand ShowPilotCommand { get; set; }
        public void ShowPilot() =>
            _navigationService.NavigateTo(nameof(StewardessesViewModel));


    }
}