using Repositories.Cantracts;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ISuperLotoRepository> _superLotoRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _superLotoRepository = new Lazy<ISuperLotoRepository>(() => new SuperLotoRepository(_context));
        }

        public ISuperLotoRepository SuperLoto => _superLotoRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
