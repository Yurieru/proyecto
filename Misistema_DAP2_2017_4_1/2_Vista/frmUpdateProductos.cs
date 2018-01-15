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
    public partial class frmUpdateProductos : Form
    {

        private ControlProductos ct1_productos = new ControlProductos(Frame.config);
        public bool huvocambio = false;
        private string modo;
        private int codigo;


        public frmUpdateProductos()
        {
            InitializeComponent();
            this.modo = "Agregar";
            this.Text = modo + "producto";
            //ct1_productos.clavesDeProveedores(ref cboProveedor);
            ct1_productos.llenarComboProv(ref cboProveedor);
            txtCodigo.Text = ct1_productos.sigCodigoProducto().ToString();
            txtCapacidad.Text = txtRespuesta.Text = txtEntrega.Text = "0";
        }

        public frmUpdateProductos(int _codigo)
        {
            InitializeComponent();
            this.modo = "Modificar";
            this.Text = modo + "Producto";
            codigo = _codigo;
            ct1_productos.clavesDeProveedores(ref cboProveedor);
            DataRow reg = ct1_productos.localizarProducto(_codigo);
            txtCodigo.Text = reg[0].ToString();
            txtProducto.Text = reg[1].ToString();
            txtDescripcion.Text = reg[2].ToString();
            txtRespuesta.Text = reg[3].ToString();
            txtCapacidad.Text = reg[4].ToString();
            txtEntrega.Text = reg[5].ToString();
            cboProveedor.Text = reg[6].ToString();




        }



        private void frmUpdateProductos_Load(object sender, EventArgs e)
        {
            SendKeys.Send("{Right}");
        }

        private bool validarTodo()
        {
            if (txtProducto.Text == "")
            {
                errorProvider1.SetError(txtProducto, "no se ha capturado la descripcion");
                txtProducto.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtProducto, "");
            }

            if (Validar.soloDigitos(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "");
            }
            else
            {
                errorProvider1.SetError(txtCodigo, "solo digitos:0123456789");
                return true;
            }
            if (Validar.soloDigitos(txtRespuesta.Text))
            {
                errorProvider1.SetError(txtRespuesta, "");
            }
            else
            {
                errorProvider1.SetError(txtCodigo, "formato no valido");
                return true;
            }
            if (Validar.soloDigitos(txtCapacidad.Text))
            {
                errorProvider1.SetError(txtCapacidad, "");
            }
            else
            {
                errorProvider1.SetError(txtCapacidad, "solo digitos:0123456789");
                return true;
            }
            if (cboProveedor.Text == "")
            {
                errorProvider1.SetError(cboProveedor, "no se ha establecido el proveedor.");
                cboProveedor.Focus();
                return false;
            }
            else
                errorProvider1.SetError(cboProveedor, "");
            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarTodo() == false) return;
            string[] campos = { "codigo", "descripcion", "unidad", "precio_unitario", "existencia", "p_reorden", "clave_prov" };
            object[] datos_cli = {txtCodigo.Text.Trim(), txtProducto.Text.Trim(), txtDescripcion.Text.Trim(),double.Parse(txtRespuesta.Text.Trim()), txtCapacidad.Text.Trim(),txtEntrega.Text.Trim(), cboProveedor.Text};

            switch (modo)
            {

                case "Agregar":
                    if(ct1_productos.insertarProducto(datos_cli))
                    {

                        new frmMensaje("los datos han sido guardados", 1000).ShowDialog();
                        huvocambio = true;
                    }
                    break;

                case"Modificar":
                    if(ct1_productos.modificarProducto(campos,datos_cli,"codigo",this.codigo))
                    {

                        new frmMensaje("Datos Actualizados", 1000).ShowDialog();
                        huvocambio = true;
                    }
                    break;
            }

            
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
