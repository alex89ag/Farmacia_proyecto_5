using System;
using System.Collections;
using System.Collections.Generic;

namespace Proyecto_5
{
    class Program
    {
        static Farmacia farmacia;
        static string rSoc;
        static string dir;
        static int tel;
        static int op = 0;
        static List <int> codAsig = new List<int>(); // Se guardan los codigos de empleados para no repetirlos.
        static List <int> ticketAsig = new List<int>(); // Se guardan los ticket ingresados para no repetirlos.
        static DateTime fechaHora = DateTime.Now;
        static void Main(string[] args)
        {
            Console.Clear();

            Msj.bien_desp();    // Mensaje de bienvenida

            creaFarma();        // Crea la farmacia
            
            preCargaEmp();      // Se cargan 2 empleados

            menu();             // Inicia el Menu
        }

        public static void creaFarma() // Solicita datos y crea la farmacia
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
           
            
            farmacia = new Farmacia(rSoc,dir,tel); // Se crea la farmacia
            Console.Clear();
            Console.WriteLine($"Se creo la Farmacia: {rSoc}");
            Console.ReadKey();
        }

        /******************************************** METODO DE MENU *********************************************************/
        public static void menu()
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
                                Farmacia.todasVentas();
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
                    op=0; // Evita que al seleccionar la opcion 3 se salga.
                    break;
                    case 2: // VENDEDORES
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
                                Farmacia.todosEmpleados();
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
                    op=0; // evita que al seleccionar la opcion 3 se salga.
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
        public static int opPrincipal()
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

        public static int opVenta()
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

        public static int opEmpleado()
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

        public static int selecOp() // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
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
        /******************************************* METODO DE PRE-CARGA *********************************************************/

        public static void preCargaEmp(){ // Carga de dos Empleados
        Farmacia.agregarEmp("COSME", "FULANITO", 1, 0);
        codAsig.Add(1);
        Farmacia.agregarEmp("MAX", "POWER", 2, 0);
        codAsig.Add(2);
        }

        /*********************************************************************************************************************/
        /******************************************** METODOS DE VENTAS ******************************************************/

