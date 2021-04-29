using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Assignment> GetAllAssignments(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Assignment GetAssignment(Guid companyId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefault();

        public void CreateAssignment(Assignment assignment) => Create(assignment);

        public IEnumerable<Assignment> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();

        public void DeleteAssignment(Assignment assignment)
        {
            Delete(assignment);
        }

        public IEnumerable<Assignment> GetAssignments(AssignmentParameters assignmentParameters)
        {
            return FindAll()
                .OrderBy(on => on.Name)
                .Skip((assignmentParameters.PageNumber - 1) * assignmentParameters.PageSize)
                .Take(assignmentParameters.PageSize)
                .ToList();
        }
    }
}