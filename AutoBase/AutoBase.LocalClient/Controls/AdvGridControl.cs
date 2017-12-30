using DevExpress.Xpf.Grid;
using System;
using System.Linq;

namespace AutoBase.LocalClient.Controls
{
    public class AdvGridControl : GridControl
    {
        public AdvGridControl()
        {
            Initialized += OnInitializedChanged;
        }

        private void OnInitializedChanged(object obj, EventArgs e)
        {
            var advGrdControl = obj as AdvGridControl;
            if (advGrdControl != null)
            {
                
                    advGrdControl.SetColumns();
                
                var tableView = advGrdControl.View as TableView;
                if (tableView != null)
                    tableView.GroupSummaryDisplayMode = GroupSummaryDisplayMode.AlignByColumns;
                //Set column filter mode

                foreach (var column in Columns)
                    column.FilterPopupMode = FilterPopupMode.CheckedList;
            }
         
        }
        private void SetColumns()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                Columns.ElementAt(i).ReadOnly = false;
            }
        }
    }
}
