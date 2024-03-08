using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace progetto_settimanaleS18L5.Models
{
    public class Prenotazione
    {
        public int IdPrenotazione { get; set; }
        public String CodiceFiscale { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int Anno { get; set; }
        public DateTime DataInizioSoggiorno { get; set; }
        public DateTime DataFineSoggiorno { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public bool MezzaPensione { get; set; }
        public bool PensioneCompleta { get; set; }
        public bool PernottamentoPrimaColazione { get; set; }


        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void InserisciPrenotazione()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "INSERT INTO Prenotazioni (CodiceFiscale, NumeroCamera, DataPrenotazione, Anno, DataInizioSoggiorno, DataFineSoggiorno, CaparraConfirmatoria, TariffaApplicata, MezzaPensione, PensioneCompleta, PernottamentoPrimaColazione) " +
                               "VALUES (@CodiceFiscale, @NumeroCamera, @DataPrenotazione, @Anno, @DataInizioSoggiorno, @DataFineSoggiorno, @CaparraConfirmatoria, @TariffaApplicata, @MezzaPensione, @PensioneCompleta, @PernottamentoPrimaColazione)";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
                    cmd.Parameters.AddWithValue("@NumeroCamera", NumeroCamera);
                    cmd.Parameters.AddWithValue("@DataPrenotazione", DataPrenotazione);
                    cmd.Parameters.AddWithValue("@Anno", Anno);
                    cmd.Parameters.AddWithValue("@DataInizioSoggiorno", DataInizioSoggiorno);
                    cmd.Parameters.AddWithValue("@DataFineSoggiorno", DataFineSoggiorno);
                    cmd.Parameters.AddWithValue("@CaparraConfirmatoria", CaparraConfirmatoria);
                    cmd.Parameters.AddWithValue("@TariffaApplicata", TariffaApplicata);
                    cmd.Parameters.AddWithValue("@MezzaPensione", MezzaPensione);
                    cmd.Parameters.AddWithValue("@PensioneCompleta", PensioneCompleta);
                    cmd.Parameters.AddWithValue("@PernottamentoPrimaColazione", PernottamentoPrimaColazione);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AggiornaPrenotazione()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "UPDATE Prenotazioni SET CodiceFiscale = @CodiceFiscale, NumeroCamera = @NumeroCamera, " +
                               "DataPrenotazione = @DataPrenotazione, Anno = @Anno, " +
                               "DataInizioSoggiorno = @DataInizioSoggiorno, DataFineSoggiorno = @DataFineSoggiorno, " +
                               "CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata, " +
                               "MezzaPensione = @MezzaPensione, PensioneCompleta = @PensioneCompleta, " +
                               "PernottamentoPrimaColazione = @PernottamentoPrimaColazione " +
                               "WHERE IdPrenotazione = @IdPrenotazione";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdPrenotazione", IdPrenotazione);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
                    cmd.Parameters.AddWithValue("@NumeroCamera", NumeroCamera);
                    cmd.Parameters.AddWithValue("@DataPrenotazione", DataPrenotazione);
                    cmd.Parameters.AddWithValue("@Anno", Anno);
                    cmd.Parameters.AddWithValue("@DataInizioSoggiorno", DataInizioSoggiorno);
                    cmd.Parameters.AddWithValue("@DataFineSoggiorno", DataFineSoggiorno);
                    cmd.Parameters.AddWithValue("@CaparraConfirmatoria", CaparraConfirmatoria);
                    cmd.Parameters.AddWithValue("@TariffaApplicata", TariffaApplicata);
                    cmd.Parameters.AddWithValue("@MezzaPensione", MezzaPensione);
                    cmd.Parameters.AddWithValue("@PensioneCompleta", PensioneCompleta);
                    cmd.Parameters.AddWithValue("@PernottamentoPrimaColazione", PernottamentoPrimaColazione);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminaPrenotazione()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "DELETE FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdPrenotazione", IdPrenotazione);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Prenotazione> ListaPrenotazioni()
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Prenotazioni";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prenotazione prenotazione = new Prenotazione
                            {
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]),
                                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                CaparraConfirmatoria = Convert.ToDecimal(reader["CaparraConfirmatoria"]),
                                TariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]),
                                MezzaPensione = Convert.ToBoolean(reader["MezzaPensione"]),
                                PensioneCompleta = Convert.ToBoolean(reader["PensioneCompleta"]),
                                PernottamentoPrimaColazione = Convert.ToBoolean(reader["PernottamentoPrimaColazione"])
                            };

                            prenotazioni.Add(prenotazione);
                        }
                    }
                }
            }
            return prenotazioni;
        }

        public static Prenotazione GetPrenotazioneById(int idPrenotazione)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Prenotazione
                            {
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]),
                                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                CaparraConfirmatoria = Convert.ToDecimal(reader["CaparraConfirmatoria"]),
                                TariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]),
                                MezzaPensione = Convert.ToBoolean(reader["MezzaPensione"]),
                                PensioneCompleta = Convert.ToBoolean(reader["PensioneCompleta"]),
                                PernottamentoPrimaColazione = Convert.ToBoolean(reader["PernottamentoPrimaColazione"])
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
    }
}