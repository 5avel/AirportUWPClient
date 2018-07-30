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
    
    public class TicketsViewModel : BaseViewModel
    {
        private ITicketsService _service;

        public TicketsViewModel(INavigationService navigationService, ITicketsService service)
            : base(navigationService)
        {
            Title = "Tickets";
            _service = service;

            UpdateDataAsync();

            SearchCommand = new RelayCommand(SearchAsync);
            BuyCommand = new RelayCommand(Buy);
            ReturnCommand = new RelayCommand(Return);
        }

        private async Task UpdateDataAsync()
        {
            var _tickets = await _service.GetAll();
            this.Tickets = new ObservableCollection<Ticket>(_tickets);
            RaisePropertyChanged(nameof(Tickets));
        }

        public ObservableCollection<Ticket> Tickets { get; private set; }

       

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
            List<Ticket> temp = Tickets.ToList();
            Tickets.Clear();
            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                temp.Clear();
                UpdateDataAsync();
            }
            else
            {
                Tickets = new ObservableCollection<Ticket>(temp.Where(s => s.FlightNumber.StartsWith(SearchFilter, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            RaisePropertyChanged(nameof(Tickets));
        }

        private Ticket _selectedTicket;
        public Ticket SelectedTicket
        {
            get
            {
                return _selectedTicket;
            }
            set
            {
                _selectedTicket = value;
                RaisePropertyChanged(() => SelectedTicket);
            }
        }

        public ICommand BuyCommand { get; set; }
        public void Buy()
        {
            if (_selectedTicket != null)
            {
                _service.Buy(_selectedTicket.Id);
            }
        }

        public ICommand ReturnCommand { get; set; }
        public void Return()
        {
            if (_selectedTicket != null)
            {
                _service.Return(_selectedTicket.Id);
            }
        }

    }
}
