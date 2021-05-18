using System;
using System.Net;
using System.Net.Mail;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects;

namespace Project.Business.Concrete
{
    public class EmailManager : IEmailService
    {

        private SmtpClient _client;

        public EmailManager()
        {
            _client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("erenyilmazgazi@hotmail.com", "03123897461eren"),
                Port = 587,
                Host = "smtp.live.com",
                EnableSsl = true
            };
        }



        public IResult SendConfirmAccountEmail(string email, string link)
        {
            
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("erenyilmazgazi@hotmail.com"),
                    Subject = "Hesap Onayı",
                    Body =
                        $"<h4>Hesabınız Başarıyla Oluşturuldu. Giriş Yapabilmek İçin Mail Adresinizi Onaylamanız Gerekmektedir.</h4>" +
                        $"<h4>Mail Adresinizi Onaylamak İçin Aşağıdaki Linke Tıklayınız.</h4>" +
                        $"{link}",

                    IsBodyHtml = true

                };

                mail.To.Add(email);


                try
                {
                    _client.Send(mail);
                    return new SuccessResult();
                }
                catch
                {
                    return new ErrorResult();
                }
                

           
        }



        public IResult SendPasswordResetEmail(string email, string link)
        {
            
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("erenyilmazgazi@hotmail.com"),
                Subject = "Hesap Onayı",
                Body = $"<h3>Şifrenizi Aşağıdaki Linkten Sıfırlayabilirsiniz.</h3> {link}",
                IsBodyHtml = true
            };

            mail.To.Add(email);


            try
            {
                _client.Send(mail);
                return new SuccessResult();
            }
            catch
            {
                return new ErrorResult();
            }


        }


    }
}