using System;
using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;

namespace AppConEmpleado
{
    class Program
    {
        static List<Empleado> ListaEmpleados = new List<Empleado>(); // instanciar la clase para crear el objeto (ListaEmpleados)
        static Validaciones Validar = new Validaciones();
        static void Main(string[] args)
        {
            int OpcionMenu;
            string temporal;
            bool EntradaValida = false;

            do
            {
                Console.Clear();
                Console.WriteLine("*** App Empleados ***");
                Console.WriteLine("-- Menu Principal ---");
                Console.WriteLine("1. Crear un Empleado");
                Console.WriteLine("2. Listar Empleados");
                Console.WriteLine("3. Buscar un Empleado");
                Console.WriteLine("-------------------");
                Console.WriteLine("8. Guardar Archivo");
                Console.WriteLine("9. Cargar Archivo");
                Console.WriteLine("-------------------");
                Console.WriteLine("0. Salir del Sistema");
                Console.WriteLine("-------------------");


                do
                {
                    Console.WriteLine("Digite una opcion");
                    temporal = Console.ReadLine();
                    if (!Validar.Vacio(temporal))
                        if (Validar.TipoNumero(temporal))
                            EntradaValida = true;

                } while (!EntradaValida);
                


                OpcionMenu = Convert.ToInt32(temporal);


                switch (OpcionMenu)
                {
                    case 1:
                        CrearEmpleado();
                        break;
                    case 2:
                        ListarEmpleados();
                        break;
                    case 3:
                        BuscarEmpleado();
                        break;
                    case 0:
                        Console.WriteLine(" ... saliendo de la aplicacion ");
                        break;
                    case 8:
                        EscribirXml();
                        break;
                    case 9:
                        LeerXml();
                        break;
                    default:
                        Console.WriteLine("  Opcion No Valida");
                        break;
                }
                Console.WriteLine(" ... .............. presiones cualquier tecla para continuar ");
                Console.ReadKey();
            } while (OpcionMenu > 0);

        }

        //------------ nuestros metodos
         static void CrearEmpleado()
        {
            string cod, nom, sal, caj;
            bool CodigoValido = false;
            bool NombreValido = false;
            bool SalarioValido = false;
            bool CajaValido = false;


            Console.Clear();
            Console.WriteLine(" ...... crear empleado");
            Console.WriteLine(" ......................");

            do {
                Console.Write(" Digite Codigo Empleado: ");
                cod = Console.ReadLine();
                if (!Validar.Vacio(cod))
                    if (Validar.TipoNumero(cod))
                        CodigoValido = true;
            } while (!CodigoValido);

            //----------------------------------

            if (Existe(Convert.ToInt32(cod)))
                Console.WriteLine("El usuario " + cod + " Ya existe en el sistema");
            else
            {
                // -----------------------------------------------  inicia el else
                do
                {
                    Console.Write(" Digite Nombre Empleado: ");
                    nom = Console.ReadLine();
                    if (!Validar.Vacio(nom))
                        if (Validar.TipoTexto(nom))
                            NombreValido = true;
                } while (!NombreValido);

                do
                {
                    Console.Write("  Digite Sueldo del Empleado: ");
                    sal = Console.ReadLine();
                    if (!Validar.Vacio(sal))
                        if (Validar.TipoNumero(sal))
                            SalarioValido = true;
                } while (!SalarioValido);


                do
                {
                    Console.Write(" Tiene caja compensacion S/N: ");
                    caj = Console.ReadLine();
                    if (!Validar.Vacio(caj))
                        if (Validar.SiNo(caj))
                            CajaValido = true;
                } while (!CajaValido);

                //******** probar a aca la vallidacion si existe


                // crear el objeto myEmpleado intanciando la clase Empleado
                Empleado myEmpleado = new Empleado();
                myEmpleado.Codigo = Convert.ToInt32(cod);
                myEmpleado.Nombre = nom;
                myEmpleado.Sueldo = Convert.ToInt32(sal);
                if (caj == "s")
                    myEmpleado.Caja = true;
                else
                    myEmpleado.Caja = false;
                //------------------------------------------------------------------

                //---------------------------------

                ListaEmpleados.Add(myEmpleado);

                // -----------------------------------------------  termina el else
            }

        }

