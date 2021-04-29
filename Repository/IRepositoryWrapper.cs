using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        void Save();
    }
}
