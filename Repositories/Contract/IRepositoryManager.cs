﻿using Repositories.Contract;

namespace Repositories.Cantracts
{
    public interface IRepositoryManager
    {
        ISuperLotoRepository SuperLoto { get; }
        ISayisalLotoRepository SayisalLoto { get; }
        IOnNumaraRepository OnNumara { get; }
        ISansTopuRepository SansTopu { get; }


        IOnNumaraLogsRepository OnNumaraLogs { get; }
        ISansTopuLogsRepository SansTopuLogs { get; }

        Task SaveAsync();
    }
}
