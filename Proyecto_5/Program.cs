using System;
using System.Collections;
using System.Collections.Generic;

namespace Proyecto_5
{
    class Program
    {
        static string rSoc;
        static string dir;
        static int tel;
        static int op = 0;
        static List <int> codAsig = new List<int>(); // Se guardan los codigos de empleados para no repetirlos.
        static List <int> ticketAsig = new List<int>(); // Se guardan los ticket ingresados para no repetirlos.
        static ArrayList listaDrogaPlan = new ArrayList(); // lista de busquedas
        static DateTime fechaHora = DateTime.Now;
        static void Main(string[] args)
        {
            Console.Clear();

            Msj.bien_desp();    // Mensaje de bienvenida
            
            creaFarm();         // Crea la farmacia
            
            preCargaEmp();      // Se cargan 2 empleados

            menu();             // Inicia el Menu
        }

    /******************************************** METODO DE FARMACIA *****************************************************/
        static void creaFarm() // Solicita datos y crea la farmacia
        {
            Console.WriteLine("Debe crear una Farmacia");
            Console.Write("Ingrese el nombre: ");    
            rSoc = Console.ReadLine();
            Console.Write($"Ingrese la dirección de {rSoc}: ");    
            dir = Console.ReadLine();
            bool ok;
            do
            {
                ok = true;
                Console.Write($"Ingrese el telefono(sin guiones) de {rSoc}: ");    
                try
                {
                    tel = int.Parse(Console.ReadLine()); 
                }
                catch (System.Exception)
                {
                    ok = false;
                    Msj.tryCatch();
                }
            } while (ok != true);
           
            
            Farmacia farmacia = new Farmacia(rSoc,dir,tel); // Se crea la farmacia
            Console.Clear();
            Console.WriteLine($"Se creo la Farmacia: {rSoc}");
            Console.ReadKey();
        }

    /*********************************************************************************************************************/
    /******************************************** METODO DE MENU *********************************************************/

