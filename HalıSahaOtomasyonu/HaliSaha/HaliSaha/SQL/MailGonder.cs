using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HalıSahaOtomasyonu.SQL;

namespace HalıSahaOtomasyonu.SQL
{
    public class MailGonder
    {
        WindowsFormEntitiesConnectionDB db = new WindowsFormEntitiesConnectionDB();

        public void Microsoft(string GondericiMail, string GondericiPass, string AliciMail)
        {
            GirisTablo Gt = db.GirisTablo.FirstOrDefault(x => x.MailAdress == GondericiMail); //Mail uyuşmazlığı olursa burdan kaynaklı olur.
            Random rnd = new Random();
            Gt.Password = rnd.Next(1000, 10000).ToString();
            db.SaveChanges();
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.outlook.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(GondericiMail, GondericiPass);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(GondericiMail, "Halısaha Otomasyonu");
            mail.To.Add(AliciMail);
            mail.Subject = "Şifre Sıfırlama Talebi";
            mail.IsBodyHtml = true;
            mail.Body = $@"{DateTime.Now.ToString()} Tarihinde şifre sıfırlama talebinde bulunuldu. Yeni şifreniz: {Gt.Password}";
            //sc.Timeout = 100;
            sc.Send(mail);
        }
    }
}
