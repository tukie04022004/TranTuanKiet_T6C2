using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebNhaSach.Models
{
    public class DataModels
    {
        private string connectionString = "workstation id=datacnpmnc.mssql.somee.com;packet size=4096;user id=huyenlam140704_SQLLogin_1;pwd=Huyenlam2004xx@;data source=datacnpmnc.mssql.somee.com;persist security info=False;initial catalog=datacnpmnc;TrustServerCertificate=True";
        public ArrayList get(String sql)
        {
            ArrayList datalist = new ArrayList();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            using (SqlDataReader r = command.ExecuteReader())

            {

                while (r.Read())

                {

                    ArrayList row = new ArrayList();
                    for (int i = 0; i < r.FieldCount; i++)

                    {

                        row.Add(r.GetValue(i).ToString());

                    }
                    datalist.Add(row);

                }


            }
            return datalist;

        }
    }

}