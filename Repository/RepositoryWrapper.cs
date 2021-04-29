using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private ISortHelper<User> _userSortHelper;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    //_user = new UserRepository(_repoContext, _userSortHelper);
                }

                return _user;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext,
            ISortHelper<User> userSortHelper)
        {
            _repoContext = repositoryContext;
            _userSortHelper = userSortHelper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}