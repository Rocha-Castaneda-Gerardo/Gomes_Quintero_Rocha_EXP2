using System;
using System.Collections.Generic;
using System.IO;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;

var servicio = new ServicioVideojuegos();
servicio.CargarCatalogoInicial();

Console.WriteLine("=== SISTEMA DE GESTIÓN DE VIDEOJUEGOS ===");
Console.Write("Nombre del jugador: ");
string? nombre = Console.ReadLine();
var usuario = servicio.RegistrarUsuario(string.IsNullOrWhiteSpace(nombre) ? "Jugador 1" : nombre);

const string colInicial = "Favoritos";
servicio.CrearColeccion(colInicial);
var cat = servicio.ObtenerCatalogo();
if (cat.Count >= 2)
{
    servicio.AgregarJuegoAColeccion(colInicial, cat[0]);
    servicio.AgregarJuegoAColeccion(colInicial, cat[1]);
}
Console.WriteLine($"Bienvenido {usuario.Nombre}. Colección '{colInicial}' creada con 2 juegos.");

bool salir = false;
while (!salir)
{
    Console.WriteLine("\n1. Ver catálogo disponible");
    Console.WriteLine("2. Agregar videojuego al catálogo");
    Console.WriteLine("3. Crear nueva colección");
    Console.WriteLine("4. Agregar juego del catálogo a una colección");
    Console.WriteLine("5. Ver colecciones del jugador");
    Console.WriteLine("6. Buscar videojuego por título");
    Console.WriteLine("7. Ordenar catálogo por horas de juego");
    Console.WriteLine("8. Exportar colecciones a .txt");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    try
    {
        switch (Console.ReadLine())
        {
            case "1":
                Mostrar(servicio.ObtenerCatalogo(), "CATÁLOGO");
                break;
            case "2":
                Console.Write("Título: "); string t = Console.ReadLine() ?? "";
                Console.Write("Estudio: "); string e = Console.ReadLine() ?? "";
                Console.Write("Horas: ");
                if (int.TryParse(Console.ReadLine(), out int h) && h >= 0)
                {
                    servicio.AgregarJuegoAlCatalogo(t, e, h);
                    Console.WriteLine("Juego agregado.");
                }
                else Console.WriteLine("Horas inválidas.");
                break;
            case "3":
                Console.Write("Nombre de colección: ");
                servicio.CrearColeccion(Console.ReadLine() ?? "");
                Console.WriteLine("Colección creada.");
                break;
            case "4":
                var juegos = servicio.ObtenerCatalogo();
                Mostrar(juegos, "ELIGE UN JUEGO");
                Console.Write("Número: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= juegos.Count)
                {
                    Console.Write("Colección destino: ");
                    servicio.AgregarJuegoAColeccion(Console.ReadLine() ?? "", juegos[idx - 1]);
                    Console.WriteLine("Agregado a la colección.");
                }
                else Console.WriteLine("Número inválido.");
                break;
            case "5":
                Console.WriteLine($"Total de juegos: {usuario.ContarJuegos()}");
                usuario.MostrarColecciones();
                break;
            case "6":
                Console.Write("Título a buscar: ");
                string q = Console.ReadLine() ?? "";
                Mostrar(servicio.BuscarPorTitulo(q), "RESULTADOS");
                int bin = servicio.BuscarBinarioPorTitulo(q);
                if (bin >= 0) Console.WriteLine($"Índice binario exacto: {bin}");
                break;
            case "7":
                Console.Write("Algoritmo (1 = QuickSort, 2 = MergeSort): ");
                var ord = Console.ReadLine() == "2"
                    ? servicio.ObtenerCatalogoOrdenadoMergeSort()
                    : servicio.ObtenerCatalogoOrdenadoQuickSort();
                Mostrar(ord, "CATÁLOGO ORDENADO");
                break;
            case "8":
                Console.Write("Archivo (Enter para 'colecciones.txt'): ");
                string? ruta = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ruta)) ruta = "colecciones.txt";
                servicio.ExportarColeccionesATxt(ruta);
                Console.WriteLine($"Exportado en: {Path.GetFullPath(ruta)}");
                break;
            case "0":
                salir = true;
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void Mostrar(List<Videojuego> lista, string titulo)
{
    Console.WriteLine($"--- {titulo} ---");
    if (lista.Count == 0) { Console.WriteLine("Sin resultados."); return; }
    for (int i = 0; i < lista.Count; i++) Console.WriteLine($"{i + 1}. {lista[i]}");
}