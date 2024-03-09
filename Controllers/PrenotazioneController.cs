using Newtonsoft.Json;
using progetto_settimanaleS18L5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace progetto_settimanaleS18L5.Controllers
{
    public class PrenotazioneController : Controller
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public ActionResult CreaPrenotazione(string CodiceFiscale)
        {
            List<Cliente> listaClienti = new Cliente().ListaClienti();
            List<SelectListItem> listaCamere = Camera.GetCamereDisponibili();

            Prenotazione model = new Prenotazione();
            model.CodiceFiscale = CodiceFiscale;
            model.DataInizioSoggiorno = DateTime.Today;
            model.DataFineSoggiorno = DateTime.Today;
            model.DataPrenotazione = DateTime.Today;


            List<SelectListItem> listaClientiSelectList = listaClienti
                .Select(c => new SelectListItem { Text = $"{c.Nome} {c.Cognome}", Value = c.CodiceFiscale })
                .ToList();

            ViewBag.ListaClienti = listaClientiSelectList;
            ViewBag.ListaCamere = listaCamere;

            return View(model);
        }


        [HttpPost]
        public ActionResult CreaPrenotazione(Prenotazione model)
        {
            if (ModelState.IsValid)
            {
                model.InserisciPrenotazione();
                return RedirectToAction("ListaPrenotazione");
            }
            return View(model);
        }

        public ActionResult DettagliPrenotazione(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);
            List<ServizioAggiuntivo> serviziAggiuntivi = ServizioAggiuntivo.GetServiziAggiuntiviByIdPrenotazione(IdPrenotazione);


            if (prenotazione != null)
            {
                return View(prenotazione);
            }

            return HttpNotFound();
        }

        [HttpGet]
        public async Task<ActionResult> ListaPrenotazione()
        {
            var prenotazioni = new Prenotazione().ListaPrenotazioni();

            ViewBag.TotalePensioneCompleta = await ConteggioPensioneCompleta();

            return View(prenotazioni);
        }


        public ActionResult EliminaPrenotazione(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);

            if (prenotazione != null)
            {
                prenotazione.EliminaPrenotazione();
            }
            return RedirectToAction("ListaPrenotazione");
        }

        public ActionResult RicercaPrenotazione()
        {
            return View();
        }

        public async Task<ActionResult> RicercaPrenotazioneCliente(string CodiceFiscale)
        {
            List<dynamic> prenotazioni = new List<dynamic>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdPrenotazione, NumeroCamera, DataInizioSoggiorno, DataFineSoggiorno, TariffaApplicata FROM Prenotazioni WHERE CodiceFiscale = @CodiceFiscale";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            prenotazioni.Add(new
                            {
                                IdPrenotazione = reader.GetInt32(reader.GetOrdinal("IdPrenotazione")),
                                NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                                DataInizioSoggiorno = reader.GetDateTime(reader.GetOrdinal("DataInizioSoggiorno")).ToString("yyyy-MM-dd"),
                                DataFineSoggiorno = reader.GetDateTime(reader.GetOrdinal("DataFineSoggiorno")).ToString("yyyy-MM-dd"),
                                TariffaApplicata = reader.GetDecimal(reader.GetOrdinal("TariffaApplicata"))
                            });
                        }
                    }
                }
            }

            return Json(prenotazioni, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> ConteggioPensioneCompleta()
        {
            int conteggio = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM Prenotazioni WHERE PensioneCompleta = 1";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    conteggio = (int)await command.ExecuteScalarAsync();
                }
            }

            return conteggio.ToString();
        }





    }
}