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
    public class PlaneTypesViewModel : BaseViewModel
    {
        private IPlaneTypesService _service;
        public PlaneTypesViewModel(INavigationService navigationService, IPlaneTypesService service)
            : base(navigationService)
        {
            Title = "PlaneTypes";
            _service = service;
            AddNewItemCommand = new RelayCommand(AddNewItem);
            EditSelectedItemCommand = new RelayCommand(EditSelectedItem);
            DeleteSelectedItemCommand = new RelayCommand(DeleteSelectedItem);
            SearchCommand = new RelayCommand(SearchAsync);


            UpdateDataAsync().GetAwaiter();

            MessengerInstance.Register<PlaneType>(this, entity =>
            {
                if (entity != null)
                    UpdateDataAsync().GetAwaiter();
            });
        }

        private async Task UpdateDataAsync()
        {
            var _planeType = await _service.GetAll();
            this.PlaneTypes = new ObservableCollection<PlaneType>(_planeType);
            RaisePropertyChanged(nameof(PlaneTypes));
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
            List<PlaneType> temp = PlaneTypes.ToList();
            PlaneTypes.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                PlaneTypes = new ObservableCollection<PlaneType>(temp.Where(s => s.Model.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(PlaneTypes));
        }

        public ObservableCollection<PlaneType> PlaneTypes { get; private set; }


        private PlaneType _selectedPlaneType;
        public PlaneType SelectedPlaneType
        {
            get => _selectedPlaneType;
            set
            {
                _selectedPlaneType = value;
                RaisePropertyChanged(() => SelectedPlaneType);
            }
        }




        public ICommand AddNewItemCommand { get; set; }
        public void AddNewItem()
        {
            MessengerInstance.Send<PlaneType, PlaneTypeViewModel>(new PlaneType());
            _navigationService.NavigateTo(nameof(PlaneTypeViewModel));
        }

        public ICommand EditSelectedItemCommand { get; set; }
        public void EditSelectedItem()
        {
            if (_selectedPlaneType != null)
            {
                MessengerInstance.Send<PlaneType, PlaneTypeViewModel>(_selectedPlaneType);
                _navigationService.NavigateTo(nameof(PlaneTypeViewModel));
            }
        }

        public ICommand DeleteSelectedItemCommand { get; set; }
        public void DeleteSelectedItem()
        {
            if (_selectedPlaneType != null)
            {
                _service.Delete(_selectedPlaneType.Id);
                UpdateDataAsync();
            }
        }

    }
}
