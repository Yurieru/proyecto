/* 
 * namespace Controlador
 * 
 * Autor: Prof. José Padilla Duarte
 * email: jopadu@gmail.com
 * Última modificación: 03-Octubre-2017
 * 
 * Clase ControlBD y demás controladores
 * =====================================
 * 
 * ControlBD define los atributos mínimos obligatorios para cada clase tipo Control que se encuentra
 * en este archivo de código.
 * 
 * Propósito: Clases intermediarias entre la Vista (formas/ventanas) y el Modelo (manejo de archivos y 
 *            comunicación con el DBMS).
 */

using System;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using BD_Comm_MySQL;    //Para acceder a las clases que hay en el Modelo

namespace Controlador
{
    #region public class ControlConfig
    /// <summary>
    /// Define el DBMS y la cadena_de_conexión que tendrá la aplicación.
    /// </summary>
    public class ControlConfig
    {   //Atributos:
        private string _DBMS;       // El motor de base de datos 
        private string _cadconn;    // La cadena de conexión a la base de datos

        public string DBMS { get { return _DBMS; } }        //Propiedad de solo lectura
        public string cadconn { get { return _cadconn; } }  //Propiedad de solo lectura


        //Métodos:
        public ControlConfig(string DBMS, string cadconn)
        {
            this._DBMS = DBMS;
            this._cadconn = cadconn;
        }
    }
    #endregion


    public abstract class ControlBD
    {   //Atributos:
        protected BDMySQL bd;   // Objeto base de datos
        protected string iSql;

        //Métodos:
        public ControlBD()  // Constructor vacío (requisito de la herencia).
        { }

    }

    #region public class ControlEmpleados : ControlBD
    public class ControlEmpleados : ControlBD
    {

        public ControlEmpleados(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                            // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
        }


        /// <summary>
        /// Retorna true si el usuario y la contraseña son correctos, false si algún dato no es correcto.
        /// </summary>
        /// <param name="_usu">El usuario</param>
        /// <param name="_cve">La clave, contraseña, password, etc.</param>
        /// <returns>true/false</returns>
        public bool validarUsuario(string _usu, string _cve)
        {
            string error = "";

            DataRow dr = bd.Leer1Registro("SELECT * FROM usuarios WHERE usuario = '" + _usu + "';", ref error);
            if (error.Contains("Unable to connect"))
            {
                new frmMensaje("No se detecta servidor de MySQL.", 2000, 2).ShowDialog();
                Application.Exit();
                return false;
            }
            if (dr == null)
            {
                if (error.Contains("Authentication"))
                {
                    new frmMensaje("Error de Autenticación. Revise la cadena de conexión.", 2500, 2).ShowDialog();
                    return false;
                }
                else
                {
                    new frmMensaje("El usuario no está registrado.", 2000, 2).ShowDialog();
                    return false;
                }
            }
            else if (error.Contains("No hay ninguna fila"))
            {
                new frmMensaje("El usuario no está registrado.", 2000, 2).ShowDialog();
                return false;
            }
            else
            {
                // if (_cve == dr["clave"].ToString())

                if (obtieneMD5(_cve) == dr["clave"].ToString())
                {
                    new frmMensaje("Bienvenido(a) " + _usu, 500).ShowDialog();
                    return true;
                }
                else
                {
                    new frmMensaje("Contraseña no válida.", 2000, 2).ShowDialog();
                    return false;
                }
                //return true;
            }
        }

        public DataSet LeerEmpleados()
        {
            return (bd.LeerRegistros("SELECT usuario,nivel FROM usuarios where estado = '1' ORDER BY usuario;"));
        }

