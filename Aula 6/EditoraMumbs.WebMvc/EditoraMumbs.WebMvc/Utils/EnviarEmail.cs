using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EditoraMumbs.WebMvc.Utils
{
    public class EnviarEmail
    {
        SmtpClient smtpClient;
        MailAddress from;
        MailAddress to;
        MailMessage mailMessage;
       
        
        public EnviarEmail(string emailFrom, string nameFrom, string emailTo, string message, string subject)
        {
         
            //objeto responsável por enviar o e-mail
            smtpClient = new SmtpClient();
            //objeto que encaminha para a origem
            from = new MailAddress(emailFrom, nameFrom, System.Text.Encoding.UTF8);
            //objeto para conter o email destino
            to = new MailAddress(emailTo);
            // a mensagem completa com from, to e  message
            mailMessage = new MailMessage(from, to);
            //mensagem
            mailMessage.Body = message;
            //assunto
            mailMessage.Subject = subject;
             
            
        }

        public void Send()
        {
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            var Credenciais = new System.Net.NetworkCredential("antonioletic@gmail.com", "noturno157");
            smtpClient.Credentials = Credenciais;
            smtpClient.Send(mailMessage);

        }
    }
}