using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1.Services
{
    public class User : ConnectDB
    {
        public int ID { get; set; }
        public string Ism { get; set; }
        public string Familiya { get; set; }
        public string Telefon { get; set; }
        public int ViloyatID { get; set; }
        public int TumanID { get; set; }
        public byte[] Rasm { get; set; } 
        public string P_seria { get; set; }
        public string P_raqam { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
        public string Rol { get; set; }


        public static int AddNewUser(string ism, string familiya, string telefon, int viloyatID, int tumanID, Image rasm, string p_seria, string p_raqam, string login, string parol, string rol)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                string insertQuery = @"INSERT INTO Foydalanuvchi (Ism, Familiya, Telefon, ViloyatID, TumanID, Rasm, P_seria, P_raqam, Login, Parol, Rol)
                                   VALUES (@Ism, @Familiya, @Telefon, @ViloyatID, @TumanID, @Rasm, @P_seria, @P_raqam, @Login, @Parol, @Role);
                                   SELECT SCOPE_IDENTITY();";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Ism", ism);
                    insertCmd.Parameters.AddWithValue("@Familiya", familiya);
                    insertCmd.Parameters.AddWithValue("@Telefon", telefon);
                    insertCmd.Parameters.AddWithValue("@ViloyatID", viloyatID);
                    insertCmd.Parameters.AddWithValue("@TumanID", tumanID);
                    insertCmd.Parameters.AddWithValue("@Rasm", ConvertImageToByteArray(rasm)); 
                    insertCmd.Parameters.AddWithValue("@P_seria", p_seria);
                    insertCmd.Parameters.AddWithValue("@P_raqam", p_raqam);
                    insertCmd.Parameters.AddWithValue("@Login", login);
                    insertCmd.Parameters.AddWithValue("@Parol", parol);
                    insertCmd.Parameters.AddWithValue("@Role", rol);

                    int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    return insertedId;
                }
            }
        }


        public static DataTable UserSearch(string name, string surname, string phone, string pseria, string  pnum, string region, string district)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    string query = @"
                            SELECT 
                                f.ID, 
                                f.Ism, 
                                f.Familiya, 
                                f.Telefon,
                                f.P_seria, 
                                f.P_raqam, 
                                v.Nomi AS Viloyat, 
                                t.Nomi AS Tuman
                            FROM Foydalanuvchi f
                            LEFT JOIN Tumanlar t ON f.TumanID = t.ID
                            INNER JOIN Viloyatlar v ON f.ViloyatID = v.ID
                            WHERE 1=1";
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        query += " AND f.Ism LIKE @Name";
                        parameters.Add(new SqlParameter("@Name", "%" + name + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(surname))
                    {
                        query += " AND f.Familiya LIKE @Surname";
                        parameters.Add(new SqlParameter("@Surname", "%" + surname + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        query += " AND f.Telefon LIKE @Phone";
                        parameters.Add(new SqlParameter("@Phone", "%" + phone + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(pseria))
                    {
                        query += " AND f.P_seria LIKE @Seria";
                        parameters.Add(new SqlParameter("@Seria", "%" + pseria + "%"));
                    }

                    if (!string.IsNullOrWhiteSpace(pnum))
                    {
                        query += " AND f.P_raqam LIKE @Num";
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
                MessageBox.Show("Error searching user : " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }


        public static DataTable GetAllUsersWithDetails()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = @"SELECT f.ID, f.Ism, f.Familiya, f.Telefon, 
                                          v.Nomi AS ViloyatNomi, t.Nomi AS TumanNomi, 
                                          f.Rasm, f.P_seria, f.P_raqam, f.Rol
                                   FROM Foydalanuvchi f
                                   JOIN Viloyatlar v ON f.ViloyatID = v.ID
                                   JOIN Tumanlar t ON f.TumanID = t.ID";

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
                MessageBox.Show("Error retrieving User data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }


        private static byte[] ConvertImageToByteArray(Image image)
        {
            if (image == null)
                return null;

            using (var stream = new System.IO.MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                return stream.ToArray();
            }
        }

        public static User GetUserByLoginAndPassword(string login, string password)
        {
            User foydalanuvchi = null;

            string query = @"SELECT *
                     FROM Foydalanuvchi
                     WHERE Login = @Login AND Parol = @Password";

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foydalanuvchi = new User
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Ism = reader.GetString(reader.GetOrdinal("Ism")),
                                Familiya = reader.GetString(reader.GetOrdinal("Familiya")),
                                Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                ViloyatID = reader.GetInt32(reader.GetOrdinal("ViloyatID")),
                                TumanID = reader.GetInt32(reader.GetOrdinal("TumanID")),
                                Rasm = (byte[])reader["Rasm"],  
                                P_seria = reader.GetString(reader.GetOrdinal("P_seria")),
                                P_raqam = reader.GetString(reader.GetOrdinal("P_raqam")),
                                Login = reader.GetString(reader.GetOrdinal("Login")),
                                Parol = reader.GetString(reader.GetOrdinal("Parol")),
                                Rol = reader.GetString(reader.GetOrdinal("Rol")),
                            };
                        }
                    }
                }
            }

            return foydalanuvchi;
        }

        public static bool DeleteUserById(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string deleteQuery = @"DELETE FROM Foydalanuvchi WHERE ID = @UserId";

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@UserId", userId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting User: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdateUser(int id, string ism, string familiya, string telefon, int viloyatID, int tumanID, Image rasm, string p_seria, string p_raqam, string login, string parol, string rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string updateQuery = @"UPDATE Foydalanuvchi 
                                           SET Ism = @Ism, Familiya = @Familiya, Telefon = @Telefon, 
                                               ViloyatID = @ViloyatID, TumanID = @TumanID, Rasm = @Rasm,
                                               P_seria = @P_seria, P_raqam = @P_raqam, Login = @Login, Parol = @Parol, Rol = @Role
                                           WHERE ID = @ID";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Ism", ism);
                        updateCmd.Parameters.AddWithValue("@Familiya", familiya);
                        updateCmd.Parameters.AddWithValue("@Telefon", telefon);
                        updateCmd.Parameters.AddWithValue("@ViloyatID", viloyatID);
                        updateCmd.Parameters.AddWithValue("@TumanID", tumanID);
                        updateCmd.Parameters.AddWithValue("@Rasm", ConvertImageToByteArray(rasm)); 
                        updateCmd.Parameters.AddWithValue("@P_seria", p_seria);
                        updateCmd.Parameters.AddWithValue("@P_raqam", p_raqam);
                        updateCmd.Parameters.AddWithValue("@Login", login);
                        updateCmd.Parameters.AddWithValue("@Parol", parol);
                        updateCmd.Parameters.AddWithValue("@Role", rol);
                        updateCmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating User: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT ID, Ism, Familiya, Telefon, ViloyatID, TumanID, Rasm, P_seria, P_raqam, '*****' AS Login, '*****' AS Parol, Rol FROM Foydalanuvchi";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectCmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    row["Login"] = "*****";
                    row["Parol"] = "*****";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving User data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static User GetUserById(int id)
        {
            User foydalanuvchi = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM Foydalanuvchi WHERE ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foydalanuvchi = new User
                                {
                                    ID = id,
                                    Ism = reader.GetString(reader.GetOrdinal("Ism")),
                                    Familiya = reader.GetString(reader.GetOrdinal("Familiya")),
                                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                                    ViloyatID = reader.GetInt32(reader.GetOrdinal("ViloyatID")),
                                    TumanID = reader.GetInt32(reader.GetOrdinal("TumanID")),
                                    Rasm = (byte[])reader["Rasm"], 
                                    P_seria = reader.GetString(reader.GetOrdinal("P_seria")),
                                    P_raqam = reader.GetString(reader.GetOrdinal("P_raqam")),
                                    Login = reader.GetString(reader.GetOrdinal("Login")),
                                    Parol = reader.GetString(reader.GetOrdinal("Parol")),
                                    Rol = reader.GetString(reader.GetOrdinal("Rol"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving User by ID: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return foydalanuvchi;
        }
    }
}
