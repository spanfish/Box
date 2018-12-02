using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box
{
    public class AppConfig : INotifyPropertyChanged
    {
        private SynchronizationContext _context;

        public AppConfig(SynchronizationContext context)
        {
            _context = context;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region LoginResponse 
        private LoginResponse _login;

        public LoginResponse Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Login"));
                }
            }
        }
        #endregion

        #region Account 
        private Account _account;
        public Account Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Account"));
                }
                bool isLogin = true;
                if (this.Login == null || this.Login.Status != 0 || String.IsNullOrEmpty(this.Login.Userid) || String.IsNullOrEmpty(this.Login.Loginsession))
                {
                    isLogin = false;
                }
                if(_account == null || String.IsNullOrEmpty(_account.Grouptype) || String.IsNullOrEmpty(_account.OemfactoryId))
                {
                    isLogin = false;
                }
                this.IsLoginSuccess = isLogin;
            }
        }
        #endregion
        private bool _loginSuccess;
        public bool IsLoginSuccess
        {
            get
            {
                return _loginSuccess;
            }
            set
            {
                _loginSuccess = value;
                OnPropertyChanged("IsLoginSuccess");
            }
        }

        #region Utility 
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler tempEvent = PropertyChanged;
            if (tempEvent != null)
            {
                _context.Post(new SendOrPostCallback(o =>
                {
                    tempEvent.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }), null);
                
            }
        }
        #endregion
    }
}
