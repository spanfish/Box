using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using RestSharp;

namespace Box
{
    public partial class LoginWindow : Form
    {
        public AppConfig AppConfig
        {
            get;
            set;
        }

        private List<Account> accounts;

        private RestClient client;

        private readonly SynchronizationContext synchronizationContext;

        public LoginWindow(AppConfig theApp)
        {
            InitializeComponent();

            this.AppConfig = theApp;
            synchronizationContext = SynchronizationContext.Current;
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(IdTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("输入登陆ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (PwdTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("输入密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                progressBar1.Show();
                button1.Enabled = false;

                try
                {
                    var request = new RestRequest("dlicense/v2/account/handshake", Method.GET);

                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                    var asyncHandle = client.ExecuteAsync<HandshakeResponse>(request, response =>
                    {
                        if(!response.IsSuccessful)
                        {
                            ShowError(response.ErrorMessage);
                            return;
                        }
                        HandshakeResponse handshake = response.Data;

                        if (handshake != null && handshake.Status == 0)
                        {
                            Login(IdTextBox.Text, PwdTextBox.Text, handshake);
                        }
                        else
                        {
                            ShowError(handshake == null ? null : handshake.Msg);
                        }
                    });
                }
                catch
                {
                    progressBar1.Hide();
                    button1.Enabled = true;
                }
            }

        }

        private void ShowError(string errMsg)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                progressBar1.Hide();
                button1.Enabled = true;

                string msg = o as string;
                if (msg == null)
                {
                    msg = "未知错误，请重试";
                }

                MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }), errMsg);
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            progressBar1.Hide();

            IdTextBox.Text = "15372098619";
            PwdTextBox.Text = "yunduan412";

            this.comboBox1.Visible = false;
            this.button2.Visible = false;
        }

        private void Login(string Id, string pwd, HandshakeResponse res)
        {
            var request = new RestRequest("dlicense/v2/account/login", Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("timestamp", res.Timestamp);
            

            byte[] data = System.Text.Encoding.ASCII.GetBytes(pwd + "4969fj#k23#");
            byte[] encryptedPwdByte;

            SHA1 sha = new SHA1CryptoServiceProvider();
            encryptedPwdByte = sha.ComputeHash(data);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte curByte in encryptedPwdByte)
            {
                sb.Append(curByte.ToString("x2"));
            }
            string encryptedPwd = sb.ToString();

            sb = new System.Text.StringBuilder();
            sb.Append("{\"phone\":\"");
            sb.Append("15372098619");
            sb.Append("\",");
            sb.Append("\"password\":\"");
            sb.Append(encryptedPwd);
            sb.Append("\"");
            sb.Append("}");

            string body = sb.ToString();

            string token = body + "xgx3d*fe3478$ukx";

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] srcBytes = System.Text.Encoding.UTF8.GetBytes(token);
            byte[] destBytes = md5.ComputeHash(srcBytes);
            sb = new System.Text.StringBuilder();
            foreach (byte curByte in destBytes)
            {
                sb.Append(curByte.ToString("x2"));
            }

            request.AddHeader("token", sb.ToString());

            request.AddParameter("application/json", body, ParameterType.RequestBody);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var asyncHandle = client.ExecuteAsync<LoginResponse>(request, response => {
                if (!response.IsSuccessful)
                {
                    ShowError(response.ErrorMessage);
                    return;
                }

                LoginResponse loginRes = response.Data;

                if (loginRes != null && loginRes.Status == 0)
                {                    
                    AppConfig.Login = loginRes;

                    if (loginRes.Status == 0)
                    {
                        QueryAccount();
                    }
                }
                else
                {
                    ShowError(loginRes == null ? null : loginRes.Msg);
                }
            });
        }


        private void QueryAccount()
        {
            var request = new RestRequest("dlicense/v2/account/role/query", Method.GET);

            request.AddHeader("reqUserId", AppConfig.Login.Userid);
            request.AddHeader("reqUserSession", AppConfig.Login.Loginsession);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var asyncHandle = client.ExecuteAsync<AccountResponse>(request, response => {
                if (!response.IsSuccessful)
                {
                    ShowError(response.ErrorMessage);
                    return;
                }

                AccountResponse accountRes = response.Data;
                if (accountRes != null && accountRes.Data != null)
                {
                    if (accountRes.Data.Count == 1)
                    {
                        AppConfig.Account = accountRes.Data[0];

                        synchronizationContext.Post(new SendOrPostCallback(o =>
                        {
                            this.Close();
                        }), null);
                    }
                    else
                    {
                        synchronizationContext.Post(new SendOrPostCallback(o =>
                        {
                            this.accounts = o as List<Account>;
                            var source = new BindingSource(accounts, null);
                            this.comboBox1.DataSource = source;
                            this.comboBox1.ValueMember = "OemfactoryId";
                            this.comboBox1.DisplayMember = "OemfactoryName";
                            this.comboBox1.Visible = true;
                            this.button2.Visible = true;
                        }), accountRes.Data);

                    }
                }
                else
                {
                    ShowError(accountRes == null ? null : accountRes.Msg);
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.accounts != null && this.accounts.Count > 0)
            {
                foreach(Account a in this.accounts)
                {
                    if(a.OemfactoryId == (this.comboBox1.SelectedValue as string))
                    {
                        AppConfig.Account = a;
                        this.Close();
                        break;
                    }
                }
            }
        }
    }
}
