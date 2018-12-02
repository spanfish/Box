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
        private RestClient client;

        private readonly SynchronizationContext synchronizationContext;

        public LoginWindow()
        {
            InitializeComponent();

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
            } else
            {
                progressBar1.Show();
                button1.Enabled = false;

                try
                {
                    var request = new RestRequest("dlicense/v2/account/handshake", Method.GET);

                    var asyncHandle = client.ExecuteAsync<HandshakeResponse>(request, response =>
                    {
                        HandshakeResponse handshake = response.Data;
                        Console.WriteLine(response.Data);
                        if (handshake.Status == 0)
                        {
                            Login(IdTextBox.Text, PwdTextBox.Text, handshake);
                        }
                        else
                        {
                            //
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

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            progressBar1.Hide();

            IdTextBox.Text = "15372098619";
            PwdTextBox.Text = "yunduan412";
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

            var asyncHandle = client.ExecuteAsync<LoginResponse>(request, response => {
                LoginResponse loginRes = response.Data;
                App.LoginResponse = loginRes;

                if(loginRes.Status == 0)
                {
                    QueryAccount();
                }
                //synchronizationContext.Post(new SendOrPostCallback(o =>
                //{
                //    this.Close();
                //}), null);
            });
        }


        private void QueryAccount()
        {
            var request = new RestRequest("dlicense/v2/account/role/query", Method.GET);

            request.AddHeader("reqUserId", App.LoginResponse.Userid);
            request.AddHeader("reqUserSession", App.LoginResponse.Loginsession);
            
            var asyncHandle = client.ExecuteAsync<AccountResponse>(request, response => {
                AccountResponse accountRes = response.Data;
                App.Account = accountRes.Data[0];

                synchronizationContext.Post(new SendOrPostCallback(o =>
                {
                    this.Close();
                }), null);
            });
        }
    }
}
