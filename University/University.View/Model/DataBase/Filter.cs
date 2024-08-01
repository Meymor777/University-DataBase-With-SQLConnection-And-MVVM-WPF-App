namespace University.Model
{
    public class Filter
    {
        public int? LessThanStudents { get; set; }
        public Course? SelectedCourse { get; set; }

        public Filter(int? lessThanStudents, Course? selectedCourse)
        {
            LessThanStudents = lessThanStudents;
            SelectedCourse = selectedCourse;
        }
    }
}
