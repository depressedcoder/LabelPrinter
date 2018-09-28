

using System.Windows;

namespace LabelPrinter 
{
    public class MessageView
    {
        private readonly static object _padLoack = new object();
        private static MessageView _messageViewer;

        private MessageView()
        {
        }

        public static MessageView Instance
        {
            get
            {
                if (_messageViewer == null)
                {
                    lock (_padLoack)
                    {
                        if (_messageViewer == null)
                        {
                            _messageViewer = new MessageView();
                        }
                    }
                }
                return _messageViewer;
            }
        }

        public void ShowInformation(string message)
        {
            MessageBox.Show(message, "Info!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
