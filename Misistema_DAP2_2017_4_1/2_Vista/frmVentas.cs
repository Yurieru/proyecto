using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controlador;                  // <---


namespace Misistema_DAP2_2017_4_1
{
    public partial class frmVentas : Form
    {
        //2.- Crear una instancia estática y privada:
        private static frmVentas _instancia = new frmVentas();

        private ControlDatosEmpresa ctl_emp = new ControlDatosEmpresa(Frame.config);
        private ControlVenta ctl_vta = new ControlVenta(Frame.config);
        private ControlParametros ctl_par = new ControlParametros(Frame.config);
        private bool cliente_valido = false;
        //private string error = "";
        private double subtotal, iva, total, pago, cambio;
        private int prc_iva;

        private frmVentas()     //1. constructor privado
        {
            InitializeComponent();

            inicializarVenta();
        }

        //3.- Crear propiedad get:
        public static frmVentas Instancia { get { return _instancia; } }

        private void inicializarVenta()
        {
            this.Cursor = Cursors.WaitCursor;
            string str_err = "";
            DataRow dr = ctl_emp.leerDatosEmpresa(ref str_err);
            if (dr == null)
                this.Text = "Ventas - Lo Atiende: " + Frame.Atiende;
            else
            {
                this.Text = "Ventas - " + dr[1].ToString();   //razon_social
                this.Text = this.Text + " -Lo Atiende: " + Frame.Atiende;
            } 
            
            checaSiguienteCons();
            checaParametros();

            numCantidad.Value = 1;
            cliente_valido = false;
            subtotal = iva = total = pago = cambio = 0.0;

            ctl_vta.inicializaTempDVentas();   // <-- Checa. Es necesario comenzar con la tabla vacía.
            cargarYFormatearDataGrid();

            lblFecha.Text = DateTime.Now.ToString();
            txtPago.Text = "0";
            calculos();
            txtClave.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtClave.Focus();
            this.Cursor = Cursors.Default;
        }


        private void checaSiguienteCons()
        {
            double cons = ctl_vta.leerSiguiente();
            lblCons.Text = cons.ToString();
        }


        private void checaParametros()
        {
            string str_err = "";
            DataRow dr = ctl_par.leerParametros(ref str_err);
            if (dr == null)
            {
                prc_iva = 0;
                txtFolio.Text = "0";
            }
            else
            {
                prc_iva = Convert.ToInt32(dr["iva"].ToString());
                txtFolio.Text = (Convert.ToInt32( dr["folio_venta"].ToString() ) + 1).ToString();
            }
        }


        private void cargarYFormatearDataGrid()
        {
            dataGridView1.DataSource = ctl_vta.leerTempDVentas().Tables[0];    // <-- Nice!!

            dataGridView1.Columns["Consecutivo"].Visible = false;
            dataGridView1.Columns["Codigo"].HeaderText = "Código";
            dataGridView1.Columns["Codigo"].Width = 100;
            dataGridView1.Columns["Descripcion"].HeaderText = "Descripción";
            dataGridView1.Columns["Descripcion"].Width = 350;
            dataGridView1.Columns["Cantidad"].Width = 120;
            dataGridView1.Columns["Precio_Unitario"].HeaderText = "P. Unit.";
            dataGridView1.Columns["Precio_Unitario"].Width = 140;
            dataGridView1.Columns["Importe"].Width = 140;

            dataGridView1.Columns["Cantidad"].DefaultCellStyle.Alignment =
            dataGridView1.Columns["precio_unitario"].DefaultCellStyle.Alignment =
            dataGridView1.Columns["Importe"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;       //Justifica a la derecha las columnas

            dataGridView1.Columns["precio_unitario"].DefaultCellStyle.Format =
            dataGridView1.Columns["Importe"].DefaultCellStyle.Format = "c"; //Formato Moneda para estas 2 columnas
        }


        private void calculos()
        {
            total = ctl_vta.sumaImportesTempDVentas();
            subtotal = total / (1 + prc_iva / 100.0);
            iva = total - subtotal;

            try { pago = Convert.ToDouble(txtPago.Text); }
            catch { pago = 0; }

            cambio = pago - total;

            lblSubtotal.Text = subtotal.ToString("c");
            lblIVA.Text = iva.ToString("c");
            lblTotal.Text = total.ToString("c");
            lblCambio.Text = cambio.ToString("c");
            if (cambio < 0)
                lblCambio.ForeColor = Color.Red;
            else
                lblCambio.ForeColor = Color.Blue;
        }


        private void btnSelCliente_Click(object sender, EventArgs e)
        {
            frmCatClientes cli = new frmCatClientes(true);    //activa boton seleccionar
            cli.ShowDialog();
            txtClave.Text = cli.clave_cli;
            txtNombre.Text = cli.nombre;
            txtTelefono.Text = cli.telefono;
            if (cli.clave_cli.Trim() != "") 
                cliente_valido = true;
            else 
                cliente_valido = false;
            txtCodigo.Focus();
        }


        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13 && txtClave.Text.Trim() != "")
            {
                ControlClientes cli = new ControlClientes(Frame.config);
                DataRow dr = cli.localizarClienteParaVenta(txtClave.Text.Trim());
                if (dr == null)
                {
                    new frmMensaje("No está registrado el cliente con clave: " + txtClave.Text, 2500, 2).ShowDialog();
                    txtCodigo.Clear();
                    txtNombre.Clear();
                    txtTelefono.Clear();
                    cliente_valido = false;
                    txtClave.Focus();
                }
                else
                {
                    txtNombre.Text = dr["nombre"].ToString() + " " + dr["apellidos"].ToString();
                    txtTelefono.Text = dr["telefono"].ToString();
                    cliente_valido = true;
                    txtCodigo.Focus();
                }
            }
        }


