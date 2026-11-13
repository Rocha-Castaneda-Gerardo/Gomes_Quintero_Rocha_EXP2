using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Tests.Datos;

public static class DatosPrueba
{
    public static List<Videojuego> Catalogo()
    {
        return new List<Videojuego>
        {
            new Videojuego("Celeste", "Maddy Makes Games", 8),
            new Videojuego("Hollow Knight", "Team Cherry", 40),
            new Videojuego("Minecraft", "Mojang", 100),
            new Videojuego("Tetris", "Pajitnov", 5),
            new Videojuego("Portal 2", "Valve", 10),
            new Videojuego("Stardew Valley", "ConcernedApe", 60),
            new Videojuego("Hades", "Supergiant", 25),
            new Videojuego("Zelda: BOTW", "Nintendo", 120)
        };
    }

    public static GestorVideojuegos GestorPoblado()
    {
        var gestor = new GestorVideojuegos();
        foreach (var juego in Catalogo())
        {
            gestor.AgregarJuego(juego);
        }
        return gestor;
    }

    public static Usuario UsuarioConColecciones()
    {
        var usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        usuario.AgregarJuegoAColeccion("Pendientes", new Videojuego("Celeste", "Maddy Makes Games", 8));
        return usuario;
    }
}
