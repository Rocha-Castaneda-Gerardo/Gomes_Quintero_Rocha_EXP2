using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores
{
    /// <summary>
    /// Gestiona el catálogo de videojuegos disponibles, su búsqueda y ordenamiento.
    /// </summary>
    public class GestorVideojuegos : IGestorVideojuegos
    {
        // Propiedad requerida por IGestorVideojuegos
        public List<Videojuego> JuegosDisponibles { get; private set; }

        /// <summary>
        /// Constructor sin parámetros. Cada instancia inicializa su propia lista.
        /// </summary>
        public GestorVideojuegos()
        {
            JuegosDisponibles = new List<Videojuego>();
        }

        // FASE 1: MÉTODOS BÁSICOS DEL CATÁLOGO (Rama: feature/gestor)

        /// <summary>
        /// Añade un videojuego al catálogo evitando duplicados (comparando título sin importar mayúsculas).
        /// </summary>
        public void AgregarJuego(Videojuego juego)
        {
            if (juego == null) return;

            // Verificar si ya existe un juego con el mismo título
            bool yaExiste = false;
            foreach (var item in JuegosDisponibles)
            {
                if (item.Titulo.Equals(juego.Titulo, StringComparison.OrdinalIgnoreCase))
                {
                    yaExiste = true;
                    break;
                }
            }

            if (!yaExiste)
            {
                JuegosDisponibles.Add(juego);
            }
        }

        /// <summary>
        /// Elimina un videojuego específico del catálogo.
        /// </summary>
        public bool EliminarJuego(Videojuego juego)
        {
            if (juego == null || JuegosDisponibles.Count == 0)
                return false;

            // Buscamos coincidencia por referencia o por título
            for (int i = 0; i < JuegosDisponibles.Count; i++)
            {
                if (JuegosDisponibles[i] == juego ||
                    JuegosDisponibles[i].Titulo.Equals(juego.Titulo, StringComparison.OrdinalIgnoreCase))
                {
                    JuegosDisponibles.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Elimina el videojuego ubicado en el índice especificado.
        /// </summary>
        public bool EliminarPorIndice(int indice)
        {
            if (indice < 0 || indice >= JuegosDisponibles.Count)
                return false;

            JuegosDisponibles.RemoveAt(indice);
            return true;
        }

        /// <summary>
        /// Busca videojuegos cuyo título contenga el texto indicado (ignorando mayúsculas/minúsculas).
        /// </summary>
        public List<Videojuego> BuscarPorTitulo(string texto)
        {
            List<Videojuego> resultados = new List<Videojuego>();

            if (string.IsNullOrWhiteSpace(texto) || JuegosDisponibles.Count == 0)
                return resultados;

            foreach (var juego in JuegosDisponibles)
            {
                if (juego.Titulo.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultados.Add(juego);
                }
            }

            return resultados;
        }

        /// <summary>
        /// Vacía completamente la lista del catálogo actual.
        /// </summary>
        public void LimpiarCatalogo()
        {
            JuegosDisponibles.Clear();
        }

        // FASE 2: ALGORITMOS MANUALES (Rama: feature/algoritmos)
        // NOTA: Prohibido usar List.Sort(), OrderBy() o List.BinarySearch().
        // En Fase 1 puedes dejar estos métodos lanzando NotImplementedException
        // o retornando valores vacíos hasta llegar a Fase 2.
   
        /// <summary>
        /// Ordena EN EL LUGAR la lista recibida, de forma ascendente por HorasJuego usando QuickSort.
        /// </summary>
        public void QuickSort(List<Videojuego> lista)
        {
            if (lista == null || lista.Count <= 1)
                return;

            QuickSortRecursivo(lista, 0, lista.Count - 1);
        }

        private void QuickSortRecursivo(List<Videojuego> lista, int izquierda, int derecha)
        {
            if (izquierda < derecha)
            {
                int indicePivote = Particionar(lista, izquierda, derecha);
                QuickSortRecursivo(lista, izquierda, indicePivote - 1);
                QuickSortRecursivo(lista, indicePivote + 1, derecha);
            }
        }

        private int Particionar(List<Videojuego> lista, int izquierda, int derecha)
        {
            int pivoteHoras = lista[derecha].HorasJuego;
            int i = izquierda - 1;

            for (int j = izquierda; j < derecha; j++)
            {
                if (lista[j].HorasJuego <= pivoteHoras)
                {
                    i++;
                    // Intercambiar elementos
                    Videojuego temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }

            // Colocar el pivote en su posición correcta
            Videojuego tempPivote = lista[i + 1];
            lista[i + 1] = lista[derecha];
            lista[derecha] = tempPivote;

            return i + 1;
        }

        /// <summary>
        /// Retorna una NUEVA lista ordenada ascendentemente por HorasJuego sin modificar la original.
        /// </summary>
        public List<Videojuego> MergeSort(List<Videojuego> lista)
        {
            if (lista == null)
                return new List<Videojuego>();

            if (lista.Count <= 1)
                return new List<Videojuego>(lista);

            int medio = lista.Count / 2;
            List<Videojuego> izquierda = new List<Videojuego>();
            List<Videojuego> derecha = new List<Videojuego>();

            for (int i = 0; i < medio; i++)
                izquierda.Add(lista[i]);

            for (int i = medio; i < lista.Count; i++)
                derecha.Add(lista[i]);

            izquierda = MergeSort(izquierda);
            derecha = MergeSort(derecha);

            return Mezclar(izquierda, derecha);
        }

        private List<Videojuego> Mezclar(List<Videojuego> izquierda, List<Videojuego> derecha)
        {
            List<Videojuego> resultado = new List<Videojuego>();
            int i = 0, j = 0;

            while (i < izquierda.Count && j < derecha.Count)
            {
                if (izquierda[i].HorasJuego <= derecha[j].HorasJuego)
                {
                    resultado.Add(izquierda[i]);
                    i++;
                }
                else
                {
                    resultado.Add(derecha[j]);
                    j++;
                }
            }

            while (i < izquierda.Count)
            {
                resultado.Add(izquierda[i]);
                i++;
            }

            while (j < derecha.Count)
            {
                resultado.Add(derecha[j]);
                j++;
            }

            return resultado;
        }

        /// <summary>
        /// Ordena por Titulo una COPIA de JuegosDisponibles y retorna el índice del juego encontrado, o -1.
        /// </summary>
        public int BusquedaBinaria(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo) || JuegosDisponibles == null || JuegosDisponibles.Count == 0)
                return -1;

            // 1. Crear una copia para no alterar JuegosDisponibles
            List<Videojuego> copiaOrdenada = new List<Videojuego>(JuegosDisponibles);

            // 2. Ordenar la copia alfabéticamente por Titulo (ordenamiento manual Insertion/Bubble Sort)
            for (int i = 0; i < copiaOrdenada.Count - 1; i++)
            {
                for (int j = 0; j < copiaOrdenada.Count - i - 1; j++)
                {
                    if (string.Compare(copiaOrdenada[j].Titulo, copiaOrdenada[j + 1].Titulo, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        Videojuego temp = copiaOrdenada[j];
                        copiaOrdenada[j] = copiaOrdenada[j + 1];
                        copiaOrdenada[j + 1] = temp;
                    }
                }
            }

            // 3. Ejecutar Búsqueda Binaria sobre la copia ordenada
            int inicio = 0;
            int fin = copiaOrdenada.Count - 1;

            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;
                int comparacion = string.Compare(copiaOrdenada[medio].Titulo, titulo, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                    return medio; // Encontrado en la lista ordenada
                else if (comparacion < 0)
                    inicio = medio + 1;
                else
                    fin = medio - 1;
            }

            return -1; // No encontrado
        }
    }
}