using System;
using System.Collections.Generic;

namespace Juegoteca.Biblioteca.Modelos
{
    public class Usuario
    {
        public string Nombre { get; set; }

        // Diccionario para almacenar las colecciones de juegos
        public Dictionary<string, List<Videojuego>> Colecciones { get; set; }

        public Usuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del usuario no puede estar vacío.");
            }

            Nombre = nombre;
            Colecciones = new Dictionary<string, List<Videojuego>>(StringComparer.OrdinalIgnoreCase);
        }

        public void CrearColeccion(string nombreColeccion)
        {
            if (Colecciones.ContainsKey(nombreColeccion))
            {
                throw new InvalidOperationException($"La colección '{nombreColeccion}' ya existe.");
            }

            Colecciones[nombreColeccion] = new List<Videojuego>();
        }

        public void AgregarJuegoAColeccion(string nombreColeccion, Videojuego juego)
        {
            if (!Colecciones.ContainsKey(nombreColeccion))
            {
                throw new KeyNotFoundException($"No se encontró la colección '{nombreColeccion}'.");
            }

            if (juego == null)
            {
                throw new ArgumentNullException(nameof(juego), "El juego no puede ser nulo.");
            }

            var coleccion = Colecciones[nombreColeccion];

            // Evitar agregar el mismo juego dos veces a la misma colección
            if (!coleccion.Contains(juego))
            {
                coleccion.Add(juego);
            }
        }

        public void EliminarColeccion(string nombreColeccion)
        {
            if (!Colecciones.ContainsKey(nombreColeccion))
            {
                throw new KeyNotFoundException($"No se puede eliminar porque la colección '{nombreColeccion}' no existe.");
            }

            Colecciones.Remove(nombreColeccion);
        }

        public List<Videojuego> ObtenerColeccion(string nombreColeccion)
        {
            if (Colecciones.TryGetValue(nombreColeccion, out var coleccion))
            {
                return coleccion;
            }

            throw new KeyNotFoundException($"No se encontró la colección '{nombreColeccion}'.");
        }

        public int ContarJuegos()
        {
            int total = 0;
            foreach (var coleccion in Colecciones.Values)
            {
                total += coleccion.Count;
            }
            return total;
        }

        public void MostrarColecciones()
        {
            Console.WriteLine($"\n--- Colecciones de {Nombre} ---");
            if (Colecciones.Count == 0)
            {
                Console.WriteLine("No hay colecciones creadas.");
                return;
            }

            foreach (var kvp in Colecciones)
            {
                Console.WriteLine($"Colección: {kvp.Key} ({kvp.Value.Count} juegos)");
                foreach (var juego in kvp.Value)
                {
                    Console.WriteLine($"  - {juego.ToString()}");
                }
            }
            Console.WriteLine("---------------------------------\n");
        }
    }
}