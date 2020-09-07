using System;
using System.Collections;

namespace Proyecto_5
{
    class Msj
    {
        public static void bien_desp()
        {
            Console.Clear();
            Console.WriteLine("\t ___________________________________");
            Console.WriteLine("\t|                                   |");
            Console.WriteLine("\t|***********************************|");
            Console.WriteLine("\t|*      FARMACIAS PROYECTO 5       *|");
            Console.WriteLine("\t|***********************************|");
            Console.WriteLine("\t|___________________________________|");
            Console.WriteLine("\t|                                   |");
            Console.WriteLine("\t|   Sistema para gestion de Venta   |");
            Console.WriteLine("\t|___________________________________|\n");
            
        }

        public static void pausa() // crea una pausa y se solicita que presione una tecla
        {
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void opcIncorrecta() // Mensaje de opcion invalida para switch-case
        {
            Console.Clear(); 
            Console.WriteLine("Ingreso incorrecto!!\nIngrese una opcion valida\n");
            pausa();
            Console.Clear();
        }
        public static void opCancelada()
        {
            Console.Clear();
            Console.WriteLine("Operacion cancelada.");
        }

        /*********************************** Msj de Try-Catch****************************************/

        public static void tryCatch() // Mensaje para el try/catch general
        {
            Console.Clear();
            Console.WriteLine($"Ingreso invalido!\nIntente nuevamente\n");
        }

        /********************************************************************************************/
        public static bool conf(string pregunta) // Hace una pregunta la cual requiera confirmación
        {                                                 
            char respC = ' ';
            string resp;                             
            bool msjConf= false;
            try
            {            
                do{
                    Console.WriteLine(pregunta); // Imprime la pregunta que requiere confirmación
                    Console.Write("Ingrese s o n: "); 
                    resp = Console.ReadLine().ToLower();

                    respC = resp[0]; // Solo se guarda la primera posición
                    switch (respC)
                    {
                        case 's':
                            msjConf = true;
                            break;
                        case 'n':
                            msjConf = false;
                            break;
                        default:
                            Msj.opcIncorrecta();
                            break;
                    }
                } while (respC != 's' && respC != 'n');   
            }
            catch (System.Exception)
            {
                tryCatch();
            }
            return msjConf; // Si acepta realizar la acción devuelve la TRUE, si no FALSE
        }

        public static bool advVenta(int codE, ArrayList ventas, Empleado x) // Busca si un empleado determinado tiene una venta asignada
        {
            bool emplConVenta= false; // Se inicializa en FALSE y luego se hace la comprobación

            foreach (Venta v in ventas)
            {
                if (codE == v.CodVendedor)
                {
                    emplConVenta = true; // El vendedor tiene venta/as asignada
                }
            }
            if (emplConVenta == true)
            {   
                Console.Clear();
                Console.WriteLine($"ADVERTENCIA: El empleado {x.Nombre.ToUpper()} {x.Apellido.ToUpper()} tiene una o mas venta asignada\n");
            }
            return emplConVenta;
        }
    }
}