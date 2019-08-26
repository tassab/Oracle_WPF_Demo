using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowYesNoDialog(string message, string title, MessageBoxImage image);
    }

    public enum MessageDialogResult
    {
        Yes,
        No
    }

    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowYesNoDialog(string message, string title, MessageBoxImage image)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, image) == MessageBoxResult.Yes ? MessageDialogResult.Yes : MessageDialogResult.No;
        }
    }
}
