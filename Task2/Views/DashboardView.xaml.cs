using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Task2.Infrastructure.Extensions;

namespace Task2.Views
{
    /// <summary>
    /// Логика взаимодействия для DashboardView.xaml
    /// </summary>
    public partial class DashboardView
    {
        public DashboardView()
        {
            InitializeComponent();

            if (Context.Current.Frame == null || Panel.GetChildOfType<Frame>() != null) return;

            var sp = Context.Current.Frame.Parent as StackPanel;
            sp?.Children.Remove(Context.Current.Frame);

            Context.Current.Frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            Context.Current.Frame.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            Context.Current.Frame.VerticalContentAlignment = VerticalAlignment.Stretch;
            Panel.Children.Add(Context.Current.Frame);
            Panel.SizeChanged += OnPanelSizeChanged;
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Panel.Height = DockPanel.ActualHeight - OptionPanel.ActualHeight;
        }

        private void OnPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Context.Current.Frame.Height = Panel.Height;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Context.Current.Frame = null;
        }
    }
}
