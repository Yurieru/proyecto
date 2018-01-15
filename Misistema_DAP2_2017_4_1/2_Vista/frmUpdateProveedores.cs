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
using Validaciones;
namespace Misistema_DAP2_2017_4_1
{
    public partial class frmUpdateProveedores : Form
    {
        ControlProveedores ctrl_proveedores = new ControlProveedores(Frame.config);
        public bool HuboCambio = false;
        private string modo;              // Modo de la ventana (Agregar o Modificar)
        private string clave;   
        public frmUpdateProveedores()
        {
            InitializeComponent();
            this.modo = "Agregar";
            this.Text = modo + " id_prov";
            //txtemail_cont.Text = "0";
            //txttelf_cont.Text = "Hermosillo";
            //txtExtension.Text = "Sonora";

        }

        public frmUpdateProveedores(string id_prov)  // Constructor Alterno: Modificar
        {
            InitializeComponent();
            this.modo = "Modificar";
            this.Text = modo + " id_prov";
            this.clave = id_prov;
            DataRow dr = ctrl_proveedores.LocalizarProveedor(id_prov);
            txtClave.Text = dr[0].ToString();
            txtprovedor.Text = dr[1].ToString();
            txtdireccion.Text = dr[2].ToString();
            txtciudad.Text = dr[3].ToString();
            txtestado.Text = dr[4].ToString();
            txttelefono.Text = dr[5].ToString();
            txtContacto.Text = dr[6].ToString();
            //txtemail_cont.Text = "0";
            txtemail_cont.Text = dr[7].ToString();
            txttelf_cont.Text = dr[8].ToString();
            txtExtension.Text = dr[9].ToString();
            txtid_tipf.Text = dr[10].ToString();
            txtestatus.Text = dr[11].ToString();






        }


        private void frmUpdateProveedores_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtprovedor.Text.Trim() == "" || txtdireccion.Text.Trim() == "" || txtciudad.Text.Trim() == "" || txtestado.Text.Trim() == "" || txttelefono.Text.Trim() == "" || txtContacto.Text.Trim() == "" || txtemail_cont.Text.Trim() == "" || txttelf_cont.Text.Trim() == "" || txtExtension.Text.Trim() == "" || txtid_tipf.Text.Trim() == "" || txtestatus.Text.Trim() == "")
            {
                new frmMensaje("Error: hay campos vacios.", 2000, 2).ShowDialog();
                return; // impide que avance la ejecucion
            }

            if (!Validar.soloLetras(txtprovedor.Text))
            {
                new frmMensaje("Solo se permiten letras en la razon social.", 3000, 2).ShowDialog();
                txtprovedor.Focus();
                return; // impide que avance la ejecucion
            }
            //if (!Validar.RFC(txtdireccion.Text))
            //{
            //    new frmMensaje("Solo se permiten letras mayusculas y numeros en el RFC.", 3000, 2).ShowDialog();
            //    txtdireccion.Focus();
            //    return; // impide que avance la ejecucion
            //}
            //if (!Validar.soloDigitos(txtemail_cont.Text))
            //{
            //    new frmMensaje("Solo se permiten numeros en el codigo postal.", 3000, 2).ShowDialog();
            //    txtemail_cont.Focus();
            //    return; // impide que avance la ejecucion
            //}
            //if (!Validar.soloDigitos(txtciudad.Text))
            //{
            //    new frmMensaje("Solo se permiten numeros en el telefono.", 3000, 2).ShowDialog();
            //    txtciudad.Focus();
            //    return; // impide que avance la ejecucion
            //}
            object[] datosA = { txtClave.Text.Trim(), txtprovedor.Text.Trim(), txtdireccion.Text.Trim(), txtciudad.Text, txtestado.Text.Trim(), txttelefono.Text.Trim(), txtContacto.Text.Trim(), txtemail_cont.Text.Trim(), txttelf_cont.Text.Trim(), txtExtension.Text.Trim(), txtid_tipf.Text.Trim(), txtestatus.Text.Trim() };
            string[] campos = { "id_prov", "proveedor", "direccion", "ciudad", "estado", "telefono", "contacto", "emailContac", "telContac", "extension", "id_tipf", "status" };
            //int.Parse(txtemail_cont.Text.Trim())
            switch (modo)
            {
                case "Agregar":

                    if (ctrl_proveedores.insertarProveedor(datosA))
                    {
                        new frmMensaje("Los datos han sido guardados.", 1000).ShowDialog();
                        HuboCambio = true;
                    }
                    break;

                case "Modificar":

                    if (ctrl_proveedores.actualizarProveedor(campos, datosA, "id_prov", this.clave))
                    {
                        new frmMensaje("Datos actualizados", 1000).ShowDialog();
                        HuboCambio = true;
                    }
                    break;
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
