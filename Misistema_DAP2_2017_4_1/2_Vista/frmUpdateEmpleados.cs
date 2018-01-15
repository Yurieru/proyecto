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
    public partial class frmUpdateEmpleados : Form
    {
        //Variables:
        private ControlEmpleados ctl_empleado = new ControlEmpleados(Frame.config);
        public bool HuboCambio = false;
        private string modo;
        private string usuario = "";
        private string clave_cifrada;
        public frmUpdateEmpleados()
        {
            InitializeComponent();
            this.modo = "Agregar";
            this.Text = modo + "Empleado";
            cboNivel.Text = "Vendedor";
        }


        public frmUpdateEmpleados(string id_usuario)
        {
            InitializeComponent();
            this.modo = "Modificar";
            //this.Text = modo + "Empleado";
            this.Text = modo + "usuario";
            this.usuario = id_usuario;

            DataRow dr = ctl_empleado.localizarEmpleado(id_usuario);
            txtUsuario.Text = dr["usuario"].ToString();
            clave_cifrada = dr["clave"].ToString();
            cboNivel.Text = dr["nivel"].ToString();
            txtestado.Text = dr["estado"].ToString();
        }



        private void frmUpdateEmpleados_Load(object sender, EventArgs e)
        {
             SendKeys.Send("{Right}");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //if (txtUsuario.Text.Trim() == "" || txtNombre.Text.Trim() == "")
            if (txtUsuario.Text.Trim() == "")
            {
                new frmMensaje("Error: Hay campos en blanco.", 2000, 2).ShowDialog();
                return; //impide que avance la ejecución.
            }
            if (Validar.soloLetras(txtestado.Text) == false)
            {

                new frmMensaje("solo se permiten letras en el nombre.", 3000, 2).ShowDialog();
                txtestado.Focus();
                return;
            }
            switch (modo)
            {
                case "Agregar":
                    object[] datosA = {txtUsuario.Text.Trim(), txtClave.Text.Trim(), cboNivel.Text.Trim(),
                    txtestado.Text.Trim() };

                    if (ctl_empleado.InsertarEmpleado(datosA))
                        HuboCambio = true;
                    break;

                case "Modificar":
                    //string[] camposM = { "usuario", "clave", "nivel", "nombre_completo" };
                    string[] camposM = { "usuario", "clave", "nivel", "estado"};
                    if (txtClave.Text.Trim() == "")
                    {

                        object[] datosM = {txtUsuario.Text.Trim(), clave_cifrada, cboNivel.Text.Trim(),
                    txtestado.Text.Trim() };
                        if (ctl_empleado.modificarEmpleado("empleados", camposM, datosM, "usuario", usuario))
                        {

                            new frmMensaje("datos guardados", 1000).ShowDialog();
                            HuboCambio = true;
                        }
                    }
                    else
                    {
                        object[] datosM = { txtUsuario.Text.Trim(), txtClave.Text.Trim(), cboNivel.Text.Trim(), txtestado.Text.Trim() };

                        if (ctl_empleado.modificarEmpleadoNuevaClave("empleados", camposM, datosM, "usuario", usuario))
                        {

                            new frmMensaje("datos guardados", 1000).ShowDialog();
                            HuboCambio = true;

                        }

                    }

                    break;
            }
            //this.HuboCambio = false;
            this.Close();
        }
    }
}
