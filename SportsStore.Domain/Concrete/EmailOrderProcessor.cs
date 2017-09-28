using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailtoAddress = "836376242@qq.com";
        public string MailFromAddress = "lingjiamian@outlook.com";
        public bool UseSsl = true;
        public string Username = "lingjiamian@outlook.com";
        public string Password = "cxz.05570783";
        public string ServerName = "smtp-mail.outlook.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }

    public class EmailOrderProcessor:IOrderProcessor
    {
        private EmailSettings emaillSettings;

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emaillSettings.UseSsl;
                smtpClient.Host = emaillSettings.ServerName;
                smtpClient.Port = emaillSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emaillSettings.Username,
                    emaillSettings.Password);
                if (emaillSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emaillSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("A new order has been submitted!")
                    .AppendLine("---")
                    .AppendLine("Items:");
                foreach(var line in cart.lines)
                {
                    var subtotal = line.Product.Price * line.Quatity;
                    sb.AppendFormat("{0} × {1}  subtotal:{2:c}", line.Product.Price
                        , line.Product.Name, subtotal);
                }

                sb.AppendFormat("Total order value :{0:c}", cart.ComputeTotal());
                sb.AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.line1)
                    .AppendLine(shippingDetails.line2 ?? "")
                    .AppendLine(shippingDetails.line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---");
                MailMessage mailMessage = new MailMessage(
                    emaillSettings.MailFromAddress,
                    emaillSettings.MailtoAddress,
                    "New order submitted!",
                    sb.ToString()
                    );
                if(emaillSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                //smtpClient.Send(mailMessage);
            }
        }

        public EmailOrderProcessor(EmailSettings settings)
        {
            emaillSettings = settings;
        }
    }
}
