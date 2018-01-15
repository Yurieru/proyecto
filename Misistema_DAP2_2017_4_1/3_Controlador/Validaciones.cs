/* 
 * namespace Validaciones
 * 
 * Autor: Prof. José Padilla Duarte
 * email: jopadu@gmail.com
 * Última modificación: 15-Agosto-2014
 * 
 * Referencias: 
 * http://www.entrecodigos.net/2012/08/c-validacion-con-expresiones-regulares/
 * http://social.msdn.microsoft.com/Forums/es-ES/2cdabac1-c439-4191-b919-8fbfff97a2db/aporte-usando-expresiones-regulares-para-validar-datoscorreo-electrnicosolo-letrassolo?forum=vcses
*/

using System.Text.RegularExpressions;

namespace Validaciones
{
    public class Validar
    {
        public static bool soloLetras(string _cad)
        {   //Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            Regex regex = new Regex(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$");
            return regex.IsMatch(_cad);
        }

        public static bool soloDigitos(string _cad)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(_cad);
        }


        public static bool RFC(string _cad)
        {   //Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            Regex regex = new Regex(@"^[A-Z0-9]+$");
            return regex.IsMatch(_cad);
        }

        public static bool es_telefono(string _cad)
        {
            Regex regex = new Regex(@"^([0-9]{3})[-. ]?([0-9]{4})$");
            return regex.IsMatch(_cad);
        }

        public static bool es_decimal(string _cad)
        {
            Regex regex = new Regex(@"^[0-9]{1,9}([\.\,][0-9]{1,3})?$");
            return regex.IsMatch(_cad);
        }

        public static bool es_url(string _cad)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$");
            return regex.IsMatch(_cad);
        }

        public static bool es_email(string _cad)
        {
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            // Resultado: 
            //       Valid: david.jones@proseware.com 
            //       Valid: d.j@server1.proseware.com 
            //       Valid: jones@ms1.proseware.com 
            //       Invalid: j.@server1.proseware.com 
            //       Invalid: j@proseware.com9 
            //       Valid: js#internal@proseware.com 
            //       Valid: j_9@[129.126.118.1] 
            //       Invalid: j..s@proseware.com 
            //       Invalid: js*@proseware.com 
            //       Invalid: js@proseware..com 
            //       Invalid: js@proseware.com9 
            //       Valid: j.s@server1.proseware.com

            return regex.IsMatch(_cad);
        }
    }
}


/*
A continuacion les enlistaré varias expresiones Regulares que pueden usar para validar datos:

Validar una URL:

/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \?=.-]*)*\/?$/  

Validar un Email:

\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)* 

Validar un numero de Telefono:

^[+-]?\d+(\.\d+)?$

Validar una tarjeta de credito

^((67\d{2})|(4\d{3})|(5[1-5]\d{2})|(6011))(-?\s?\d{4}){3}|(3[4,7])\ d{2}-?\s?\d{6}-?\s?\d{5}$  

Validar un codigo postal

^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$  

Validar un Nombre

[a-zA-ZñÑ\s]{2,50}

Validar Domicilio:

^.*(?=.*[0-9])(?=.*[a-zA-ZñÑ\s]).*$

Validar IFE

^.*(?=.{13})[+-]?\d+(\.\d+)?$

Validar CURP

^.*(?=.{18})(?=.*[0-9])(?=.*[A-ZÑ]).*$

Solo Numeros

[0-9]{1,9}(\.[0-9]{0,2})?$

Solo Letras

[a-zA-ZñÑ\s]

Solo tienen que poner la expresion donde dice Exp:

Regex Val = new Regex(@"Exp");
*/