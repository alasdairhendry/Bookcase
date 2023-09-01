using Caliburn.Micro;
using DataManager.Library.DataAccess;
using DataManager.Library.Internal.DataAccess;
using DesktopInterface.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DesktopInterface.Core
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container.Instance(container);

            // Configure Instance types
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            container.Instance(config);

            // Configure Per Request types
            container.PerRequest<ISqlConnector, SqlConnector>()
                .PerRequest<IBookDataConnector, BookDataConnector>();

            // Configure Singleton types
            container.Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            // Connect our ViewModels to our Views using reflection
            // These are PER REQUEST!
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
