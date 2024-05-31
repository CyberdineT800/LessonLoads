using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ConnectDB
    {
        protected static SqlConnection conn;
        protected SqlCommand cmd;
        protected SqlDataReader reader;
        protected SqlDataAdapter adapter;
        protected string sqlText;
        public static string connectString = "Data Source=DESKTOP-OKRPHD4\\SQLEXPRESS;Initial Catalog=Aholi_Manitotingi;Integrated Security=True";

        public ConnectDB()
        {
            try
            {
                conn = new SqlConnection(connectString);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ~ConnectDB()
        {
            conn.Close();
        }

        public static List<string> GetAllTables()
        {
            List<string> tables = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    string query = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader.GetString(0);

                                if (tableName != "sysdiagrams")
                                    tables.Add(tableName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving tables: " + ex.Message);
            }

            return tables;
        }


        public static DataTable GetDataFromTable(string tableName)
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectDB.connectString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {tableName}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data from table {tableName}: {ex.Message}");
            }

            return table;
        }


        public static DataTable GetDataWithRelatedTables(string tableName)
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    Dictionary<string, string> foreignKeys = GetForeignKeyRelationships(tableName, connection);

                    string query = ConstructQuery(tableName, foreignKeys);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data from table {tableName}: {ex.Message}");
            }

            return table;
        }


        private static Dictionary<string, string> GetForeignKeyRelationships(string tableName, SqlConnection connection)
        {
            Dictionary<string, string> foreignKeys = new Dictionary<string, string>();

            try
            {
                string query = $@"SELECT
                                    f.name AS ForeignKeyName,
                                    OBJECT_NAME(f.parent_object_id) AS TableName,
                                    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
                                    OBJECT_NAME(f.referenced_object_id) AS ReferencedTableName,
                                    COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferencedColumnName
                                  FROM 
                                    sys.foreign_keys AS f
                                  INNER JOIN 
                                    sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
                                  WHERE 
                                    OBJECT_NAME(f.parent_object_id) = '{tableName}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader["ColumnName"].ToString();
                            string referencedTableName = reader["ReferencedTableName"].ToString();
                            string referencedColumnName = reader["ReferencedColumnName"].ToString();

                            foreignKeys.Add(columnName, $"{referencedTableName}.{referencedColumnName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving foreign key relationships for table {tableName}: {ex.Message}");
            }

            return foreignKeys;
        }

        public static List<string> GetAllColumnNames(string tableName)
        {
            List<string> columnNames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    string query = $@"SELECT COLUMN_NAME 
                              FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME = '{tableName}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string columnName = reader.GetString(0);
                                columnNames.Add(columnName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving column names for table {tableName}: {ex.Message}");
            }

            return columnNames;
        }


        private static string ConstructQuery(string tableName, Dictionary<string, string> foreignKeys)
        {
            StringBuilder queryBuilder = new StringBuilder();
            List<string> columnNames = GetAllColumnNames(tableName);
            var keys = foreignKeys.Keys;

            queryBuilder.Append("SELECT ");

            foreach (string columnName in columnNames)
            {
                if (!keys.Contains(columnName))
                {
                    queryBuilder.Append($"{tableName}.{columnName}, ");
                }
            }

            int t = 1;
            string replacedColumnName;
            string[] referensed;

            foreach (var kvp in foreignKeys)
            {
                referensed = kvp.Value.Split('.');

                columnNames = GetAllColumnNames(referensed[0]);
                replacedColumnName = columnNames.Contains("Nomi") ? "Nomi" : "Ismi";

                string new_column_name = kvp.Key;
                new_column_name = new_column_name.Replace("_ID", " " + replacedColumnName);

                queryBuilder.Append($"{referensed[0]}{t}.{replacedColumnName} AS [{new_column_name}], ");

                t++;
            }

            queryBuilder.Remove(queryBuilder.Length - 2, 2);

            queryBuilder.Append($" FROM {tableName}");
            t = 1;

            foreach (var kvp in foreignKeys)
            {
                referensed = kvp.Value.Split('.');

                queryBuilder.Append($" INNER JOIN {referensed[0]} {referensed[0]}{t} ON {tableName}.{kvp.Key} = {referensed[0]}{t}.ID");

                t++;
            }

            return queryBuilder.ToString();
        }
    }
}