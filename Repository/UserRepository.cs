using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Entities.Helpers;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /*
        private ISortHelper<User> _sortHelper;

        public UserRepository(RepositoryContext repositoryContext, ISortHelper<User> sortHelper)
            : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }*/

        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<User> GetAllUsers(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public User GetUser(Guid companyId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefault();

        public void CreateUser(User user) => Create(user);

        public IEnumerable<User> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public PagedList<User> GetUsers(UserParameters userParameters)
        {
            var users = FindByCondition(o => o.Age >= userParameters.MinAge &&
                                             o.Age <= userParameters.MaxAge)
                .OrderBy(on => on.Name);

            SearchByName(ref users, userParameters.Name);

            ApplySort(ref users, userParameters.OrderBy);
            //var sortedUsers = _sortHelper.ApplySort(users, usersParameters.OrderBy);

            return PagedList<User>.ToPagedList(users,
                userParameters.PageNumber,
                userParameters.PageSize);
        }

        private void SearchByName(ref IOrderedQueryable<User> users, string userName)
        {
            if (!users.Any() || string.IsNullOrWhiteSpace(userName))
                return;
            users = (IOrderedQueryable<User>)users.Where(o => o.Name.ToLower().Contains(userName.Trim().ToLower()));
        }

        private void ApplySort(ref IOrderedQueryable<User> users, string orderByQueryString)
        {
            if (!users.Any())
                return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                users = users.OrderBy(x => x.Name);
                return;
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                users = users.OrderBy(x => x.Name);
                return;
            }
            users = users.OrderBy(orderQuery);
        }
    }
}