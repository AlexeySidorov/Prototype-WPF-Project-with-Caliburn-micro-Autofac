using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task2.Infrastructure.Models
{
    public class Catalog
    {
        private string _cover;
        private IList<string> _platformSupport;
        public string Id { get; set; }

        public string Owner { get; set; }

        public TranslatableValue Title { get; set; }

        public string ShortTitle { get; set; }

        public TranslatableValue Description { get; set; }

        public string Instruction { get; set; }

        public string ContentType { get; set; }

        public string ContentUrl { get; set; }

        public string Cover
        {
            get => _cover;
            set
            {
                _cover = value;
                ImageCover = string.IsNullOrEmpty(_cover) ? null :
                    "https://www.kinopoisk.ru/images/film_big/841813.jpg";
            }
        }

        public IList<string> Tags { get; set; }

        public IList<string> PlatformSupport
        {
            get => _platformSupport;
            set
            {
                _platformSupport = value;

                PlatformCount = _platformSupport == null || _platformSupport.Count == 0
                    ? 0
                    : _platformSupport.Count;
            }
        }

        public IList<string> PreviewImages { get; set; }

        public IList<string> PreviewVideos { get; set; }

        public IDictionary<string, string> Options { get; set; }

        public bool IsPublic { get; set; }

        public string PublishingStatus { get; set; }

        public string RejectReason { get; set; }

        public IList<LicenseParams> LicenseParams { get; set; }

        public Links Links { get; set; }

        public bool ExistsInPlaylist { get; set; }

        #region Design

        [JsonIgnore]
        public string ImageCover { get; set; }

        [JsonIgnore]
        public int PlatformCount { get; set; }

        #endregion
    }
}
