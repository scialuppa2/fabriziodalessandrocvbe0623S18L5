using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace progetto_settimanaleS18L5.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public String CodiceFiscale { get; set; }
        public String Cognome { get; set; }
        public String Nome { get; set; }
        public String Citta { get; set; }
        public String Provincia { get; set; }
        public String Email { get; set; }
        public String Telefono { get; set; }
        public String Cellulare { get; set; }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void InserisciCliente()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) " +
                               "VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
                    cmd.Parameters.AddWithValue("@Cognome", Cognome);
                    cmd.Parameters.AddWithValue("@Nome", Nome);
                    cmd.Parameters.AddWithValue("@Citta", Citta);
                    cmd.Parameters.AddWithValue("@Provincia", Provincia);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Cellulare", Cellulare);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificaCliente()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "UPDATE Clienti SET Cognome = @Cognome, Nome = @Nome, Citta = @Citta, " +
                               "Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare " +
                               "WHERE CodiceFiscale = @CodiceFiscale";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
                    cmd.Parameters.AddWithValue("@Cognome", Cognome);
                    cmd.Parameters.AddWithValue("@Nome", Nome);
                    cmd.Parameters.AddWithValue("@Citta", Citta);
                    cmd.Parameters.AddWithValue("@Provincia", Provincia);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Cellulare", Cellulare);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListaClienti()
        {
            List<Cliente> clienti = new List<Cliente>();

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Clienti";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Citta = reader["Citta"].ToString(),
                                Provincia = reader["Provincia"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Cellulare = reader["Cellulare"].ToString()
                            };

                            clienti.Add(cliente);
                        }
                    }
                }
            }
            return clienti;
        }

        public void EliminaCliente()
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                string query = "DELETE FROM Clienti WHERE CodiceFiscale = @CodiceFiscale";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cliente GetClienteByIdCliente(int IdCliente)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();

                    string query = "SELECT * FROM Clienti WHERE IdCliente = @IdCliente";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", IdCliente);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Cliente
                                {
                                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                    CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                    Cognome = reader["Cognome"].ToString(),
                                    Nome = reader["Nome"].ToString(),
                                    Citta = reader["Citta"].ToString(),
                                    Provincia = reader["Provincia"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Cellulare = reader["Cellulare"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Errore durante l'accesso al database: " + ex.Message, ex);
                }
            }
        }

        public Cliente GetClienteByCodiceFiscale(string CodiceFiscale)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();

                    string query = "SELECT * FROM Clienti WHERE CodiceFiscale = @CodiceFiscale";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Cliente
                                {
                                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                    CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                    Cognome = reader["Cognome"].ToString(),
                                    Nome = reader["Nome"].ToString(),
                                    Citta = reader["Citta"].ToString(),
                                    Provincia = reader["Provincia"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Cellulare = reader["Cellulare"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Errore durante l'accesso al database: " + ex.Message, ex);
                }
            }
        }
    }
}