        public static void nuevaVenta(){ /*Punto (A)*/

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

                    Farmacia.agregarVenta(nomCom, droga, obSocial, plan, importe, codVendedor, nroTicket, fechaHora); // Envia datos a agregar venta

                    Console.Clear();
                    Console.WriteLine("Venta Registrada");    
                }else{
                    Msj.opCancelada();
                }
                
            }
        }

        public static void borrarVenta(){ /* Punto (C) */

            if (Farmacia.cantidadVentas() != 0) // Verifica que haya ventas para eliminar. Si no hay emite un mensaje.
            {
                Console.WriteLine("Eliminar Venta\n"); // Titulo
                 
                int ticket = validTicket("existe"); // Valida el Ticket sea un valor correcto y verifica que exista
                Console.Clear();
                if (ticket != 000)
                {
                    if (Msj.conf($"Quiere eliminar la venta con numero {ticket}?") == true)
                    {
                        Farmacia.eliminarVenta(ticket);
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

        public static void modificarCodVend(){ /*Punto (B)*/
            
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
                        Farmacia.modificaCodVend(ticket,codNuevoVendedor);
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
        
        public static void porcVentasQuinOS(){ /*Punto (D)*/
            int cantV = Farmacia.cantidadVentas();     // Cantidad de ventas
            if (cantV != 0) // Verifica si la lista esta vacia.
            { 
                int cantVOS = Farmacia.infoVentasQuinOS();            // Cantidad de ventas con Obra Social
                if (cantVOS == 0) // Si el cantidad es 0 significa que no hay ventas con obra social en la primera quincena
                {
                    Console.WriteLine("No hay ventas en la primera quincena del corriente mes.");
                }else
                {
                    double porc = (cantVOS * 100) / cantV;          // Realiza cuenta de porcentaje
                    Console.WriteLine($"El porcentaje de ventas de la primera quincena con Obra Social es: {porc}%");
                }
            }else
            {
                Console.Clear();
                Console.WriteLine("No se registran ventas");
            }
        }
        public static void buscaDrogaPlan(){ /*Punto (E)*/

            
            if (Farmacia.cantidadVentas() != 0) // Verifica si hay ventas, si las hay solicita los parametros de busqueda.
            { 
                Console.WriteLine("Listado de ventas por Droga y Plan determinado\n");

                string droga= "";
                string plan= "";
                bool esPlan= true;
                do{
                    Console.Write("Indique la droga del medicamento: ");
                    droga= Console.ReadLine().ToUpper(); // Pasa a Mayusc. para realizar la comparacion
                    Console.Write("Indique el Plan: ");
                    plan= Console.ReadLine().ToUpper();  // Pasa a Mayusc. para realizar la comparacion
                    if(plan == "PARTICULAR"){
                        esPlan= false;
                        Msj.noPlan(); // Sale el aviso para ingreso de "PARTICULAR" como plan
                        Msj.pausa();
                    }

                }while(esPlan != true);

                ArrayList listaDrogaPlan = Farmacia.ventasDrogaPlan(droga,plan); // Se envia los parametros para que devuelva una lista de lo solicitado
                if (listaDrogaPlan.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Hay ventas con los parametros de busqueda ingresado");
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
       

    /*********************************************************************************************************************/

    /******************************************* METODOS DE EMPLEADOS ****************************************************/

        public static void nuevoEmp(){ 

            Console.WriteLine("Registro de nuevo empleado\n");
            Console.Write("Ingrese nombre: ");
            string nom= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
            Console.Write("Ingrese apellido: ");
            string ape= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
            int codVendedor = asingCod();
            
            Farmacia.agregarEmp(nom, ape, codVendedor, 0); // Crea objeto Empleado
           
            Console.Clear();
            Console.WriteLine($"Se ha completado el registro del empleado\n{codVendedor}: {nom} {ape}");
        }
        
        public static void borrarEmp(){
            if (Farmacia.cantEm() != 0) // Verifica si hay un empleado cargado
            {
                Console.WriteLine("Eliminar empleado\n");   

                int codVendedor = validCodigo(); // Valida y busca si el codigo de empleado esta cargado
                if (codVendedor == 000) // Si no se recuerda el codigo se ingresa 000 para cancelar
                {
                    Msj.opCancelada();
                }else
                {
                     if (Msj.conf($"Seguro quiere eliminar el usuario?") == true) // Consulta si se quiere eliminar el empleado
                    {
                        Farmacia.eliminarEmp(codVendedor); // Elimina el empleado
                        codAsig.Remove(codVendedor); // Elimina el codigo que tenia asignado
                        Console.Clear();
                        Console.WriteLine("Usuario del empleado fue eliminado.");
                    }else
                    {
                        Msj.opCancelada();
                    }
                }
            }else{
                Console.Clear();
                Console.WriteLine("No hay empleados cargados");
            }
        }

    /*********************************************************************************************************************/

    /******************************************** METODO DE VENDEDOR *****************************************************/

        public static void reporteMayorVendedor(){ /*Punto (F)*/
            
            if (Farmacia.cantEm() != 0) // Verifica si hay empleados cargados
            {
                int codVendedorMayor = Farmacia.informarMayorVendedor();
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

        public static int validCodigo(){ /*Valida codigo de empleado*/
            int codVendedor = 123;
            bool encontro = false;
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
                                encontro = true;
                            }
                        }    
                        Farmacia_Exception.exe_tryCatch(encontro,"empleado"); // Si no encuentra el empleado ejecuta el try-catch EmpleadoNoEncont()                    
                        
                    }else{
                        encontro = true; // Se setea en true para salir del blucle
                    }
                }
                catch (Farmacia_Exception.EmpleadoNoEncont)
                {
                    Console.Clear();
                    Msj.tcNoEmpleado(codVendedor);
                    Console.WriteLine("Si no se acuerda ingrese 000 para salir.\n");
                }
                catch (Exception)
                {
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                }
                
            } while (encontro != true && codVendedor != 000); // Se ejecuta el While hasta que ingresa un codigo valido o ingrese 000, en este ultimo caso se cancela la operación siguiente
            return codVendedor;
        }
        public static double validImporte(string nomCom)
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
        public static int validTicket(string que) // Solicita un codigo, lo valida y verifica que no se repita o que exista segun el string que recibe
        {
            bool encontre = true;
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
                                        Console.WriteLine($"Si no recuerda o sabe el numero de ticket correcto, ingrese 000 para cancelar\n");
                                        break; // Cuando encuentra coincidencia se sale del foreach   
                                    }else // Si que es igual a existe tiene que verificar que el codigo exista
                                    {
                                        ok = true;
                                        break; // Para que deje de seguir buscando
                                    }
                                }else
                                {
                                    if (que == "existe") // Solo si se esta verificando si el ticket exite se va a enviar el try en caso de no encontrar
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Si no recuerda o sabe el numero de ticket correcto, el numero de ticket ingrese 000 para cancelar\n");  
                                        encontre = false;
                                    }
                                }
                            }
                            Farmacia_Exception.exe_tryCatch(encontre,"ticket");
                        }else{
                            ok = true; // Se setea ok en true para salir del bucle
                        }
                        
                    }
                    catch (Farmacia_Exception.TicketNoValido){ // Si no se encuentra(encontre en false) nada muestra el mesnaje
                        Msj.tcTicket(nroTicket);
                    }
                    catch (System.Exception)
                    {
                        ok = false;
                        Msj.tryCatch();
                    }
                }while (ok != true && nroTicket != 000); // Hasta que no ingrese un Ticket valido y no repetido o exita se ejecuta el while
            return nroTicket;  // Devuelve un Ticket valido y que exista o no se repita segun lo requerido  
        } 
        public static int asingCod()
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
}
