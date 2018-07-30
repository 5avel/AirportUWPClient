using AirportUWPClient.Services;
using AirportUWPClient.ViewModels;
using AirportUWPClient.Views;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;


namespace AirportUWPClient
{
    public class ViewModelLocator
    {
        public NavigationService navigationService;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            navigationService = new NavigationService();
            navigationService.Configure(nameof(FlightsViewModel), typeof(FlightsView));
            navigationService.Configure(nameof(FlightViewModel), typeof(FlightView));
            navigationService.Configure(nameof(PilotsViewModel), typeof(PilotsView));
            navigationService.Configure(nameof(PilotViewModel), typeof(PilotView));
            navigationService.Configure(nameof(StewardessesViewModel), typeof(StewardessesView));
            navigationService.Configure(nameof(StewardessViewModel), typeof(StewardessView));
            navigationService.Configure(nameof(PlanesViewModel), typeof(PlanesView));
            navigationService.Configure(nameof(PlaneViewModel), typeof(PlaneView));
            navigationService.Configure(nameof(PlaneTypesViewModel), typeof(PlaneTypesView));
            navigationService.Configure(nameof(PlaneTypeViewModel), typeof(PlaneTypeView));

            navigationService.Configure(nameof(TicketsViewModel), typeof(TicketsView));

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IPilotsService, PilotsService>();
                SimpleIoc.Default.Register<IStewardessesService, StewardessesService>();
                SimpleIoc.Default.Register<IFlightsService, FlightsService>();
                SimpleIoc.Default.Register<IPlanesService, PlanesService>();
                SimpleIoc.Default.Register<IPlaneTypesService, PlaneTypesService>();

                SimpleIoc.Default.Register<ITicketsService, TicketsService>();
            }

            //Register your services used here
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<FlightsViewModel>();
            SimpleIoc.Default.Register<FlightViewModel>();
            SimpleIoc.Default.Register<PilotsViewModel>();
            SimpleIoc.Default.Register<PilotViewModel>();
            SimpleIoc.Default.Register<StewardessesViewModel>();
            SimpleIoc.Default.Register<StewardessViewModel>();
            SimpleIoc.Default.Register<PlanesViewModel>();
            SimpleIoc.Default.Register<PlaneViewModel>();
            SimpleIoc.Default.Register<PlaneTypesViewModel>();
            SimpleIoc.Default.Register<PlaneTypeViewModel>();
            SimpleIoc.Default.Register<TicketsViewModel>();


            // ServiceLocator.Current.GetInstance<StewardessesViewModel>(); // <-- не понятно зачем но без вызова первый раз не работает
            ServiceLocator.Current.GetInstance<PilotViewModel>();
            ServiceLocator.Current.GetInstance<StewardessViewModel>();
            ServiceLocator.Current.GetInstance<FlightViewModel>();
            ServiceLocator.Current.GetInstance<PlaneViewModel>();
            ServiceLocator.Current.GetInstance<PlaneTypeViewModel>();
        }

     

        public FlightsViewModel FlightsVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FlightsViewModel>();
            }
        }

        public FlightViewModel FlightVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FlightViewModel>();
            }
        }

        public PilotsViewModel PilotsVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PilotsViewModel>();
            }
        }

        public PilotViewModel PilotVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PilotViewModel>();
            }
        }

        public StewardessesViewModel StewardessesVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StewardessesViewModel>();
            }
        }
        public StewardessViewModel StewardessVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StewardessViewModel>();
            }
        }
        public PlanesViewModel PlanesVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlanesViewModel>();
            }
        }

        public PlaneViewModel PlaneVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlaneViewModel>();
            }
        }

        public PlaneTypesViewModel PlaneTypesVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlaneTypesViewModel>();
            }
        }

        public PlaneTypeViewModel PlaneTypeVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlaneTypeViewModel>();
            }
        }
        public TicketsViewModel TicketsVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TicketsViewModel>();
            }
        }

        // <summary>
        // The cleanup.
        // </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
