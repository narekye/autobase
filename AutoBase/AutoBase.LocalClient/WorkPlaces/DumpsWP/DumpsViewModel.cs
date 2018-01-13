using AutoBase.DAL.AutoBaseEntities;
using AutoBase.DAL.Filters;
using AutoBase.LocalClient.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoBase.LocalClient.WorkPlaces.DumpsWP
{
    public class DumpsViewModel : WorkPlaceViewModelBase
    {
        private FilterDump _filter;
        private Make _selectedMake;
        private Dump _selectedDump;
        private ObservableCollection<Dump> _dumps;
        private ObservableCollection<Make> _makes;
        private ObservableCollection<Model> _models;
        private ObservableCollection<Module> _modules;

        public DumpsViewModel(MainViewModel place, string displayName) : base(place, displayName)
        {
            Filter = new FilterDump();
            LoadControl();
        }

        #region Command

        private ICommand _deleteMakeCommand;

        public ICommand DeleteMakeCommand
        {
            get { return _deleteMakeCommand ?? (_deleteMakeCommand = new RelayCommand(DeleteMakeExecute)); }
        }

        private ICommand _okBtnCommand;

        public ICommand OkBtnCommand
        {
            get { return _okBtnCommand ?? (_okBtnCommand = new RelayCommand(OkBtnExecute)); }
        }

        private ICommand _deleteDumpCommand;

        public ICommand DeleteDumpCommand
        {
            get { return _deleteDumpCommand ?? (_deleteDumpCommand = new RelayCommand(DeleteDumpExecute, CanDeleteDump)); }
        }


        #endregion

        #region Properties 
        public Dump SelectedDump
        {
            get { return _selectedDump; }
            set
            {
                if (_selectedDump == value) return;
                _selectedDump = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Dump> Dumps
        {
            get { return _dumps; }
            set
            {
                if (_dumps == value) return;
                _dumps = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Module> Modules
        {
            get { return _modules; }
            set
            {
                if (_modules == value) return;
                _modules = value;
                RaisePropertyChanged();
            }
        }

        public Make SelectedMake
        {
            get { return _selectedMake; }
            set
            {
                if (_selectedMake == value) return;
                _selectedMake = value;
                RaisePropertyChanged();
                LoadModels();
            }
        }

        public ObservableCollection<Model> Models
        {
            get { return _models; }
            set
            {
                if (_models == value) return;
                _models = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Make> Makes
        {
            get { return _makes; }
            set
            {
                if (_makes == value) return;
                _makes = value;
                RaisePropertyChanged();
            }
        }

        public FilterDump Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value) return;
                _filter = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Execution

        private void DeleteMakeExecute()
        {
            Filter.MakeId = null;
            SelectedMake = null;
        }

        private async void OkBtnExecute()
        {
            Dumps = await Globals.DataProvider.GetDumpsByFilter(Filter);
        }


        private void DeleteDumpExecute()
        {
            Globals.FileSystem.DeleteFile(SelectedDump);
            Dumps.Remove(SelectedDump);
        }

        #endregion

        #region Private methods 

        private async void LoadControl()
        {
            Makes = await Globals.DataProvider.GetMakes();
            Modules = AppCache.Modules;
        }

        private async void LoadModels()
        {
            Models = await Globals.DataProvider.GetModelsForMake(SelectedMake.Id);
        }

        private bool CanDeleteDump()
        {
            return SelectedDump != null;
        }

        #endregion
    }
}
