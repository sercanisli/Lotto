using Repositories.Cantracts;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ISuperLotoRepository> _superLotoRepository;
        private readonly Lazy<ISayisalLotoRepository> _sayisalLotoRepository;
        private readonly Lazy<IOnNumaraRepository> _onNumaraRepository;
        private readonly Lazy<ISansTopuRepository> _sansTopuRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _superLotoRepository = new Lazy<ISuperLotoRepository>(() => new SuperLotoRepository(_context));
            _sayisalLotoRepository = new Lazy<ISayisalLotoRepository>(() => new SayisalLotoRepository(_context));
            _onNumaraRepository = new Lazy<IOnNumaraRepository>(() => new  OnNumaraRepository(_context));
            _sansTopuRepository = new Lazy<ISansTopuRepository>(() => new SansTopuRepository(_context));

        }

        public ISuperLotoRepository SuperLoto => _superLotoRepository.Value;

        public ISayisalLotoRepository SayisalLoto => _sayisalLotoRepository.Value;

        public IOnNumaraRepository OnNumara => _onNumaraRepository.Value;

        public ISansTopuRepository SansTopu => _sansTopuRepository.Value;


        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
