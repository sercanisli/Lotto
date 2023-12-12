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

        private readonly Lazy<IOnNumaraLogsRepository> _onNumaraLogsRepository;
        private readonly Lazy<ISansTopuLogsRepository> _sansTopuLogsRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _superLotoRepository = new Lazy<ISuperLotoRepository>(() => new SuperLotoRepository(_context));
            _sayisalLotoRepository = new Lazy<ISayisalLotoRepository>(() => new SayisalLotoRepository(_context));
            _onNumaraRepository = new Lazy<IOnNumaraRepository>(() => new  OnNumaraRepository(_context));
            _sansTopuRepository = new Lazy<ISansTopuRepository>(() => new SansTopuRepository(_context));


            _onNumaraLogsRepository = new Lazy<IOnNumaraLogsRepository>(() => new OnNumaraLogsRepository(_context));
            _sansTopuLogsRepository = new Lazy<ISansTopuLogsRepository>(() => new SansTopuLogsRepository(_context));

        }

        public ISuperLotoRepository SuperLoto => _superLotoRepository.Value;

        public ISayisalLotoRepository SayisalLoto => _sayisalLotoRepository.Value;

        public IOnNumaraRepository OnNumara => _onNumaraRepository.Value;

        public ISansTopuRepository SansTopu => _sansTopuRepository.Value;



        public ISansTopuLogsRepository SansTopuLogs => _sansTopuLogsRepository.Value;

        public IOnNumaraLogsRepository OnNumaraLogs => _onNumaraLogsRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
