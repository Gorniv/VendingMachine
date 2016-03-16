using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += VendingMachine_DispatcherUnhandledException;
        }

        private void VendingMachine_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //Тут например отправляем письмо разработчикам
            MessageBox.Show(e.Exception.Message);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Тут например отправляем письмо разработчикам
            MessageBox.Show(((Exception)e.ExceptionObject).Message);

        }
    }
}
