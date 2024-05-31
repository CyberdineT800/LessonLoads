using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Services
{
    public class Populace : ConnectDB
    {
        public int ID { get; set; }
        public string Ism { get; set; }
        public string Familiya { get; set; }
        public string Telefon { get; set; }
        public int ViloyatID { get; set; }
        public int TumanID { get; set; }
        public string P_seria { get; set; }
        public string P_raqam { get; set; }

        public static int AddNewPopulace(string ism, string familiya, string telefon, int viloyatID, int tumanID, string p_seria, string p_raqam)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                string insertQuery = @"
                        INSERT INTO Aholi (Ism, Familiya, Telefon, ViloyatID, TumanID, P_seria, P_raqam)
                        VALUES (@Ism, @Familiya, @Telefon, @ViloyatID, @TumanID, @P_seria, @P_raqam);
                        SELECT SCOPE_IDENTITY();";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Ism", ism);
                    insertCmd.Parameters.AddWithValue("@Familiya", familiya);
                    insertCmd.Parameters.AddWithValue("@Telefon", telefon);
                    insertCmd.Parameters.AddWithValue("@ViloyatID", viloyatID);
                    insertCmd.Parameters.AddWithValue("@TumanID", tumanID);
                    insertCmd.Parameters.AddWithValue("@P_seria", p_seria);
                    insertCmd.Parameters.AddWithValue("@P_raqam", p_raqam);

                    int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    return insertedId;
                }
            }
        }


        public static DataTable PopulaceSearch(string name, string surname, string phone, string pseria, string pnum, string region, string district)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    string query = @"
                            SELECT 
                                a.ID, 
                                a.Ism, 
                                a.Familiya, 
                                a.Telefon,
                                a.P_seria, 
                                a.P_raqam, 
                                v.Nomi AS Viloyat, 
                                t.Nomi AS Tuman
                            FROM Aholi a
                            LEFT JOIN Tumanlar t ON a.TumanID = t.ID
                            INNER JOIN Viloyatlar v ON a.ViloyatID = v.ID
                            WHERE 1=1";
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        query += " AND a.Ism LIKE @Name";
                        parameters.Add(new SqlParameter("@Name", "%" + name + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(surname))
                    {
                        query += " AND a.Familiya LIKE @Surname";
                        parameters.Add(new SqlParameter("@Surname", "%" + surname + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        query += " AND a.Telefon LIKE @Phone";
                        parameters.Add(new SqlParameter("@Phone", "%" + phone + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(pseria))
                    {
                        query += " AND a.P_seria LIKE @Seria";
                        parameters.Add(new SqlParameter("@Seria", "%" + pseria + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(pnum))
                    {
                        query += " AND a.P_raqam LIKE @Num";
                        parameters.Add(new SqlParameter("@Num", "%" + pnum + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(region))
                    {
                        query += " AND v.Nomi LIKE @Region";
                        parameters.Add(new SqlParameter("@Region", "%" + region + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(district))
                    {
                        query += " AND t.Nomi LIKE @District";
                        parameters.Add(new SqlParameter("@District", "%" + district + "%"));
                    }

                    using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());

                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching Populace : " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static DataTable GetAllPopulaceWithDetails()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = @"SELECT a.ID, a.Ism, a.Familiya, a.Telefon, 
                                          v.Nomi AS ViloyatNomi, t.Nomi AS TumanNomi, 
                                          a.P_seria, a.P_raqam
                                   FROM Aholi a
                                   JOIN Viloyatlar v ON a.ViloyatID = v.ID
                                   JOIN Tumanlar t ON a.TumanID = t.ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectCmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Populace data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static bool DeletePopulaceById(int aholiId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Aholi WHERE ID = @AholiId";

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@AholiId", aholiId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Populace: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdatePopulace(int id, string ism, string familiya, string telefon, int viloyatID, int tumanID, string p_seria, string p_raqam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string updateQuery = @"UPDATE Aholi 
                                           SET Ism = @Ism, Familiya = @Familiya, Telefon = @Telefon, 
                                               ViloyatID = @ViloyatID, TumanID = @TumanID,
                                               P_seria = @P_seria, P_raqam = @P_raqam
                                           WHERE ID = @ID";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Ism", ism);
                        updateCmd.Parameters.AddWithValue("@Familiya", familiya);
                        updateCmd.Parameters.AddWithValue("@Telefon", telefon);
                        updateCmd.Parameters.AddWithValue("@ViloyatID", viloyatID);
                        updateCmd.Parameters.AddWithValue("@TumanID", tumanID);
                        updateCmd.Parameters.AddWithValue("@P_seria", p_seria);
                        updateCmd.Parameters.AddWithValue("@P_raqam", p_raqam);
                        updateCmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Populace: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable GetAllPopulace()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT ID, Ism, Familiya, Telefon, ViloyatID, TumanID, P_seria, P_raqam FROM Aholi";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectCmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Populace data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static Populace GetPopulaceById(int id)
        {
            Populace aholi = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM Aholi WHERE ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                aholi = new Populace
                                {
                                    ID = id,
                                    Ism = reader.GetString(reader.GetOrdinal("Ism")),
                                    Familiya = reader.GetString(reader.GetOrdinal("Familiya")),
                                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                    ViloyatID = reader.GetInt32(reader.GetOrdinal("ViloyatID")),
                                    TumanID = reader.GetInt32(reader.GetOrdinal("TumanID")),
                                    P_seria = reader.GetString(reader.GetOrdinal("P_seria")),
                                    P_raqam = reader.GetString(reader.GetOrdinal("P_raqam")),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Populace by ID: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return aholi;
        }
    }
}
