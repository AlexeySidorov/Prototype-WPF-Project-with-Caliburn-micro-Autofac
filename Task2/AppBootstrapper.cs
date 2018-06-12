using System;
using System.Diagnostics;
using System.Windows.Controls;
using Autofac;
using Caliburn.Micro;
using Task2.Infrastructure;
using Task2.Infrastructure.Services;
using Task2.ViewModels;

namespace Task2
{
    public class AppBootstrapper : AutofacBootstrapper<DashboardViewModel>
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureBootstrapper()
        {
            //  you must call the base version first!
            base.ConfigureBootstrapper();

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            //  override namespace naming convention
            EnforceNamespaceConvention = false;
            //  change our view model base type
            ViewModelBaseType = typeof(IScreen);

            if (Execute.InDesignMode == false)
            {

            }
        }
        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e);
        }

        protected override void Configure()
        {
            base.Configure();
            ServiceLocator.SetContainer(Container);
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            Context.Current.Frame = new Frame() { Name = "MainFrame" };
            builder.Register((c) => new FrameAdapter(Context.Current.Frame)).AsSelf().As<INavigationService>();
            builder.Register((c) => new FrameService()).AsSelf().As<IFrameService>();
        }
    }
}

