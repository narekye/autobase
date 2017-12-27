using AutoBase.LocalClient.ViewModel;
using DevExpress.Xpf.Docking;
using System.Windows;

namespace AutoBase.LocalClient.Controls
{
    public class AdvDocumentGroup : DocumentGroup
    {
        public AdvDocumentGroup()
        {
            SelectedItemChanged += AdvDocumentGroup_SelectedItemChanged;
        }

        void AdvDocumentGroup_SelectedItemChanged(object sender, DevExpress.Xpf.Docking.Base.SelectedItemChangedEventArgs e)
        {

            WorkPlaceViewModelBase vm = null;
            if (this.SelectedTabIndex >= 0)
            {
                BaseLayoutItem item = this.Items[this.SelectedTabIndex];
                vm = item.DataContext as WorkPlaceViewModelBase;
            }

            if (SelectedItem != vm)
                SelectedItem = vm;
        }

        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(WorkPlaceViewModelBase),
            typeof(AdvDocumentGroup), new PropertyMetadata(OnSelectedItemChanged));

        public new WorkPlaceViewModelBase SelectedItem
        {
            get { return GetValue(SelectedItemProperty) as WorkPlaceViewModelBase; }
            set { SetValue(SelectedItemProperty, value); }
        }


        public static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AdvDocumentGroup dGroup = sender as AdvDocumentGroup;
            WorkPlaceViewModelBase vm = null;

            int tabIndex = -1;
            if (e.NewValue == null)
            {
                if (dGroup.Items.Count > 0)
                    tabIndex = 0;
            }
            else
            {
                foreach (var item in dGroup.Items)
                {
                    vm = item.DataContext as WorkPlaceViewModelBase;
                    if (vm != null)
                    {
                        if (vm == e.NewValue)
                        {
                            tabIndex = dGroup.Items.IndexOf(item);
                            break;
                        }
                    }
                }
            }

            if (dGroup.SelectedTabIndex != tabIndex)
                dGroup.SelectedTabIndex = tabIndex;

        }
    }
}
