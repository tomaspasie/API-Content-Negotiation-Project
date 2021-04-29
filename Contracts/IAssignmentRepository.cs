using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAllAssignments(bool trackChanges);
        Assignment GetAssignment(Guid companyId, bool trackChanges);
        void CreateAssignment(Assignment assignment);
        IEnumerable<Assignment> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteAssignment(Assignment assignment);
        IEnumerable<Assignment> GetAssignments(AssignmentParameters assignmentParameters);
    }
}
