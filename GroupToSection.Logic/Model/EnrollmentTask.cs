namespace Logic.Model
{
    public class EnrollmentTask
    {
        public EnrollmentTask(string task)
        {
            this.task = task;
        }

        public string task { get; private set; }
    }
}
