using System;
using System.Collections;


namespace Proyecto_5
{
    class Farmacia{

        static ArrayList lista_Empleados = new ArrayList();
        static ArrayList lista_Ventas= new ArrayList(); 
        static DateTime fechaHora = DateTime.Now;

        string razonSocial;
        string direccion;
        int telefono;
        
        public Farmacia(string rSoc, string dir, int tel)
        {
            this.razonSocial = rSoc;
            this.direccion = dir;
            this.telefono = tel;
        }

    /*********************************************************************************************************************/
    /******************************************** METODOS DE VENTAS ******************************************************/

        public static void agregarVenta(Venta venta){ /*Punto (A)*/ 
            lista_Ventas.Add(venta); // Guarda objeto en la lista
        }

        public static void eliminarVenta(Venta v){ /* Punto (C) */
            lista_Ventas.Remove(v);     // Elimina la venta            
        }

        public static void verVenta(int ticket){ // Busca la venta a mostrar por numero tickets                        
            foreach(Venta v in lista_Ventas){
                if (v.NroTicket == ticket){
                    Console.WriteLine($"\nFecha y hora: {v.FechaHora}\nTicket: {v.NroTicket}\nNombre comercial: {v.NombreCom.ToUpper()} \nDroga: {v.Droga.ToUpper()} \nObra Social: {v.ObraSocial.ToUpper()} \nPlan: {v.Plan.ToUpper()} \nImporte: ${v.Importe}");
                    
                    if (v.CodVendedor == 000)   // Si la venta tiene codigo de vendedor 0 va a informar que el vendedor fue eliminado
                    {
                        Console.WriteLine("Vendedor: El vendedor de esta venta fue eliminado y no se asigno otro.");
                        break;
                    }
                    else                        // Si tiene un vendedor asignado lo busca y lo muestra
                    {
                        Console.WriteLine($"Vendedor: {verEmpleado(v.CodVendedor)}");
                    }
                }
            }
        }

        public static ArrayList todasVentas()
        {
            return lista_Ventas;
        }

        public static int cantidadVentas()
        {
            return lista_Ventas.Count;
        }       

    /*********************************************************************************************************************/

    /******************************************* METODOS DE EMPLEADOS ****************************************************/

        public static void agregarEmp(Empleado empleado){ 
            lista_Empleados.Add(empleado); // Agrega el nuevo empleado a la lista
        }
        
        public static void eliminarEmp(Empleado empleado){
            lista_Empleados.Remove(empleado); // Elimina el empleado
        }
        
        public static string verEmpleado(int cod){  // Busca por codigo y muestra el vendedor 
         
            string datos= "";
            
            foreach(Empleado e in lista_Empleados){
                if (e.CodEmpleado == cod){
                    if(e.MontoVenta > 0){
                        datos = ($"Empleado {cod}: {e.Nombre} {e.Apellido} - Monto de ventas: ${e.MontoVenta}"); // Si el empleado es vendedor
                    }
                    else{
                        datos = ($"Empleado {cod}: {e.Nombre} {e.Apellido} - Sin ventas"); // Si el empleado no es vendedor o no ha vendido
                    }
                }
            }
            return datos;
        }

        public static ArrayList todosEmpleados(){ // Retorna lista de empleados
            return lista_Empleados;
        }

        public static int cantEm() // Cuenta la cantidad de empleados cargados
        {
            return lista_Empleados.Count;
        }

    /*********************************************************************************************************************/

    }
}