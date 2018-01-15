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
    public partial class frmAcercaDe : Form
    {
        //2. creas una instancia estatoca y privada 
        private static frmAcercaDe _instancia = new frmAcercaDe();
        //3. crear propiedad get
        public static frmAcercaDe Instancia 
        { get { return _instancia; } 
            set { _instancia = value;}} 


        
        //1.constructor privado:

        private frmAcercaDe()
        {
            InitializeComponent();
            label4.Text = "Corriendo en " + Environment.OSVersion +
                " con base de datos en " + Frame.config.DBMS + ".";

            label4.Text = label4.Text.Replace("NT 6.2.9200.0", "10 Pro,");
         }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAcercaDe_Load(object sender, EventArgs e)
        {

        }

        //4.evitar que el objeto se cierre.
        private void frmAcercaDe_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false; // ocultar ventana,no cerrarla
            e.Cancel = true; // decirle que al prgorama que la forma no se cerrara
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:j.ortiz.f91@gmail.com");
        }
    }
}
