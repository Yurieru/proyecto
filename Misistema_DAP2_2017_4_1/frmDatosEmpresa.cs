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
    public partial class frmDatosEmpresa : Form
    {
        ControlDatosEmpresa ctl_empresa = new ControlDatosEmpresa(Frame.config);
        DataRow dr;
        private bool primera_vez = false;
        private string nombre = "";
        public frmDatosEmpresa()
        {
            InitializeComponent();
            prepararForma();
        }

        private void prepararForma()
        {

            string error = "";
            dr = ctl_empresa.leerDatosEmpresa(ref error);
            if (dr == null)
                primera_vez = true;
            else
            {

                primera_vez = false;
                nombre = txtNombre.Text = dr[0].ToString();
                txtruta.Text = dr[1].ToString();
                txtrfc.Text = dr[2].ToString();
                txttelefono.Text = dr[3].ToString();
                txtcalleyno.Text = dr[4].ToString();
                txtEntrecalles.Text = dr[5].ToString();
                txtcol.Text = dr[6].ToString();
                txtcp.Text = dr[7].ToString();
                txtciudad.Text = dr[8].ToString();
                txtestado.Text = dr[9].ToString();
                txtleyenda.Text = dr[10].ToString();
                txtslogan.Text = dr[11].ToString();
                txtrutalogotipo.Text = dr[12].ToString();
                pictureBox1.ImageLocation = "";
                toolTip1.SetToolTip(pictureBox1, "");
                if (txtrutalogotipo.Text.Trim() != "")
                {

                    try
                    {

                        pictureBox1.Load(txtrutalogotipo.Text);
                        toolTip1.SetToolTip(pictureBox1, txtrutalogotipo.Text);
                    }
                    catch
                    {

                        toolTip1.SetToolTip(pictureBox1, "No se pudo cargar la imagen");
                    }



                }

            }
        }

        private void frmDatosEmpresa_Load(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string t_ruta = txtrutalogotipo.Text.Trim();
            if (Frame.config.DBMS.ToUpper() == "MYSQL")
                t_ruta = t_ruta.Replace("\\", "\\\\");

            string[] campos = {"nombre","razon_social","RFC","telefono","calle_y_num",
                               "entre_calles","colonia","codigo_postal","ciudad",
                               "estado","leyenda","slogan","ruta_logotipo"};

            object[] datos_emp = {txtNombre.Text.Trim(),txtruta.Text.Trim(),txtrfc.Text.Trim(),
                                   txttelefono.Text.Trim(), txtcalleyno.Text.Trim(), txtEntrecalles.Text.Trim(),
                                   txtcol.Text.Trim(), txtcp.Text.Trim(),txtciudad.Text.Trim(),
                                   txtestado.Text.Trim(),txtleyenda.Text.Trim(),txtslogan.Text.Trim(),t_ruta};

            if (primera_vez)
                ctl_empresa.InsertarDatosEmpresa(datos_emp);
            else
                ctl_empresa.modificarDatosEmpresa(campos, datos_emp, "nombre", this.nombre);

            this.Close();

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
                return;

            txtrutalogotipo.Text = openFileDialog1.FileName;
            try
            {

                pictureBox1.Load(txtrutalogotipo.Text);
                toolTip1.SetToolTip(pictureBox1, txtrutalogotipo.Text);


            }
           
            catch(Exception ex)
            {

                toolTip1.SetToolTip(pictureBox1, "no se pudo cargar la imagen..");
                if (ex.Message.Contains("imageLocation") == false)
                    MessageBox.Show(ex.Message, "Error");

            }
        
        }
    }
}
