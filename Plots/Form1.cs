using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plots
{
    public partial class Form1 : Form
    {
        private int[] x,y;
        public Form1(int[] x, int[] y)
        {
            InitializeComponent();

            this.x = x;
            this.y = y;

            chart1.Titles.Add("Graf");
            chart1.Series["Odchykly od správných výsledků"].Points.AddXY("0","0");
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
