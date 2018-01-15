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
    public partial class Frame : Form
    {
        //Atributos
        public static ControlConfig config =
             new ControlConfig("MySQL", "Server=localhost; Database=mina; Uid=root; Pwd=12345678;");
        public static string Atiende = "";
        //Métodos
        
        
        public Frame()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Frame_Load(object sender, EventArgs e)
        {

            frmValidarUsuario login = new frmValidarUsuario();
            login.ShowDialog();
            if (login.entrar == false)
                this.Close();//Se cierra el Frame
            this.Text = this.Text + "/ Lo atiende: " + Frame.Atiende;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCatEmpleados empleados = new FrmCatEmpleados();
            empleados.MdiParent = this;
            empleados.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatProductos productos = new frmCatProductos();
            productos.MdiParent = this;
            productos.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe acerca = frmAcercaDe.Instancia;
            acerca.MdiParent = this;
            acerca.Show();
            acerca.BringToFront();
        }

        private void Frame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmAcercaDe.Instancia.Visible)
                frmAcercaDe.Instancia = null;
            Application.Exit();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatClientes clientes = new frmCatClientes();
            clientes.MdiParent = this;
            clientes.Show();
        }

        private void provedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatProveedores proveedores = new frmCatProveedores();
            proveedores.MdiParent = this;
            proveedores.Show();
        }

        private void datosDeLaEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDatosEmpresa demp = new frmDatosEmpresa();
            demp.MdiParent = this;
            demp.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmVentas ventas = frmVentas.Instancia;
            ventas.MdiParent = this;
            ventas.Show();
            ventas.BringToFront();

        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void visorDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmvisorventas demp = new frmvisorventas();
            demp.MdiParent = this;
            demp.Show();
        }
    }
}
