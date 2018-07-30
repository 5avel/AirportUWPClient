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
    public class StewardessesViewModel : BaseViewModel
    {
        private IStewardessesService _service;
        public StewardessesViewModel(INavigationService navigationService, IStewardessesService service)
            :base(navigationService)
        {
            Title = "Stewardesses";
            _service = service;
            AddNewItemCommand = new RelayCommand(AddNewItem);
            EditSelectedItemCommand = new RelayCommand(EditSelectedItem);
            DeleteSelectedItemCommand = new RelayCommand(DeleteSelectedItem);
            SearchCommand = new RelayCommand(SearchAsync);


            UpdateDataAsync().GetAwaiter();

            MessengerInstance.Register<Stewardess>(this, entity =>
            {
                if (entity != null)
                    UpdateDataAsync().GetAwaiter();
            });
        }

        public ObservableCollection<Stewardess> Stewardesses { get; private set; }

        private async Task UpdateDataAsync()
        {
            var temp = await _service.GetAll();
            this.Stewardesses = new ObservableCollection<Stewardess>(temp);
            RaisePropertyChanged(nameof(Stewardesses));
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
            List<Stewardess> temp = Stewardesses.ToList();
            Stewardesses.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                Stewardesses = new ObservableCollection<Stewardess>(temp.Where(s => s.FirstName.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)
                                            || s.LastName.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(Stewardesses));
        }

        private Stewardess _selectedItem;
        public Stewardess SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SearchCommand { get; set; }

        public ICommand AddNewItemCommand { get; set; }
        public void AddNewItem()
        {
            MessengerInstance.Send<Stewardess, StewardessViewModel>(new Stewardess());
            _navigationService.NavigateTo(nameof(StewardessViewModel));
        }

        public ICommand EditSelectedItemCommand { get; set; }
        public void EditSelectedItem()
        {
            if (_selectedItem != null)
            {
                MessengerInstance.Send<Stewardess, StewardessViewModel>(_selectedItem);
                _navigationService.NavigateTo(nameof(StewardessViewModel));
            }
        }

        public ICommand DeleteSelectedItemCommand { get; set; }
        public void DeleteSelectedItem()
        {
            if (_selectedItem != null)
            {
                _service.Delete(_selectedItem.Id);
                UpdateDataAsync();
            }
        }
    }
}
