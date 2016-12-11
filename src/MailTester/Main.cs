using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace MailTester
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region ::ACTIONS::

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
                txbStatus.Text += "\r\nFrom: " + strTo;
                txbStatus.Text += "\r\nTo: " + strFrom;

                MailMessage mail = new MailMessage();
                mail.Subject = strSubject;
                mail.Body = strMensagem;
                mail.IsBodyHtml = true;

                mail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mail.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                string from = strFrom;
                mail.From = new System.Net.Mail.MailAddress(from);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpHost;
                smtp.Port = int.Parse(smtpPort);

                if (chkAutentication.Checked)
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtPwd);
                }

                mail.To.Add(strTo);

                smtp.Send(mail);

                txbStatus.Text += "\r\nSuccess!";
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

        #endregion
    }
}
