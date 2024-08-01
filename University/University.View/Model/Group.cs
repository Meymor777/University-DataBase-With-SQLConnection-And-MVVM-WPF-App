using System.ComponentModel;

namespace University.Model
{
    public class Group
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public List<Student>? Students { get; set; }

        public Group(string? name, Guid courseId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CourseId = courseId;
        }
        public Group(Guid id, string? name, Guid courseId)
        {
            Id = id;
            Name = name;
            CourseId = courseId;
        }

        public override string? ToString()
        {
            if (Course == null || Course.ToString() == "")
            {
                return $"{Name}";
            }
            return $"{Name} ({Course})";
        }
    }
}
