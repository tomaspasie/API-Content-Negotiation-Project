using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace SchoolAPI.Controllers
{
    public class ForCreate
    {
        public OrganizationForUpdateDto organization;
        public UserForUpdateDto user;
        public AssignmentForUpdateDto assignment;
        public CourseForUpdateDto course;
        public SectionForUpdateDto section;
    }
}
