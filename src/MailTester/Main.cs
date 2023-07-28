using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace MailTester
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txbStatus.Text = string.Empty;

            try
            {
                string smtpHost = txbHost.Text;
                string smtpPort = txbPort.Text;
                string smtpUser = txbUser.Text;
                string smtPwd = txbPass.Text;

                string strFrom = txbFrom.Text;
                string strTo = txbTo.Text;

                string strSubject = txbSubject.Text;
                string strMensagem = txbMessage.Text;

                txbStatus.Text += "\r\nSMTP Host: " + smtpHost;
                txbStatus.Text += "\r\nSMTP Port: " + int.Parse(smtpPort);
                txbStatus.Text += "\r\nSubject: " + strSubject;
                txbStatus.Text += "\r\nMessage: " + strMensagem;
                txbStatus.Text += "\r\nFrom: " + strFrom;
                txbStatus.Text += "\r\nTo: " + strTo;

                MailMessage mail = new MailMessage();
                mail.Priority = 0;
                mail.Subject = strSubject;
                mail.Body = strMensagem;
                mail.IsBodyHtml = true;

                mail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mail.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                mail.From = new MailAddress(strFrom);

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpHost;
                    smtp.Port = int.Parse(smtpPort);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    if (chkAutentication.Checked)
                    {                    
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(smtpUser, smtPwd);
                        
                        // Descomentar as linha abaixo para uso com Office 365
                        // smtp.EnableSsl = true;
                        // smtp.TargetName = "STARTTLS/smtp.office365.com";
                    }

                    mail.To.Add(strTo);

                    smtp.Send(mail);

                    txbStatus.Text += "\r\nSuccess!";
                }
            }
            catch (Exception ex)
            {
                txbStatus.Text += "\r\nFail, details:  " + ex.Message;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbStatus.Text = string.Empty;
        }
    }
}