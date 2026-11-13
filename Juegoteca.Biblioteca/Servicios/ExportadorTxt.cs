using System.Text;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios;

public class ExportadorTxt : IExportador
{
    public void ExportarATxt(Usuario usuario, string rutaArchivo)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        if (string.IsNullOrWhiteSpace(rutaArchivo))
            throw new ArgumentException("La ruta no puede estar vacía.", nameof(rutaArchivo));

        var sb = new StringBuilder();
        sb.AppendLine($"Jugador: {usuario.Nombre}");
        sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm}");
        sb.AppendLine(new string('=', 40));

        foreach (var coleccion in usuario.Colecciones)
        {
            sb.AppendLine($"Colección: {coleccion.Key}");
            int totalHoras = 0;

            foreach (var juego in coleccion.Value)
            {
                sb.AppendLine($"  - {juego}");
                totalHoras += juego.HorasJuego;
            }

            sb.AppendLine($"Total de horas ({coleccion.Key}): {totalHoras} h");
            sb.AppendLine(new string('-', 40));
        }

        File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
    }
}
