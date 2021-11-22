using System;
using System.Collections.Generic;

namespace proyecto_final_algoritmos
{
    class Program
    {
        struct Vuelo
        {
            public int num_pasajeros;
            public DateTime fecha_salida;
            public DateTime fecha_llegada;
            public string[] trayectoria;
            public string id_vuelo;
            public double precio_boleto;
            public double ganancias_totales;
            public TimeSpan tiempo_trayecto;
        }
        static string GenerarID()
        {
            Random rnd = new Random();
            string id = "";
            for (int i = 0; i < 4; i++)
            {
                id += rnd.Next(0, 9);
            }
            return id;
        }
        static void AgregarVuelo(List<Vuelo> vuelos)
        {
            Vuelo vuelo = new Vuelo();
            vuelo.id_vuelo = GenerarID();
            Console.WriteLine("El ID del vuelo es " + vuelo.id_vuelo);
            string input;
            do
            {
                Console.WriteLine("Ingrese el numero de pasajeros");
                input = Console.ReadLine();
                if (int.TryParse(input, out vuelo.num_pasajeros))
                {
                    if (vuelo.num_pasajeros < 1 || vuelo.num_pasajeros > 99)
                    {
                        Console.WriteLine("El numero de pasajeros debe estar entre 1 y 99");
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida, ingrese un entero");
                }
            } while (vuelo.num_pasajeros < 1 || vuelo.num_pasajeros > 99);
            do
            {
                Console.WriteLine("Ingrese la fecha y hora de salida (DD/MM/AA hh:mm): ");
                input = Console.ReadLine();
                if (DateTime.TryParse(input, out vuelo.fecha_salida))
                {
                    if (vuelo.fecha_salida < DateTime.Now)
                    {
                        Console.WriteLine("Opción inválida, debe de ser mayor a la fecha actual.");
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida, ingrese una fecha válida");
                }
            } while (vuelo.fecha_salida < DateTime.Now);
            do
            {
                Console.WriteLine("Ingrese la fecha y hora de llegada (DD/MM/AA hh:mm): ");
                input = Console.ReadLine();
                if (DateTime.TryParse(input, out vuelo.fecha_llegada))
                {
                    if (vuelo.fecha_llegada < vuelo.fecha_salida)
                    {
                        Console.WriteLine("Opción inválida, debe de ser mayor a la fecha de salida.");
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida, ingrese una fecha válida");
                }

            } while (vuelo.fecha_llegada < vuelo.fecha_salida);
            Console.WriteLine("Ingrese la trayectoria");
            vuelo.trayectoria = Console.ReadLine().Split('-');
            do
            {
                Console.WriteLine("Ingrese el precio del boleto");
                input = Console.ReadLine();
                if (double.TryParse(input, out vuelo.precio_boleto))
                {
                    if (vuelo.precio_boleto < 1000 || vuelo.precio_boleto > 99999)
                    {
                        Console.WriteLine("El precio del boleto debe estar entre 1000 y 99,999");
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida, ingrese un numero");
                }
            } while (vuelo.precio_boleto < 1000 || vuelo.precio_boleto > 99999);
            vuelo.ganancias_totales = vuelo.num_pasajeros * vuelo.precio_boleto;
            vuelo.tiempo_trayecto = vuelo.fecha_llegada - vuelo.fecha_salida;
            vuelos.Add(vuelo);
            Console.WriteLine();
        }
        static void BuscarVuelo(List<Vuelo> vuelos)
        {
            int encontrado = 0;
            Console.WriteLine("Ingrese el id del vuelo");
            string id = Console.ReadLine();
            foreach (Vuelo vuelo in vuelos)
            {
                if (vuelo.id_vuelo == id)
                {
                    Console.WriteLine("El vuelo con id " + id + " tiene " + vuelo.num_pasajeros + " pasajeros");
                    Console.WriteLine("El vuelo con id " + id + " tiene un costo de " + vuelo.precio_boleto + " por boleto, acumulando un total de ganancias de $" + vuelo.ganancias_totales);
                    Console.WriteLine("El vuelo con id " + id + " sale de la ciudad de " + vuelo.trayectoria[0] + " con fecha de " + vuelo.fecha_salida);
                    if(vuelo.trayectoria.Length>2)
                    {
                        Console.Write("Haciendo escala en: " + vuelo.trayectoria[1]);
                        for(int i=2;i<vuelo.trayectoria.Length-1;i++)
                        {
                            Console.Write(", " + vuelo.trayectoria[i]);
                        }
                    }
                    Console.WriteLine("El vuelo con id " + id + " llega a la ciudad de " + vuelo.trayectoria[vuelo.trayectoria.Length-1] + " con fecha de " + vuelo.fecha_llegada + " con un tiempo total de trayecto de " + vuelo.tiempo_trayecto);
                    encontrado = 1;
                }
            }
            if (encontrado==0)
            {
                Console.WriteLine("No existe un vuelo con esa ID, verifique sus datos");
            }
        }
        static void OrdenacionAscendente(List<Vuelo> vuelos)
        {
            int k = 0;
            for (int i = 0; i < vuelos.Count; i++)
            {
                for (int j = 0; j < vuelos.Count; j++)
                {
                    if (vuelos[i].precio_boleto < vuelos[j].precio_boleto)
                    {
                        Vuelo aux = vuelos[i];
                        vuelos[i] = vuelos[j];
                        vuelos[j] = aux;
                    }
                }
            }
            foreach (Vuelo vuelo in vuelos)
            {
                if (k < 5)
                {
                    Console.WriteLine("El vuelo con id " + vuelo.id_vuelo + " tiene una ganancia de " + vuelo.ganancias_totales);
                }
                k++;
            }
        }
        static void Resultados(List<Vuelo> vuelos)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|id  |num pasajeros|   fecha salida    |   fecha llegada   |Precio boleto|origen|destino|Tiempo trayecto|Ganancias|");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            foreach (Vuelo vuelo in vuelos)
            {
                Console.Write($"|{vuelo.id_vuelo}|{((vuelo.num_pasajeros < 10) ? "       " + vuelo.num_pasajeros + "     " : "      " + vuelo.num_pasajeros + "     ")}|");
                Console.Write($"{((vuelo.fecha_salida.Day < 10) ? "0" + vuelo.fecha_salida.Day : vuelo.fecha_salida.Day)}/{((vuelo.fecha_salida.Month < 10) ? "0" + vuelo.fecha_salida.Month : vuelo.fecha_salida.Month)}/{vuelo.fecha_salida.Year} {vuelo.fecha_salida.ToString().Substring(vuelo.fecha_salida.ToString().Length - 8)}|");
                Console.Write($"{((vuelo.fecha_llegada.Day < 10) ? "0" + vuelo.fecha_llegada.Day : vuelo.fecha_llegada.Day)}/{((vuelo.fecha_llegada.Month < 10) ? "0" + vuelo.fecha_llegada.Month : vuelo.fecha_llegada.Month)}/{vuelo.fecha_llegada.Year} {vuelo.fecha_llegada.ToString().Substring(vuelo.fecha_llegada.ToString().Length - 8)}|");
                Console.Write($"{((vuelo.precio_boleto < 10000) ? "     " + vuelo.precio_boleto + "    " : "    " + vuelo.precio_boleto + "    ")}|");
                Console.Write($"{((vuelo.trayectoria[0].Length >= 6) ? vuelo.trayectoria[0].Substring(0, 6) + "|" : " " + vuelo.trayectoria[0])}");
                for (int i = 0; i <= (6 - vuelo.trayectoria[0].Length); i++)
                {
                    Console.Write(" ");
                    if (6 - vuelo.trayectoria[0].Length - 1 == i)
                    {
                        Console.Write("|");
                    }
                }
                Console.Write($"{((vuelo.trayectoria[vuelo.trayectoria.Length-1].Length >= 6) ? vuelo.trayectoria[vuelo.trayectoria.Length - 1].Substring(0, 6) + "|" : " " + vuelo.trayectoria[vuelo.trayectoria.Length - 1])}");
                for (int i = 0; i <= (6 - vuelo.trayectoria[vuelo.trayectoria.Length - 1].Length); i++)
                {
                    Console.Write(" ");
                    if (6 - vuelo.trayectoria[vuelo.trayectoria.Length - 1].Length - 1 == i)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine($"    {vuelo.tiempo_trayecto}  |{vuelo.ganancias_totales}");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            }
        }

        static void EditarVuelo(List<Vuelo> vuelos)
        {
            int i = -1;
            Console.WriteLine("Ingrese el id del vuelo a editar");
            string id = Console.ReadLine();
            foreach (Vuelo vuelo in vuelos)
            {
                if (vuelo.id_vuelo == id)
                {
                    i = vuelos.IndexOf(vuelo);
                }
            }
            if (i != -1)
            {
                MenuEditarVuelo(vuelos, i);
            }
            else
            {
                Console.WriteLine("El vuelo con id " + id + " no existe");
            }
        }

        static void MenuEditarVuelo(List<Vuelo> vuelos, int index)
        {
            int opcion = 0;
            Vuelo vuelo = vuelos[index];
            string input;
            do
            {
                Console.WriteLine("1. Editar numero de pasajeros");
                Console.WriteLine("2. Editar fecha de salida");
                Console.WriteLine("3. Editar fecha de llegada");
                Console.WriteLine("4. Editar trayectoria");
                Console.WriteLine("5. Editar precio del boleto");
                Console.WriteLine("6. Salir");
                do
                {
                    Console.WriteLine("Ingrese su elección (1-6)");
                    string opcion_pro = Console.ReadLine();
                    if (int.TryParse(opcion_pro, out opcion))
                    {
                        if (opcion < 1 || opcion > 6)
                        {
                            Console.WriteLine("Opción inválida, debe de ser del 1 al 6");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una opción válida, ingrese un entero");
                    }
                } while (opcion < 1 || opcion > 6);

                switch (opcion)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("Ingrese el numero de pasajeros");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out vuelo.num_pasajeros))
                            {
                                if (vuelo.num_pasajeros < 1 || vuelo.num_pasajeros > 100)
                                {
                                    Console.WriteLine("El numero de pasajeros debe estar entre 1 y 100");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opción inválida, ingrese un entero");
                            }
                            vuelo.ganancias_totales = vuelo.num_pasajeros * vuelo.precio_boleto;
                        } while (vuelo.num_pasajeros < 1 || vuelo.num_pasajeros > 100);
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine("Ingrese la fecha de salida");
                            input = Console.ReadLine();
                            if (DateTime.TryParse(input, out vuelo.fecha_salida))
                            {
                                if (vuelo.fecha_salida < DateTime.Now)
                                {
                                    Console.WriteLine("Opción inválida, debe de ser mayor a la fecha actual.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opción inválida, ingrese una fecha válida");
                            }
                            vuelo.tiempo_trayecto = vuelo.fecha_llegada - vuelo.fecha_salida;
                        } while (vuelo.fecha_salida < DateTime.Now);
                        break;
                    case 3:
                        do
                        {
                            Console.WriteLine("Ingrese la fecha de llegada");
                            input = Console.ReadLine();
                            if (DateTime.TryParse(input, out vuelo.fecha_llegada))
                            {
                                if (vuelo.fecha_llegada < vuelo.fecha_salida)
                                {
                                    Console.WriteLine("Opción inválida, debe de ser mayor a la fecha de salida.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opción inválida, ingrese una fecha válida");
                            }
                            vuelo.tiempo_trayecto = vuelo.fecha_llegada - vuelo.fecha_salida;
                        } while (vuelo.fecha_llegada < vuelo.fecha_salida);
                        break;
                    case 4:
                        Console.WriteLine("Ingrese la nueva trayectoria");
                        vuelo.trayectoria = Console.ReadLine().Split(' ');
                        break;
                    case 5:
                        do
                        {
                            Console.WriteLine("Ingrese el precio del boleto");
                            input = Console.ReadLine();
                            if (double.TryParse(input, out vuelo.precio_boleto))
                            {
                                if (vuelo.precio_boleto < 1)
                                {
                                    Console.WriteLine("Opción inválida, debe de ser mayor a 1.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opción inválida, ingrese un entero");
                            }
                            vuelo.ganancias_totales = vuelo.num_pasajeros * vuelo.precio_boleto;
                        } while (vuelo.precio_boleto < 1);
                        break;
                }
            }
            while (opcion != 6);
            vuelos[index] = vuelo;
        }

        static void EliminarVuelo(List<Vuelo> vuelos)
        {
            int i = -1;
            Console.WriteLine("Ingrese el id del vuelo a eliminar");
            string id = Console.ReadLine();
            foreach (Vuelo vuelo in vuelos)
            {
                if (vuelo.id_vuelo == id)
                {
                    i = vuelos.IndexOf(vuelo);
                }
            }
            if (i != -1)
            {
                vuelos.Remove(vuelos[i]);
                Console.WriteLine("El vuelo con id " + id + " ha sido eliminado");
            }
            else
            {
                Console.WriteLine("El vuelo con id " + id + " no existe");
            }
        }

        static void Menu()
        {
            int opcion = 0;
            List<Vuelo> vuelos = new List<Vuelo>();
            do
            {
                Console.WriteLine("Bienvenide al programa de la aerolínea");
                Console.WriteLine("1.- Entrada de Datos");
                Console.WriteLine("2.- Resultados.");
                Console.WriteLine("3.- Terminar.");
                do
                {
                    Console.Write("Ingrese su elección (1-3):");
                    string opcion_pro = Console.ReadLine();
                    if (int.TryParse(opcion_pro, out opcion))
                    {
                        if (opcion < 1 || opcion > 3)
                        {
                            Console.WriteLine("Opción inválida, debe de ser del 1 al 3");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida,Ingrese un entero");
                    }
                } while (opcion < 1 || opcion > 3);

                switch (opcion)
                {
                    case 1:
                        {
                            Console.WriteLine("Usted está capturando los datos.");
                            Console.WriteLine("---------------------------------");
                            int numVuelos = 0;
                            do
                            {
                                Console.WriteLine("¿Cuántos vuelos desea agregar? ");
                                string x = Console.ReadLine();
                                if (int.TryParse(x, out numVuelos))
                                {
                                    if (numVuelos < 1)
                                    {
                                        Console.WriteLine("El numero de vuelos debe ser mayor a 0");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Opción inválida, ingrese un entero");
                                }
                            } while (numVuelos < 1);
                            for (int i=0; i<numVuelos;i++)
                            {
                                AgregarVuelo(vuelos);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (vuelos.Count != 0)
                            {
                                Console.WriteLine("Usted está emitiendo los resultados.");
                                Console.WriteLine("---------------------------------");
                                Submenu(vuelos);
                            }
                            else
                            {
                                Console.WriteLine("Usted debe capturar los datos");
                            }
                            break;
                        }
                }
            } while (opcion != 3);
        }

        static void Submenu(List<Vuelo> vuelos)
        {
            int opcion2;
            do
            {
                if (vuelos.Count == 0)
                {
                    Console.WriteLine("No hay vuelos registrados");
                    break;
                }
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("1.- Imprimir todos los vuelos.");
                Console.WriteLine("2.- Buscar vuelo por id.");
                Console.WriteLine("3.- Editar vuelo");
                Console.WriteLine("4.- Eliminar vuelo");
                Console.WriteLine("5.- 5 vuelos más baratos");
                Console.WriteLine("6.- Terminar.");
                do
                {
                    Console.Write("\nIngrese su elección (1-6): ");
                    string opcion_pro = Console.ReadLine();
                    if (int.TryParse(opcion_pro, out opcion2))
                    {
                        if (opcion2 < 1 || opcion2 > 6)
                        {
                            Console.WriteLine("Opción inválida, debe de ser del 1 al 6");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida,Ingrese un entero");
                    }
                } while (opcion2 < 1 || opcion2 > 6);

                switch (opcion2)
                {
                    case 1:
                        {
                            Console.WriteLine("Usted está imprimiendo todos los vuelos.");
                            Resultados(vuelos);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Usted entró a la búsqueda de vuelos");
                            Console.WriteLine("---------------------------------");
                            BuscarVuelo(vuelos);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Usted entró a la edición de vuelos");
                            Console.WriteLine("---------------------------------");
                            EditarVuelo(vuelos);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Usted entró a la eliminación de vuelos");
                            Console.WriteLine("---------------------------------");
                            EliminarVuelo(vuelos);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Los 5 vuelos más baratos son:");
                            Console.WriteLine("---------------------------------");
                            OrdenacionAscendente(vuelos);
                            break;
                        }
                }
            } while (opcion2 != 6);
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}