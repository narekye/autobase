using AutoBase.LocalClient.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
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

        
        #endregion

        #region Execution

        private void AddMakeExecute()
        {
            Makes.Add(new Make());
        }

        #endregion

        #region Private methods 

        private async void LoadControl()
        {
            Makes = await Globals.DataProvider.Makes();
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
