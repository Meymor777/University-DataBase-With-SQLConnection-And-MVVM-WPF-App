using System.ComponentModel;

namespace University.Model
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }

        public Student(string? firstName, string? lastName, Guid groupId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            GroupId = groupId;
        }
        public Student(Guid id, string? firstName, string? lastName, Guid groupId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            GroupId = groupId;
        }

        public override string? ToString()
        {
            if (Group == null || Group.ToString() == "")
            {
                return $"{FirstName} {LastName}";
            }
            return $"{FirstName} {LastName} ({Group})";
        }

    }
}
