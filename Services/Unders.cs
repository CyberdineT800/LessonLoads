using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.Services
{
    public class Unders : ConnectDB
    {
        public int ID { get; set; }
        public int AholiID { get; set; }
        public string FI { get; set; }

        public Unders()
        {
            this.ID = 0;
            this.AholiID = 0;
            this.FI = string.Empty;
        }

        public static DataTable GetAllUnders()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT k.ID, k.aholiID AS Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Kam_taminlanganlar k
                        INNER JOIN Aholi a ON k.aholiID = a.ID";

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
                MessageBox.Show("Error retrieving unders data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static Unders GetUndersById(int id)
        {
            Unders unders = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT k.ID, k.aholiID as Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Kam_taminlanganlar k
                        INNER JOIN Aholi a ON k.aholiID = a.ID
                        WHERE k.ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                unders = new Unders
                                {
                                    ID = id,
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
                MessageBox.Show("Error retrieving unders by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return unders;
        }


        public static Unders GetUndersByPopulaceId(int id)
        {
            Unders unders = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string selectQuery = @"
                        SELECT k.ID, k.aholiID as Aholi_ID, CONCAT(a.Familiya, ' ', a.Ism) AS FI
                        FROM Kam_taminlanganlar k
                        INNER JOIN Aholi a ON k.aholiID = a.ID
                        WHERE k.aholiID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                unders = new Unders
                                {
                                    ID = id,
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
                MessageBox.Show("Error retrieving unders by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return unders;
        }


        public static int AddNewUnders(int aholiID)
        {
            Unders u = GetUndersByPopulaceId(aholiID);

            if (u == null)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();

                        string insertQuery = @"
                        INSERT INTO Kam_taminlanganlar (aholiID)
                        VALUES (@AholiID);
                        SELECT SCOPE_IDENTITY();";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
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

        public static bool DeleteUndersById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Kam_taminlanganlar WHERE ID = @ID";

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
                MessageBox.Show("Error deleting unders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool DeleteUndersByPopulaceId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Kam_taminlanganlar WHERE aholiID = @ID";

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
                MessageBox.Show("Error deleting unders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
