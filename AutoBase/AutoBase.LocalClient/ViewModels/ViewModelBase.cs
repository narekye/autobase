using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoBase.LocalClient.ViewModel
{
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public bool IsInDesignMode
        {
            get { return GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic; }
        }

        public Window GetWindow(Type type)
        {
            return Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window.GetType().FullName == type.FullName);
        }

        protected void CloseCurrentWindow(bool dialogResult, Type windowType)
        {
            Window currentWindow = GetWindow(windowType);
            if (currentWindow != null)
            {
                currentWindow.DialogResult = dialogResult;
                currentWindow.Close();
            }
        }

    }
}
