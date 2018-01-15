using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Misistema_DAP2_2017_4_1
{
    public partial class frmseldestinoimpresion : Form
    {
        public int opcion = 1;
        public frmseldestinoimpresion()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            opcion = 0;
            this.Close();
        }

        private void frmseldestinoimpresion_Load(object sender, EventArgs e)
        {

        }

        private void HizoClic(object sender, EventArgs e)
        {
            if (rbWord.Checked) opcion = 1;
            if (rbExcel.Checked) opcion = 2;
            if (rbHTML.Checked) opcion = 3;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
