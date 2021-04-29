using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetAllSections(bool trackChanges);
        Section GetSection(Guid companyId, bool trackChanges);
        void CreateSection(Section section);
        IEnumerable<Section> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteSection(Section section);
        IEnumerable<Section> GetSections(SectionParameters sectionParameters);
    }
}