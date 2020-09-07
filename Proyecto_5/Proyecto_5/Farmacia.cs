using System;
using System.Collections;


namespace Proyecto_5
{
    class Farmacia{

        static ArrayList lista_Empleados = new ArrayList();
        static ArrayList lista_Ventas= new ArrayList();   
        static Venta venta;
        static Empleado empleado;
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

        public string RazonSocial 
        {
            get
            {
                return this.razonSocial;
            }
        }

    /*********************************************************************************************************************/
    /******************************************** METODOS DE VENTAS ******************************************************/

        public static void agregarVenta(string nomCom,string droga,string obSocial,string plan,double importe,int codVendedor,int nroTicket,DateTime fechaHora){ /*Punto (A)*/

            venta = new Venta(nomCom, droga, obSocial, plan, importe, codVendedor, nroTicket, fechaHora); // Crea objeto v
            lista_Ventas.Add(venta); // Guarda objeto en la lista

            foreach(Empleado e in lista_Empleados){ // Se busaca y suma el monto de la venta al vendedor ingresado.
                if(e.CodEmpleado == codVendedor){
                    e.MontoVenta += importe; //Suma y actualiza el monto de venta del Vendedor
                } 
            }
        }

        public static void eliminarVenta(int ticket){ /* Punto (C) */
            bool eliminado = false;
                foreach (Venta v in lista_Ventas)
                {  
                    if (v.NroTicket == ticket)
                    {
                        lista_Ventas.Remove(v);               // Elimina la venta
                        if (v.CodVendedor != 000) // Verifica que la venta este asignada a un empleado
                        {
                            foreach(Empleado e in lista_Empleados){ // Se busca el vendedor asignado para restar la venta
                                if(e.CodEmpleado == v.CodVendedor){
                                    e.MontoVenta -= v.Importe; // Resta el importe de la venta
                                }
                            }
                        }
                        eliminado = true;
                        break; // Evita que salga error de ingreso                   
                    }
                }
            if (eliminado == true) // Muestra mensaje de eliminado
            {
                Console.Clear();
                Console.WriteLine("Venta Eliminada.");
            }            
        }

