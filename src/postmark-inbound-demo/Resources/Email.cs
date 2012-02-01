using System.Collections.Generic;

namespace postmark_inbound_demo.Resources
{
    public class Email
    {
        public string Cc { get; set; }
        public string From { get; set; }
        public string HtmlBody { get; set; }
        public string MailBoxHash { get; set; }
        public string MessageID { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Tag { get; set; }
        public string TextBody { get; set; }
        public string To { get; set; }
        public _attachments Attachments { get; set; }
        public _headers Headers { get; set; }
    }

    public class _attachments : List<Attachment> { }
    public class _headers : List<Header> { }

    public class Attachment
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
    }

    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}