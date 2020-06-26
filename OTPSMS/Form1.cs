using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Mail;

namespace OTPSMS
{
    public partial class Form1 : Form
    {
		string randomNumber;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Please Wait a Moment. \n We are processing your request!!");
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String to = txtPhone.Text;
                String name = txtName.Text;
                String token = "938cef6c1d8cc5c4c8c552d624ca88de";
                Random rnd = new Random();
                randomNumber = (rnd.Next(100000, 999999)).ToString();
                String message = "Hello " + name + ", Your OTP is " + randomNumber;
                String url = "http://api.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + message;
                request = WebRequest.Create(url);

                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new
                System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("otpclientservice@gmail.com", "Otpclient!");
                MailMessage msg = new MailMessage();
                msg.To.Add(txtMail.Text);
                msg.From = new MailAddress("otpclientservice@gmail.com");
                msg.Subject = "OTP";
                msg.Body = "Hello " + name + ", Your OTP is " + randomNumber;
                client.Send(msg);
				MessageBox.Show("Please provide your OTP to Verify.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }            
        }

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if(txtVerOTP.Text == randomNumber)
			{
				MessageBox.Show("Login Successfully");
                txtVerOTP.Text = null;
            }
			else
			{
				MessageBox.Show("You have entered wrong OTP.\nPlease Try Again !");
			}
		}

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    
}
