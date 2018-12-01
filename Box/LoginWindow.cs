using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

using RestSharp;

namespace Box
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private RestClient client = new RestClient("https://lifecycletest.ibroadlink.com");

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

                var request = new RestRequest("dlicense/v2/account/handshake", Method.GET);

                var asyncHandle = client.ExecuteAsync<Handshake>(request, response => {
                    Handshake res = response.Data;
                    Console.WriteLine(response.Data);
                    if(res.Status == 0)
                    {
                        Login(IdTextBox.Text, PwdTextBox.Text, res);
                    }
                });
            }

            

        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            progressBar1.Hide();

            IdTextBox.Text = "15372098619";
            PwdTextBox.Text = "yunduan412";
        }

        private void Login(string Id, string pwd, Handshake res)
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
            var response = client.Execute(request);
            var content = response.Content; // raw content as string  
        }
    }
}
