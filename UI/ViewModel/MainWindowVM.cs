using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using FileRacks.Model;
using FileRacks.Resources;
using FileRacks.UI.View;

namespace FileRacks.UI.ViewModel
{
    public class MainWindowVM : VMBase
    {
        #region Private Members
        private ICommand _menuNavCommand;
        private UserControl _appViewPage = null;
        private bool _showAppHeader = false;
        private ObservableCollection<NavMenu> _navData = new ObservableCollection<NavMenu>();
        #endregion Private Members

        #region Constructor
        public MainWindowVM()
        {
            MenuNavCommand = new RelayCommand(cmd => Navigate(cmd));
            AppViewPage = new LoginView();
        }
        #endregion Constructor

        #region Public Members
        public static UserInfo AppUser = null;

        public string ApplicationName => "FileRacks ScanDoc";
        public string ProfileMenuName => EnumExt.DisplayEnumText(AppMenu.Profile);
        public string ZoneSelectionMenuName => EnumExt.DisplayEnumText(AppMenu.ZoneSelection);
        public string ScanMenuName => EnumExt.DisplayEnumText(AppMenu.Scan);
        public string BatchManagerMenuName => EnumExt.DisplayEnumText(AppMenu.BatchManager);
        public string BatchDetailsMenuName => EnumExt.DisplayEnumText(AppMenu.BatchDetails);
        public string LogOutMenuName => EnumExt.DisplayEnumText(AppMenu.LogOut);

        public UserControl AppViewPage
        {
            get { return _appViewPage; }

            set { _appViewPage = value; OnPropertyChanged(); }
        }

        public bool ShowAppHeader
        {
            get { return _showAppHeader; }

            set { _showAppHeader = value; OnPropertyChanged(); }
        }

        public ObservableCollection<NavMenu> NavData
        {
            get { return _navData; }

            set { _navData = value; OnPropertyChanged(); }
        }

        public ICommand MenuNavCommand
        {
            get { return _menuNavCommand; }
            set
            {
                _menuNavCommand = value;
                OnPropertyChanged();
            }
        }

        internal void Navigate(object param)
        {
            ShowAppHeader = true;
            switch (Convert.ToString(param))
            {
                case "Profile":
                    AppViewPage = new ProfileView();
                    break;
                case "Zone Selection":
                    AppViewPage = null;
                    break;
                case "Scan":
                    AppViewPage = new ScanView();
                    break;
                case "Batch Manager":
                    AppViewPage = null;
                    break;
                case "Batch Details":
                    AppViewPage = null;
                    break;
                case "Log Out":
                    AppViewPage = new LoginView();
                    ShowAppHeader = false;
                    break;
            }
        }
        #endregion Public Members
    }
}
