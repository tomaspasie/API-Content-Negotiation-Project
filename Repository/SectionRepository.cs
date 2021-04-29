using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Section> GetAllSections(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Section GetSection(Guid companyId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefault();

        public void CreateSection(Section section) => Create(section);

        public IEnumerable<Section> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();

        public void DeleteSection(Section section)
        {
            Delete(section);
        }
        public IEnumerable<Section> GetSections(SectionParameters sectionParameters)
        {
            return FindAll()
                .OrderBy(on => on.Name)
                .Skip((sectionParameters.PageNumber - 1) * sectionParameters.PageSize)
                .Take(sectionParameters.PageSize)
                .ToList();
        }
    }
}