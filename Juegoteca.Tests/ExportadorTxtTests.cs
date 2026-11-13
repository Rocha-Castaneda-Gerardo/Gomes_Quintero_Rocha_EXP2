using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;
using Xunit;

namespace Juegoteca.Tests;

public class ExportadorTxtTests : IDisposable
{
    private readonly string _rutaTemporal;
    private readonly ExportadorTxt _exportador;

    public ExportadorTxtTests()
    {
        _rutaTemporal = Path.Combine(Path.GetTempPath(), $"juegoteca_test_{Guid.NewGuid()}.txt");
        _exportador = new ExportadorTxt();
    }

    private static Usuario CrearUsuarioPrueba()
    {
        var usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        usuario.AgregarJuegoAColeccion("Pendientes", new Videojuego("Celeste", "Maddy Makes Games", 8));
        return usuario;
    }

    [Fact]
    public void ExportarATxt_UsuarioConColecciones_CreaArchivoEnRutaIndicada()
    {
        var usuario = CrearUsuarioPrueba();
        _exportador.ExportarATxt(usuario, _rutaTemporal);
        Assert.True(File.Exists(_rutaTemporal));
    }

    [Fact]
    public void ExportarATxt_UsuarioConColecciones_ContenidoIncluyeDatosEsperados()
    {
        var usuario = CrearUsuarioPrueba();
        _exportador.ExportarATxt(usuario, _rutaTemporal);
        string contenido = File.ReadAllText(_rutaTemporal);

        Assert.Contains("Ana", contenido);
        Assert.Contains("Pendientes", contenido);
        Assert.Contains("Celeste", contenido);
        Assert.Contains("8 h", contenido);
    }

    [Fact]
    public void ExportarATxt_UsuarioSinColecciones_CreaArchivoSoloConEncabezadoSinExcepcion()
    {
        var usuario = new Usuario("Carlos");
        _exportador.ExportarATxt(usuario, _rutaTemporal);
        string contenido = File.ReadAllText(_rutaTemporal);

        Assert.True(File.Exists(_rutaTemporal));
        Assert.Contains("Carlos", contenido);
    }

    [Fact]
    public void ExportarATxt_ExportarDosVecesMismaRuta_SobrescribeConUltimaExportacion()
    {
        var u1 = CrearUsuarioPrueba();
        var u2 = new Usuario("Roberto");

        _exportador.ExportarATxt(u1, _rutaTemporal);
        _exportador.ExportarATxt(u2, _rutaTemporal);
        string contenido = File.ReadAllText(_rutaTemporal);

        Assert.Contains("Roberto", contenido);
        Assert.DoesNotContain("Ana", contenido);
    }

    public void Dispose()
    {
        if (File.Exists(_rutaTemporal))
        {
            File.Delete(_rutaTemporal);
        }
    }
}
