using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Stechuhr {
    static internal class SqlDatabaseHandling {
        public static List<String> executeQuery(string query)
        {
            //String connectionString = "datasource=10.235.116.132;port=3306;username=torben;password=torben;database=pruefung;";
            String connectionString = "server = 127.0.0.1; port = 3306; username = root; password =; database = cute_slug_zeiterfassung";

            MySqlConnection connection = new MySqlConnection(connectionString);
            //need to set query
            MySqlCommand sqlCommand = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                //createing an list to retun to get values in other functions
                List<String> queryList = new List<string>();
                //getting a reader counter to fill the return list
                int counterReader = 0;
                //execute only if there are rows to red
                if(sqlReader.HasRows)
                {   //get how much columns the table has
                    counterReader = sqlReader.FieldCount;
                    //start reader
                    sqlReader.Read();
                    //change to for with counter reader!!!
                    for(int i = 0; i < counterReader; i++)
                    {   //geting the values in the list
                        //if statment needed since we work with NULL values in table
                        //isDBNull throws fals if there is a value
                        if(sqlReader.IsDBNull(i))
                        {
                            queryList.Add("NULL");
                        }
                        else
                        {
                            queryList.Add(sqlReader.GetString(i));
                        }
                    }
                }

                connection.Close();
                return queryList;
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: " + e);
                return null;
            }
        }
    }

}
