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
            } while (vuelo.fecha_salida < DateTime.Now);
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

            } while (vuelo.fecha_llegada < vuelo.fecha_salida);
            Console.WriteLine("Ingrese la trayectoria");
            vuelo.trayectoria = Console.ReadLine().Split(' ');
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
        }
        static void BuscarVuelo(List<Vuelo> vuelos)
        {
            Console.WriteLine("Ingrese el id del vuelo");
            string id = Console.ReadLine();
            foreach (Vuelo vuelo in vuelos)
            {
                if (vuelo.id_vuelo == id)
                {
                    Console.WriteLine("El vuelo con id " + id + " tiene " + vuelo.num_pasajeros + " pasajeros");
                    Console.WriteLine("El vuelo con id " + id + " tiene una duracion de " + vuelo.tiempo_trayecto);
                    Console.WriteLine("El vuelo con id " + id + " tiene una ganancia de " + vuelo.ganancias_totales);
                }
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
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("|id  |num pasajeros|   fecha salida    |   fecha llegada   |Precio boleto|origen|destino|");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            foreach (Vuelo vuelo in vuelos)
            {
                Console.Write($"|{vuelo.id_vuelo}|{((vuelo.num_pasajeros<10)?"       "+vuelo.num_pasajeros+"     ":"      "+vuelo.num_pasajeros+"     ")}|");
                Console.Write($"{((vuelo.fecha_salida.Day<10)?"0"+vuelo.fecha_salida.Day:vuelo.fecha_salida.Day)}/{((vuelo.fecha_salida.Month<10)?"0"+vuelo.fecha_salida.Month:vuelo.fecha_salida.Month)}/{vuelo.fecha_salida.Year} {vuelo.fecha_salida.ToString().Substring(vuelo.fecha_salida.ToString().Length-8)}|" );
                Console.Write($"{((vuelo.fecha_llegada.Day<10)?"0"+vuelo.fecha_llegada.Day:vuelo.fecha_llegada.Day)}/{((vuelo.fecha_llegada.Month<10)?"0"+vuelo.fecha_llegada.Month:vuelo.fecha_llegada.Month)}/{vuelo.fecha_llegada.Year} {vuelo.fecha_llegada.ToString().Substring(vuelo.fecha_llegada.ToString().Length-8)}|");
                Console.Write($"{((vuelo.precio_boleto<10000)?"     "+vuelo.precio_boleto+"    ":"    "+vuelo.precio_boleto+"    ")}|");
                Console.Write($"{((vuelo.trayectoria[0].Length>=6)?vuelo.trayectoria[0].Substring(0,6)+"|":" "+vuelo.trayectoria[0])}");
                for(int i=0; i<(6-vuelo.trayectoria[0].Length);i++){
                    Console.Write(" ");
                    if(6-vuelo.trayectoria[1].Length-1==i){
                        Console.Write("|");
                    }
                }
                Console.Write($"{((vuelo.trayectoria[1].Length>=6)?vuelo.trayectoria[1].Substring(0,6)+"|":" "+vuelo.trayectoria[1])}");
                for(int i=0; i<(6-vuelo.trayectoria[1].Length);i++){
                    Console.Write(" ");
                    if(6-vuelo.trayectoria[1].Length-1==i){
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------------------------------");
            }
        }
        static void Menu()
        {
            int opcion = 0;
            bool band = false;
            List<Vuelo> vuelos = new List<Vuelo>();
            do
            {
                Console.WriteLine("Bienvenide al programa de la aerolínea");
                Console.WriteLine("1.- Entrada de Datos");
                Console.WriteLine("2.- Resultados.");
                Console.WriteLine("3.- Buscar vuelo por id.");
                Console.WriteLine("4.- 5 vuelos más baratos");
                Console.WriteLine("5.- Terminar.");
                do
                {
                    Console.Write("Ingrese su elección (1-5):");
                    string opcion_pro = (Console.ReadLine());
                    if (int.TryParse(opcion_pro, out opcion))
                    {
                        if (opcion < 1 || opcion > 5)
                        {
                            Console.WriteLine("Opción inválida, debe de ser 1, 2 o 3.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida,Ingrese un entero");
                    }
                } while (opcion < 1 || opcion > 5);

                switch (opcion)
                {
                    case 1:
                        {
                            Console.WriteLine("Usted está capturando los datos.");
                            Console.WriteLine("---------------------------------");
                            AgregarVuelo(vuelos);
                            band = true;
                            break;
                        }
                    case 2:
                        {
                            if (band)
                            {
                                Console.WriteLine("Usted está emitiendo los resultados.");
                                Resultados(vuelos);
                            }
                            else
                            {
                                Console.WriteLine("Usted debe capturar los datos");
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Usted entró a la búsqueda de vuelos");
                            Console.WriteLine("---------------------------------");
                            if (band)
                            {
                                BuscarVuelo(vuelos);
                            }
                            else
                            {
                                Console.WriteLine("Usted debe capturar los datos");
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Los 5 vuelos más baratos son:");
                            Console.WriteLine("---------------------------------");
                            if (band)
                            {
                                OrdenacionAscendente(vuelos);
                            }
                            else
                            {
                                Console.WriteLine("Usted debe capturar los datos");
                            }
                            break;
                        }
                }
            } while (opcion != 5);

        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
