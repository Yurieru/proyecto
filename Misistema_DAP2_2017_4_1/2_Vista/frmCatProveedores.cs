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
    public partial class frmCatProveedores : Form
    {
        ControlProveedores ctrl_proveedores = new ControlProveedores(Frame.config);
        public string clave_prov = "";
        public string razon_social = "";
        DataSet ds;
        public frmCatProveedores()
        {
            InitializeComponent();
            prepararForma();
        }

        public frmCatProveedores(bool seleccionar) //constructor alterno
        {
            InitializeComponent();

            if (seleccionar) btnseleccionar.Visible = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            prepararForma();

        }
        private void prepararForma()
        {
            if (txtBuscar.Text.Trim() == "")
            {
                ds = ctrl_proveedores.LeerProveedores();
            }
            else
            {
                ds = ctrl_proveedores.LeerProveedores(txtBuscar.Text);
            }

            dataGridView1.DataSource = ds.Tables[0];

            Font negrita = new Font(this.Font, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = negrita;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ds = null;
        }



        private void frmCatProveedores_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmUpdateProveedores agregar = new frmUpdateProveedores();
            agregar.ShowDialog();
            if (agregar.HuboCambio)
            {
                prepararForma();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            string dato_id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            frmUpdateProveedores modif = new frmUpdateProveedores(dato_id);
            modif.ShowDialog();
            if (modif.HuboCambio)
            {
                prepararForma(); //Si hay cambios, actualiza el datagridview1
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            DialogResult resp;
            string dato_id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();



            resp = MessageBox.Show("¿Seguro(a) desea eliminar el registro?" + "\n\n" + dato_id + "\n" + nombre, "ATENCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                string error = "";
                if (ctrl_proveedores.eliminarProveedor(dato_id, ref error))
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            prepararForma();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
            tw.WriteLine("<H2>Listado de proveedores</H2>");
            tw.WriteLine("<BR>");

            linea = linea + "<TABLE border=2>";     //width=600     //width=80%
            linea = linea + "<TBODY>";
            linea = linea + "<TR bgColor=#f8c591>"; // bgColor=#f8c591

            //Imprimir los encabezados:
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.HeaderText != "")
                    linea = linea + "<TD><B>" + col.HeaderText + "</B></TD> ";
            }
            linea = linea + "</TR>";
            tw.WriteLine(linea);

            //Imprimir los datos desde el datagridview1:
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                linea = "";
                linea = linea + "<TR bgColor='LightGray'>"; // bgColor='LightGray'    // bgColor=#f8c591
                linea = linea + "<TD>" + r.Cells["clave"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["razon_social"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["rfc"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["telefono"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["calle_y_num"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["entre_calles"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["colonia"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["codigo_postal"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["ciudad"].Value + "</TD> ";
                linea = linea + "<TD>" + r.Cells["estado"].Value + "</TD> ";
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
            //    hoja.Cells[2, "G"] = dataGridView1.Columns[6].HeaderText;
            //    hoja.Cells[2, "H"] = dataGridView1.Columns[7].HeaderText;
            //    hoja.Cells[2, "I"] = dataGridView1.Columns[8].HeaderText;
            //    hoja.Cells[2, "J"] = dataGridView1.Columns[9].HeaderText;// O así: dataGridView1.Columns["Codigo"].HeaderText;



            //    Excel.Range ran = hoja.get_Range("A2", "J2");   //Así se define un rango
            //    ran.Cells.Font.Bold = 1;                        //Lo ponemos en negritas

            //    //Imprimir desde el datagridview1:
            //    foreach (DataGridViewRow r in dataGridView1.Rows)
            //    {
            //        hoja.Cells[3 + x, "A"] = r.Cells["clave"].Value.ToString();
            //        hoja.Cells[3 + x, "B"] = r.Cells["razon_social"].Value.ToString();
            //        hoja.Cells[3 + x, "C"] = r.Cells["rfc"].Value.ToString();
            //        hoja.Cells[3 + x, "D"] = r.Cells["telefono"].Value.ToString();
            //        hoja.Cells[3 + x, "E"] = r.Cells["calle_y_num"].Value.ToString();
            //        hoja.Cells[3 + x, "F"] = r.Cells["entre_calles"].Value.ToString();
            //        hoja.Cells[3 + x, "G"] = r.Cells["colonia"].Value.ToString();
            //        hoja.Cells[3 + x, "H"] = r.Cells["codigo_postal"].Value.ToString();
            //        hoja.Cells[3 + x, "I"] = r.Cells["ciudad"].Value.ToString();
            //        hoja.Cells[3 + x, "J"] = r.Cells["estado"].Value.ToString();
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
            //miWord.Selection.TypeText("Listado de proveedores" + nl + nl);

            ////Alineación del parrafo izq.:
            //miWord.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            //miWord.Selection.Font.Name = "Courier New";
            //miWord.Selection.Font.Size = 7;
            //miWord.Selection.Font.Bold = 1;     //Activa negrita

            ////Imprimir los encabezados:
            //miWord.Selection.TypeText("clave        razon social             rfc                telefono                 colonia          codigo postal         ciudad              estado" + nl);
            //miWord.Selection.TypeText("---------- -------------------- -------------------- --------------------  -------------------- -------------------- -------------------- --------------------" + nl);
             
            //miWord.Selection.Font.Bold = 0;     //Desactiva negrita
            ////Imprimir desde el datagridview1:
            //foreach (DataGridViewRow r in dataGridView1.Rows)
            //{
            //    linea = r.Cells["clave"].Value.ToString().PadRight(10).Substring(0,10) + " " +
            //        r.Cells["razon_social"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //         r.Cells["rfc"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //          r.Cells["telefono"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //           //r.Cells["calle_y_num"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //          //  r.Cells["entre_calles"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //             r.Cells["colonia"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //              r.Cells["codigo_postal"].Value.ToString().PadLeft(20).Substring(0,20) + " " +
            //               r.Cells["ciudad"].Value.ToString().PadRight(20).Substring(0, 20) + " " +
            //        r.Cells["estado"].Value.ToString() + nl;




            //    miWord.Selection.TypeText(linea);
            //}

            //miWord.Visible = true;      //Hacer visible el Word
            //miDoc = null;
            //miWord = null;
        }
        #endregion

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            clave_prov = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            razon_social = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
