﻿using GroupToSection.Logic.Model;
using System;

namespace Logic.Model
{
    public class Section : Entity
    {
        public Section()
        { }
        public Section(string name, string identifier)
        {
            Name = name;
            Sis_section_id = Integration_id = identifier;

            Created_at = DateTime.Now;
        }
        public int Course_id { get; set; }
        public string Sis_section_id { get; set; }
        public string Integration_id { get; set; }
        public string Sis_course_id { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; }
        public override string ToString() => $"{Name} (course id: {Course_id})";
    }
}
