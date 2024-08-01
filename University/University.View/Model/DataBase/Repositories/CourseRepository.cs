using System.Data;
using System.Data.SqlClient;

namespace University.Model
{
    public class CourseRepository
    {
        private SqlConnection? _sqlConnection { get; set; }
        private GroupRepository? _groupRepository { get; set; }

        public CourseRepository(SqlConnection? sqlConnection, bool mainTable = true)
        {
            _sqlConnection = sqlConnection;
            if (mainTable)
            {
                _groupRepository = new GroupRepository(sqlConnection, false);
            }
        }

        public List<Course> Get()
        {
            SqlCommand cmd = new SqlCommand("CoursesGet", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Course> Get(Guid id)
        {
            SqlCommand cmd = new SqlCommand("CoursesGetById", _sqlConnection);
            cmd.Parameters.AddWithValue("COURSE_ID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Course> Get(string? name)
        {
            SqlCommand cmd = new SqlCommand("CoursesGetByName", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("NAME", name);
            return SQLCommandReadData(cmd);
        }

        private List<Course> SQLCommandReadData(SqlCommand cmd)
        {
            List<Course> courses = new List<Course>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Guid coursesId = reader.GetGuid(reader.GetOrdinal("COURSE_ID"));
                    string? coursesName = reader.GetString(reader.GetOrdinal("NAME"));
                    string? coursesDescription = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                    Course course = new Course(coursesId, coursesName, coursesDescription);
                    courses.Add(course);
                }
            }
            return IncludeOtherReference(courses);
        }
        private List<Course> IncludeOtherReference(List<Course> courses)
        {
            foreach (var course in courses)
            {
                if (course == null)
                {
                    continue;
                }
                course.Groups = _groupRepository?.GetByParent(course.Id);
            }
            return courses;
        }

        public void Add(Course course)
        {
            SqlCommand cmd = new SqlCommand("CoursesAdd", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("COURSE_ID", course.Id);
            cmd.Parameters.AddWithValue("NAME", course.Name);
            cmd.Parameters.AddWithValue("DESCRIPTION", course.Description);
            int result = cmd.ExecuteNonQuery();
        }
        public void AddMany(List<Course> courses)
        {
            foreach (var course in courses)
            {
                SqlCommand cmd = new SqlCommand("CoursesAdd", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("COURSE_ID", course.Id);
                cmd.Parameters.AddWithValue("NAME", course.Name);
                cmd.Parameters.AddWithValue("DESCRIPTION", course.Description);
                int result = cmd.ExecuteNonQuery();
            }
        }
        public void Update(Course course)
        {
            SqlCommand cmd = new SqlCommand("CoursesUpdate", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("COURSE_ID", course.Id);
            cmd.Parameters.AddWithValue("NAME", course.Name);
            cmd.Parameters.AddWithValue("DESCRIPTION", course.Description);
            int result = cmd.ExecuteNonQuery();
        }

        public void Delete(Course course)
        {
            SqlCommand cmd = new SqlCommand("CoursesDeleteById", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("COURSE_ID", course.Id);
            int result = cmd.ExecuteNonQuery();
        }
        public void Delete()
        {
            SqlCommand cmd = new SqlCommand("CoursesDeleteAll", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            int result = cmd.ExecuteNonQuery();
        }
    }
}
