
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using MimeKit.Text;

namespace baskidabaski.EmailServices
{
    public class SmtpEmailSender:IEmailSender
    {
        private string _email;
        private string _password;
        private string _host;
        private int _port;
    
        private bool _enableSSL;

        public SmtpEmailSender(string email, string password, string host, int port, bool enableSSL)
        {
            _email = email;
            _password = password;

            _host = host;
            _port = port;
         
            _enableSSL = enableSSL;
        }
		public bool SendEmailAsync(string email, string subject, string htmlMessage)
		{
            MimeMessage mimeMessagefrom = new MimeMessage();

            MailboxAddress mailboxAddressfrom = new MailboxAddress("Admin",this._email);
            mimeMessagefrom.From.Add(mailboxAddressfrom);

            MailboxAddress mailboxAddressto = new MailboxAddress("User", email);
 mimeMessagefrom.To.Add(mailboxAddressto);
            mimeMessagefrom.Subject = subject;
           
			mimeMessagefrom.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

			
            var client2 = new SmtpClient();
            
            client2.Connect(_host,_port,_enableSSL);
            client2.Authenticate(_email,_password);
            client2.Send(mimeMessagefrom);
            return true;
		}
	}
}
