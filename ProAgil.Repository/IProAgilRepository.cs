using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false);
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false);
        Task<Palestrante[]> GetPalestrantesByNameAsync(string nome, bool incluirEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false);
    }
}