using System.Collections.Generic;
using Caliburn.Micro;
using Task2.Infrastructure.Models;
using Task2.Infrastructure.Services;

namespace Task2.ViewModels
{
    public class DetailsViewModel : Screen
    {
        private readonly IFrameService _frameService;
        private string _imageCover;
        private string _title;
        private IDictionary<string, string> _options;
        private string _description;
        private BindableCollection<string> _previews;
        private bool _isVisiblePreview;
        public Catalog Parameter { get; set; }

        #region Binding

        public string ImageCover
        {
            get => _imageCover;
            set
            {
                _imageCover = value;
                NotifyOfPropertyChange();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public IDictionary<string, string> Options
        {
            get => _options;
            set
            {
                _options = value;
                NotifyOfPropertyChange(() => Options);
            }
        }

        public BindableCollection<string> Previews
        {
            get => _previews;
            set
            {
                _previews = value;
                NotifyOfPropertyChange(() => Previews);
            }
        }

        public bool IsVisiblePreview
        {
            get => _isVisiblePreview;
            set
            {
                _isVisiblePreview = value;
                NotifyOfPropertyChange(() => IsVisiblePreview);
            }
        }

        #endregion

        public DetailsViewModel(IFrameService frameService)
        {
            _frameService = frameService;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (Parameter == null)
            {
                _frameService.Back();
                return;
            }

            ImageCover = Parameter.ImageCover;
            Title = Parameter.Title != null ? Parameter.Title.Default : "";
            Options = Parameter.Options;
            Description = Parameter.Description == null ? "Не указано" : Parameter.Description.Default;
            IsVisiblePreview = Parameter.PreviewImages != null && Parameter.PreviewImages.Count > 0;

            if (Parameter.PreviewImages == null || Parameter.PreviewImages.Count == 0) return;

            if (Previews == null)
                Previews = new BindableCollection<string>();

            for (var index = 0; index < Parameter.PreviewImages.Count; index++)
                Previews.Add("http://colorfully.eu/wp-content/uploads/2012/10/empty-road-highway-with-fog-facebook-cover.jpg");
        }
    }
}
