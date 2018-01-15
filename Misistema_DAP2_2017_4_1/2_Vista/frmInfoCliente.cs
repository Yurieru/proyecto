using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controlador;

namespace Misistema_DAP2_2017_4_1
{
    public partial class frmInfoClientes : Form
    {   //Atributos:
        private ControlClientes ctl_clientes = new ControlClientes(Frame.config);
        private string modo;
        private string clave;

        public frmInfoClientes()
        {
            InitializeComponent();
        }

        public frmInfoClientes(string _modo, string _clave)
        {
            InitializeComponent();

            this.modo = _modo;
            this.Text = modo + " Cliente";
            this.clave = _clave;

            switch (modo)
            {
                case "Info":
                    DataRow reg = ctl_clientes.LocalizarClientes(clave);
                    txtClave.Text = reg[0].ToString();
                    txtNombre.Text = reg[1].ToString();
                    txtApellidos.Text = reg[2].ToString();
                    txtTelefono.Text = reg[3].ToString();
                    txtCalleyNum.Text = reg[4].ToString();
                    txtEntreCalles.Text = reg[5].ToString();
                    txtColonia.Text = reg[6].ToString();
                    txtCP.Text = reg[7].ToString();
                    txtCiudad.Text = reg[8].ToString();
                    txtEstado.Text = reg[9].ToString();
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInfoClientes_Load(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }
           
    }
}
