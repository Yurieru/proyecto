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
using System.IO;
//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
namespace Misistema_DAP2_2017_4_1
{
    public partial class frmCatProductos : Form
    {
        private ControlProductos ct1_productos = new ControlProductos(Frame.config);
        public string codigo = "";
        DataSet DS;

        public frmCatProductos()
        {
            InitializeComponent();
            prepararForma();
        }
        public frmCatProductos(bool seleccionar) //constructor alterno
        {
            InitializeComponent();

            if (seleccionar) btnseleccionar.Visible = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            prepararForma();

        }
        private void prepararForma()
        {
            if (txtBuscar.Text.Trim() == "")
                DS = ct1_productos.leerProductos();
            else
                DS = ct1_productos.leerProductos(txtBuscar.Text);
            dataGridView1.DataSource = DS.Tables[0];

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {

                col.HeaderText = col.HeaderText.Substring(0, 1).ToUpper() + col.HeaderText.Substring(1);

            }

            Font negrita = new Font(this.Font, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = negrita;

            //dataGridView1.Columns["precio_unitario"].DefaultCellStyle.Alignment =
            //dataGridView1.Columns["existencia"].DefaultCellStyle.Alignment =
            // dataGridView1.Columns["p_reorden"].DefaultCellStyle.Alignment =
            // DataGridViewContentAlignment.MiddleRight;
            //dataGridView1.Columns["precio_unitario"].DefaultCellStyle.Format = "c";
            //dataGridView1.Columns["precio_unitario"].HeaderText = "P_unit";
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            DS = null;
            //if (this.Visible)
               // coloreadatagridview();


        }

        //private void coloreadatagridview()
        //{
        //    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
        //    {
        //        if (Convert.ToInt32(dgvr.Cells[4].Value) <= Convert.ToInt32(dgvr.Cells[5].Value))
        //        {
        //            dgvr.DefaultCellStyle.BackColor = Color.IndianRed;
        //            dgvr.DefaultCellStyle.ForeColor = Color.Yellow;


        //        }
        //    }
        //}

        private void frmCatProductos_Load(object sender, EventArgs e)
        {

        }

        private void frmCatProductos_Load_1(object sender, EventArgs e)
        {
            //coloreadatagridview();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            prepararForma();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmUpdateProductos up_prod = new frmUpdateProductos();
            up_prod.ShowDialog();
            if (up_prod.huvocambio) prepararForma();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            frmUpdateProductos up_prod = new frmUpdateProductos(codigo);
            up_prod.ShowDialog();
            if (up_prod.huvocambio) prepararForma();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            DataRow dr = ct1_productos.localizarProducto(codigo);
            DialogResult resp =
                MessageBox.Show("¿esta seguro que desea eliminar el registro del producto?\n\n " + dr["descripcion"].ToString() + ".", "ATENCION",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {

                string error = "";
                if (ct1_productos.eliminarProducto(codigo, ref error))
                {
                    prepararForma();
                }
                else
                {
                    if (error.Contains("key constraint fails"))
                        new frmMensaje("Error: este registro se encuentra en otra tabla.", 3000, 2).ShowDialog();
                    else
                        new frmMensaje(error, 3000, 2).ShowDialog();
                }


            }


        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmseldestinoimpresion sdi = new frmseldestinoimpresion();
            sdi.ShowDialog();
            switch (sdi.opcion)
            {
                case 1:
                    imprimeConWord("");
                    // imprimeConWord("nombre_del_documento")  cuando ya hay un formato
                    break;

                case 2:
                    imprimeConExcel("", "");
                    // imprimeConExcel("nombre_del_libro", "nombre_de_la_hoja")  // cuando ya hay un formato
                    break;

                case 3:
                    imprimeEnHTML(Application.StartupPath + "\\reporte.html");
                    break;
            }


  
        }
        #region Reportes...
        private void imprimeEnHTML(string documento)
        {
            FileStream flujo = new FileStream(documento, FileMode.Create, FileAccess.Write);
            StreamWriter tw = new StreamWriter(flujo);
            string linea = "";

            tw.WriteLine("<META http-equiv='content-type' content='text/html; charset=UTF-8'>");
            tw.WriteLine("<BODY>");
            tw.WriteLine("<FONT face='Calibri'>");    // face='Comic Sans MS'>");
            tw.WriteLine("<H2>Listado de productos</H2>");
            tw.WriteLine("<BR>");

            linea = linea + "<TABLE border=2>";     //width=600     //width=80%
            linea = linea + "<TBODY>";
            linea = linea + "<TR bgColor=#f8c591>"; // bgColor=#f8c591

            //Imprimir los encabezados:
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.HeaderText != "clave")
                    linea = linea + "<TD><B>" + col.HeaderText + "</B></TD> ";
            }
            linea = linea + "</TR>";
            tw.WriteLine(linea);

            //Imprimir los datos desde el datagridview1:
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                linea = "";
                linea = linea + "<TR bgColor='LightGray'>"; // bgColor='LightGray'    // bgColor=#f8c591
                linea = linea + "<TD>" + r.Cells["codigo"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["descripcion"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["unidad"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["precio_unitario"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["existencia"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["p_reorden"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["clave_prov"].Value + "</TD> ";
                linea = linea + "</TR>";
                tw.WriteLine(linea);
            }

            linea = "";                     //Esto hace lo mismo:
            linea = linea + "</TBODY>";     // tw.WriteLine("</TBODY>");
            linea = linea + "</TABLE>";     // tw.WriteLine("</TABLE>");
            linea = linea + "</BODY>";      // tw.WriteLine("</BODY>");
            tw.WriteLine(linea);

            tw.Close();
            System.Diagnostics.Process.Start("iexplore.exe", documento);  //Abre el reporte en el Navegador.
           // System.Diagnostics.Process.Start(documento);  //Abre el reporte en el Navegador.
        }
        private void imprimeConExcel(string nom_libro, string nom_hoja)
        {
            //int x = 0;

            ////objetos auxiliares:
            //object oMissing = System.Reflection.Missing.Value;    //Objeto Null de Missing

            //{
            //    Excel.Application miExcel = new Excel.Application();
            //    Excel.Workbook libro = miExcel.Workbooks.Add(nom_libro);
            //    Excel.Worksheet hoja;

            //    if (nom_hoja == "")
            //        hoja = (Excel.Worksheet)libro.Worksheets[1];    //(Excel.Worksheet)libro.Worksheets["Hoja1"];  //funciona igual
            //    else
            //        hoja = (Excel.Worksheet)libro.Worksheets[nom_hoja];
            //    hoja.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            //    hoja.Activate();    //No te preocupes por la advertencia.

            //    hoja.Cells[2, "A"] = dataGridView1.Columns[0].HeaderText;     // O así: dataGridView1.Columns["Codigo"].HeaderText;
            //    hoja.Cells[2, "B"] = dataGridView1.Columns[1].HeaderText;
            //    hoja.Cells[2, "C"] = dataGridView1.Columns[2].HeaderText;
            //    hoja.Cells[2, "D"] = dataGridView1.Columns[3].HeaderText;     // O así: dataGridView1.Columns["Codigo"].HeaderText;
            //    hoja.Cells[2, "E"] = dataGridView1.Columns[4].HeaderText;
            //    hoja.Cells[2, "F"] = dataGridView1.Columns[5].HeaderText;
            //    hoja.Cells[2, "G"] = dataGridView1.Columns[6].HeaderText;     // O así: dataGridView1.Columns["Codigo"].HeaderText;
                


            //    Excel.Range ran = hoja.get_Range("A2", "G2");   //Así se define un rango
            //    ran.Cells.Font.Bold = 1;                        //Lo ponemos en negritas

            //    //Imprimir desde el datagridview1:
            //    foreach (DataGridViewRow r in dataGridView1.Rows)
            //    {
            //        hoja.Cells[3 + x, "A"] = r.Cells["codigo"].Value.ToString();
            //        hoja.Cells[3 + x, "B"] = r.Cells["descripcion"].Value.ToString();
            //        hoja.Cells[3 + x, "C"] = r.Cells["unidad"].Value.ToString();
            //        hoja.Cells[3 + x, "D"] = r.Cells["precio_unitario"].Value.ToString();
            //        hoja.Cells[3 + x, "E"] = r.Cells["existencia"].Value.ToString();
            //        hoja.Cells[3 + x, "F"] = r.Cells["p_reorden"].Value.ToString();
            //        hoja.Cells[3 + x, "G"] = r.Cells["clave_prov"].Value.ToString();
            //        x++;
            //    }

            //    hoja.Columns.AutoFit(); //Ajusta el tamaño de las columnas
            //    miExcel.Visible = true;
            //    MessageBox.Show("Se ha generado un reporte en Excel.\n\nAl presionar Aceptar, Excel intentará cerrarse automáticamente.",
            //         "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    try
            //    {
            //        hoja = null;
            //        libro.Close(false, hoja, null);
            //        libro = null;
            //        miExcel.Quit();
            //        miExcel = null;
            //    }
            //    catch { }
            //}
        }
        private void imprimeConWord(string documento)
        {
            //string nl = "\n";   //Nueva-linea

            ////objetos auxiliares:
            //object oMissing = System.Reflection.Missing.Value;    //Objeto Null de Missing
            //object oTrue = true;
            //object oFalse = false;
            //object oTemplatePath;
            //string linea = "";

            //if (documento == "")
            //{   //No se indicó documento. Se abrirá un nuevo documento blanco de la plantilla 
            //    //por default de Word:
            //    oTemplatePath = oMissing;
            //}
            //else
            //{   //La ruta al documento que se quiere abrir/guardar:
            //    oTemplatePath = Application.StartupPath + documento;
            //}

            ////Objetos Word y Documento:
            //Word.Application miWord = new Word.Application();
            //Word.Document miDoc = miWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oTrue);
            ////checar: http://msdn.microsoft.com/es-es/library/microsoft.office.interop.word.documents.add(office.11).aspx

            ////miWord.Visible = true;      //Hacer visible el Word

            //miDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

            ////Formateo de los párrafos a líneas sencillas:
            ////miWord.Selection.ParagraphFormat.LineSpacing = 12;
            //miWord.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            //miWord.Selection.ParagraphFormat.SpaceBefore = 0;
            //miWord.Selection.ParagraphFormat.SpaceAfter = 0;

            ////miDoc.Paragraphs.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;

            ////Alineación del parrafo a la derecha:
            //miWord.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            //miWord.Selection.TypeText("Fecha: " + DateTime.Now.ToLongDateString() + nl + nl);

            ////Alineación del parrafo centrado:
            //miWord.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //miWord.Selection.Font.Name = "Calibri";
            //miWord.Selection.Font.Size = 14;
            //miWord.Selection.Font.Bold = 1;     //Activa negrita
            //miWord.Selection.TypeText("Listado de Productos" + nl + nl);

            ////Alineación del parrafo izq.:
            //miWord.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            //miWord.Selection.Font.Name = "Courier New";
            //miWord.Selection.Font.Size = 7;
            //miWord.Selection.Font.Bold = 1;     //Activa negrita

            ////Imprimir los encabezados:
            //miWord.Selection.TypeText("   codigo      descripcion        unidad        precio unitario        existencia            preorden             clave_prov " + nl);
            //miWord.Selection.TypeText("  ---------   -----------------   ---------       -------------        ----------             ---------             -----------" + nl);

            //miWord.Selection.Font.Bold = 0;     //Desactiva negrita
            ////Imprimir desde el datagridview1:
            //foreach (DataGridViewRow r in dataGridView1.Rows)
            //{
            //    linea = r.Cells["codigo"].Value.ToString().PadLeft(10) + " " +
            //        r.Cells["descripcion"].Value.ToString().PadRight(20) + " " +
            //         r.Cells["unidad"].Value.ToString().PadRight(20) + " " +
            //          r.Cells["precio_unitario"].Value.ToString().PadRight(20) + " " +
            //           r.Cells["existencia"].Value.ToString().PadRight(20) +" " +
            //            r.Cells["p_reorden"].Value.ToString().PadRight(20) + " " +
            //        r.Cells["clave_prov"].Value.ToString() + nl;
               
                   


            //    miWord.Selection.TypeText(linea);
            //}

            //miWord.Visible = true;      //Hacer visible el Word
            //miDoc = null;
            //miWord = null;
        }
        #endregion

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            codigo = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            this.Close();
        }
    
    
    }
}