        public DataRow localizarEmpleado(string _usr)
        {
            iSql = "SELECT * FROM usuarios WHERE usuario = '" + _usr + "'; ";
            return (bd.Leer1Registro(iSql));
        }
        private string obtieneMD5(string _txt)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(_txt));
            string result = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return result;
        }
        public bool InsertarEmpleado(object[] _datos)
        {

            _datos[1] = obtieneMD5(_datos[1].ToString());
            ////return (bd.InsertarRegistro("empleados", _datos));
            return (bd.InsertarRegistro("usuarios", _datos));
        }

        public bool modificarEmpleado(string _tbl, string[] _campos, object[] _datos, string _key, object _valorkey)
        {

            return (bd.ModificarRegistro(_tbl, _campos, _datos, _key, _valorkey));
        }


        public bool eliminarEmpleado(string _usr)
        {
            return (bd.EjecutarSQL("DELETE FROM usuarios WHERE usuario = '" + _usr + "'; "));
        }
        public bool eliminarProveedor(string _prov, ref string _error)
        {
            return (bd.EjecutarSQL("DELETE FROM proveedores WHERE id_prov = '" + _prov + "';", ref _error));
            //if(_error.Contains == "")
        }

        public DataSet LeerEmpleados(string _dato_a_buscar)
        {
            return (bd.LeerRegistros("SELECT usuario,nivel FROM usuarios WHERE usuario LIKE ('" + _dato_a_buscar + "%')ORDER BY usuario"));
        }

        public bool modificarEmpleadoNuevaClave(string _tbl, string[] _campos, object[] _datos, string _key, object _valorkey)
        {

            _datos[1] = obtieneMD5(_datos[1].ToString());
            return (bd.ModificarRegistro(_tbl, _campos, _datos, _key, _valorkey));

        }

    }
    #endregion


    #region public class ControlProductos : ControlBD
    public class ControlProductos : ControlBD
    {
        public ControlProductos(ControlConfig _cfg)
        {
            bd = new BDMySQL(_cfg.cadconn);
        }

        public DataSet leerProductos()
        {
            iSql = "SELECT * FROM productos ORDER BY id_prod;";
            return (bd.LeerRegistros(iSql));
        }

        public DataSet leerProductos(string _dato_a_buscar)
        {
            iSql = "SELECT * FROM productos WHERE producto LIKE('" + _dato_a_buscar + "%') ORDER BY id_prod;";
            return (bd.LeerRegistros(iSql));
        }

        public int sigCodigoProducto()
        {
            int sgte = Convert.ToInt32(bd.LeerNumerico("SELECT MAX(id_prod) FROM productos;")) + 1;
            return (sgte);
        }

        public DataRow localizarProducto(int _codigo)
        {
            DataRow dr = bd.Leer1Registro("SELECT * FROM productos WHERE id_prod = " + _codigo + ";");
            return (dr);
        }

        public DataRow localizarProducto(int _codigo, ref string _error)
        {
            DataRow dr = bd.Leer1Registro("SELECT * FROM productos WHERE id_prod = " + _codigo + ";", ref _error);
            return (dr);
        }

        public bool insertarProducto(object[] _datos)
        {
            return (bd.InsertarRegistro("productos", _datos));
        }

        public bool modificarProducto(string[] _campos, object[] _datos, string _key, object _valkey)
        {
            return (bd.ModificarRegistro("productos", _campos, _datos, _key, _valkey));
        }

        public bool eliminarProducto(int _codigo)
        {
            return (bd.EjecutarSQL("DELETE FROM productos WHERE id_prod = '" + _codigo + "';"));
        }

        public bool eliminarProducto(int _codigo, ref string _error)
        {
            return (bd.EjecutarSQL("DELETE FROM productos WHERE id_prod = '" + _codigo + "';", ref _error));
            //if(_error.Contains == "")
        }
        public DataSet clavesDeProveedores(ref ComboBox _cbo)
        {
            DataSet ds = bd.LeerRegistros("SELECT id_prov,proveedor FROM proveedores WHERE status='alta' ORDER BY id_prov;");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                _cbo.Items.Add(dr[0].ToString());
                //_cbo.ValueMember = dr[0].ToString();
                //_cbo.DisplayMember = dr[1].ToString();

            }
            return (ds);
        }

        public DataTable llenarComboProv(ref ComboBox combo)
        {
            DataTable dt = bd.LeerRegistross("SELECT id_prov,proveedor FROM proveedores WHERE status='alta' ORDER BY id_prov;");
            foreach (DataRow dr in dt.Rows)
            {
                combo.ValueMember = dr[0].ToString();
                combo.DisplayMember = dr[1].ToString();
                combo.DataSource = dt;
            }
            return (dt);
        }
    }
    #endregion


    #region public class ControlClientes : ControlBD
    public class ControlClientes : ControlBD
    {
        public ControlClientes(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                            // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
        }


        public DataSet LeerClientes()
        {
            return (bd.LeerRegistros("SELECT * FROM clientes ORDER BY nombre"));
        }

        public DataSet LeerClientes(string _dato_a_buscar)
        {
            iSql = "SELECT * FROM clientes WHERE apellidos LIKE('%" + _dato_a_buscar + "%') ORDER BY nombre;";
            return (bd.LeerRegistros(iSql));
        }

        public DataRow LocalizarClientes(string _nom)
        {
            iSql = "SELECT * FROM clientes WHERE clave = '" + _nom + "'; ";
            return (bd.Leer1Registro(iSql));
        }

        public bool insertarClientes(object[] _datos)
        {
            //_datos[1] = obtieneMD5(_datos[1].ToString()); //aplica cifrado md5 a la clave
            return (bd.InsertarRegistro("clientes", _datos));
        }

        public bool actualizarClientes(string[] _campos, object[] _datos, string _key, object _valkey)
        {
            return (bd.ModificarRegistro("clientes", _campos, _datos, _key, _valkey));
            // UPDATE empleados SET usuario="jopadu", clave "123" ..... WHERE usuario ="jose";
        }

        public bool eliminarClientes(string _usr)
        {
            return (bd.EjecutarSQL("DELETE FROM clientes WHERE clave = '" + _usr + "';"));
        }

        public bool eliminarClientes(string _usr, ref string _error)
        {
            return (bd.EjecutarSQL("DELETE FROM clientes WHERE clave = '" + _usr + "';", ref _error));
            //if(_error.Contains == "")

        }

        public string sugerirClave(string _nom, string _ape)
        {
            int c = 1;
            string clavesug;
            do
            {
                clavesug = _nom.Substring(0, 2) + _ape.Substring(0, 2) + c.ToString("D2");
                clavesug = clavesug.ToUpper();
                DataRow dr = localizarClienteParaVenta(clavesug);
                if (dr == null)
                    return clavesug;
                else
                    c++;
            } while (true);
        }

        public DataRow localizarClienteParaVenta(string _clave)
        {
            string error = "";
            DataRow dr = bd.Leer1Registro("SELECT *FROM clientes WHERE clave = '" + _clave + "';", ref error);
            return (dr);
        }


    }
    #endregion

    #region  public class ControlProveedores : ControlBD
    public class ControlProveedores : ControlBD
    {
        public ControlProveedores(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                            // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
        }

        public DataSet LeerProveedores()
        {
            //return (bd.LeerRegistros("SELECT * FROM proveedores ORDER BY clave"));
            return (bd.LeerRegistros("SELECT * FROM proveedores ORDER By id_prov"));
        }

        public DataSet LeerProveedores(string _dato_a_buscar)
        {
            //iSql = "SELECT * FROM proveedores WHERE razon_social LIKE('" + _dato_a_buscar + "%') ORDER BY clave;";
            iSql = "SELECT * FROM proveedores WHERE proveedor LIKE('" + _dato_a_buscar + "%') ORDER BY id_prov;";
            return (bd.LeerRegistros(iSql));
        }

        public DataRow LocalizarProveedor(string _clave)
        {
            iSql = "SELECT * FROM proveedores WHERE proveedor = '" + _clave + "'; ";
            return (bd.Leer1Registro(iSql));
        }

        public bool insertarProveedor(object[] _datos)
        {
            //_datos[1] = obtieneMD5(_datos[1].ToString()); //aplica cifrado md5 a la clave
            return (bd.InsertarRegistro("proveedores", _datos));
        }

        public bool actualizarProveedor(string[] _campos, object[] _datos, string _key, object _valkey)
        {
            return (bd.ModificarRegistro("proveedores", _campos, _datos, _key, _valkey));
            // UPDATE empleados SET usuario="jopadu", clave "123" ..... WHERE usuario ="jose";
        }

        public bool eliminarProveedor(string _clave)
        {
            return (bd.EjecutarSQL("DELETE FROM proveedores WHERE proveedor = '" + _clave + "';"));
        }
        public bool eliminarProveedor(string _prov, ref string _error)
        {
            return (bd.EjecutarSQL("DELETE FROM proveedores WHERE proveedor = '" + _prov + "';", ref _error));
            //if(_error.Contains == "")
        }
    }

    #endregion

    #region public class ControlDatosEmpresa : ControlBD
    public class ControlDatosEmpresa : ControlBD
    {
        public ControlDatosEmpresa(ControlConfig _cfg)
        {
            bd = new BDMySQL(_cfg.cadconn);
            iSql = "SELECT * FROM datos_empresa;";
        }
        public DataRow leerDatosEmpresa(ref string _error)
        {
            return (bd.Leer1Registro(iSql, ref _error));

        }
        public bool InsertarDatosEmpresa(object[] _campos)
        {
            return (bd.InsertarRegistro("datos_empresa", _campos));
        }
        public bool modificarDatosEmpresa(string[] _campos, object[] _datos, string _key, object _valkey)
        {
            return (bd.ModificarRegistro("datos_empresa", _campos, _datos, _key, _valkey));
        }
    }




    #endregion

    #region public class ControlVenta : ControlBD
    public class ControlVenta : ControlBD
    {
        ControlConfig config;

        public ControlVenta(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                           // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
            config = _cfg;
        }

        public double leerSiguiente()
        {
            double consecutivo = bd.LeerNumerico("SELECT MAX(consecutivo) FROM MVentas;");
            consecutivo++;
            return (consecutivo);
        }

        public bool inicializaTempDVentas()
        {
            switch (config.DBMS.ToUpper())
            {
                case "ACCESS":
                    return (bd.EjecutarSQL("DELETE FROM TempDVentas;"));
                case "MYSQL":
                    return (bd.EjecutarSQL("TRUNCATE TempDVentas;"));
                default:
                    return false;
            }
        }

        public DataSet leerTempDVentas()
        {
            return (bd.LeerRegistros("SELECT TempDVentas.Consecutivo, TempDVentas.Codigo, productos.Descripcion, " +
                    "TempDVentas.Cantidad, TempDVentas.Precio_unitario, TempDVentas.Importe " +
                    "FROM TempDVentas, productos WHERE TempDVentas.codigo = productos.codigo;"));
        }

        public bool agregaProductoATempDVentasOld(object[] _campos, object[] _datos)
        {
            string error = "";
            DataRow dr = bd.Leer1Registro("SELECT * FROM TempDVentas WHERE Codigo=" + _datos[1] + "; ", ref error);
            if (dr == null)
                return (bd.InsertarRegistro("TempDVentas", _datos));
            else
            {
                return (bd.EjecutarSQL("UPDATE TempDVentas SET Cantidad = Cantidad + " + _datos[2] +
                                       ", Importe = Importe + " + _datos[4] + " WHERE Codigo = " + _datos[1] + "; "));
            }
        }

        public bool agregaProductoATempDVentas(int _codigo, int _cantidad, int _consecutivo)
        {
            int exist_en_tbl_productos = 0;
            string error = "";
            DataRow dr = bd.Leer1Registro("SELECT * FROM productos WHERE codigo=" + _codigo + "; ", ref error);
            if (dr == null)
            {
                new frmMensaje("Código de Producto no registrado: " + _codigo, 1500, 2).ShowDialog();
                //Console.Beep(440, 50);
                return false;
            }
            else
            {
                exist_en_tbl_productos = Convert.ToInt32(dr["existencia"].ToString());
                if (exist_en_tbl_productos < _cantidad)
                {
                    new frmMensaje("No hay existencia suficiente del producto seleccionado.\n" +
                        "Existencia actual: " + exist_en_tbl_productos, 2000, 2).ShowDialog();
                    return false;

                }
                double importe = _cantidad * Convert.ToDouble(dr["precio_unitario"].ToString());
                object[] campos = { "Consecutivo", "Codigo", "Cantidad", "Precio_unitario", "Importe" };
                object[] datos = {_consecutivo, dr["codigo"].ToString(), _cantidad,
                                      dr["precio_unitario"].ToString(), importe.ToString() };
                //error = "";
                dr = bd.Leer1Registro("SELECT * FROM TempDVentas WHERE Codigo=" + datos[1] + "; ", ref error);
                if (dr == null)
                    return (bd.InsertarRegistro("TempDVentas", datos));
                else
                {
                    int cant_en_TempDVentas = Convert.ToInt32(dr["Cantidad"].ToString());
                    if (exist_en_tbl_productos < (cant_en_TempDVentas + _cantidad))
                    {
                        new frmMensaje("No hay existencia suficiente del producto seleccionado.\n" +
                            "Existencia actual: " + exist_en_tbl_productos, 2000, 2).ShowDialog();
                        return false;
                    }
                    return (bd.EjecutarSQL("UPDATE TempDVentas SET Cantidad = Cantidad + " + datos[2] +
                            ", Importe = Importe + " + datos[4] + " WHERE Codigo = " + datos[1] + "; "));
                }
            }
        }

        public double sumaImportesTempDVentas()
        {
            return (bd.LeerNumerico("SELECT SUM(Importe) FROM TempDVentas;"));
        }

        public int cuentaRegsTempDVentas()
        {
            return (Convert.ToInt32(bd.LeerNumerico("SELECT COUNT(*) FROM TempDVentas;")));
        }

        public bool insertarVenta(object[] _datos)
        {
            if (bd.InsertarRegistro("MVentas", _datos))
                if (bd.EjecutarSQL("INSERT INTO DVentas SELECT * FROM TempDVentas;"))
                    //Actualiza Existencias en tabla productos:
                    if (bd.EjecutarSQL("UPDATE productos,TempDVentas SET existencia = (existencia - Cantidad) " +
                                    "WHERE productos.codigo = TempDVentas.Codigo;"))
                        return true;
                    else
                        return false;
                else
                    return false;
            else
                return false;
        }

        public DataTable leerVentas()
        {
            return (bd.LeerTabla("MVentas"));
        }

        public DataSet leerDVentas(int _cons)
        {
            return (bd.LeerRegistros("SELECT DVentas.Consecutivo, DVentas.Codigo, productos.Descripcion, " +
                    "DVentas.Cantidad, DVentas.Precio_unitario, DVentas.Importe " +
                    "FROM DVentas, productos WHERE DVentas.consecutivo = " + _cons + " AND " +
                    "productos.codigo = DVentas.codigo;"));
        }
    }
    #endregion Venta

    #region public class ControlParametros : ControlBD
    public class ControlParametros : ControlBD
    {
        //private string iSql = "SELECT * FROM parametros;";

        public ControlParametros(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                             // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
            iSql = "SELECT * FROM parametros;";
        }

        public DataRow leerParametros(ref string str_err)
        {
            return (bd.Leer1Registro(iSql, ref str_err));
        }

        public bool modificarParametros(int iva, string folio_vta)
        {
            return (bd.EjecutarSQL("UPDATE parametros SET iva=" + iva + ", folio_venta=" + folio_vta + ";"));
        }
    }
}

#endregion