        public static void verVenta(int ticket){ // Busca la venta a mostrar por numero tickets
                                                 // Aclaración: En este metodo no se hacen validaciones porque  debe recibir un numero de ticket valido
            foreach(Venta v in lista_Ventas){
                if (v.NroTicket == ticket){
                    venta= v;
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

        public static void todasVentas()
        {
            if (cantidadVentas() != 0) // Verifica si hay ventas.
            {    
                Console.WriteLine("Listado de Ventas: ");
                foreach (Venta x in lista_Ventas)
                {
                    verVenta(x.NroTicket);
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay ventas para mostrar");
            }
        }

        public static int cantidadVentas()
        {
            return lista_Ventas.Count;
        }

        public static void modificaCodVend(int ticket,int codNuevoVendedor){ /*Punto (B)*/

            Console.WriteLine("Actualización de Venta\n");  
            foreach(Venta v in lista_Ventas){    
                if(v.NroTicket == ticket){       
                    foreach(Empleado e in lista_Empleados){ // Se recorre la lista de empleados
                        if(e.CodEmpleado == v.CodVendedor){
                            e.MontoVenta -= v.Importe; // Resta el monto de venta al viejo Vendedor
                        }
                        if(e.CodEmpleado == codNuevoVendedor){
                            e.MontoVenta += v.Importe; // Suma el monto de venta al nuevo Vendedor
                        }
                    }          
                    v.CodVendedor= codNuevoVendedor; // Una vez realizadas las modificaciones se setea el codigo del nuevo vendedor de la venta               
                }
            }
        }
        
        public static int infoVentasQuinOS(){ /*Punto (D)*/

                int contVOS = 0;                    // Contador de ventas con Obra Social
                int mesAct = int.Parse(fechaHora.ToString("MM"));   // Recupera y guarda el mes actual
                
                foreach (Venta v in lista_Ventas)
                {  
                    int mesV = int.Parse(v.FechaHora.ToString("MM"));   // Recupera y guarda el mes de la venta       
                    int diaV = int.Parse(v.FechaHora.ToString("dd"));   // Recupera y guarda el dia de la venta
                    if (mesV == mesAct && diaV >= 1 && diaV <= 15) // Verifica que se este en la primera quincena del mes
                    {    
                        if (v.ObraSocial.ToLower() != "particular") // Busca las ventas por OBRA SOCIAL
                        {                                           // Ya que busca todo lo que difiere de particular
                            contVOS ++;                             // Cuenta las Ventas con Obra Social
                        }
                    }
                }
                return contVOS; // Retorna la cantidad de ventas con obra social en la primera quincena          
        }
        public static ArrayList ventasDrogaPlan(string droga, string plan){ /*Punto (E)*/
            
            Console.WriteLine("Listado de ventas por Droga y Plan determinado\n");

            ArrayList listaDrogaPlan = new ArrayList(); // lista de busquedas

            foreach(Venta v in lista_Ventas){
                if (v.FechaHora.ToString("MM") == fechaHora.ToString("MM")) // Compara el mes de venta con el actual para mostrar solo lo del mes en curso
                {
                    if (v.Droga.ToUpper() == droga && v.Plan.ToUpper() == plan){ // Busca la droga y el plan solicitado
                        listaDrogaPlan.Add(v); // Agrega la venta a la lista auxiliar para listar
                    }
                }
            }
            return listaDrogaPlan; // Devuelve una lista de las ventas que coincidan con los parametros ingresados
               
        }
       

    /*********************************************************************************************************************/

    /******************************************* METODOS DE EMPLEADOS ****************************************************/

        public static void agregarEmp(string nom,string ape,int codVendedor,int montVenta){ 
            empleado = new Empleado(nom, ape, codVendedor, 0); // Crea objeto Empleado
            lista_Empleados.Add(empleado); // Agrega el nuevo empleado a la lista
        }
        
        public static void eliminarEmp(int codVendedor){
            foreach (Empleado e in lista_Empleados)
            {
                if(e.CodEmpleado == codVendedor) // Busca el vendedor solicitado
                {
                    bool tieneVentas = Msj.advVenta(codVendedor, lista_Ventas, e); // Verifica si el vendedor tiene ventas y hace una advertencia en caso de tenerlas, ademas devueve un true en caso de tener ventas
                    
                    lista_Empleados.Remove(e); // Elimina el empleado
                    if (tieneVentas == true) // Si el vendedor tenia ventas asigna a dichas ventas 000 para indicar que el vendedor de la venta fue eliminado
                    {
                        foreach (Venta v in lista_Ventas)
                        {
                            if (e.CodEmpleado == v.CodVendedor)
                            {
                                v.CodVendedor = 000; // Asigna 000 para luego mostrar que el vendedor fue eliminado  
                            }
                        }
                    }   
                    break; // Evita que salga error de Index 
                }
            }
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

        public static void todosEmpleados(){ // Muestra todos los empleados
            if (cantEm() != 0) // Verifica si hay empleados cargados
            {
                Console.WriteLine($"Listado de empleados:\n");
            
                foreach (Empleado e in lista_Empleados){
                    Console.WriteLine(verEmpleado(e.CodEmpleado));    
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay empleados cargados");
            }
        }

        public static int cantEm() // Cuenta la cantidad de empleados cargados
        {
            return lista_Empleados.Count;
        }

    /*********************************************************************************************************************/

    /******************************************** METODO DE VENDEDOR *****************************************************/

        public static int informarMayorVendedor(){ /*Punto (F)*/
            double mayorMonto= 0;
            int codVendedorMayor= 0;

            foreach(Empleado vend in lista_Empleados){
                
                if(vend.MontoVenta > mayorMonto){
                    mayorMonto= vend.MontoVenta;        // Actualiza monto mayor de venta para comparar
                    codVendedorMayor= vend.CodEmpleado; // Guarda el codigo del empleado con mayor venta para luego mostrarlo
                }
            }

            return codVendedorMayor;
        }        

    /*********************************************************************************************************************/

    }
}