using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GenericEventAggregator.Contracts;

namespace GenericEventAggregator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var eventAggregator = new EventAggregator();
            var eventAggregator = new EventAggregatorWithCache();

            var mainWindowViewModel = new MainWindowViewModel(eventAggregator);
            var mainWindowView = new MainWindow() { DataContext = mainWindowViewModel };
            mainWindowView.Show();

            await DelayStartupStatus();

            var statusViewModel = new StatusViewModel(eventAggregator);
            var statusView = new StatusView() { DataContext = statusViewModel };
            statusView.Show();
        }

        private async Task DelayStartupStatus()
        {
            await Task.Run(() => Thread.Sleep(10000));
        }
    }
}
