using LabelPrinter.DatabaseWatcher;
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
            //Subscribe wather to objerb the database changes in the table LABLES_IN
            WatherSelector.Instance.GetWatcher().NotifyNewItem();

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
                MessageView.Instance.ShowError(argumentException.Message);
            }
            else
            {
                MessageView.Instance.ShowError("Unexpected error occurred");
            }
            exceptionEventArgs.Handled = true;
        }
    }
}
