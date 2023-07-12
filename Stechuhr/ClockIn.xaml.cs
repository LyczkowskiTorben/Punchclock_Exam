using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Stechuhr
{
    /// <summary>
    /// Interaktionslogik für ClockIn.xaml
    /// </summary>
    public partial class ClockIn : Window
    {
        //creating a globle DispatchTimer
        private DispatcherTimer dispatcherTimer;

        public ClockIn()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(tmp);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }
        private void tmp(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
        }
    }
}
