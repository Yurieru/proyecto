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
    public partial class frmvisorventas : Form
    {

        private ControlVenta ctl_vta = new ControlVenta(Frame.config);

        public frmvisorventas()
        {
            InitializeComponent();
            prepararForma();
        }


        private void prepararForma()
        {
            this.dataGridView1.DataSource = this.ctl_vta.leerVentas();
            Font font = new Font(this.Font, FontStyle.Bold);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = font;
            foreach (DataGridViewColumn dataGridViewColumn in this.dataGridView1.Columns)
            {
                dataGridViewColumn.HeaderText = dataGridViewColumn.HeaderText.Substring(0, 1).ToUpper() + dataGridViewColumn.HeaderText.Substring(1);
            }
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.Columns["Subtotal"].DefaultCellStyle.Alignment = (this.dataGridView1.Columns["Iva"].DefaultCellStyle.Alignment = (this.dataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight));
            this.dataGridView1.Columns["Subtotal"].DefaultCellStyle.Format = (this.dataGridView1.Columns["Iva"].DefaultCellStyle.Format = (this.dataGridView1.Columns["Total"].DefaultCellStyle.Format = "c"));
        }



        private void frmvisorventas_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int cons = Convert.ToInt32(this.dataGridView1[0, this.dataGridView1.CurrentRow.Index].Value);
            this.dataGridView2.DataSource = this.ctl_vta.leerDVentas(cons).Tables[0];
            Font font = new Font(this.Font, FontStyle.Bold);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = font;
            foreach (DataGridViewColumn dataGridViewColumn in this.dataGridView2.Columns)
            {
                dataGridViewColumn.HeaderText = dataGridViewColumn.HeaderText.Substring(0, 1).ToUpper() + dataGridViewColumn.HeaderText.Substring(1);
            }
            this.dataGridView2.Columns["Cantidad"].DefaultCellStyle.Alignment = (this.dataGridView2.Columns["Precio_unitario"].DefaultCellStyle.Alignment = (this.dataGridView2.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight));
            this.dataGridView2.Columns["Precio_unitario"].DefaultCellStyle.Format = (this.dataGridView2.Columns["Importe"].DefaultCellStyle.Format = "c");
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
