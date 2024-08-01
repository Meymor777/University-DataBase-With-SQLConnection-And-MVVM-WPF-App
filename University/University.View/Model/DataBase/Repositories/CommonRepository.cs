using System.Data;
using System.Data.SqlClient;

namespace University.Model
{
    public class CommonRepository
    {
        private SqlConnection? _sqlConnection { get; set; }

        public CommonRepository(SqlConnection? sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public List<CommonValues> Get(Filter filter)
        {
            SqlCommand cmd = new SqlCommand("CommonGet", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("SELECTEDCOURSE", filter?.SelectedCourse?.Id);
            cmd.Parameters.AddWithValue("LESSTHANSTUDENTS", filter?.LessThanStudents);
            return SQLCommandReadData(cmd);
        }

        private List<CommonValues> SQLCommandReadData(SqlCommand cmd)
        {
            List<CommonValues> commonValues = new List<CommonValues>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string courseName = reader.GetString(reader.GetOrdinal("CourseName")) ?? "";
                    string courseDescription = reader.GetString(reader.GetOrdinal("CourseDescription")) ?? "";
                    string groupName = reader.GetString(reader.GetOrdinal("GroupName")) ?? "";
                    string studentFirstName = reader.GetString(reader.GetOrdinal("StudentFirstName")) ?? "";
                    string studentLastName = reader.GetString(reader.GetOrdinal("StudentLastName")) ?? "";
                    int studentCount = reader.GetInt32(reader.GetOrdinal("StudentCount"));
                    CommonValues resultCommonValues = new CommonValues(courseName, courseDescription, groupName,
                        studentFirstName, studentLastName, studentCount);
                    commonValues.Add(resultCommonValues);
                }
            }
            return commonValues;
        }
    }
}
