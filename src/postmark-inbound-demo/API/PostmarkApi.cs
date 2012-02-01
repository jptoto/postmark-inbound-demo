using System;
using System.Linq;
using postmark_inbound_demo.Resources;
using postmark_inbound_demo.Repositories;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.ApplicationServer.Http.Dispatcher;
using System.Net.Http;
using System.Net;
using System.IO;

namespace postmark_inbound_demo.API
{
    [ServiceContract]
    public class PostmarkApi
    {
        static readonly IEmailRepository repository = new EmailRepository();
        string attachmentSaveFolder = @"c:\";

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Email Post(Email email)
        {
            try
            {
                repository.Add(email);

                foreach (var file in email.Attachments)
                {
                    if (!string.IsNullOrEmpty(file.Content))
                    {
                        if (File.Exists(attachmentSaveFolder + file.Name))
                            File.Delete(attachmentSaveFolder + file.Name);
                        
                        byte[] filebytes = Convert.FromBase64String(file.Content);
                        FileStream fs = new FileStream(attachmentSaveFolder + file.Name,
                                                       FileMode.CreateNew,
                                                       FileAccess.Write,
                                                       FileShare.None);
                        fs.Write(filebytes, 0, filebytes.Length);
                        fs.Close();
                    }
                }
                return email;
            }
            catch (HttpRequestException e)
            {
                // !!!
                // HttpResponseExceptions are not properly returned yet. It's a known bug in WCF WebAPI preview 6. I'm hoping it's fixed in a subsequent release.
                // !!!
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(e.InnerException.ToString()) });
            }
        }
    }
}