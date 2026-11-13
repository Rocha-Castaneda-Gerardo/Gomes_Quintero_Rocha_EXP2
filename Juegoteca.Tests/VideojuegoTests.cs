using System;
using Xunit;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Interfaces;

namespace Juegoteca.Tests
{
    public class VideojuegoTests
    {
        [Fact]
        public void Constructor_ValoresValidos_AsignaPropiedades()
        {
            Videojuego juego = new Videojuego("Hades", "Supergiant", 25);
            Assert.Equal("Hades", juego.Titulo);
            Assert.Equal("Supergiant", juego.Estudio);
            Assert.Equal(25, juego.HorasJuego);
        }

        [Fact]
        public void ToString_ValoresValidos_RetornaFormatoExacto()
        {
            Videojuego juego = new Videojuego("Hades", "Supergiant", 25);
            Assert.Equal("Hades - Supergiant (25 h)", juego.ToString());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Constructor_HorasJuegoInvalidas_LanzaArgumentException(int horas)
        {
            Assert.Throws<ArgumentException>(() => new Videojuego("A", "B", horas));
        }

        [Fact]
        public void ImplementaInterfaz_ClaseVideojuego_EsAsignableAIVideojuego()
        {
            Videojuego juego = new Videojuego("Minecraft", "Mojang", 100);
            Assert.IsAssignableFrom<IVideojuego>(juego);
        }
    }
}