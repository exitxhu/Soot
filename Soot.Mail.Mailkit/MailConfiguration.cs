using MailKit.Security;

namespace Soot.Mail.Mailkit
{
    public class MailConfiguration
    {
        public MailConfiguration(){}
        public MailConfiguration(int port, string fromName, string fromMail, string host, string username, string password, SecureSocketOptions secureSocket)
        {
            Port = port;
            FromName = fromName;
            FromMail = fromMail;
            Host = host;
            Username = username;
            Password = password;
            SecureSocket = secureSocket;
        }

        public int Port { get; set; }
        public string FromName { get; set; }
        public string FromMail { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SecureSocketOptions SecureSocket { get; set; }
    }
}
