﻿using Repositories.Contract;

namespace Repositories.Cantracts
{
    public interface IRepositoryManager
    {
        ISuperLotoRepository SuperLoto { get; }
        ISayisalLotoRepository SayisalLoto { get; }
        IOnNumaraRepository OnNumara { get; }

        void Save();
        Task SaveAsync();
    }
}
