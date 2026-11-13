using System;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Gestores;
// using Juegoteca.Biblioteca.Exportadores; // Descomenta y ajusta según el namespace que usó Fernando

namespace Juegoteca.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== INICIANDO SISTEMA JUEGOTECA ===\n");

            // 1. Prueba de Modelos (Tu parte) y Gestor (Sebastián)
            GestorVideojuegos gestor = new GestorVideojuegos();
            gestor.AgregarJuego(new Videojuego("Hades", "Supergiant", 60));
            gestor.AgregarJuego(new Videojuego("Tetris", "Pajitnov", 5));
            gestor.AgregarJuego(new Videojuego("Minecraft", "Mojang", 300));

            Console.WriteLine("Catálogo poblado con éxito.");

            // 2. Prueba de Algoritmos (Sebastián)
            gestor.QuickSort(gestor.JuegosDisponibles);
            Console.WriteLine("\nCatálogo ordenado por horas de juego:");
            foreach (var juego in gestor.JuegosDisponibles)
            {
                Console.WriteLine($" - {juego.ToString()}");
            }

            // 3. Prueba de Colecciones de Usuario (Tu parte)
            Usuario usuario = new Usuario("Gerardo");
            usuario.CrearColeccion("Favoritos");

            // Agregamos el primer juego de la lista ordenada
            usuario.AgregarJuegoAColeccion("Favoritos", gestor.JuegosDisponibles[0]);
            usuario.MostrarColecciones();

            // 4. Prueba del Exportador (Fernando)
            Console.WriteLine("Exportando datos a archivo de texto...");

            // Reemplaza "ExportadorTxt" con el nombre exacto de la clase de Fernando
            // ExportadorTxt exportador = new ExportadorTxt(); 

            // Usa una ruta relativa simple en lugar de absolutas (como C:\Users\...) para evitar errores de permisos
            // string rutaArchivo = "coleccion_gerardo.txt"; 
            // exportador.ExportarATxt(usuario, rutaArchivo);

            // Console.WriteLine($"¡Exportación completada! Revisa el archivo en la carpeta del proyecto.");
        }
    }
}