        static void menu()
        {
            do
            {   
                op = opPrincipal();

                switch (op)
                {
                    case 1: // VENTAS
                    do
                    {
                        op = opVenta();
                        switch (op)
                        {
                            case 1: // Punto a)
                                Console.Clear();
                                nuevaVenta();
                                Msj.pausa();
                            break;
                            case 2: // Punto c)
                                Console.Clear();
                                borrarVenta();
                                Msj.pausa();
                            break;
                            case 3: // Punto d)
                                Console.Clear();
                                porcVentasQuinOS();
                                Msj.pausa();
                            break;
                            case 4: /// Punto e)
                                Console.Clear();
                                buscaDrogaPlan();
                                Msj.pausa();
                            break;
                            case 5: // Punto b)
                                Console.Clear();
                                modificarCodVend();
                                Msj.pausa();
                            break;
                            case 6: // Lista de todas las ventas
                                Console.Clear();
                                todasVentas();
                                Msj.pausa();
                            break;
                            case 7: // Vuelve al menu principal
                                Console.Clear();
                            break;
                            case 0: // Opcion que evita que envie 2 mensaje
                            break;
                            default: 
                                Msj.opcIncorrecta();
                            break;
                        }
                    } while (op != 1 && op != 2 && op != 3 && op != 4 && op != 5 && op != 6 && op != 7); // Tiene que selecionar una opcion valida para poder continuar
                    op= 0; // Evita que al seleccionar la opcion 3 se salga.
                    break;
                    case 2: // EMPLEADOS
                    do
                    {
                        op = opEmpleado();
                        switch (op)
                        {
                            case 1: 
                                Console.Clear();
                                nuevoEmp();
                                Msj.pausa();                    
                            break;
                            case 2:
                                Console.Clear();
                                borrarEmp();
                                Msj.pausa();
                            break;
                            case 3: // Punto F
                                Console.Clear();
                                reporteMayorVendedor();
                                Msj.pausa();
                            break;
                            case 4:  // Muestra todos los empleados
                                Console.Clear();
                                todosEmpleados();
                                Msj.pausa();
                            break;
                            case 5: // Vuelve al menu principal
                                Console.Clear();
                            break;
                            case 0: // Opcion que evita que envie 2 mensaje
                            break;
                            default: 
                                Msj.opcIncorrecta();
                            break;
                        }
                    } while (op != 1 && op != 2 && op != 3 && op != 4 && op != 5); // Tiene que selecionar una opcion valida para poder continuar
                    op= 0; // evita que al seleccionar la opcion 3 se salga.
                    break;
                    case 3: //SALIR
                        Console.Clear();
                        Msj.bien_desp();
                        Console.Write("\nGracias, vuelva prontos!\n\nPresione cualquier tecla para finalizar...");
                        Console.ReadKey();
                        Console.Clear();
                        Environment.Exit(1);
                    break;
                    case 0: // Opcion que evita que envie 2 mensaje
                    break;
                    default: 
                        Msj.opcIncorrecta();
                    break;
                }    
                
            } while (op != 3);
        }
        static int opPrincipal()
        {
            Console.Clear();
            
            Console.WriteLine("***********************************");
            Console.WriteLine($"Farmacia: {rSoc}");
            Console.WriteLine($"Dirección: {dir}");
            Console.WriteLine($"Telefono: {tel}");
            Console.WriteLine("***********************************\n");            

            Console.WriteLine("1- Ventas.");
            Console.WriteLine("2- Empleados.");
            Console.WriteLine("3- Salir.");
            Console.Write("\nSeleccione una opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        static int opVenta()
        {
            Console.Clear();
            Console.WriteLine("1- Agregar venta.");                                    // Punto A
            Console.WriteLine("2- Eliminar venta.");                                   // Punto C
            Console.WriteLine("3- Porcentaje de ventas por OS.");                      // Punto D
            Console.WriteLine("4- Lista de ventas con Dogra dada y Plan determinado.");// Punto E
            Console.WriteLine("5- Modificar codigo de vendedor.");                     // Punto B
            Console.WriteLine("6- Listado de ventas.");                      // Listado de todas las ventas
            Console.WriteLine("7- Volver.");                                           // Vuelve al menu principal
            Console.Write("\nSeleccione otra opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        static int opEmpleado()
        {
            Console.Clear();
            Console.WriteLine("1- Nuevo."); 
            Console.WriteLine("2- Borrar.");
            Console.WriteLine("3- Vendedor con mayor monto de ventas.");                // Punto f)
            Console.WriteLine("4- Listado de empleados.");
            Console.WriteLine("5- Volver.");
            Console.Write("\nSeleccione otra opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        static int selecOp() // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
        {
            int op = 0;

            try
            {
                op = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                op = 0; // seleciona un case que no hace nada para evitar el default y que envie doble mensaje
                Msj.tryCatch();
                Msj.pausa();
            }
            return op;
        }

    /*********************************************************************************************************************/
    /******************************************* METODO DE PRE-CARGA *****************************************************/

        static void preCargaEmp(){ // Carga de dos Empleados
        Empleado empleado = new Empleado("COSME", "FULANITO", 1, 0);
        Farmacia.agregarEmp(empleado);
        codAsig.Add(1);
        empleado = new Empleado("MAX", "POWER", 2, 0);
        Farmacia.agregarEmp(empleado);
        codAsig.Add(2);
        }

    /*********************************************************************************************************************/
    /******************************************** METODOS DE VENTAS ******************************************************/

        static void nuevaVenta(){       /*Punto (A)*/

            Console.WriteLine("Registro de Venta\n"); // Titulo
            
            int codVendedor = validCodigo();
            
            if (codVendedor == 000) // Si no recuerdan y ponen 000, se cancela la operacion. Pueden ir a ver el listado de empleados para ver los disponibles
            {
                Console.Clear();
                Console.WriteLine("Venta cancelada!\nConsulte listado de empleado para ver los disponibles.");   
            }
            else
            {
                int nroTicket = validTicket("repite"); // Valida el codigo y verifica que no se repita
                if (nroTicket != 000)
                {
                    Console.Write("Ingrese el nombre comercial del medicamento: ");
                    string nomCom = Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
                    Console.Write($"Ingrese la Droga de {nomCom.ToUpper()}: ");
                    string droga = Console.ReadLine().ToUpper();    // Queda almacenado en mayusculas
                    Console.WriteLine("\nATENCIóN!  Si no es por Obra Social ingrese 'particular'.");
                    Console.Write("Ingrese la obra social: ");
                    string obSocial = Console.ReadLine().ToUpper(); // Queda almacenado en mayusculas
                    Console.WriteLine("\nATENCIóN!!  Si no es por un plan determinado deje un espacio.");
                    Console.Write("Ingrese el plan: ");
                    string plan = Console.ReadLine().ToUpper(); // Queda almacenado en mayusculas
                    double importe = validImporte(nomCom);

                    Venta venta = new Venta(nomCom, droga, obSocial, plan, importe, codVendedor, nroTicket, fechaHora); // Crea objeto venta
                    Farmacia.agregarVenta(venta); // Envia la venta
                    ticketAsig.Add(nroTicket);

                    foreach(Empleado e in Farmacia.todosEmpleados()){ // Se busaca y suma el monto de la venta al vendedor ingresado.
                        if(e.CodEmpleado == codVendedor){
                            e.MontoVenta += importe; //Suma y actualiza el monto de venta del Vendedor
                        } 
                    }

                    Console.Clear();
                    Console.WriteLine("Venta Registrada");    
                }else{
                    Msj.opCancelada();
                }
                
            }
        }

        static void borrarVenta(){      /* Punto (C) */

            if (Farmacia.cantidadVentas() != 0) // Verifica que haya ventas para eliminar. Si no hay emite un mensaje.
            {
                Console.WriteLine("Eliminar Venta\n"); // Titulo
                 
                int ticket = validTicket("existe"); // Valida el Ticket sea un valor correcto y verifica que exista
                Console.Clear();
                if (ticket != 000)
                {
                    if (Msj.conf($"Quiere eliminar la venta con numero {ticket}?") == true)
                    {
                        bool eliminado = false;
                        foreach (Venta v in Farmacia.todasVentas())
                        {  
                            if (v.NroTicket == ticket)
                            {
                                if (v.CodVendedor != 000)   // Verifica que la venta este asignada a un empleado
                                {
                                    foreach(Empleado e in Farmacia.todosEmpleados()){ // Se busca el vendedor asignado para restar la venta
                                        if(e.CodEmpleado == v.CodVendedor){
                                            e.MontoVenta -= v.Importe;      // Resta el importe de la venta
                                        }
                                    }
                                }
                                Farmacia.eliminarVenta(v); // Elimina la venta
                                ticketAsig.Remove(ticket); // Elimina el ticket que se le había asignado
                                eliminado = true;
                                break; // Evita que salga error de ingreso                   
                            }
                        }
                    if (eliminado == true) // Muestra mensaje de eliminado
                    {
                        Console.Clear();
                        Console.WriteLine("Venta Eliminada.");
                    }
                        ticketAsig.Remove(ticket); // Elimina el ticket asignado
                    }else{
                        Msj.opCancelada();
                    }     
                }else{
                    Msj.opCancelada();
                }
                
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay venta para eliminar");
            }
        }

        static void modificarCodVend(){ /*Punto (B)*/
            
            if (Farmacia.cantidadVentas() != 0) // Verifica si hay ventas)
            {
                Console.WriteLine("Actualización de Venta\n");  
                Console.Write("Ticket-factura de la venta a modificar\n");
                int ticket = validTicket("existe"); // Valida que el ticket sea un valor correcto y exista
                if (ticket != 000)
                {
                    Console.WriteLine("Nuevo codigo de vendedor\n");
                    int codNuevoVendedor = validCodigo(); // Valida que el empleado nuevo al que se le quiere asignar la venta exista
                    if (codNuevoVendedor != 000)
                    {
                        foreach(Venta v in Farmacia.todasVentas()){    
                            if(v.NroTicket == ticket){       
                                foreach(Empleado e in Farmacia.todosEmpleados()){ // Se recorre la lista de empleados
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
                        Console.Clear();
                        Console.WriteLine($"Se ha actualizado el codigo de vendedor de la venta con numero de ticket: {ticket}");
                    }else{
                        Msj.opCancelada();    
                    }
                }else{
                    Msj.opCancelada();
                }
                
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay ventas para mostrar");
            }
        }
        
        static void porcVentasQuinOS(){ /*Punto (D)*/
            int cantV = Farmacia.cantidadVentas();              // Obtiene la cantidad de ventas
            int contVOS = 0;                                    // Contador de ventas con Obra Social
            int mesAct = int.Parse(fechaHora.ToString("MM"));   // Recupera y guarda el mes actual
            if (cantV != 0)                                     // Verifica que haya al menos una venta registrada
            { 
                foreach (Venta v in Farmacia.todasVentas())
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
                if (contVOS == 0)   // Si el cantidad es 0 significa que no hay ventas con obra social en la primera quincena
                {
                    Console.WriteLine("No hay ventas en la primera quincena del corriente mes.");
                }else
                {
                    double porc = (contVOS * 100) / cantV;          // Realiza cuenta de porcentaje
                    Console.WriteLine($"El porcentaje de ventas de la primera quincena con Obra Social es: {porc}%");
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay ventas registradas.");
            }
        }
        static void buscaDrogaPlan(){ /*Punto (E)*/

            if (Farmacia.cantidadVentas() != 0) // Verifica si hay ventas, si las hay solicita los parametros de busqueda.
            { 
                Console.WriteLine("Listado de ventas por Droga y Plan determinado\n");

                string droga= "";
                string plan= "";
                bool esPlan;
                do{
                    esPlan = true; // Se setea en true para que no quede en un bucle infinito
                    Console.Write("Indique la droga del medicamento: ");
                    droga= Console.ReadLine().ToUpper(); // Pasa a Mayusc. para realizar la comparacion
                    Console.Write("Indique el Plan: ");
                    plan= Console.ReadLine().ToUpper();  // Pasa a Mayusc. para realizar la comparacion
                    if(plan == "PARTICULAR" || plan.Trim() == ""){  
                        esPlan= false;
                        Console.Clear();
                        Console.WriteLine("Ingreso 'Particular', el cual no es un plan"); // Sale el aviso para ingreso de "PARTICULAR" como plan
                        Msj.pausa();
                    }
                }while(esPlan != true);

                foreach(Venta v in Farmacia.todasVentas()){
                    if (v.FechaHora.ToString("MM") == fechaHora.ToString("MM")) // Compara el mes de venta con el actual para mostrar solo lo del mes en curso
                    {
                        if (v.Droga.ToUpper() == droga && v.Plan.ToUpper() == plan){ // Busca la droga y el plan solicitado
                            listaDrogaPlan.Add(v); // Agrega la venta a la lista auxiliar para listar
                        }
                    }
                }
                if (listaDrogaPlan.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("No hay ventas con los parametros de busqueda ingresado");
                }else{
                    Console.Clear();
                    Console.WriteLine("Resultado de busqueda: \n");
                    foreach (Venta v in listaDrogaPlan)
                    {
                        Farmacia.verVenta(v.NroTicket); // Se envia el numero de ticket para que imprima las ventas
                    }
                }

            }else
            {
                Console.Clear();
                Console.WriteLine("No se registran ventas");
            }
        }

        public static void todasVentas()
        {
            // Retorna la lista
            if (Farmacia.cantidadVentas() != 0) // Verifica si hay ventas.
            {    
                Console.WriteLine("Listado de Ventas: ");
                foreach (Venta x in Farmacia.todasVentas())
                {
                    Farmacia.verVenta(x.NroTicket);
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay ventas para mostrar");
            }
        }
       

    /*********************************************************************************************************************/

    /******************************************* METODOS DE EMPLEADOS ****************************************************/

        static void nuevoEmp(){ 

            Console.WriteLine("Registro de nuevo empleado\n");
            Console.Write("Ingrese nombre: ");
            string nom= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
            Console.Write("Ingrese apellido: ");
            string ape= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
            int codVendedor = asingCod();
            
            Empleado empleado = new Empleado(nom, ape, codVendedor, codVendedor); // Crea objeto Empleado
            Farmacia.agregarEmp(empleado); // Crea objeto Empleado
           
            Console.Clear();
            Console.WriteLine($"Se ha completado el registro del empleado\n{codVendedor}: {nom} {ape}");
        }
        
        static void borrarEmp(){
            if (Farmacia.cantEm() != 0) // Verifica si hay un empleado cargado
            {
                Console.WriteLine("Eliminar empleado\n");   

                int codVendedor = validCodigo();// Valida y busca si el codigo de empleado esta cargado
                if (codVendedor == 000)         // Si no se recuerda el codigo se ingresa 000 para cancelar
                {
                    Msj.opCancelada();
                }
                else
                {
                    foreach (Empleado e in Farmacia.todosEmpleados())
                    {
                        if(e.CodEmpleado == codVendedor) // Busca el vendedor solicitado
                        {   bool tieneVentas = Msj.advVenta(codVendedor, Farmacia.todasVentas(), e); // Verifica si el vendedor tiene ventas y hace una advertencia en caso de tenerlas, ademas devueve un true en caso de tener ventas
                            if (Msj.conf($"Seguro quiere eliminar el empleado {e.Apellido}, {e.Nombre} empleado?") == true) // Consulta si se quiere eliminar el empleado
                            {
                                Farmacia.eliminarEmp(e); // Elimina el empleado
                                if (tieneVentas == true) // Si el vendedor tenia ventas asigna a dichas ventas 000 para indicar que el vendedor de la venta fue eliminado
                                {
                                    foreach (Venta v in Farmacia.todasVentas())
                                    {
                                        if (e.CodEmpleado == v.CodVendedor)
                                        {
                                            v.CodVendedor = 000; // Asigna 000 para luego mostrar que el vendedor fue eliminado  
                                        }
                                    }
                                }   
                                break; // Evita que salga error de Index 
                            }else
                            {
                                Msj.opCancelada();
                                break; // Sale del For
                            }
                        }
                    }
                    codAsig.Remove(codVendedor);        // Elimina el codigo que tenia asignado
                    Console.Clear();
                    Console.WriteLine("El empleado fue eliminado.");
                    
                }
            }else{
                Console.Clear();
                Console.WriteLine("No hay empleados cargados");
            }
        }

        static void todosEmpleados(){ // Muestra todos los empleados
            if (Farmacia.cantEm() != 0) // Verifica si hay empleados cargados
            {
                Console.WriteLine($"Listado de empleados:\n");
            
                foreach (Empleado e in Farmacia.todosEmpleados()){
                    Console.WriteLine(Farmacia.verEmpleado(e.CodEmpleado));    
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay empleados cargados");
            }
        }

    /*********************************************************************************************************************/

    /******************************************** METODO DE VENDEDOR *****************************************************/

        static void reporteMayorVendedor(){ /*Punto (F)*/
            double mayorMonto= 0;
            int codVendedorMayor= 0;

            if (Farmacia.cantEm() != 0) // Verifica si hay empleados cargados
            {
                foreach(Empleado vend in Farmacia.todosEmpleados()){  // Recorre la lista de todos los empleados
                    if(vend.MontoVenta > mayorMonto){
                        mayorMonto= vend.MontoVenta;        // Actualiza monto mayor de venta para comparar
                        codVendedorMayor= vend.CodEmpleado; // Guarda el codigo del empleado con mayor venta para luego mostrarlo
                    }
                }
                if(codVendedorMayor == 0){
                    Console.WriteLine("No hay registrado de un vendedor con una venta.");
                
                }else{
                
                    Console.WriteLine($"Vendedor con mayor Venta: {Farmacia.verEmpleado(codVendedorMayor)}"); // Muestra el vendedor
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No hay empleados cargados");
            }
        }        

    /*********************************************************************************************************************/
    /******************************************** OTROS METODOS **********************************************************/

        static int validCodigo(){ /*Valida codigo de empleado*/
            int codVendedor = 123;
            bool encontre = false;
            do
            {   
                try
                {
                    Console.Write("Ingrese el codigo del empleado: ");
                    codVendedor = int.Parse(Console.ReadLine());
                    if (codVendedor != 000)
                    {
                        foreach (int nro in codAsig) // Recorre codigos asignados
                        {
                            if (nro == codVendedor) // Si encuentra que el codigo fue asignado quiere decir que el empleado existe
                            {
                                encontre = true;
                            }
                        }    
                        if (encontre == false)
                        {
                            throw new EmpleadoNoEncont(); // Si no se encuentra el empleado ejecuta el try-catch
                        }
                        
                    }else{
                        encontre = true; // Se setea en true para salir del blucle
                    }
                }
                catch (EmpleadoNoEncont)
                {
                    Console.Clear();
                    Console.WriteLine($"No se encontroo el Empleado con codigo: {codVendedor}\nSi no se acuerda ingrese 000 para salir.\n");
                }
                catch (Exception)
                {
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                }
                
            } while (encontre != true && codVendedor != 000); // Se ejecuta el While hasta que ingresa un codigo valido o ingrese 000, en este ultimo caso se cancela la operación siguiente
            return codVendedor;
        }
        static double validImporte(string nomCom)
        {
            double importe = 0;
            bool ok;
            do
            {
                ok = true; // Setea bool para que no entre un bucle infinito al pedir el precio
                try
                {
                    Console.Write($"Ingrese el precio (con IVA) de {nomCom.ToUpper()}: $");
                    importe = double.Parse(Console.ReadLine());  
                    if (importe < 0) // Si el valor es menor a 0 lo vuelve a consultar ya que no es valido 
                    {                // No se condiciona para cuando es igual a 0 porque el medicamento puede ser gratuito --> $0
                        ok = false;
                        Console.Clear();
                        Console.WriteLine("El importe no debe ser menor a $0!\nIntente nuevamente.");
                        Msj.pausa();
                    }
                }
                catch (Exception)
                {
                    ok = false; // si ingresa un caracter invalido cambia a false para que vuelva a preguntar
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                }
            } while (ok != true); // Se ejecuta hasta que ingrese un valor valido y mayor a 0
            return importe;
        }   
        static int validTicket(string que) // Solicita un codigo, lo valida y verifica que no se repita o que exista segun el string que recibe
        {
            bool encontre = false;
            bool ok;
            int nroTicket = 123;
                do{
                    ok = true; // Setea bool para que no entre un bucle infinito al pedir el ticket
                    try
                    {
                        Console.Write("Ingrese el Nro de Ticket: ");
                        nroTicket = int.Parse(Console.ReadLine());
                        if (nroTicket != 000)
                        {
                            foreach (int nro in ticketAsig)
                            {
                                if (nro == nroTicket) // Si encuentra el numero de ticket en los ticket asignados significa que existe la venta
                                {
                                    encontre = true;
                                    if (que == "repite") // Si que es igual a repite significa que tiene que verificar que el codigo no se repita
                                    {    
                                        ok = false;
                                        Console.Clear();
                                        Console.WriteLine($"El numero de ticket {nroTicket} ya fue ingresado!\nVuelva a intentar\n");
                                        Console.WriteLine($"Si no recuerda o desconoce el numero de ticket correcto, ingrese 000 para cancelar\n");
                                        break; // Cuando encuentra coincidencia se sale del foreach   
                                    }else // Si que es igual a existe tiene que verificar que el codigo exista
                                    {
                                        ok = true;
                                        break; // Para que deje de seguir buscando
                                    }
                                }
                            }
                            if (que == "existe" && encontre == false)
                            {
                                throw new TicketNoValido();  // Si no se encuentra el ticket ejecuta el try-catch
                            }
                        }else{
                            ok = true; // Se setea ok en true para salir del bucle
                            break;
                        }
                        
                    }
                    catch (TicketNoValido){ // Si no se encuentra(encontre en false) nada muestra el mesnaje
                        ok = false;
                        Console.Clear();
                        Console.WriteLine($"No se encontro el Ticket con codigo: {nroTicket}"); 
                        Console.WriteLine("Si no recuerda o desconoce el numero de ticket correcto, el numero de ticket ingrese 000 para cancelar\n");  
                    }
                    catch (System.Exception)
                    {
                        ok = false;
                        Msj.tryCatch();
                    }
                }while (ok != true && nroTicket != 000); // Hasta que no ingrese un Ticket valido y no repetido o exita se ejecuta el while
            return nroTicket;  // Devuelve un Ticket valido y que exista o no se repita segun lo requerido  
        } 
        static int asingCod()
        {
            int codVendedor = 0;
            if (codAsig.Count == 0)
            {
                codVendedor = 1;
                codAsig.Add(codVendedor); // Se guarda el codigo asignado
            }else
            {
                int ultimo = codAsig[codAsig.Count - 1]; // Se busca el ultimo codigo asignado
                codVendedor = ultimo + 1; // Se le suma 1 al ultimo codigo asignado y se lo guarda
                codAsig.Add(codVendedor); // Se guarda el codigo asignado
            }
            return codVendedor;
        }    

        
    /*********************************************************************************************************************/
    }
    /************************************************ TRY-CATCH **********************************************************/

    class TicketNoValido : Exception{} // try-catch para cuando el ticket no es valido
    class EmpleadoNoEncont : Exception{} // try-catch para cuando no se encuentra el empleado

    /*********************************************************************************************************************/
}
