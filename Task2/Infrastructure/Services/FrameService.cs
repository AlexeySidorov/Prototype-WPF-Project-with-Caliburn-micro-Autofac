using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Action = System.Action;

namespace Task2.Infrastructure.Services
{
    public class FrameService : IFrameService
    {
        public void NavigateTo<T>(object param = null)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, (Action)(() =>
                    ServiceLocator.Resolve<INavigationService>().NavigateToViewModel<T>(param)));
        }

        public void Close()
        {
            BackToViewModel();
        }

        public void Back()
        {
            BackToViewModel();
        }

        private void BackToViewModel()
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, (Action)(() =>
               {
                   var nav = ServiceLocator.Resolve<INavigationService>();
                   if (nav.CanGoBack)
                       nav.GoBack();
               }));
        }
    }

    public interface IFrameService
    {
        void NavigateTo<T>(object param = null);
        void Close();
        void Back();
    }
}
