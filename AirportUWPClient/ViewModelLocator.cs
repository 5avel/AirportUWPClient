﻿using AirportUWPClient.Services;
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
            
            
            navigationService.Configure(nameof(MainPageViewModel), typeof(MainPage));
            navigationService.Configure(nameof(PilotsViewModel), typeof(PilotsView));
            navigationService.Configure(nameof(PilotViewModel), typeof(PilotView));
            navigationService.Configure(nameof(StewardessesViewModel), typeof(StewardessesView));
            navigationService.Configure(nameof(StewardessViewModel), typeof(StewardessView));

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IPilotsService, PilotsService>();
                SimpleIoc.Default.Register<IStewardessesService, StewardessesService>();
            }

            //Register your services used here
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<PilotsViewModel>();
            SimpleIoc.Default.Register<PilotViewModel>();
            SimpleIoc.Default.Register<StewardessesViewModel>();
            SimpleIoc.Default.Register<StewardessViewModel>();


            // ServiceLocator.Current.GetInstance<StewardessesViewModel>(); // <-- не понятно зачем но без вызова первый раз не работает
            ServiceLocator.Current.GetInstance<PilotViewModel>();
            ServiceLocator.Current.GetInstance<StewardessViewModel>();
        }

        public MainPageViewModel MainPageVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
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

        // <summary>
        // The cleanup.
        // </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
