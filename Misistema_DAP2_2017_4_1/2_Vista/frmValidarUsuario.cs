using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controlador;
namespace Misistema_DAP2_2017_4_1
{
    public partial class frmValidarUsuario : Form
    {
        //Atributos
        private ControlEmpleados cEmpleado = new ControlEmpleados(Frame.config);
        private int intentos = 0;
        public bool entrar = false;
        //Métodos
        public frmValidarUsuario()
        {
            InitializeComponent();
        }

        private void frmValidarUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "")
            {
                Console.Beep();
                txtUsuario.Focus();
                return;
            }
            else
            {
                intentos++;
                entrar = cEmpleado.validarUsuario(txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
                if (entrar)
                {
                    Frame.Atiende = this.txtUsuario.Text.Trim();
                    this.Close();
                }
                if (intentos == 3)
                    this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            entrar = false;
            this.Close();
        }
    }
}
