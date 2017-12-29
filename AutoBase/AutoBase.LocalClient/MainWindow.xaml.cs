using AutoBase.LocalClient.Controls;
using AutoBase.LocalClient.ViewModel;
using DevExpress.Xpf.Docking.Base;
using System.Linq;

namespace AutoBase.LocalClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowViewModel : AdvWindow
    {
        public MainWindowViewModel()
        {
            InitializeComponent();
        }

        private void DocManager_OnDockItemClosing(object sender, ItemCancelEventArgs e)
        {
            var layout = sender as DevExpress.Xpf.Docking.DockLayoutManager;
            if (layout == null || layout.ActiveDockItem == null) return;
            var uniqueId = layout.ActiveDockItem.Caption;
            var context = layout.DataContext as MainViewModel;
            if (context == null) return;

            if (context.WorkPlaceList.Any(x => x.DisplayName.Equals(uniqueId)))
            {
                WorkPlaceViewModelBase configPanel = context.WorkPlaceList.First(x => x.DisplayName.Equals(uniqueId));
                configPanel.Close();
            }
        }
    }
}
