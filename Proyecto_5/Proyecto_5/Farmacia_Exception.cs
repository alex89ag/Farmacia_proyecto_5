using System;

namespace Proyecto_5
{
    class Farmacia_Exception
    {

        public static void exe_tryCatch(bool encontre, string quien) // ejecuta el try-catch EmpleadoNoEncont()
        {
            switch (quien)
            {
                case "empleado":
                    if (encontre == false)
                    {
                        throw new Farmacia_Exception.EmpleadoNoEncont(); // Si no se encuentra el empleado ejecuta el try-catch
                    }
                break;
                case "ticket":
                    if (encontre == false)
                    {
                        throw new Farmacia_Exception.TicketNoValido();  // Si no se encuentra el ticket ejecuta el try-catch
                    }
                break;
            }
        }

        public class TicketNoValido : Exception{} // try-catch para cuando el ticket no es valido
        public class EmpleadoNoEncont : Exception{} // try-catch para cuando no se encuentra el empleado
    }
}