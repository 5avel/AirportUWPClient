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
    public class PilotsViewModel : BaseViewModel
    {
        private IPilotsService _service;

        public PilotsViewModel(INavigationService navigationService, IPilotsService service)
            : base(navigationService)
        {
            Title = "Pilots";
            _service = service;
            AddNewItemCommand = new RelayCommand(AddNewItem);
            EditSelectedItemCommand = new RelayCommand(EditSelectedItem);
            SearchCommand = new RelayCommand(SearchAsync);


            UpdateDataAsync().GetAwaiter();
        }

        private async Task UpdateDataAsync()
        {
            var _pilots = await _service.GetAll();
            this.Pilots = new ObservableCollection<Pilot>(_pilots);
           
            RaisePropertyChanged(nameof(Pilots));

        }

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

        protected void SearchAsync()
        {
            List<Pilot> temp = Pilots.ToList();
            Pilots.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                Pilots = new ObservableCollection<Pilot>(temp.Where(s => s.FirstName.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)
                                            || s.LastName.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(Pilots));
        }

        public ObservableCollection<Pilot> Pilots { get; private set; }


        private Pilot _selectedPilot;
        public Pilot SelectedPilot
        {
            get => _selectedPilot;
            set
            {
                _selectedPilot = value;
                RaisePropertyChanged(() => SelectedPilot);
            }
        }

        public ICommand SearchCommand { get; set; }


        public ICommand AddNewItemCommand { get; set; }
        public void AddNewItem()
        {
            //_navigationService.NavigateTo(nameof(MainPageViewModel));
        }

        public ICommand EditSelectedItemCommand { get; set; }
        public void EditSelectedItem()
        {
            MessengerInstance.Send(_selectedPilot);
            _navigationService.NavigateTo(nameof(PilotViewModel));
        }

    }
}
