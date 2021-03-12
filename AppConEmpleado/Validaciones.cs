using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppConEmpleado
{
    class Validaciones
    {
        public bool Vacio(string texto)
        {
            if (texto.Equals(""))
            {
                Console.WriteLine(" La entrada no puede ser VACIO");
                return true;
            }
            else
                return false;
        }

        public bool TipoNumero(string texto)
        {
            Regex regla = new Regex("[0-9]{1,9}(\\.[0-9]{0,2})?$");

            if (regla.IsMatch(texto))
                return true;
            else
            {
                Console.WriteLine(" La entrada no debe ser NUMERICA");
                return false;
            }
        }

        public bool TipoTexto(string texto)
        {
            Regex regla = new Regex("^[a-zA-Z ]*$");

            if (regla.IsMatch(texto))
                return true;
            else
            {
                Console.WriteLine(" La entrada no debe ser SOLO TEXTO");
                return false;
            }
        }

        public bool SiNo(string texto)
        {
            texto.ToLower(); // convertimos la entra en minuscula 

            if (texto.Equals("s") || texto.Equals("n"))
                return true;
            else
            {
                Console.WriteLine(" La entrada deve ser S o N ");
                return false;
            }
        }

    }
}
