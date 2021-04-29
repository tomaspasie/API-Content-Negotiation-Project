using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasData
            (
                new Section
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"),
                    Name = "Section 1",
                },
                new Section
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce2"),
                    Name = "Section 2",
                }
            );
        }
    }
}
