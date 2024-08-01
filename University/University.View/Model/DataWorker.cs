using System.Data;

namespace University.Model
{
    public class DataWorker
    {
        #region CommonValuesMethod

        public List<CommonValues> GetCommonValues(int? lessThanStudents = null, Course? selectedCourse = null)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                Filter filter = new Filter(lessThanStudents, selectedCourse);
                return universityDBConnection.CommonValues?.Get(filter) ?? new List<CommonValues>();
            }
        }

        #endregion

        #region CourseMethod

        public List<Course> GetCourse()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Course> result = universityDBConnection.Courses?.Get() ?? new List<Course>();
                return OrderCourses(result);
            }
        }
        public List<Course> GetCourse(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Course> result = universityDBConnection.Courses?.Get(id) ?? new List<Course>();
                return OrderCourses(result);
            }
        }
        public List<Course> GetCourse(string name)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Course> result = universityDBConnection.Courses?.Get(name) ?? new List<Course>();
                return OrderCourses(result);
            }
        }

        public string AddCourse(string name, string description)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                Course course = new Course(name, description);
                if (universityDBConnection.Courses != null)
                {
                    universityDBConnection.Courses.Add(course);
                    return "Added course";
                }
                else
                {
                    return "Failed to add course";
                }
            }
        }
        public string  AddCourses(List<(Guid id, string name, string description)> courses)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                int existingCourse = 0;
                if (universityDBConnection.Courses == null)
                {
                    return "Failed to add courses";
                }
                for (int i = 0; i < courses.Count; i++)
                {
                    (Guid id, string name, string description) courseData = courses[i];
                    if (universityDBConnection.Courses.Get(courseData.id).FirstOrDefault() != null)
                    {
                        existingCourse++;
                        continue;
                    }
                    Course course = new Course(courseData.id, courseData.name, courseData.description);
                    universityDBConnection.Courses.Add(course);;
                }
                return $"Added courses - {courses.Count - existingCourse}";
            }
        }

        public string UpdateCourse(Guid id, string name, string description)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Courses?.Get(id).FirstOrDefault() == null)
                {
                    return "Failed to edit course";
                }
                Course course = new Course(id, name, description);
                if (universityDBConnection.Courses != null)
                {
                    universityDBConnection.Courses.Update(course);
                    return "Edit course";
                }
                else
                {
                    return "Failed to edit course";
                }
            }
        }

        public string DeleteCourse(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Courses != null)
                {
                    Course? course = universityDBConnection.Courses.Get(id).FirstOrDefault();
                    if (course == null)
                    {
                        return "Failed to delete course";
                    }
                    universityDBConnection.Courses.Delete(course);
                    return "Delete course";
                }
                else
                {
                    return "Failed to delete course";
                }
            }
        }
        public string DeleteCourses()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Courses != null)
                {
                    List<Course> courses = universityDBConnection.Courses.Get();
                    universityDBConnection.Courses.Delete();
                    return string.Format("Delete courses - {0}", courses.Count);
                }
                else
                {
                    return "Failed to delete courses";
                }
            }
        }

        private List<Course> OrderCourses(List<Course> courses)
        {
            return courses
            .OrderBy(x =>
            {
                if (x == null)
                {
                    return "";
                }
                else
                {
                    return x.Name;
                }
            })
            .ToList();
        }

        #endregion

        #region GroupMethod

        public List<Group> GetGroup()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Group> result = universityDBConnection.Groups?.Get() ?? new List<Group>();
                return OrderGroups(result);
            }
        }
        public List<Group> GetGroup(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Group> result = universityDBConnection.Groups?.Get(id) ?? new List<Group>();
                return OrderGroups(result);
            }
        }
        public List<Group> GetGroup(string name)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Group> result = universityDBConnection.Groups?.Get(name) ?? new List<Group>();
                return OrderGroups(result);
            }
        }

        public string AddGroup(string name, Guid courseID)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Courses?.Get(courseID).FirstOrDefault() == null)
                {
                    return "Failed to add group";
                }
                Group group = new Group(name, courseID);
                if (universityDBConnection.Groups != null)
                {
                    universityDBConnection.Groups.Add(group);
                    return "Added group";
                }
                else
                {
                    return "Failed to add group";
                }

            }
        }
        public string AddGroups(List<(Guid id, string name, Guid courseID)> groups)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                int existingGroups = 0;
                if (universityDBConnection.Groups == null)
                {
                    return "Failed to add groups";
                }
                for (int i = 0; i < groups.Count; i++)
                {
                    (Guid id, string name, Guid courseID) groupData = groups[i];
                    if (universityDBConnection.Groups.Get(groupData.id).FirstOrDefault() != null)
                    {
                        existingGroups++;
                        continue;
                    }
                    if (universityDBConnection.Courses?.Get(groupData.courseID).FirstOrDefault() == null)
                    {
                        return $"Failed to add groups, added groups - {i}";
                    }
                    Group group = new Group(groupData.id, groupData.name, groupData.courseID);
                    universityDBConnection.Groups.Add(group);
                }
                return $"Added groups - {groups.Count - existingGroups}";
            }
        }

        public string UpdateGroup(Guid id, string name, Guid courseID)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Groups?.Get(id).FirstOrDefault() == null ||
                    universityDBConnection.Courses?.Get(courseID).FirstOrDefault() == null)
                {
                    return "Failed to edit group";
                }
                Group group = new Group(id, name, courseID);
                if (universityDBConnection.Groups != null)
                {
                    universityDBConnection.Groups.Update(group);
                    return "Edit group";
                }
                else
                {
                    return "Failed to edit group";
                }
            }
        }

        public string DeleteGroup(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Groups != null)
                {
                    Group? group = universityDBConnection.Groups.Get(id).FirstOrDefault();
                    if (group == null)
                    {
                        return "Failed to delete group";
                    }
                    universityDBConnection.Groups.Delete(group);
                    return "Delete group";
                }
                else
                {
                    return "Failed to delete group";
                }
            }
        }
        public string DeleteGroups()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Groups != null)
                {
                    List<Group> groups = universityDBConnection.Groups.Get();
                    universityDBConnection.Groups.Delete();
                    return string.Format("Delete groups - {0}", groups.Count);
                }
                else
                {
                    return "Failed to delete groups";
                }
            }
        }

        private List<Group> OrderGroups(List<Group> groups)
        {
            return groups
            .OrderBy(x =>
            {
                if (x == null || x.Course == null)
                {
                    return "";
                }
                else
                {
                    return x.Course.Name;
                }
            })
            .ThenBy(x =>
            {
                if (x == null)
                {
                    return "";
                }
                else
                {
                    return x.Name;
                }
            })
            .ToList();
        }

        #endregion

        #region StudentMethod

        public List<Student> GetStudent()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Student> result = universityDBConnection.Students?.Get() ?? new List<Student>();
                return OrderStudents(result);
            }
        }
        public List<Student> GetStudent(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Student> result = universityDBConnection.Students?.Get(id) ?? new List<Student>();
                return OrderStudents(result);
            }
        }
        public List<Student> GetStudent(string lastName)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Student> result = universityDBConnection.Students?.Get(lastName) ?? new List<Student>();
                return OrderStudents(result);
            }
        }
        public List<Student> GetStudent(string firstName, string lastName)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                List<Student> result = universityDBConnection.Students?.Get(firstName, lastName) ?? new List<Student>();
                return OrderStudents(result);
            }
        }

        public string AddStudent(string firstName, string lastName, Guid groupId)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Groups?.Get(groupId).FirstOrDefault() == null)
                {
                    return "Failed to add student";
                }
                Student student = new Student(firstName, lastName, groupId);
                if (universityDBConnection.Students != null)
                {
                    universityDBConnection.Students.Add(student);
                    return "Added student";
                }
                else
                {
                    return "Failed to add student";
                }

            }
        }
        public string AddStudents(List<(Guid id, string firstName, string lastName, Guid groupId)> students)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                int existingStudents = 0;
                if (universityDBConnection.Students == null)
                {
                    return "Failed to add students";
                }
                for (int i = 0; i < students.Count; i++)
                {
                    (Guid id, string firstName, string lastName, Guid groupId) studentData = students[i];
                    if (universityDBConnection.Students.Get(studentData.id).FirstOrDefault() != null)
                    {
                        existingStudents++;
                        continue;
                    }
                    if (universityDBConnection.Groups?.Get(studentData.groupId).FirstOrDefault() == null)
                    {
                        return $"Failed to add students, added students - {i}";
                    }
                    Student student = new Student(studentData.id, studentData.firstName, studentData.lastName, studentData.groupId);
                    universityDBConnection.Students.Add(student);
                }
                return $"Added students - {students.Count - existingStudents}";
            }
        }

        public string UpdateStudent(Guid id, string firstName, string lastName, Guid groupId)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Students?.Get(id).FirstOrDefault() == null ||
                    universityDBConnection.Groups?.Get(groupId).FirstOrDefault() == null)
                {
                    return "Failed to edit student";
                }
                Student student = new Student(id, firstName, lastName, groupId);
                if (universityDBConnection.Students != null)
                {
                    universityDBConnection.Students.Update(student);
                    return "Edit student - {0}";
                }
                else
                {
                    return "Failed to edit student";
                }
            }
        }

        public string DeleteStudent(Guid id)
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Students != null)
                {
                    Student? student = universityDBConnection.Students.Get(id).FirstOrDefault();
                    if (student == null)
                    {
                        return "Failed to delete student";
                    }
                    universityDBConnection.Students.Delete(student);
                    return "Delete student";
                }
                else
                {
                    return "Failed to delete student";
                }
            }
        }
        public string DeleteStudents()
        {
            using (UniversityDBConnection universityDBConnection = new UniversityDBConnection())
            {
                if (universityDBConnection.Students != null)
                {
                    List<Student> students = universityDBConnection.Students.Get();
                    universityDBConnection.Students.Delete();
                    return string.Format("Delete students - {0}", students.Count);
                }
                else
                {
                    return "Failed to delete students";
                }
            }
        }

        private List<Student> OrderStudents(List<Student> students)
        {
            return students
            .OrderBy(x =>
            {
                if (x == null || x.Group == null)
                {
                    return "";
                }
                else
                {
                    return x.Group.Name;
                }
            })
            .ThenBy(x =>
            {
                if (x == null)
                {
                    return "";
                }
                else
                {
                    return x.FirstName;
                }
            })
            .ThenBy(x =>
            {
                if (x == null)
                {
                    return "";
                }
                else
                {
                    return x.LastName;
                }
            })
            .ToList();
        }

        #endregion
    }
}
