using System.Threading.Tasks;
using Caliburn.Micro;
using Task2.Infrastructure.Models;
using Task2.Infrastructure.Services;

namespace Task2.ViewModels
{
    public class DashboardViewModel : Conductor<Screen>.Collection.OneActive, IHandle<Catalog>, IHandle<bool>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFrameService _frameService;
        private bool _isActiveProgress;

        public DashboardViewModel(IEventAggregator eventAggregator, IFrameService frameService)
        {
            _eventAggregator = eventAggregator;
            _frameService = frameService;
            eventAggregator.Subscribe(this);
        }

        #region Binding

        /// <summary>
        /// Активация прогресс бара
        /// </summary>
        public bool IsActiveProgress
        {
            get => _isActiveProgress;
            set
            {
                if (value.Equals(_isActiveProgress)) return;
                _isActiveProgress = value;
                NotifyOfPropertyChange(() => IsActiveProgress);
            }
        }

        #endregion

        protected override void OnActivate()
        {
            base.OnActivate();
            ActivateItem(ServiceLocator.Resolve<RibbonViewModel>());
            _frameService.NavigateTo<EmptyDetailsViewModel>();
        }

        public void Handle(Catalog message)
        {
            _frameService.Close();
            _frameService.NavigateTo<DetailsViewModel>(message);
        }

        public void Handle(bool message)
        {
            IsActiveProgress = message;
        }

        public void CloseDetailsCatalog()
        {
            _frameService.Close();
        }

        public void Search(string searchLine)
        {
            if (string.IsNullOrWhiteSpace(searchLine))
            {
                _frameService.Close();
                _eventAggregator.Publish(string.Empty, action => Task.Factory.StartNew(action));
                return;
            }

            if (searchLine.Length >= 3)
            {
                _frameService.Close();
                _eventAggregator.Publish(searchLine, action => Task.Factory.StartNew(action));
            }
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            _eventAggregator.Unsubscribe(this);
        }
    }
}
