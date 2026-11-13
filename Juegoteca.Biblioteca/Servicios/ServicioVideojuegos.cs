using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios
{
    public class ServicioVideojuegos
    {
        public IGestorVideojuegos Gestor { get; }
        public IExportador Exportador { get; }
        public Usuario? UsuarioActual { get; private set; }

        public ServicioVideojuegos() : this(new GestorVideojuegos(), new ExportadorTxt()) { }

        public ServicioVideojuegos(IGestorVideojuegos gestor, IExportador exportador)
        {
            Gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
            Exportador = exportador ?? throw new ArgumentNullException(nameof(exportador));
        }

        public Usuario RegistrarUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.", nameof(nombre));
            UsuarioActual = new Usuario(nombre.Trim());
            return UsuarioActual;
        }

        public void CargarCatalogoInicial()
        {
            if (Gestor.JuegosDisponibles.Count > 0) return;
            Gestor.AgregarJuego(new Videojuego("The Legend of Zelda: Breath of the Wild", "Nintendo", 120));
            Gestor.AgregarJuego(new Videojuego("Hollow Knight", "Team Cherry", 45));
            Gestor.AgregarJuego(new Videojuego("Elden Ring", "FromSoftware", 150));
            Gestor.AgregarJuego(new Videojuego("Celeste", "Maddy Makes Games", 20));
            Gestor.AgregarJuego(new Videojuego("Red Dead Redemption 2", "Rockstar Games", 95));
        }

        public void AgregarJuegoAlCatalogo(string titulo, string estudio, int horasJuego) =>
            Gestor.AgregarJuego(new Videojuego(titulo, estudio, horasJuego));

        public List<Videojuego> ObtenerCatalogo() => Gestor.JuegosDisponibles;

        public void CrearColeccion(string nombreColeccion)
        {
            ValidarUsuario();
            UsuarioActual!.CrearColeccion(nombreColeccion);
        }

        public void AgregarJuegoAColeccion(string nombreColeccion, Videojuego juego)
        {
            ValidarUsuario();
            UsuarioActual!.AgregarJuegoAColeccion(nombreColeccion, juego);
        }

        public List<Videojuego> ObtenerColeccion(string nombreColeccion)
        {
            ValidarUsuario();
            return UsuarioActual!.ObtenerColeccion(nombreColeccion);
        }

        public List<Videojuego> BuscarPorTitulo(string texto) => Gestor.BuscarPorTitulo(texto);
        public int BuscarBinarioPorTitulo(string titulo) => Gestor.BusquedaBinaria(titulo);

        public List<Videojuego> ObtenerCatalogoOrdenadoQuickSort()
        {
            var copia = new List<Videojuego>(Gestor.JuegosDisponibles);
            Gestor.QuickSort(copia);
            return copia;
        }

        public List<Videojuego> ObtenerCatalogoOrdenadoMergeSort() =>
            Gestor.MergeSort(new List<Videojuego>(Gestor.JuegosDisponibles));

        public void ExportarColeccionesATxt(string rutaArchivo)
        {
            ValidarUsuario();
            Exportador.ExportarATxt(UsuarioActual!, rutaArchivo);
        }

        private void ValidarUsuario()
        {
            if (UsuarioActual == null)
                throw new InvalidOperationException("Primero debes registrar un jugador.");
        }
    }
}
