using System;
using System.Linq;
using Xunit;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using postmark_inbound_demo.Resources;

namespace postmark_inbound_demo.tests
{
    public class InboundTest
    {
        HttpWebResponse httpWebResponse;
        JavaScriptSerializer jsSerializer;
        string jsonString;

        public InboundTest()
        {
            Email addRequest = new Email
            {
                Cc = "",
                From = "jp@wildbit.com",
                HtmlBody = "\nEmail Body Test\n",
                MailBoxHash = "",
                MessageID = "402b9ab1-6ce4-4a72-aa00-177cd079ed10",
                ReplyTo = "",
                Subject = "Send Test",
                Tag = "",
                TextBody = "Email Body Test",
                To = "no_a_real_hash@inbound.postmarkapp.com",
                Attachments = new _attachments 
                    { 
                        new Attachment 
                        { 
                            Name = "smallfile.txt", 
                            Content="anA=", 
                            ContentType = "test/plain"
                        },  
                    },
                Headers = new _headers 
                {
                    new Header { Name = "X-Spam-Checker-Version", Value = "SpamAssassin 3.3.1 (2010-03-16) onrs-iad-pm-inbound1.wildbit.com"},
                    new Header { Name = "X-Spam-Status", Value = "No" },
                    new Header { Name = "X-Spam-Score", Value = "-0.1"},
                    new Header { Name = "X-Spam-Tests", Value = "DKIM_SIGNED,DKIM_VALID,DKIM_VALID_AU,HTML_MESSAGE,SPF_PASS"},
                    new Header { Name = "Received-SPF", Value = "Pass (sender SPF authorized) identity=mailfrom; client-ip=209.85.214.52; helo=mail-bk0-f52.google.com; envelope-from=jp@wildbit.com; receiver=not_a_real_hash@inbound.postmarkapp.com"},
                    new Header { Name = "MIME-Version", Value = "1.0"},
                    new Header { Name = "Message-ID", Value = "<CACUE7-fsiap3ZRj6uLWuL6GxRmxcxsuRL2juTMbKjtT4Jq1Gzw@mail.gmail.com>"}   
                }
            };
            
            var url = "http://localhost:8080/api/emails/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            jsSerializer = new JavaScriptSerializer();
            var jsonAddRequest = jsSerializer.Serialize(addRequest);

            var writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonAddRequest);
            writer.Close();

            httpWebResponse = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                jsonString = sr.ReadToEnd();
            }            
        }

        [Fact]
        public void ReceivedCorrectResponseCode()
        {
            Assert.Equal(httpWebResponse.StatusDescription, "OK");
        }

        [Fact]
        public void RequestCorrectlySerialized()
        {
            var jsonAddResponse = jsSerializer.Deserialize<dynamic>(jsonString);
            Assert.Equal(jsonAddResponse["Subject"], "Send Test");
        }
    }
}
