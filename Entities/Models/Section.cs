using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Section
    {
        [Column("SectionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Section name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        [MinLength(5, ErrorMessage = "Minimum length for the Name is 60 characters.")]
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}