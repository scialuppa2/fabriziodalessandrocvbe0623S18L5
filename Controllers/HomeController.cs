using progetto_settimanaleS18L5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace progetto_settimanaleS18L5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Utente model)
        {

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
                    try
                    {
                        sqlConnection.Open();

                        string query = "SELECT * FROM Utenti WHERE Username = @Username AND Password = @Password";

                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Username", model.Username);
                        sqlCommand.Parameters.AddWithValue("Password", model.Password);

                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            FormsAuthentication.SetAuthCookie(model.Username, false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.AuthError = "Autenticazione non riuscita, credenziali non corrette";
                            return View();
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Checkout(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);

            if (prenotazione == null)
            {
                return HttpNotFound();
            }

            TimeSpan durataSoggiorno = prenotazione.DataFineSoggiorno - prenotazione.DataInizioSoggiorno;
            int giorniSoggiorno = durataSoggiorno.Days;
            decimal importoSoggiorno = giorniSoggiorno * prenotazione.TariffaApplicata;

            decimal importoServiziAggiuntivi = ServizioAggiuntivo.GetTotaleServiziAggiuntivi(IdPrenotazione);

            decimal importoDaSaldare = importoSoggiorno + importoServiziAggiuntivi - prenotazione.CaparraConfirmatoria;

            List<ServizioAggiuntivo> serviziAggiuntivi = ServizioAggiuntivo.GetServiziAggiuntiviByIdPrenotazione(IdPrenotazione);

            Checkout checkoutModel = new Checkout
            {
                NomeTitolare = prenotazione.CodiceFiscale,
                NumeroStanza = prenotazione.NumeroCamera,
                DataCheckIn = prenotazione.DataInizioSoggiorno,
                DataCheckOut = prenotazione.DataFineSoggiorno,
                CaparraConfirmatoria = prenotazione.CaparraConfirmatoria,
                TariffaApplicata = prenotazione.TariffaApplicata,
                ServiziAggiuntivi = serviziAggiuntivi,
                ImportoDaSaldare = importoDaSaldare
            };

            return View(checkoutModel);
        }
    }
}