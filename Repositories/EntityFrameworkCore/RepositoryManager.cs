using Repositories.Cantracts;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ISuperLotoRepository> _superLotoRepository;
        private readonly Lazy<ISayisalLotoRepository> _sayisalLotoRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _superLotoRepository = new Lazy<ISuperLotoRepository>(() => new SuperLotoRepository(_context));
            _sayisalLotoRepository = new Lazy<ISayisalLotoRepository>(() => new SayisalLotoRepository(_context));
        }

        public ISuperLotoRepository SuperLoto => _superLotoRepository.Value;

        public ISayisalLotoRepository SayisalLoto => _sayisalLotoRepository.Value;

        public void Save() => _context.SaveChanges();

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
