using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FileRacks.UI.ViewModel
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames != null && propertyNames.Length > 0)
            {
                foreach (string s in propertyNames)
                {
                    OnPropertyChanged(s);
                }
            }
        }

        public void OnAllPropertiesChanged()
        {
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                OnPropertyChanged(prop.Name);
            }
        }
    }
}