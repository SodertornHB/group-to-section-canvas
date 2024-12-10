namespace Logic.Model
{
    public class Enrollment
    {
        public Enrollment(int userId, int sectionId)
        {
            type = "StudentEnrollment";
            enrollment_state = "active";
            user_id = userId;
            course_section_id = sectionId;
            self_enrolled = false;
        }

        public int id { get; set; }
        public int user_id { get; private set; }

        public int course_section_id { get; private set; }
        public string type { get; private set; }
        public string enrollment_state { get; private set; }
        public bool self_enrolled { get; private set; }
        //public int course_id { get; set; }
        public int? course_enrollment_id { get; private set; }
        //public int? role_id { get; set; }
    }
}
