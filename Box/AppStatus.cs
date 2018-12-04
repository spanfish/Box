using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box
{
    public class AppStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly SynchronizationContext synchronizationContext;

        public AppStatus(System.Windows.Forms.ToolStripProgressBar _ProgressBar, System.Windows.Forms.ToolStripStatusLabel _MsgStatusLabel)
        {
            this.ProgressBar = _ProgressBar;
            this.MsgStatusLabel = _MsgStatusLabel;
            synchronizationContext = SynchronizationContext.Current;
        }

        #region 状态栏
        public System.Windows.Forms.ToolStripProgressBar ProgressBar
        {
            get;
            set;
        }

        public System.Windows.Forms.ToolStripStatusLabel MsgStatusLabel
        {
            get;
            set;
        }
        #endregion

        #region 查询中
        private bool executing;

        public bool Executing
        {
            get
            {
                return executing;
            }
            set
            {
                executing = value;
                this.ProgressBar.Visible = executing;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Executing"));
                }
            }
        }
        #endregion

        #region 状态栏消息
        private string msg;

        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                this.MsgStatusLabel.Text = msg;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Msg"));
                }
            }
        }
        #endregion
    }
}
