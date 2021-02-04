using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private ProAgilContext _dbContext;
        public ProAgilRepository(ProAgilContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        private IQueryable<Evento> allEventos(bool incluirPalestrantes)
        {
            IQueryable<Evento> eventos = _dbContext.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);
            if (incluirPalestrantes)
            {
                eventos = eventos.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            return eventos.OrderByDescending(d => d.DataEvento);
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
        {
            return await allEventos(incluirPalestrantes).ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
        {
            return await allEventos(incluirPalestrantes).Where(i => i.Id == eventoId).FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            return await allEventos(incluirPalestrantes).Where(i => i.Tema.ToLower().Contains(tema.ToLower())).ToArrayAsync();
        }

        private IQueryable<Palestrante> AllPalestrantes(bool incluirEventos)
        {
            IQueryable<Palestrante> palestrantes = _dbContext.Palestrantes;
            if (incluirEventos)
            {
                palestrantes = palestrantes.Include(pe => pe.PalestrantesEventos).ThenInclude(e => e.Evento);
            }
            return palestrantes.OrderBy(p => p.Nome);
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false)
        {
            return await AllPalestrantes(incluirEventos).ToArrayAsync();
        }

        public async Task<Palestrante[]> GetPalestrantesByNameAsync(string nome, bool incluirPalestrantes = false)
        {
            return await AllPalestrantes(incluirPalestrantes).Where(p => p.Nome.ToLower().Contains(nome.ToLower())).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false)
        {
            return await AllPalestrantes(incluirEventos).Where(p => p.Id == palestranteId).FirstOrDefaultAsync();
        }
    }
}