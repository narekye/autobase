using AutoBase.LocalClient.ViewModel;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoBase.DAL.AutoBaseEntities;
using System.Threading.Tasks;

namespace AutoBase.LocalClient.WorkPlaces.MakesWP
{
    public class MakesViewModel : WorkPlaceViewModelBase
    {
        private ObservableCollection<Make> _makes;

        public MakesViewModel(MainViewModel place, string displayName) : base(place, displayName)
        {
            
        }

        #region Properties 

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

        

        #endregion

        #region Execution

        private void AddMakeExecute()
        {
            Makes.Add(new Make());
        }

        private async void SaveMakeExecute()
        {
            await Globals.DataProvider.SaveMakesAsync(Makes);
        }
        #endregion

        #region Private methods 

        private async void LoadControl()
        {
            Makes = await Globals.DataProvider.GetMakes();
        }

        public async override Task StartUp()
        {
            await base.StartUp();
            if (IsInDesignMode) return;
            LoadControl();
        }

        #endregion
    }
}