        private void btnSelProducto_Click(object sender, EventArgs e)
        {
            frmCatProductos prod = new frmCatProductos(true);    //activa boton seleccionar
            prod.ShowDialog();
            txtCodigo.Text = prod.codigo;
            txtCodigo.Focus();
            SendKeys.Send("{ENTER}");
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCodigo.Text.Trim() != "")
            {
                try
                {
                    if (ctl_vta.agregaProductoATempDVentas(int.Parse(txtCodigo.Text),
                                                           int.Parse(numCantidad.Value.ToString()),
                                                           int.Parse(lblCons.Text)))
                    {
                        calculos();
                        cargarYFormatearDataGrid();
                    }
                }
                catch //(Exception ex)
                {
                    new frmMensaje("El Código '" + txtCodigo.Text + "' no es correcto.", 2000, 2).ShowDialog();
                }
                txtCodigo.Clear(); txtCodigo.Focus();
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            calculos();
            if (cliente_valido == false)
            {
                new frmMensaje("No se ha registrado el cliente, o no es un cliente válido.", 2000, 2).ShowDialog();
                txtClave.Focus();
                return;
            }
            if (ctl_vta.cuentaRegsTempDVentas() == 0)
            {
                //MessageBox.Show("No se han registrado productos en esta venta.", "Error");
                new frmMensaje("No se han registrado productos en esta venta.", 2000, 2).ShowDialog();
                txtCodigo.Focus();
                return;
            }
            if (cambio > 0)
                MessageBox.Show("Cambio para el Cliente: \n\n" + cambio.ToString("c"), "Cambio");
            else if (cambio < 0)
            {
                MessageBox.Show("No se ha cubierto el total de la venta\n\nFaltante: " + 
                                (-1 * cambio).ToString("c"), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object[] master = { lblCons.Text, txtFolio.Text, Convert.ToDateTime(lblFecha.Text),
                                Frame.Atiende, txtClave.Text.Trim(), subtotal, iva, total, ""};    //leyenda no se utilizó. 

            if (ctl_vta.insertarVenta(master))           // Agregar registros a MVentas y DVentas
            {
                ctl_par.modificarParametros(prc_iva, txtFolio.Text);      // actualizar folio
                inicializarVenta();
            }
        }


        private void frmVentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)    //Tecla ESCAPE
                this.Close();
        }


        private void btnInfoCliente_Click(object sender, EventArgs e)
        {
            if (!(cliente_valido))
            {
                new frmMensaje("No se ha seleccionado un cliente.", 1500, 2).ShowDialog();
                txtClave.Focus();
                return;
            }
            frmInfoClientes infocli = new frmInfoClientes("Info", txtClave.Text.Trim());
            infocli.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (ctl_vta.cuentaRegsTempDVentas() > 0)
            {
                if (MessageBox.Show("¿Desea cancelar la venta que está en proceso?", "Venta en proceso",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { inicializarVenta(); }
            }
            else
                Console.Beep(440, 50);
        }

        //4.-
        private void frmVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ctl_vta.cuentaRegsTempDVentas() > 0)
            {
                if (MessageBox.Show("Una venta está en proceso. ¿Está seguro(a) de que desea salir del módulo de Ventas?",
                    "Venta en proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;        // decirle al programa la forma no se cerrará.
                    return;
                }
            }
            inicializarVenta();
            this.Visible = false;   // ocultar la ventana, no cerrarla
            e.Cancel = true;        // decirle al programa la forma no se cerrará.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString();
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmVentas_Load(object sender, EventArgs e)
        {

        }

    }
}
