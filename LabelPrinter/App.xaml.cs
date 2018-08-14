using System;
using System.Windows;
using System.Windows.Threading;

namespace LabelPrinter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Environment.Exit(0);
        }

        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs exceptionEventArgs)
        {
            if (exceptionEventArgs.Exception is ArgumentException argumentException)
            {
                MessageBox.Show(argumentException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Unexpected error occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            exceptionEventArgs.Handled = true;
        }
    }
}
