using AutoBase.DAL.AutoBaseEntities;
using AutoBase.LocalClient.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace AutoBase.LocalClient.Dialogs.AddModuleDlg
{
    public class AddModuleViewModel : ViewModelBase
    {
        private ICommand _saveBtnCommand;
        private string _moduleName;

        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                if (_moduleName == value) return;
                _moduleName = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveBtnCommand
        {
            get { return _saveBtnCommand ?? (_saveBtnCommand = new RelayCommand(SaveBtnExecute)); }
        }

        private async void SaveBtnExecute()
        {
            var module = new Module { Name = ModuleName };
            await Globals.DataProvider.SaveModuleAsync(module);
            AppCache.Modules.Add(module);
            CloseCurrentWindow(true, typeof(AddModuleWindow));
        }
    }
}
