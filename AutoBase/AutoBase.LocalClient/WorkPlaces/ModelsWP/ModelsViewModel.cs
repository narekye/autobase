using AutoBase.DAL.AutoBaseEntities;
using AutoBase.LocalClient.ViewModel;
using System.Collections.ObjectModel;

namespace AutoBase.LocalClient.WorkPlaces.ModelsWP
{
    public class ModelsViewModel : WorkPlaceViewModelBase
    {
        public ModelsViewModel(MainViewModel window, string displayName) : base(window, displayName)
        {
            LoadControl();
        }

        private ObservableCollection<Model> _models;

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

        private async void LoadControl()
        {
            Models = await Globals.DataProvider.GetModels();
        }
    }
}
