using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace University.Model
{
    public class UniversityDBConnection : IDisposable
    {
        public SqlConnection? SqlConnection { get; set; } = null;
        public CourseRepository? Courses { get; set; } = null;
        public GroupRepository? Groups { get; set; } = null;
        public StudentRepository? Students { get; set; } = null;
        public CommonRepository? CommonValues { get; set; } = null;

        public UniversityDBConnection()
        {
            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
            SqlConnection.Open();
            if (SqlConnection.State == ConnectionState.Open)
            {
                Courses = new CourseRepository(SqlConnection);
                Groups = new GroupRepository(SqlConnection);
                Students = new StudentRepository(SqlConnection);
                CommonValues = new CommonRepository(SqlConnection);
                Courses.Get("test");
            }
        }

        public void Dispose()
        {
            SqlConnection?.Close();
        }
    }
}
