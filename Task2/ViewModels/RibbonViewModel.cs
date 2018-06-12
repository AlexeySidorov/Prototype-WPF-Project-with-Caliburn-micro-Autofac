using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Plugin.Connectivity;
using Refit;
using Task2.Infrastructure.Helpers;
using Task2.Infrastructure.Models;
using Task2.Infrastructure.Rest;

namespace Task2.ViewModels
{
    public class RibbonViewModel : Screen, IHandle<string>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRestApi _restApi;
        private BindableCollection<Catalog> _ribbon;
        private int _selectedIndex;

        #region Binding

        public BindableCollection<Catalog> Ribbon
        {
            get => _ribbon;
            set
            {
                _ribbon = value;
                NotifyOfPropertyChange(() => Ribbon);
            }
        }

        /// <summary>
        /// Selected index
        /// </summary>
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        public RibbonViewModel(IEventAggregator eventAggregator, IRestApi restApi)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _restApi = restApi;
            SelectedIndex = -1;
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            await GetCatalogs();
        }

        private void ShowProgress(bool isShow)
        {
            _eventAggregator.Publish(isShow, action => Task.Factory.StartNew(action));
        }

        public void SelectCatalog(Catalog catalog)
        {
            if (catalog == null) return;

            SelectedIndex = -1;
            _eventAggregator.Publish(catalog, action => Task.Factory.StartNew(action));
        }

        private bool IsRequest { get; set; }
        public async void Handle(string message)
        {
            if (IsRequest) return;
            if (!await CheckConnectionServer()) return;

            IsRequest = true;
            ShowProgress(true);

            if (string.IsNullOrWhiteSpace(message))
            {
                await GetCatalogs();
                IsRequest = false;
                ShowProgress(false);
                return;
            }

            try
            {
                var catalogs = await _restApi.Request().SearchCatalogs("title.default", message);
                if (catalogs != null && catalogs.Count > 0)
                    SetCatalogs(catalogs);
                else
                    MessageBox.Show($"Результат поиска для \"{message}\". Ничего не найдено.", "Search");
            }
            catch (ApiException error)
            {
                if (!string.IsNullOrEmpty(error.Content))
                    ErrorMessageHelper.ErrorMessage(error.Content);

                Debug.WriteLine(error);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show("Некорректный ответ от сервера.", "Error");
            }
            
            ShowProgress(false);
            IsRequest = false;
        }

        private async Task GetCatalogs()
        {
            if (!await CheckConnectionServer()) return;

            ShowProgress(true);

            try
            {
                var catalogs = await _restApi.Request().GetCatalogs();
                if (catalogs != null && catalogs.Count > 0)
                    SetCatalogs(catalogs);
            }
            catch (ApiException error)
            {
                if (!string.IsNullOrEmpty(error.Content))
                    ErrorMessageHelper.ErrorMessage(error.Content);

                Debug.WriteLine(error);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show("Некорректный ответ от сервера.", "Error");
            }

            ShowProgress(false);
        }

        private void SetCatalogs(IEnumerable<Catalog> catalogs)
        {
            if (Ribbon != null)
            {
                Ribbon.Clear();
                Ribbon.AddRange(catalogs);
            }
            else
                Ribbon = new BindableCollection<Catalog>(catalogs);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            _eventAggregator.Unsubscribe(this);
        }

        private async Task<bool> CheckConnectionServer()
        {
            if (!await CrossConnectivity.Current.IsRemoteReachable(Context.BaseUrl))
            {
                MessageBox.Show("Соединение с интернетом отсутствует.", "Error");
                return false;
            }

            return true;
        }
    }
}
