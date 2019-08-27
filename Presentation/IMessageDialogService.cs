﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowYesNoDialog(string message, string title);
    }

    public enum MessageDialogResult
    {
        Yes,
        No
    }
}
