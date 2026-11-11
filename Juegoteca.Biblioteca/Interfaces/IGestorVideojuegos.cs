using System.Collections.Generic;
using Juegoteca.Biblioteca.Modelos; // Importamos la carpeta de modelos

namespace Juegoteca.Biblioteca.Interfaces
{
    public interface IGestorVideojuegos
    {
        // Propiedad del catálogo
        List<Videojuego> JuegosDisponibles { get; }

        // Métodos de gestión
        void AgregarJuego(Videojuego juego);
        bool EliminarJuego(Videojuego juego);
        bool EliminarPorIndice(int indice);
        List<Videojuego> BuscarPorTitulo(string texto);
        void LimpiarCatalogo();

        // Algoritmos de ordenamiento y búsqueda
        void QuickSort(List<Videojuego> lista);
        List<Videojuego> MergeSort(List<Videojuego> lista);
        int BusquedaBinaria(string titulo);
    }
}