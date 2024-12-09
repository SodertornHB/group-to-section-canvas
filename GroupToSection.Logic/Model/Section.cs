using GroupToSection.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Model
{
    public class Section : Entity
    {
        public int Course_id { get; set; }
        public string Sis_section_id { get; set; }
        public string Sis_course_id { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; }
        public override string ToString() => $"{Name} (course id: {Course_id})";
    }
}
