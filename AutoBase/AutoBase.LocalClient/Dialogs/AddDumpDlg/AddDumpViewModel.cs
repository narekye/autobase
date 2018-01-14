using AutoBase.DAL.AutoBaseEntities;
using AutoBase.LocalClient.ViewModel;
using AutoBase.UI.Res.Properties;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoBase.LocalClient.Dialogs.AddDumpDlg
{
    public class AddDumpViewModel : ViewModelBase
    {
        public AddDumpViewModel(Make make)
        {
            Make = make;
            Title = Strings.AddDump + $" / {Make.Name}";
            Models = new ObservableCollection<Model>(Make.Models);
            Modules = AppCache.Modules;
        }

        private Make _make;
        private ObservableCollection<Model> _models;
        private ObservableCollection<Module> _modules;
        private Model _selectedModel;
        private Module _selectedModule;
        private string _year;
        private string _memory;
        private string _blockNumber;
        private string _selectedPath;
        private string _title;

        public string BlockNumber
        {
            get { return _blockNumber; }
            set
            {
                if (_blockNumber == value) return;
                _blockNumber = value;
                RaisePropertyChanged();
            }
        }

        public string Memory
        {
            get { return _memory; }
            set
            {
                if (_memory == value) return;
                _memory = value;
                RaisePropertyChanged();
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (_year == value) return;
                _year = value;
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
        public Module SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                if (_selectedModule == value) return;
                _selectedModule = value;
                RaisePropertyChanged();
            }
        }

        public Model SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                if (_selectedModel == value) return;
                _selectedModel = value;
                RaisePropertyChanged();
            }
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

        public Make Make
        {
            get { return _make; }
            set
            {
                if (_make == value) return;
                _make = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                if (_selectedPath == value) return;
                _selectedPath = value;
                RaisePropertyChanged();
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

        private ICommand _browseForFileCommand;

        public ICommand BrowseForFileCommand
        {
            get { return _browseForFileCommand ?? (_browseForFileCommand = new RelayCommand(BrowseForFileExecute)); }
        }

        private ICommand _okBtnCommand;

        public ICommand OkBtnCommand
        {
            get { return _okBtnCommand ?? (_okBtnCommand = new RelayCommand(OkBtnExecute, CanExecuteOkCommand)); }
        }

        private bool CanExecuteOkCommand()
        {
            return SelectedPath != null && SelectedModel != null && !string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(Memory) && !string.IsNullOrEmpty(BlockNumber);
        }

        private async void OkBtnExecute()
        {
            int year = 0;
            bool isYearParsed = int.TryParse(Year, out year);

            var dump = new Dump
            {
                MakeId = Make.Id,
                ModelId = SelectedModel.Id,
                Year = year,
                Memory = Memory,
                BlockNumber = BlockNumber,
                ModuleId = SelectedModule.Id
            };

            var path = Globals.FileSystem.UploadFileToDestination(SelectedPath, dump);
            dump.Path = path;
            dump.Make = null;
            dump.Model = null;
            dump.Module = null;
            try { await Globals.DataProvider.Dal.Save<Dump>(dump); }
            catch { }
            CloseCurrentWindow(true, typeof(AddDumpWindow));
        }

        private void BrowseForFileExecute()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var filePathWithName = dialog.FileName;
                SelectedPath = filePathWithName;
            }
        }
    }
}