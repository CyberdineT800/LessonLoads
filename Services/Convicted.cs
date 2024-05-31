using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.Services
{
    public class Convicted : ConnectDB
    {
        public int ID { get; set; }
        public string JinoyatTuri { get; set; }
        public int AholiID { get; set; }
        public string FI { get; set; }


        public Convicted()
        {
            this.ID = 0;
            this.JinoyatTuri = string.Empty;
            this.AholiID = 0;
            this.FI = string.Empty;
        }

        public static DataTable GetAllConvicted()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT s.ID, s.jinoyat_turi as Jinoyat_Turi, s.aholiID as Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Sudlanganlar s
                        INNER JOIN Aholi a ON s.aholiID = a.ID";

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
                MessageBox.Show("Error retrieving convicted data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static Convicted GetConvictedById(int id)
        {
            Convicted convicted = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT s.ID, s.jinoyat_turi as Jinoyat_Turi, s.aholiID as Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Sudlanganlar s
                        INNER JOIN Aholi a ON s.aholiID = a.ID
                        WHERE s.ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                convicted = new Convicted
                                {
                                    ID = id,
                                    JinoyatTuri = reader.GetString(reader.GetOrdinal("Jinoyat_Turi")),
                                    AholiID = reader.GetInt32(reader.GetOrdinal("Aholi_ID")),
                                    FI = reader.GetString(reader.GetOrdinal("FI"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving convicted by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return convicted;
        }


        public static Convicted GetConvictedByPopulaceId(int id)
        {
            Convicted convicted = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT s.ID, s.jinoyat_turi as Jinoyat_Turi, s.aholiID as Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Sudlanganlar s
                        INNER JOIN Aholi a ON s.aholiID = a.ID
                        WHERE s.aholiID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                convicted = new Convicted
                                {
                                    ID = id,
                                    JinoyatTuri = reader.GetString(reader.GetOrdinal("Jinoyat_Turi")),
                                    AholiID = reader.GetInt32(reader.GetOrdinal("Aholi_ID")),
                                    FI = reader.GetString(reader.GetOrdinal("FI"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving convicted by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return convicted;
        }


        public static int AddNewConvicted(string jinoyatTuri, int aholiID)
        {
            Convicted c = GetConvictedByPopulaceId(aholiID);

            if (c == null)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();

                        string insertQuery = @"
                            INSERT INTO Sudlanganlar (jinoyat_turi, aholiID)
                            VALUES (@JinoyatTuri, @AholiID);
                            SELECT SCOPE_IDENTITY();";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@JinoyatTuri", jinoyatTuri);
                            insertCmd.Parameters.AddWithValue("@AholiID", aholiID);

                            int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());
                            return insertedId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Unders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("Bu fuqaro avval qo'shilgan !");
            }

            return -1;
        }

        public static bool UpdateConvicted(int id, string jinoyatTuri, int aholiID)
        {
            Convicted c = GetConvictedByPopulaceId(aholiID);

            if (c == null)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();

                        string updateQuery = @"
                        UPDATE Sudlanganlar 
                        SET jinoyat_turi = @JinoyatTuri, aholiID = @AholiID
                        WHERE ID = @ID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@JinoyatTuri", jinoyatTuri);
                            updateCmd.Parameters.AddWithValue("@AholiID", aholiID);
                            updateCmd.Parameters.AddWithValue("@ID", id);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating convicted: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Bu fuqaro avval qo`shilgan !");
            }

            return false;
        }

        public static bool DeleteConvictedById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Sudlanganlar WHERE ID = @ID";

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting convicted: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool DeleteConvictedByPopulaceId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Sudlanganlar WHERE aholiID = @ID";

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting convicted: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
