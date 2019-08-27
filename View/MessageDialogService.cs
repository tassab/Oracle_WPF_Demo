using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowYesNoDialog(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes ? MessageDialogResult.Yes : MessageDialogResult.No;
        }
    }
}
