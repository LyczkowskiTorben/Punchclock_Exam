using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;

namespace Stechuhr {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        
        public MainWindow()
		{
            InitializeComponent();
            BackgroundImage();
		}
        
        private void BackgroundImage()
		{
			try
			{
				//need to change to the picture u want to have in the background
				string ftp = "ftp://test@127.0.0.1/ouaigvdbj.PNG";
				//username for the FTP
				//make sure its running before truning on the programm
				string username = "test";
				//password for the ftp
				string password = "root";

				//impliments a ftp client
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp);
				//starts a request to download file from ftp path
				request.Method = WebRequestMethods.Ftp.DownloadFile;
				//uses the username and password to connect to ftp
				request.Credentials = new NetworkCredential(username, password);

				//recives the respond from the server
				//returns the data stream from the ftp
				//creating a new bitmap to put in the background of the wpf
				using(FtpWebResponse response = (FtpWebResponse)request.GetResponse())
				{   //creating a new bitmap with the data from the stream to produce a new bitmap
					//creating a variable/object from stream class 
					Stream stream = response.GetResponseStream();
					//create new bit image object
                    BitmapImage bitmapImage = new BitmapImage();
					//starts the bitmapImage initilisation
                    bitmapImage.BeginInit();
					//sets the BitmapCachOption to on load
                    //bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					//sets the source for the data stream to get the bitmapImage
					//the streamSource takes the data from the stream object
                    bitmapImage.StreamSource = stream;
					//end the initilisation
                    bitmapImage.EndInit();

					//creating a brush to use in the xaml
					//giving the brusch the mitmapImage
                    ImageBrush costumBrush = new ImageBrush(bitmapImage);
					//seting the background of the stackpanel object to the costum brush
                    mainStackPanel.Background = costumBrush;
					//need to be here so the img has time to laod
					
					//if time optimasation here nassesary
					//await load?
					Thread.Sleep(1000);
				}
			}
			catch(Exception e)
			{
				MessageBox.Show("Error loading background"+e);
			}
		}
        public static void messageOpen(string mb_id)
        {
            ClockIn test = new ClockIn();
			test.Show();
			test.Activate();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
			Punchclock.checkInput("3141592");
			
        }
    }
}
