using System.ComponentModel;

namespace University.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Group>? Groups { get; set; }

        public Course(Guid id, string? name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public Course(string? name, string? description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public override string? ToString()
        {
            return Name;
        }
    }
}
