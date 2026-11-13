using System.Collections.Generic;
using Xunit;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;

namespace Juegoteca.Tests
{
    public class GestorVideojuegosTests
    {
        private readonly GestorVideojuegos _gestor;

        // xUnit ejecuta este constructor antes de cada prueba (Página 21 y 24)
        public GestorVideojuegosTests()
        {
            _gestor = DatosPrueba.GestorPoblado();
        }

        // 1. Constructor
        [Fact]
        public void Constructor_InicializaListaVaciaEIndependiente()
        {
            var gestor1 = new GestorVideojuegos();
            var gestor2 = new GestorVideojuegos();

            Assert.NotNull(gestor1.JuegosDisponibles);
            Assert.Empty(gestor1.JuegosDisponibles);

            gestor1.AgregarJuego(new Videojuego("Doom", "id Software", 15));
            Assert.Single(gestor1.JuegosDisponibles);
            Assert.Empty(gestor2.JuegosDisponibles);
        }

        // 2. AgregarJuego(juego)
        [Fact]
        public void AgregarJuego_JuegoNuevo_AumentaCatalogoYEvitaDuplicados()
        {
            var nuevoJuego = new Videojuego("Hollow Knight: Silksong", "Team Cherry", 50);

            _gestor.AgregarJuego(nuevoJuego);
            Assert.Equal(9, _gestor.JuegosDisponibles.Count);

            // Intentar agregar el mismo juego otra vez (debe mantenerse en 9)
            _gestor.AgregarJuego(new Videojuego("HOLLOW KNIGHT: SILKSONG", "Team Cherry", 50));
            Assert.Equal(9, _gestor.JuegosDisponibles.Count);
        }

        // 3. EliminarJuego / EliminarPorIndice
        [Fact]
        public void EliminarJuego_JuegoExistenteYNoExistente_RetornaResultadoEsperado()
        {
            var juegoExistente = _gestor.JuegosDisponibles[0];
            bool eliminado = _gestor.EliminarJuego(juegoExistente);

            Assert.True(eliminado);
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);

            // Intentar eliminar un juego que no está en el catálogo
            var inexistente = new Videojuego("Juego Fantasma", "Nadie", 10);
            Assert.False(_gestor.EliminarJuego(inexistente));
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);
        }

        [Fact]
        public void EliminarPorIndice_IndiceValidoEInvalido_RetornaResultadoEsperado()
        {
            bool eliminado = _gestor.EliminarPorIndice(0);
            Assert.True(eliminado);
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);

            // Índices fuera de rango
            Assert.False(_gestor.EliminarPorIndice(-1));
            Assert.False(_gestor.EliminarPorIndice(99));
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);
        }

        // 4. BuscarPorTitulo(texto)
        [Fact]
        public void BuscarPorTitulo_CoincidenciaParcialEInexistente_RetornaListaCorrecta()
        {
            var encontrados = _gestor.BuscarPorTitulo("KNIGHT");
            Assert.Single(encontrados);
            Assert.Equal("Hollow Knight", encontrados[0].Titulo);

            var noEncontrados = _gestor.BuscarPorTitulo("xyz");
            Assert.Empty(noEncontrados);
        }

        // 5. LimpiarCatalogo()
        [Fact]
        public void LimpiarCatalogo_VaciaLaLista()
        {
            _gestor.LimpiarCatalogo();
            Assert.Empty(_gestor.JuegosDisponibles);
        }

        // 6. Catálogo vacío
        [Fact]
        public void CatalogoVacio_BuscarOEliminar_NoLanzaExcepcion()
        {
            _gestor.LimpiarCatalogo();

            var busqueda = _gestor.BuscarPorTitulo("Hades");
            bool elimPorIndice = _gestor.EliminarPorIndice(0);
            bool elimPorJuego = _gestor.EliminarJuego(new Videojuego("Hades", "Supergiant", 25));

            Assert.Empty(busqueda);
            Assert.False(elimPorIndice);
            Assert.False(elimPorJuego);
        }
    }
}
