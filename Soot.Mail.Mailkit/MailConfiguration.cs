﻿namespace Soot.Mail.Mailkit
{
    public class MailConfiguration
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
