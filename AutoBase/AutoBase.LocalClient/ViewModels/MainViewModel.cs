using AutoBase.LocalClient.Properties;
using AutoBase.LocalClient.WorkPlaces.MakesWP;
using DevExpress.Xpf.Core;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AutoBase.UI.Res.Properties;

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
            if (IsInDesignMode) return;
            WorkPlaceList = new ObservableCollection<WorkPlaceViewModelBase>();
        }

        #region Command 

        private ICommand _themeClick;

        public ICommand ThemeClick
        {
            get { return _themeClick ?? (_themeClick = new RelayCommand<string>(ThemeClickExecute, s => true)); }
        }

        private ICommand _makesClick;

        public ICommand MakesClick
        {
            get { return _makesClick ?? (_makesClick = new RelayCommand(MakesClickExecute)); }
        }

        private ICommand _addMakeCommand;

        public ICommand AddMakeCommand
        {
            get { return _addMakeCommand ?? (_addMakeCommand = new RelayCommand(AddMakeExecute)); }
        }


        #endregion

        #region Execution 

        private void AddMakeExecute()
        {
            // ShowFromWorkplaceDialog()
        }

        private void MakesClickExecute()
        {
            ActivateWP(new MakesViewModel(this, Strings.Makes));
        }

        #endregion

        public ObservableCollection<WorkPlaceViewModelBase> WorkPlaceList
        {
            get { return _workPlaceList; }
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
            ThemeManager.SetThemeName(GetWindow(typeof(MainWindowViewModel)), param);
        }

        #endregion
    }
}