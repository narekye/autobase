using AutoBase.DAL.AutoBaseEntities;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using AutoBase.UI.Res.Properties;

namespace AutoBase.LocalClient.Dialogs.AddModelDlg
{
    public class AddModelViewModel : ViewModel.ViewModelBase
    {
        private Make _selectedMake;
        private string _modelName;
        private string _title;

        public AddModelViewModel(Make selectedMake)
        {
            _selectedMake = selectedMake;
            Title = Strings.AddModel + $" / {_selectedMake.Name}";
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged();
            }
        }
        public string ModelName
        {
            get { return _modelName; }
            set
            {
                if (_modelName == value) return;
                _modelName = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _saveModelCommand;

        public ICommand SaveModelCommand
        {
            get { return _saveModelCommand ?? (_saveModelCommand = new RelayCommand(SaveModelExecute, CanSaveModel)); }
        }

        private bool CanSaveModel()
        {
            return ModelName != null;
        }

        private void SaveModelExecute()
        {
            Globals.DataProvider.SaveModelAsync(new Model
            {
                MakeId = _selectedMake.Id,
                ModelName = ModelName
            });
            CloseCurrentWindow(true, typeof(AddModelWindow));
        }
    }
}
