using WebSistemaVentas.Repository.Implementation;
using WebSistemaVentas.Repository.Interfaces;

namespace WebSistemaVentas.UOW;

public class UOWVentas: IUOWVentas
{
    public IMantenimientosRepository mantenimientosRepository { get; private set; }
    public IConfiguracionesRepository configuracionesRepository { get; private set; }

    public UOWVentas(string connectionString)
    {
        mantenimientosRepository = new MantenimientosRepository(connectionString);
        configuracionesRepository = new ConfiguracionesRepository(connectionString);
    }
}