using AirportUWPClient.Models;
using AirportUWPClient.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirportUWPClient.ViewModels
{
    public class PlanesViewModel : BaseViewModel
    {
        private IPlanesService _service;
        public PlanesViewModel(INavigationService navigationService, IPlanesService service)
            : base(navigationService)
        {
            Title = "Planes";
            _service = service;
            AddNewItemCommand = new RelayCommand(AddNewItem);
            EditSelectedItemCommand = new RelayCommand(EditSelectedItem);
            DeleteSelectedItemCommand = new RelayCommand(DeleteSelectedItem);
            SearchCommand = new RelayCommand(SearchAsync);


            UpdateDataAsync().GetAwaiter();

            MessengerInstance.Register<Plane>(this, entity =>
            {
                if (entity != null)
                    UpdateDataAsync().GetAwaiter();
            });
        }
        private async Task UpdateDataAsync()
        {
            var _planes = await _service.GetAll();
            this.Planes = new ObservableCollection<Plane>(_planes);
            RaisePropertyChanged(nameof(Planes));
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

        public ICommand SearchCommand { get; set; }
        protected void SearchAsync()
        {
            List<Plane> temp = Planes.ToList();
            Planes.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                Planes = new ObservableCollection<Plane>(temp.Where(s => s.Name.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(Planes));
        }

        public ObservableCollection<Plane> Planes { get; private set; }

        private Plane _selectedPlane;
        public Plane SelectedPlane
        {
            get => _selectedPlane;
            set
            {
                _selectedPlane = value;
                RaisePropertyChanged(() => SelectedPlane);
            }
        }

        public ICommand AddNewItemCommand { get; set; }
        public void AddNewItem()
        {
            MessengerInstance.Send<Plane, PlaneViewModel>(new Plane());
            _navigationService.NavigateTo(nameof(PlaneViewModel));
        }

        public ICommand EditSelectedItemCommand { get; set; }
        public void EditSelectedItem()
        {
            if (_selectedPlane != null)
            {
                MessengerInstance.Send<Plane, PlaneViewModel>(_selectedPlane);
                _navigationService.NavigateTo(nameof(PlaneViewModel));
            }
        }

        public ICommand DeleteSelectedItemCommand { get; set; }
        public void DeleteSelectedItem()
        {
            if (_selectedPlane != null)
            {
                _service.Delete(_selectedPlane.Id);
                UpdateDataAsync();
            }
        }
    }
}
