using System;
using System.Collections.Generic;
using Xunit;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public void Constructor_NombreValido_InicializaDiccionarioVacio()
        {
            // Arrange
            string nombre = "Gerardo";

            // Act
            Usuario usuario = new Usuario(nombre);

            // Assert
            Assert.Equal(nombre, usuario.Nombre);
            Assert.NotNull(usuario.Colecciones);
            Assert.Empty(usuario.Colecciones);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_NombreInvalido_LanzaArgumentException(string nombreInvalido)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Usuario(nombreInvalido));
        }

        [Fact]
        public void CrearColeccion_ColeccionNueva_AgregaAlDiccionario()
        {
            // Arrange
            Usuario usuario = new Usuario("Fernando");
            string nombreColeccion = "Favoritos";

            // Act
            usuario.CrearColeccion(nombreColeccion);

            // Assert
            Assert.True(usuario.Colecciones.ContainsKey(nombreColeccion));
        }

        [Fact]
        public void CrearColeccion_ColeccionDuplicada_LanzaInvalidOperationException()
        {
            // Arrange
            Usuario usuario = new Usuario("Sebastián");
            string nombreColeccion = "Pendientes";
            usuario.CrearColeccion(nombreColeccion);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => usuario.CrearColeccion(nombreColeccion));
        }

        [Fact]
        public void AgregarJuegoAColeccion_ColeccionExistente_AgregaJuego()
        {
            // Arrange
            Usuario usuario = new Usuario("Gerardo");
            string nombreColeccion = "Completados";
            usuario.CrearColeccion(nombreColeccion);
            Videojuego juego = new Videojuego("Hollow Knight", "Team Cherry", 40);

            // Act
            usuario.AgregarJuegoAColeccion(nombreColeccion, juego);

            // Assert
            var coleccionRecuperada = usuario.ObtenerColeccion(nombreColeccion);
            Assert.Single(coleccionRecuperada);
            Assert.Contains(juego, coleccionRecuperada);
        }

        [Fact]
        public void AgregarJuegoAColeccion_ColeccionNoExiste_LanzaKeyNotFoundException()
        {
            // Arrange
            Usuario usuario = new Usuario("Fernando");
            Videojuego juego = new Videojuego("Doom", "id Software", 15);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => usuario.AgregarJuegoAColeccion("Inexistente", juego));
        }

        [Fact]
        public void ContarJuegos_MultiplesColecciones_RetornaTotalCorrecto()
        {
            // Arrange
            Usuario usuario = new Usuario("Sebastián");
            usuario.CrearColeccion("RPG");
            usuario.CrearColeccion("Accion");

            Videojuego juego1 = new Videojuego("The Witcher 3", "CD Projekt", 100);
            Videojuego juego2 = new Videojuego("Elden Ring", "FromSoftware", 80);
            Videojuego juego3 = new Videojuego("Devil May Cry 5", "Capcom", 20);

            usuario.AgregarJuegoAColeccion("RPG", juego1);
            usuario.AgregarJuegoAColeccion("RPG", juego2);
            usuario.AgregarJuegoAColeccion("Accion", juego3);

            // Act
            int total = usuario.ContarJuegos();

            // Assert
            Assert.Equal(3, total);
        }
    }
}