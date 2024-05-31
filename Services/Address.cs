using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Services
{
    public class Address
    {
        public class Region
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class District
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int RegionId { get; set; }

        }

        public static List<Region> GetAllRegions()
        {
            List<Region> regions = new List<Region>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM Viloyatlar";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Region region = new Region
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Name = reader.GetString(reader.GetOrdinal("Nomi")),
                                };

                                regions.Add(region);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Regions data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return regions;
        }


        public static Region GetRegionById(int regionId)
        {
            Region region = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM Viloyatlar WHERE ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", regionId);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                region = new Region
                                {
                                    ID = regionId,
                                    Name = reader.GetString(reader.GetOrdinal("Nomi")),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Region by ID: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return region;
        }


        public static List<District> GetAllDistricts(int regionId)
        {
            List<District> districts = new List<District>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = @"SELECT
                                             tuman.ID AS ID,
                                             tuman.Nomi AS Nomi
                                          FROM Tumanlar tuman
                                          WHERE tuman.ViloyatID = @RegionId";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("RegionId", regionId);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                District district = new District
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Name = reader.GetString(reader.GetOrdinal("Nomi")),
                                };

                                districts.Add(district);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Regions data: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return districts;
        }


        public static Region GetDistrictById(int districtId)
        {
            Region region = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectDB.connectString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM Tumanlar WHERE ID = @ID";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ID", districtId);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                region = new Region
                                {
                                    ID = districtId,
                                    Name = reader.GetString(reader.GetOrdinal("Nomi")),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving District by ID: " + ex.Message, "Market", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return region;
        }
    }
}
