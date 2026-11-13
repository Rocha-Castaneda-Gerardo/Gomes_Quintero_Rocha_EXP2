using Xunit;
using Juegoteca.Tests.Datos;
using Juegoteca.Biblioteca.Gestores;

namespace Juegoteca.Tests
{
    public class AlgoritmosTests
    {
        [Fact]
        public void QuickSort_OrdenaAscendentePorHorasJuego()
        {
            var gestor = DatosPrueba.GestorPoblado();
            gestor.QuickSort(gestor.JuegosDisponibles);

            Assert.Equal("Tetris", gestor.JuegosDisponibles[0].Titulo);
            Assert.Equal(5, gestor.JuegosDisponibles[0].HorasJuego);
            Assert.Equal("Zelda: BOTW", gestor.JuegosDisponibles[gestor.JuegosDisponibles.Count - 1].Titulo);
        }

        [Fact]
        public void MergeSort_RetornaNuevaListaOrdenadaSinModificarOriginal()
        {
            var gestor = DatosPrueba.GestorPoblado();
            var primerTituloAntes = gestor.JuegosDisponibles[0].Titulo;

            var nuevaLista = gestor.MergeSort(gestor.JuegosDisponibles);

            Assert.NotSame(gestor.JuegosDisponibles, nuevaLista);
            Assert.Equal(5, nuevaLista[0].HorasJuego);
            Assert.Equal(primerTituloAntes, gestor.JuegosDisponibles[0].Titulo);
        }

        [Fact]
        public void BusquedaBinaria_EncuentraTituloIgnorandoMayusculas()
        {
            var gestor = DatosPrueba.GestorPoblado();
            int indice = gestor.BusquedaBinaria("HADES");

            Assert.True(indice >= 0);
        }

        [Fact]
        public void BusquedaBinaria_NoAlteraCatalogoOriginal()
        {
            var gestor = DatosPrueba.GestorPoblado();
            var primerTituloAntes = gestor.JuegosDisponibles[0].Titulo;

            gestor.BusquedaBinaria("Hades");

            Assert.Equal(primerTituloAntes, gestor.JuegosDisponibles[0].Titulo);
        }
    }
}