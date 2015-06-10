using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using SendGrid;




namespace OmgevingsboekMVC.Controllers
{
      [Authorize(Roles="Administrator")]
    public class GebruikerController : Controller
    {
        // GET: Gebruiker
        public ActionResult Index()
        { 
            List<AspNetUsers> lijst = new List<AspNetUsers>();
            using(OmgevingsboekContext context =new OmgevingsboekContext())
            {
               
                GebruikerRepository gebruikerrepo=new GebruikerRepository(context);
                lijst = gebruikerrepo.All().ToList();
            }
            ViewBag.Gebruikers = lijst;
            return View("GebruikersOverzicht");
        }

        public ActionResult Toestaan(AspNetUsers aspuser)
        {
            using(OmgevingsboekContext context=new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsPending = false;
                repo.Update(aspuser);
                repo.SaveChanges();

                var Sendgebruikersnaam = "NielsDeryckere";
                var SendPass = "Rome2-Totalwar";
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(User.Identity.Name);
                mail.AddTo(aspuser.Email);
                mail.Subject = "Account geaccepteerd";
                mail.Html = "<p>Beste " + aspuser.Firstname + ",</p><br /><p>Uw aanvraag voor een Surroundings-account werd geaccepteerd.</ br>Welkom bij surrroundings, voor meer informatie kan u terecht op</p>";


                var credentials = new NetworkCredential(Sendgebruikersnaam, SendPass);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(mail);

            }



            return RedirectToAction("Index");
        }
        public ActionResult Weigeren(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);

                repo.Delete(aspuser);
                repo.SaveChanges();

                var Sendgebruikersnaam = "NielsDeryckere";
                var SendPass = "Rome2-Totalwar";
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(User.Identity.Name);
                mail.AddTo(aspuser.Email);
                mail.Subject = "Account geweigerd";
                mail.Html = "<p>Beste " + aspuser.Firstname + ",</p><br /><p>Uw aanvraag voor een Surroundings-account werd geweigerd.</ br>Indien dit een vergissing is, kan u een mail sturen naar "+ User.Identity.Name+"</p>";


                var credentials = new NetworkCredential(Sendgebruikersnaam, SendPass);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(mail);

            }

            return RedirectToAction("Index");
        }

        public ActionResult Verwijderen(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsDeleted = true;

                repo.Update(aspuser);
                repo.SaveChanges();
                var Sendgebruikersnaam = "NielsDeryckere";
                var SendPass = "Rome2-Totalwar";
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(User.Identity.Name);
                mail.AddTo(aspuser.Email);
                mail.Subject = "Account verwijderd";
                mail.Html = "<p>Beste " + aspuser.Firstname + ",</p><br /><p>Uw Surroundings-account werd verwijderd.</ br>Indien dit een vergissing is, kan u een mail sturen naar niels.deryckere@student.howest.be</p>";


                var credentials = new NetworkCredential(Sendgebruikersnaam, SendPass);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(mail);


            }

            return RedirectToAction("Index");
        }

        public ActionResult Activeren(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsDeleted = false;

                repo.Update(aspuser);
                repo.SaveChanges();
                
                var Sendgebruikersnaam = "NielsDeryckere";
                var SendPass = "Rome2-Totalwar";
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(User.Identity.Name);
                mail.AddTo(aspuser.Email);
                mail.Subject = "Account terug geactiveerd";
                mail.Html = "<p>Beste "+aspuser.Firstname+",</p><br /><p>Uw Surroundings-account werd terug geactiveerd</p>";
               

                var credentials = new NetworkCredential(Sendgebruikersnaam,SendPass);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(mail);
               


            }

            return RedirectToAction("Index");
        }
        
    }
}