        static void ListarEmpleados()
        {
            decimal salud = 0;
            decimal pension = 0;
            decimal arl = 0;
            decimal cajaCompensacion = 0;
            decimal totalParafiscales = 0;
            int y = 20;

            Console.WriteLine(" ...... lista de empleados");
            Console.WriteLine(" ......................");

            Console.SetCursorPosition(5, y);Console.Write("Codigo");
            Console.SetCursorPosition(15, y); Console.Write("Nombre");
            Console.SetCursorPosition(35, y); Console.Write("Salario");
            Console.SetCursorPosition(50, y); Console.Write("Caja");
            Console.SetCursorPosition(65, y); Console.Write("Base Cotizacion");
            Console.SetCursorPosition(80, y); Console.Write("Salud");
            Console.SetCursorPosition(95, y); Console.Write("Pension");
            Console.SetCursorPosition(105, y); Console.Write("Arl");
            Console.SetCursorPosition(120, y); Console.Write("Caja Compensacion");
            Console.SetCursorPosition(135, y); Console.Write("Total A Pagar");
            Console.Write("\n");

           
            foreach (Empleado itemEmpleado in ListaEmpleados)
            {
                y++;
                //---------------------------------------- aca vamos a hacer nuetros calculos
                decimal baseCotizacion = itemEmpleado.Sueldo * Convert.ToDecimal(0.4);
                if (baseCotizacion <= 908000)
                    baseCotizacion = 908000;
                salud = baseCotizacion * Convert.ToDecimal(0.12);
                pension = baseCotizacion * Convert.ToDecimal(0.16);
                arl = baseCotizacion * Convert.ToDecimal(0.052);
                if (itemEmpleado.Caja)
                    cajaCompensacion = baseCotizacion * Convert.ToDecimal(0.04);

                totalParafiscales = salud + pension + arl + cajaCompensacion;

                //-------------------------------------------------------------------------
                Console.SetCursorPosition(5, y); Console.Write(itemEmpleado.Codigo);
                Console.SetCursorPosition(15, y); Console.Write(itemEmpleado.Nombre);
                Console.SetCursorPosition(35, y); Console.Write(itemEmpleado.Sueldo);
                Console.SetCursorPosition(50, y); Console.Write(itemEmpleado.Caja);
                Console.SetCursorPosition(65, y); Console.Write(baseCotizacion);
                Console.SetCursorPosition(80, y); Console.Write(salud);
                Console.SetCursorPosition(95, y); Console.Write(pension);
                Console.SetCursorPosition(105, y); Console.Write(arl);
                Console.SetCursorPosition(120, y); Console.Write(cajaCompensacion);
                Console.SetCursorPosition(135, y); Console.Write(totalParafiscales);
                
            }
            Console.Write("\n");

        }

        static void BuscarEmpleado()
        {
            string cod;
            bool CodigoValido = false;

            Console.Clear();
            Console.WriteLine(" ...... buscar empleado");
            Console.WriteLine(" ......................");

            do
            {
                Console.Write(" Digite Codigo Empleado que desea buscar: ");
                cod = Console.ReadLine();
                if (!Validar.Vacio(cod))
                    if (Validar.TipoNumero(cod))
                        CodigoValido = true;
            } while (!CodigoValido);

            if (Existe(Convert.ToInt32(cod)))
            {
                Empleado myEmpleado = ObtenerDatos(Convert.ToInt32(cod));
                Console.WriteLine("Codigo: " + myEmpleado.Codigo + "\t Nombre: " + myEmpleado.Nombre + "\t  Salario: " + myEmpleado.Sueldo + "\t  Caja: " + myEmpleado.Caja);
            }
               
              else
                Console.WriteLine(" El Usuario " + cod + " NO  existe en el sistema");


        }

        static bool Existe(int cod)
        {
            bool aux = false;
            foreach (Empleado  objetoEmpleado in  ListaEmpleados)
            {
                if (objetoEmpleado.Codigo == cod)
                    aux = true;
            }
            return aux;
        }

        static Empleado ObtenerDatos(int cod)
        {
            foreach (Empleado objetoEmpleado in ListaEmpleados)
            {
                if (objetoEmpleado.Codigo == cod)
                    return objetoEmpleado;
            }
            return null;
        }


        static void EscribirXml()
        {
            XmlSerializer codificador = new XmlSerializer(typeof(List<Empleado>));
            TextWriter escribirXml = new StreamWriter("C:/datosnet/listaEmpleados.xml");
            codificador.Serialize(escribirXml, ListaEmpleados);
            escribirXml.Close();

            Console.WriteLine(" Archivo Guardado ---- ");
        }

        static void LeerXml()
        {
            ListaEmpleados.Clear();
            if (File.Exists("C:/datosnet/listaEmpleados.xml")) { 
            XmlSerializer codificador = new XmlSerializer(typeof(List<Empleado>));
            FileStream leerXml = File.OpenRead("C:/datosnet/listaEmpleados.xml");
            ListaEmpleados = (List<Empleado>)codificador.Deserialize(leerXml);
            leerXml.Close();
            }
            Console.WriteLine(" Archivo Cargado ---- ");
        }
    }
}
