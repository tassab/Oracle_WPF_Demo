using Model;
using System;
using System.Windows.Input;

namespace TestApp
{
    public abstract class BaseConfiguratorVM : VMBase
    {
        #region fields
        protected OracleDB _db;
        protected VMBase _content;
        protected VMBase _prevContent;
        protected IMessageDialogService _messageDialogService;

        private ICommand _forwardButton;
        private ICommand _backButton;
        private string _forwardButtonText;
        private string _backButtonText;
        private string _title;
        #endregion fields

        #region properties
        public VMBase Content { get { return _content; } set { SetProperty(ref _content, value); } }
        public string ForwardButtonText { get { return _forwardButtonText; } set { SetProperty(ref _forwardButtonText, value); } }
        public string BackButtonText { get { return _backButtonText; } set { SetProperty(ref _backButtonText, value); } }
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        public ICommand ForwardButton
        {
            get { return _forwardButton ?? new RelayCommand(OnForwardButton); }
            set { _forwardButton = value; }
        }
        public ICommand BackButton
        {
            get { return _backButton ?? new RelayCommand(OnBackButton); }
            set { _backButton = value; }
        }
        #endregion properties

        protected abstract void OnForwardButton(object param);

        protected abstract void OnBackButton(object param);
    }
}