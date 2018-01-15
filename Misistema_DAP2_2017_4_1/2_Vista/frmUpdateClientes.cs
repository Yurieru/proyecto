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
    public partial class frmUpdateClientes : Form

    {
        ControlClientes ctrl_clientes = new ControlClientes(Frame.config);
        public bool HuboCambio = false;
        private string modo;              // Modo de la ventana (Agregar o Modificar)
        private string clave;   
        private GroupBox groupBox1;
        private Label label10;
        private TextBox txtClave;
        private TextBox txtEstado;
        private TextBox txtCiudad;
        private TextBox txtCP;
        private TextBox txtColonia;
        private TextBox txtEnCa;
        private TextBox txtCaNu;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtTelefono;
        private TextBox txtApellidos;
        private TextBox txtNombre;
        private Button btnAceptar;
        private Button btnSugerirClave;
        private Button btnCancelar;
    
        public frmUpdateClientes()
        {
            InitializeComponent();
            this.modo = "Agregar";
            txtCP.Text = "0";
            txtCiudad.Text = "Hermosillo";
            txtEstado.Text = "Sonora";
            this.Text = modo + " Cliente";
        }

        public frmUpdateClientes(string id_cliente)  // Constructor Alterno: Modificar
        {
            InitializeComponent();
            this.modo = "Modificar";
            this.Text = modo + " Cliente";
            this.clave = id_cliente;
            DataRow dr = ctrl_clientes.LocalizarClientes(id_cliente);
            txtClave.Text = dr[0].ToString();
            txtNombre.Text = dr[1].ToString();
            txtApellidos.Text = dr[2].ToString();
            txtTelefono.Text = dr[3].ToString();
            txtCaNu.Text = dr[4].ToString();
            txtEnCa.Text = dr[5].ToString();
            txtColonia.Text = dr[6].ToString();
            txtCP.Text = dr[7].ToString();
            txtCiudad.Text = dr[8].ToString();
            txtEstado.Text = dr[9].ToString();



        }






        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSugerirClave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.txtColonia = new System.Windows.Forms.TextBox();
            this.txtEnCa = new System.Windows.Forms.TextBox();
            this.txtCaNu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSugerirClave);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtClave);
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.txtCiudad);
            this.groupBox1.Controls.Add(this.txtCP);
            this.groupBox1.Controls.Add(this.txtColonia);
            this.groupBox1.Controls.Add(this.txtEnCa);
            this.groupBox1.Controls.Add(this.txtCaNu);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.txtApellidos);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 384);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnSugerirClave
            // 
            this.btnSugerirClave.AutoSize = true;
            this.btnSugerirClave.Location = new System.Drawing.Point(374, 20);
            this.btnSugerirClave.Name = "btnSugerirClave";
            this.btnSugerirClave.Size = new System.Drawing.Size(80, 23);
            this.btnSugerirClave.TabIndex = 20;
            this.btnSugerirClave.Text = "Sugerir Clave";
            this.btnSugerirClave.Click += new System.EventHandler(this.btnSugerirClave_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(65, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Clave:";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(169, 20);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(199, 20);
            this.txtClave.TabIndex = 1;
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(169, 348);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(137, 20);
            this.txtEstado.TabIndex = 10;
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(169, 313);
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(137, 20);
            this.txtCiudad.TabIndex = 9;
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(169, 277);
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(137, 20);
            this.txtCP.TabIndex = 8;
            // 
            // txtColonia
            // 
            this.txtColonia.Location = new System.Drawing.Point(168, 239);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(200, 20);
            this.txtColonia.TabIndex = 7;
            // 
            // txtEnCa
            // 
            this.txtEnCa.Location = new System.Drawing.Point(168, 201);
            this.txtEnCa.Name = "txtEnCa";
            this.txtEnCa.Size = new System.Drawing.Size(254, 20);
            this.txtEnCa.TabIndex = 6;
            // 
            // txtCaNu
            // 
            this.txtCaNu.Location = new System.Drawing.Point(168, 164);
            this.txtCaNu.Name = "txtCaNu";
            this.txtCaNu.Size = new System.Drawing.Size(200, 20);
            this.txtCaNu.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(62, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Estado:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Ciudad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Codigo Postal:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Colonia:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Entre calles:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Calle y Numero:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Telefono:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Apellidos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nombre:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(168, 124);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono.TabIndex = 4;
            // 
            // txtApellidos
            // 
            this.txtApellidos.Location = new System.Drawing.Point(168, 88);
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(200, 20);
            this.txtApellidos.TabIndex = 3;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(168, 52);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Location = new System.Drawing.Point(125, 413);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(82, 40);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(249, 413);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmUpdateClientes
            // 
            this.ClientSize = new System.Drawing.Size(476, 465);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(492, 504);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(492, 504);
            this.Name = "frmUpdateClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.frmUpdateClientes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void frmUpdateClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "" || txtApellidos.Text.Trim() == "" || txtTelefono.Text.Trim() == "" || txtCaNu.Text.Trim() == "" || txtEnCa.Text.Trim() == "" || txtColonia.Text.Trim() == "")
            {
                new frmMensaje("Error: hay campos vacios.", 2000, 2).ShowDialog();
                return; 
            }

            if (!Validar.soloLetras(txtNombre.Text))
            {
                new frmMensaje("Solo se permiten letras en el nombre.", 3000, 2).ShowDialog();
                txtNombre.Focus();
                return; 
            }
            if (!Validar.soloLetras(txtApellidos.Text))
            {
                new frmMensaje("Solo se permiten letras en el apellido.", 3000, 2).ShowDialog();
                txtApellidos.Focus();
                return; 
            }
            if (!Validar.soloDigitos(txtCP.Text))
            {
                new frmMensaje("Solo se permiten numeros en el codigo postal.", 3000, 2).ShowDialog();
                txtCP.Focus();
                return; 
            }
            if (!Validar.soloDigitos(txtTelefono.Text))
            {
                new frmMensaje("Solo se permiten numeros en el telefono.", 3000, 2).ShowDialog();
                txtTelefono.Focus();
                return; 
            }
            object[] datosA = { txtClave.Text.Trim(), txtNombre.Text.Trim(), txtApellidos.Text.Trim(), txtTelefono.Text, txtCaNu.Text.Trim(), txtEnCa.Text.Trim(), txtColonia.Text.Trim(), int.Parse(txtCP.Text.Trim()), txtCiudad.Text.Trim(), txtEstado.Text.Trim() };
            string[] campos = { "clave", "nombre", "apellidos", "telefono", "calle_y_num", "entre_calles", "colonia", "codigo_postal", "ciudad", "estado" };

            switch (modo)
            {
                case "Agregar":

                    if (ctrl_clientes.insertarClientes(datosA))
                    {
                        new frmMensaje("Los datos han sido guardados.", 1000).ShowDialog();
                        HuboCambio = true;
                    }
                    break;

                case "Modificar":

                    if (ctrl_clientes.actualizarClientes(campos, datosA, "clave", this.clave))
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

        private void btnSugerirClave_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtApellidos.Text == "")
            {
                new frmMensaje("Debe llenar los campos de nombre y apellidos para sugerir una clave", 4000, 2).ShowDialog();
                txtNombre.Focus();
            }
            else
            {
                txtClave.Text = ctrl_clientes.sugerirClave(txtNombre.Text, txtApellidos.Text);

            }
        }
    }

}

