using DevExpress.Xpf.Docking;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutoBase.LocalClient.ViewModel
{
    public class WorkPlaceViewModelBase : ViewModelBase
    {
        #region Fields

        private bool _isFocused;
        protected readonly MainViewModel _mainModel;

        #endregion // Fields       

        #region Ctor

        public WorkPlaceViewModelBase(MainViewModel mainModel, string displayName, string uniqueId)
        {
            DisplayName = displayName;
            _uniqueId = uniqueId;
            _mainModel = mainModel;
            CloseCommand = new RelayCommand(Close);

            MVVMHelper.SetTargetName(this, "DocumentHost");
        }

        #endregion // Ctor       

        #region Events

        public event WorkplaceClosingEventHandler WorkplaceClosing;



        protected virtual void OnWorkplaceClosing(WorkplaceClosingEventArgs e)
        {
            if (this.WorkplaceClosing != null) this.WorkplaceClosing(this, e);
        }

        public delegate void WorkplaceClosingEventHandler(object sender, WorkplaceClosingEventArgs e);


        #endregion

        #region properties

        public ICommand CloseCommand { get; set; }

        //private string _displayName;

        //public string DisplayName
        //{
        //	get { return _displayName; }
        //	set
        //	{
        //		if (_displayName != value)
        //		{
        //			_displayName = value;
        //			RaisePropertyChanged("DisplayName");
        //		}
        //	}
        //}

        private string _uniqueId;

        public string UniqueId
        {
            get { return _uniqueId; }
        }

        public bool IsStayOpen { get; set; }

        public bool IsFocused
        {
            get { return _isFocused; }
            set
            {
                if (_isFocused == value) return;
                _isFocused = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Workplace windows/dialogs show implementation

        protected delegate void CloseDialogEventHandler();
        protected event CloseDialogEventHandler CloseDialogEvent;

        /// <summary>
        /// Show window from workplace
        /// After closing workplace, window will be closed and disposed 
        /// and event about closing will be removed
        /// </summary>
        /// <param name="window">Window which should be showed</param>
        protected void ShowFromWorkplaceWindow(Window window)
        {
            // dispose object after closing
            window.Closed += (sender, args) =>
            {
                // clear event from old window
                CloseDialogEvent -= window.Close;
                window = null;
            };
            // call close method when workplace close method happend        
            CloseDialogEvent += window.Close;
            // show window
            window.Show();
        }

        /// <summary>
        /// Show dialog from workplace
        /// After closing workplace, dialog will be closed and disposed 
        /// and event about closing will be removed
        /// </summary>
        /// <param name="window">Dialog which should be showed</param>
        /// <returns>Dialog close result (true/false : nullable)</returns>
        protected bool? ShowFromWorkplaceDialog(Window window)
        {
            // dispose object after closing
            window.Closed += (sender, args) =>
            {
                // clear event from old window
                CloseDialogEvent -= window.Close;
                window = null;
            };
            // call close method when workplace close method happend        
            CloseDialogEvent += window.Close;
            // show dialog
            return window.ShowDialog();
        }

        /// <summary>
        /// Check and run event handlers if items exists
        /// </summary>
        protected void CloseDialogEventChecker()
        {
            // close other dialogs
            if (CloseDialogEvent != null)
                CloseDialogEvent();
        }



        #endregion // Workplace windows/dialogs show implementation

        #region methods


        public virtual Task StartUp()
        {
            return Task.FromResult(true);
        }

        public virtual bool HasChanges()
        {
            return false;
        }

        public virtual void Close()
        {
            CloseDialogEventChecker();

            if (this.WorkplaceClosing != null)
            {
                var args = new WorkplaceClosingEventArgs();
                this.WorkplaceClosing(this, args);
                if (args.Cancel) return;
            }
            _mainModel.CloseWorkPlace(this);
        }


        protected WorkPlaceViewModelBase()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region static

        public static void ReplaceItem<T>(IList<T> collection, T currItem, T newItem)
        {
            int pos = collection.IndexOf(currItem);
            if (pos >= 0)
            {
                collection.Remove(currItem);
                if (newItem != null)
                    collection.Insert(pos, newItem);
            }
        }

        #endregion
    }

    public class WorkplaceClosingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}
