using AutoBase.LocalClient.Properties;
using DevExpress.Xpf.Core;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoBase.LocalClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private static ObservableCollection<WorkPlaceViewModelBase> _workPlaceList;
        private WorkPlaceViewModelBase _currWorkPlace;
        public WorkPlaceViewModelBase CurrWorkPlace
        {
            get { return _currWorkPlace; }
            set
            {
                if (Equals(_currWorkPlace, value)) return;
                _currWorkPlace = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {

        }

        #region Command 

        private ICommand _themeClick;

        public ICommand ThemeClick
        {
            get { return _themeClick ?? (_themeClick = new RelayCommand<string>(ThemeClickExecute, s => true)); }
        }

        #endregion

        public ObservableCollection<WorkPlaceViewModelBase> WorkPlaceList
        {
            get { return _workPlaceList; } // ?? (workPlaceList = new ObservableCollection<WorkPlaceViewModelBase>()); }
            set
            {
                _workPlaceList = value;
                RaisePropertyChanged();
            }
        }

        public async void ActivateWP(WorkPlaceViewModelBase givenWindow)
        {
            if (WorkPlaceList.Any(x => x.DisplayName.Equals(givenWindow.DisplayName)))
            {
                WorkPlaceViewModelBase configPanel = WorkPlaceList.First(x => x.DisplayName == givenWindow.DisplayName);
                CurrWorkPlace = configPanel;
                return;
            }
            await givenWindow.StartUp();
            WorkPlaceList.Add(givenWindow);
            CurrWorkPlace = givenWindow;
        }

        internal void CloseWorkPlace(WorkPlaceViewModelBase workPlaceViewModelBase)
        {
            if (!WorkPlaceList.Contains(workPlaceViewModelBase)) return;
            bool hasChanges = workPlaceViewModelBase.HasChanges();
            if (hasChanges)
            {
                WorkPlaceList.Remove(workPlaceViewModelBase);
            }
            else
            {
                WorkPlaceList.Remove(workPlaceViewModelBase);
            }
        }

        #region Private methods 

        void ThemeClickExecute(string param)
        {
            if (string.IsNullOrEmpty(param)) return;
            Settings.Default.Theme = param;
            Settings.Default.Save();
            Globals.DevExpressStyle = param;
            ThemeManager.SetThemeName(GetWindow(typeof(MainWindow)), param);
        }

        #endregion
    }
}