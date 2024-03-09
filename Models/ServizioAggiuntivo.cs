using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace progetto_settimanaleS18L5.Models
{
    public class ServizioAggiuntivo
    {
        public int IdServizio { get; set; }
        public String Servizio { get; set; }

        [Display(Name = "Data Servizio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataServizio { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public int IdPrenotazione { get; set; }


        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void InserisciServizioAggiuntivo()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "INSERT INTO ServiziAggiuntivi (Servizio, DataServizio, Quantita, Prezzo, IdPrenotazione) " +
                               "VALUES (@Servizio, @DataServizio, @Quantita, @Prezzo, @IdPrenotazione)";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Servizio", Servizio);
                    cmd.Parameters.AddWithValue("@DataServizio", DataServizio);
                    cmd.Parameters.AddWithValue("@Quantita", Quantita);
                    cmd.Parameters.AddWithValue("@Prezzo", Prezzo);
                    cmd.Parameters.AddWithValue("@IdPrenotazione", IdPrenotazione);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AggiornaServizioAggiuntivo()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "UPDATE ServiziAggiuntivi SET Servizio = @Servizio, DataServizio = @DataServizio, " +
                               "Quantita = @Quantita, Prezzo = @Prezzo WHERE IdServizio = @IdServizio";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdServizio", IdServizio);
                    cmd.Parameters.AddWithValue("@Servizio", Servizio);
                    cmd.Parameters.AddWithValue("@DataServizio", DataServizio);
                    cmd.Parameters.AddWithValue("@Quantita", Quantita);
                    cmd.Parameters.AddWithValue("@Prezzo", Prezzo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminaServizioAggiuntivo()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "DELETE FROM ServiziAggiuntivi WHERE IdServizio = @IdServizio";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdServizio", IdServizio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ServizioAggiuntivo> GetServiziAggiuntiviByIdPrenotazione(int idPrenotazione)
        {
            List<ServizioAggiuntivo> serviziAggiuntivi = new List<ServizioAggiuntivo>();

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "SELECT * FROM ServiziAggiuntivi WHERE IdPrenotazione = @IdPrenotazione";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServizioAggiuntivo servizio = new ServizioAggiuntivo
                            {
                                IdServizio = Convert.ToInt32(reader["IdServizio"]),
                                Servizio = reader["Servizio"].ToString(),
                                DataServizio = Convert.ToDateTime(reader["DataServizio"]),
                                Quantita = Convert.ToInt32(reader["Quantita"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"]),
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"])
                            };

                            serviziAggiuntivi.Add(servizio);
                        }
                    }
                }
            }

            return serviziAggiuntivi;
        }

        public ServizioAggiuntivo GetServizioAggiuntivoById(int IdServizio)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "SELECT * FROM ServiziAggiuntivi WHERE IdServizio = @IdServizio";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdServizio", IdServizio);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ServizioAggiuntivo
                            {
                                IdServizio = Convert.ToInt32(reader["IdServizio"]),
                                Servizio = reader["Servizio"].ToString(),
                                DataServizio = Convert.ToDateTime(reader["DataServizio"]),
                                Quantita = Convert.ToInt32(reader["Quantita"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"]),
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static decimal GetTotaleServiziAggiuntivi(int IdPrenotazione)
        {
            decimal totale = 0;

            var serviziPrenotazione = GetServiziAggiuntiviByIdPrenotazione(IdPrenotazione);
            if (serviziPrenotazione != null)
            {
                totale = serviziPrenotazione.Sum(servizio => servizio.Prezzo);
            }

            return totale;
        }

        public List<SelectListItem> ServiziAggiuntivi { get; set; }

        public static List<SelectListItem> GetListaServiziAggiuntivi()
        {
            List<SelectListItem> serviziAggiuntivi = new List<SelectListItem>
            {
                new SelectListItem { Value = "Colazione in camera", Text = "Colazione in camera" },
                new SelectListItem { Value = "Bevande nel minibar", Text = "Bevande nel minibar" },
                new SelectListItem { Value = "Internet", Text = "Internet" },
                new SelectListItem { Value = "Letto aggiuntivo", Text = "Letto aggiuntivo" },
                new SelectListItem { Value = "Culla", Text = "Culla" }
            };

            return serviziAggiuntivi;
        }
    }
}