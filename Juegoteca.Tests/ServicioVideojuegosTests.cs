using System;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;
using Xunit;

namespace Juegoteca.Tests
{
    public class ServicioVideojuegosTests
    {
        private class ExportadorFalso : IExportador
        {
            public bool FueLlamado { get; private set; }
            public string RutaRecibida { get; private set; } = string.Empty;
            public void ExportarATxt(Usuario usuario, string rutaArchivo)
            {
                FueLlamado = true;
                RutaRecibida = rutaArchivo;
            }
        }

        [Fact]
        public void RegistrarUsuario_NombreValido_AsignaUsuarioActual()
        {
            var servicio = new ServicioVideojuegos();
            var usuario = servicio.RegistrarUsuario("Fernando");
            Assert.NotNull(servicio.UsuarioActual);
            Assert.Equal("Fernando", usuario.Nombre);
        }

        [Fact]
        public void CargarCatalogoYAgregarAColeccion_FlujoCompleto_Funciona()
        {
            var servicio = new ServicioVideojuegos();
            servicio.RegistrarUsuario("Fernando");
            servicio.CargarCatalogoInicial();
            servicio.CrearColeccion("RPGs");
            var juego = servicio.ObtenerCatalogo()[0];
            servicio.AgregarJuegoAColeccion("RPGs", juego);
            Assert.Single(servicio.ObtenerColeccion("RPGs"));
        }

        [Fact]
        public void ExportarColeccionesATxt_ConUsuario_InvocaExportador()
        {
            var mock = new ExportadorFalso();
            var servicio = new ServicioVideojuegos(new GestorVideojuegos(), mock);
            servicio.RegistrarUsuario("Fernando");
            servicio.CrearColeccion("Favoritos");
            servicio.ExportarColeccionesATxt("salida.txt");
            Assert.True(mock.FueLlamado);
            Assert.Equal("salida.txt", mock.RutaRecibida);
        }

        [Fact]
        public void CrearColeccion_SinUsuario_LanzaExcepcion()
        {
            var servicio = new ServicioVideojuegos();
            Assert.Throws<InvalidOperationException>(() => servicio.CrearColeccion("Favoritos"));
        }
    }
}
