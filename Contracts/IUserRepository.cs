using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(bool trackChanges);
        User GetUser(Guid companyId, bool trackChanges);
        void CreateUser(User user);
        IEnumerable<User> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteUser(User user);
        PagedList<User> GetUsers(UserParameters userParameters);
    }
}
