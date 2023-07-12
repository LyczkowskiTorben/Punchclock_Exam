using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;

namespace Stechuhr {
   static internal class Punchclock {
        public static void checkInput(String input)
        {
            //for developing reasons input
            String rfid = input;

            String findMBQuery = "SELECT * FROM mitarbeiter WHERE rfid = " + rfid + ";";
            //getting the data from sql tabel in an list
            List<String> dataListMitarbeiter = SqlDatabaseHandling.executeQuery(findMBQuery);
            //getting the data mb_id from the list its always on index 0
            if(dataListMitarbeiter.Count == 0)
            {
                MessageBox.Show("Fehlerhafter Chip! Bitte bei HR melden!");
                return;
            }
            //creating a string to put in the Query strings
            string mb_id = dataListMitarbeiter.ElementAt(0);
            //create a query to use in the function to execute
            //IMPORTANT it searces for a point where mb_id existst and clouckout is NULL so we can see if
            //we need to create a new entry to just put in the clockout DATETIME
            String findZeitenEmptyOut = "SELECT * FROM zeiten WHERE mb_id = " + mb_id + " AND clockOut IS NULL";
            //createing a ist to fill with query results
            List<String> dataListZeitenEmptyOut = SqlDatabaseHandling.executeQuery(findZeitenEmptyOut);
            if(dataListZeitenEmptyOut.Count != 0)
            {   
                String clockInDateTime = dataListZeitenEmptyOut.ElementAt(1);
                clockOut(mb_id, clockInDateTime);
                MainWindow.messageOpen(mb_id);
            }
            else
            {
                clockIn(mb_id);
            }
        }

        private static void clockIn(string mb_id)
        {
            String clockInQuery = "INSERT INTO zeiten VALUES(" + mb_id + ",CURRENT_TIMESTAMP,NULL);";
            SqlDatabaseHandling.executeQuery(clockInQuery);
        }

        private static void clockOut(string mb_id, String clockIN)
        {   //parse to DateTime to convert easy to sql string
            DateTime clockInDateTime = DateTime.ParseExact(clockIN, "dd.MM.yyyy HH:mm:ss", null);
            //convert date time to sql string
            String sqlFormattedClockIn = clockInDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            //using the string to put in clock out
            String clockOutQuery = "UPDATE zeiten SET clockOut=CURRENT_TIMESTAMP WHERE mb_id AND clockIn='" + sqlFormattedClockIn + "';";
            SqlDatabaseHandling.executeQuery(clockOutQuery);
        }



    }
}
