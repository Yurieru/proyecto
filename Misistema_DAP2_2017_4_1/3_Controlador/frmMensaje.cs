using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controlador
{
    public partial class frmMensaje : Form
    {
        public frmMensaje(string txt)
        {
            InitializeComponent();

            this.label1.Text = txt;
        }

        public frmMensaje(string txt, int time)
        {
            InitializeComponent();

            this.timer1.Interval = time;
            this.label1.Text = txt;
        }

        public frmMensaje(string txt, int time, int tipo)
        {
            InitializeComponent();

            this.timer1.Interval = time;
            this.label1.Text = txt;
            switch (tipo)
            {
                case 1: break;
                case 2: 
                    label1.ForeColor = Color.DarkRed;
                    Console.Beep(880, 350);
                    break;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
