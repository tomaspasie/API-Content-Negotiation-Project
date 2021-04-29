using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace SchoolAPI.Controllers
{
    public class ForUpdate
    {
        public OrganizationForCreationDto organization;
        public UserForCreationDto user;
        public AssignmentForCreationDto assignment;
        public CourseForCreationDto course;
        public SectionForCreationDto section;
    }
}
