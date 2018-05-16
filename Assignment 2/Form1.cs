using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment_2
{
    public partial class Form1 : Form
    {
        class row
        {
            double time;
            double velocity;
            double acceleration;
            double altitude;
        }

        List<row> table = new List<row>();
        public Form1()
        {
            InitializeComponent();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = " csv Files|* . csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            { 
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        string line = sr.ReadLine();
                        while (!)
        }
    }
}
