namespace University.Model
{
    public class CommonValues
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string GroupName { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int StudentCount { get; set; }

        public CommonValues(string courseName, string courseDescription, string groupName, string studentFirstName, string studentLastName, int studentCount)
        {
            CourseName = courseName;
            CourseDescription = courseDescription;
            GroupName = groupName;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            StudentCount = studentCount;
        }
    }
}
