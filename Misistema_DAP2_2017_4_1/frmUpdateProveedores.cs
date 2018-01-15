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
            this.Text = modo + " Proveedor";
        }

        public frmUpdateProveedores(string id_proveedor)  // Constructor Alterno: Modificar
        {
            InitializeComponent();
            this.modo = "Modificar";
            this.Text = modo + " Proveedor";
            this.clave = id_proveedor;
            DataRow dr = ctrl_proveedores.LocalizarProveedor(id_proveedor);
            txtClave.Text = dr[0].ToString();
            txtRaSo.Text = dr[1].ToString();
            txtRFC.Text = dr[2].ToString();
            txtTelefono.Text = dr[3].ToString();
            txtCaNu.Text = dr[4].ToString();
            txtEnCa.Text = dr[5].ToString();
            txtColonia.Text = dr[6].ToString();
            txtCP.Text = dr[7].ToString();
            txtCiudad.Text = dr[8].ToString();
            txtEstado.Text = dr[9].ToString();
        }


        private void frmUpdateProveedores_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtRaSo.Text.Trim() == "" || txtRFC.Text.Trim() == "" || txtTelefono.Text.Trim() == "" || txtCaNu.Text.Trim() == "" || txtEnCa.Text.Trim() == "" || txtColonia.Text.Trim() == "")
            {
                new frmMensaje("Error: hay campos vacios.", 2000, 2).ShowDialog();
                return; // impide que avance la ejecucion
            }

            if (!Validar.soloLetras(txtRaSo.Text))
            {
                new frmMensaje("Solo se permiten letras en la razon social.", 3000, 2).ShowDialog();
                txtRaSo.Focus();
                return; // impide que avance la ejecucion
            }
            if (!Validar.RFC(txtRFC.Text))
            {
                new frmMensaje("Solo se permiten letras mayusculas y numeros en el RFC.", 3000, 2).ShowDialog();
                txtRFC.Focus();
                return; // impide que avance la ejecucion
            }
            if (!Validar.soloDigitos(txtCP.Text))
            {
                new frmMensaje("Solo se permiten numeros en el codigo postal.", 3000, 2).ShowDialog();
                txtCP.Focus();
                return; // impide que avance la ejecucion
            }
            if (!Validar.soloDigitos(txtTelefono.Text))
            {
                new frmMensaje("Solo se permiten numeros en el telefono.", 3000, 2).ShowDialog();
                txtTelefono.Focus();
                return; // impide que avance la ejecucion
            }
            object[] datosA = { txtClave.Text.Trim(), txtRaSo.Text.Trim(), txtRFC.Text.Trim(), txtTelefono.Text, txtCaNu.Text.Trim(), txtEnCa.Text.Trim(), txtColonia.Text.Trim(), int.Parse(txtCP.Text.Trim()), txtCiudad.Text.Trim(), txtEstado.Text.Trim() };
            string[] campos = { "clave", "razon_social", "rfc", "telefono", "calle_y_num", "entre_calles", "colonia", "codigo_postal", "ciudad", "estado" };

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

                    if (ctrl_proveedores.actualizarProveedor(campos, datosA, "clave", this.clave))
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
    }
}
