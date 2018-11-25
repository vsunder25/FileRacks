using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using FileRacks.Model;
using FileRacks.Resources;
using FileRacksUI;

namespace FileRacks.UI.ViewModel
{
    public class LoginPageVM : VMBase
    {
        #region Private Members
        private List<NavMenu> _navData = new List<NavMenu>();
        private ICommand _loginCommand;
        private ICommand _clearCommand;
        private string _emailInfo;
        private string _passInfo;
        #endregion Private Members

        #region Constructor
        public LoginPageVM()
        {
            LoginCommand = new RelayCommand(cmd => PerformLogin());
            ClearCommand = new RelayCommand(cmd => PerformClear());
            GetNavigationMenu();
        }
        #endregion Constructor

        #region Public Members
        public string HeaderText => "Scanner Login";
        public string EmailLabel => "Email Address";
        public string PasswordLabel => "Password";
        public string LoginLabel => "Login";
        public string ClearLabel => "Clear";
        public List<NavMenu> NavData
        { get { return _navData; } }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set
            {
                _loginCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClearCommand
        {
            get { return _clearCommand; }
            set
            {
                _clearCommand = value;
                OnPropertyChanged();
            }
        }

        public string EmailInfo
        {
            get
            {
                return _emailInfo;
            }

            set
            {
                _emailInfo = value;
                OnPropertyChanged();
            }
        }

        public string PassInfo
        {
            get
            {
                return _passInfo;
            }

            set
            {
                _passInfo = value;
                OnPropertyChanged();
            }
        }
        #endregion Public Members

        #region Private Methods
        private void GetNavigationMenu()
        {

        }

        private void PerformLogin()
        {
            (Application.Current.MainWindow.DataContext as MainWindowVM)
                .Navigate(EnumExt.DisplayEnumText(AppMenu.Scan));
        }

        private void PerformClear()
        {
            EmailInfo = string.Empty;
            PassInfo = string.Empty;
        }
        #endregion Private Methods

    }
}
