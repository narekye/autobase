using AutoBase.LocalClient.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoBase.DAL.AutoBaseEntities;
using System.Threading.Tasks;
using System.Linq;
using AutoBase.LocalClient.Dialogs.AddModelDlg;
using AutoBase.LocalClient.Dialogs.AddModuleDlg;

namespace AutoBase.LocalClient.WorkPlaces.MakesWP
{
    public class MakesViewModel : WorkPlaceViewModelBase
    {
        private ObservableCollection<Make> _makes;
        private Make _selectedMake;
        public MakesViewModel(MainViewModel place, string displayName) : base(place, displayName)
        {

        }

        #region Properties 
        public Make SelectedMake
        {
            get { return _selectedMake; }
            set
            {
                if (_selectedMake == value) return;
                _selectedMake = value;
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

        #endregion

        #region Command

        private ICommand _addMakeCommand;

        public ICommand AddMakeCommand
        {
            get { return _addMakeCommand ?? (_addMakeCommand = new RelayCommand(AddMakeExecute)); }
        }

        private ICommand _saveMakeCommand;

        public ICommand SaveMakeCommand
        {
            get { return _saveMakeCommand ?? (_saveMakeCommand = new RelayCommand(SaveMakeExecute)); }
        }

        private ICommand _addModelCommand;

        public ICommand AddModelCommand
        {
            get { return _addModelCommand ?? (_addModelCommand = new RelayCommand(AddModelExecuteAsync, CanExecuteAddModel)); }

        }

        private ICommand _attachDumpCommad;

        public ICommand AttachDumpCommand
        {
            get { return _attachDumpCommad ?? (_attachDumpCommad = new RelayCommand(AttachDumpExecuteAsync, CanExecuteAttachDump)); }
        }

        private ICommand _addModuleCommand;

        public ICommand AddModuleCommand
        {
            get { return _addModuleCommand ?? (_addModuleCommand = new RelayCommand(AddModuleExecute)); }
        }

        #endregion

        #region Execution
        private async void AttachDumpExecuteAsync()
        {
            var dataContext = new Dialogs.AddDumpDlg.AddDumpViewModel(SelectedMake);
            ShowFromWorkplaceDialog(new Dialogs.AddDumpDlg.AddDumpWindow { DataContext = dataContext });
            await LoadControl();
        }

        private bool CanExecuteAttachDump()
        {
            return SelectedMake != null;
        }

        private void AddModuleExecute()
        {
            var dataContext = new AddModuleViewModel();
            ShowFromWorkplaceDialog(new AddModuleWindow { DataContext = dataContext });
        }

        private void AddMakeExecute()
        {
            Makes.Add(new Make());
        }

        private async void SaveMakeExecute()
        {
            await Globals.DataProvider.SaveMakeAsync(Makes.Last());
            await LoadControl();
        }

        private void AddModelExecuteAsync()
        {
            var dataContext = new AddModelViewModel(SelectedMake);
            ShowFromWorkplaceDialog(new AddModelWindow() { DataContext = dataContext });
        }

        private bool CanExecuteAddModel()
        {
            return SelectedMake != null;
        }
        #endregion

        #region Private methods 

        private async Task LoadControl()
        {
            Makes = await Globals.DataProvider.GetMakes();
        }

        public async override Task StartUp()
        {
            await base.StartUp();
            if (IsInDesignMode) return;
            await LoadControl();
        }

        #endregion
    }
}
