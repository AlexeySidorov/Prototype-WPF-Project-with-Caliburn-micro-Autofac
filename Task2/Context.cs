using System;
using System.Windows.Controls;

namespace Task2
{
    public class Context
    {
        private static readonly Lazy<Context> Instance = new Lazy<Context>(() => new Context());
        public static Context Current => Instance.Value;

        /// <summary>
        /// Главный  фрейм
        /// </summary>
        public Frame Frame { get; set; }

        public static string BaseUrl = "http://ds.aggregion.com/";

        public string BaseApiUrl = $"http://ds.aggregion.com/api";
    }
}
