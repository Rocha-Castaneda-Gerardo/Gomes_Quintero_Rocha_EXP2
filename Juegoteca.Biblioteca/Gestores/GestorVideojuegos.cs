using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores
{
    /// <summary>
    /// Gestiona el catálogo de videojuegos, incluyendo agregar, eliminar, buscar y algoritmos de ordenamiento/búsqueda.
    /// </summary>
    public class GestorVideojuegos : IGestorVideojuegos
    {
        public List<Videojuego> JuegosDisponibles { get; set; }

        public GestorVideojuegos()
        {
            JuegosDisponibles = new List<Videojuego>();
        }

        public void AgregarJuego(Videojuego juego)
        {
            if (juego != null && !JuegosDisponibles.Exists(j => j.Titulo.Equals(juego.Titulo, StringComparison.OrdinalIgnoreCase)))
            {
                JuegosDisponibles.Add(juego);
            }
        }

        public bool EliminarJuego(Videojuego juego)
        {
            return JuegosDisponibles.Remove(juego);
        }

        public bool EliminarPorIndice(int indice)
        {
            if (indice >= 0 && indice < JuegosDisponibles.Count)
            {
                JuegosDisponibles.RemoveAt(indice);
                return true;
            }
            return false;
        }

        public List<Videojuego> BuscarPorTitulo(string texto)
        {
            var resultados = new List<Videojuego>();
            if (string.IsNullOrWhiteSpace(texto)) return resultados;

            foreach (var juego in JuegosDisponibles)
            {
                if (juego.Titulo.Contains(texto, StringComparison.OrdinalIgnoreCase))
                {
                    resultados.Add(juego);
                }
            }
            return resultados;
        }

        public void LimpiarCatalogo()
        {
            JuegosDisponibles.Clear();
        }

        // --- FASE 2: ALGORITMOS ---

        public void QuickSort(List<Videojuego> lista)
        {
            if (lista == null || lista.Count <= 1) return;
            QuickSortRecursive(lista, 0, lista.Count - 1);
        }

        private void QuickSortRecursive(List<Videojuego> lista, int baja, int alta)
        {
            if (baja < alta)
            {
                int pi = Particionar(lista, baja, alta);
                QuickSortRecursive(lista, baja, pi - 1);
                QuickSortRecursive(lista, pi + 1, alta);
            }
        }

        private int Particionar(List<Videojuego> lista, int baja, int alta)
        {
            int pivot = lista[alta].HorasJuego;
            int i = (baja - 1);
            for (int j = baja; j < alta; j++)
            {
                if (lista[j].HorasJuego < pivot)
                {
                    i++;
                    var temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }
            var tempSwap = lista[i + 1];
            lista[i + 1] = lista[alta];
            lista[alta] = tempSwap;
            return i + 1;
        }

        public List<Videojuego> MergeSort(List<Videojuego> lista)
        {
            if (lista == null) return new List<Videojuego>();
            if (lista.Count <= 1) return new List<Videojuego>(lista);

            int medio = lista.Count / 2;
            var izquierda = lista.GetRange(0, medio);
            var derecha = lista.GetRange(medio, lista.Count - medio);

            return UnirMerge(MergeSort(izquierda), MergeSort(derecha));
        }

        private List<Videojuego> UnirMerge(List<Videojuego> izq, List<Videojuego> der)
        {
            var resultado = new List<Videojuego>();
            int i = 0, j = 0;
            while (i < izq.Count && j < der.Count)
            {
                if (izq[i].HorasJuego <= der[j].HorasJuego)
                {
                    resultado.Add(izq[i]);
                    i++;
                }
                else
                {
                    resultado.Add(der[j]);
                    j++;
                }
            }
            while (i < izq.Count) { resultado.Add(izq[i]); i++; }
            while (j < der.Count) { resultado.Add(der[j]); j++; }
            return resultado;
        }

        public int BusquedaBinaria(string titulo)
        {
            if (JuegosDisponibles == null || JuegosDisponibles.Count == 0) return -1;

            // Ordena una copia por Título (sin alterar el original)
            var copia = new List<Videojuego>(JuegosDisponibles);
            for (int i = 0; i < copia.Count - 1; i++)
            {
                for (int j = 0; j < copia.Count - i - 1; j++)
                {
                    if (string.Compare(copia[j].Titulo, copia[j + 1].Titulo, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        var temp = copia[j];
                        copia[j] = copia[j + 1];
                        copia[j + 1] = temp;
                    }
                }
            }

            int baja = 0;
            int alta = copia.Count - 1;

            while (baja <= alta)
            {
                int medio = baja + (alta - baja) / 2;
                int comparacion = string.Compare(copia[medio].Titulo, titulo, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0) return medio;
                if (comparacion < 0) baja = medio + 1;
                else alta = medio - 1;
            }

            return -1;
        }
    }
}