using WebSistemaVentas.Repository.Interfaces;

namespace WebSistemaVentas.UOW;

public interface IUOWVentas
{
    IMantenimientosRepository mantenimientosRepository { get; }
    IConfiguracionesRepository configuracionesRepository { get; }
}