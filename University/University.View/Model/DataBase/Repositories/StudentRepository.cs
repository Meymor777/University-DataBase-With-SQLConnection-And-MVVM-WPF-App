using System.Data;
using System.Data.SqlClient;

namespace University.Model
{
    public class StudentRepository
    {
        private SqlConnection? _sqlConnection { get; set; }
        private GroupRepository? _groupRepository { get; set; }
        public StudentRepository(SqlConnection? sqlConnection, bool mainTable = true)
        {
            _sqlConnection = sqlConnection;
            if (mainTable)
            {
                _groupRepository = new GroupRepository(sqlConnection, false);
            }
        }

        public List<Student> Get()
        {
            SqlCommand cmd = new SqlCommand("StudentsGet", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Student> Get(Guid id)
        {
            SqlCommand cmd = new SqlCommand("StudentsGetById", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("STUDENT_ID", id);
            return SQLCommandReadData(cmd);
        }
        public List<Student> Get(string? lastName)
        {
            SqlCommand cmd = new SqlCommand("StudentsGetByLastName", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("LAST_NAME", lastName);
            return SQLCommandReadData(cmd);
        }
        public List<Student> Get(string? firstName, string? lastName)
        {
            SqlCommand cmd = new SqlCommand("StudentsGetByFirstAndLastNames", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FIRST_NAME", firstName);
            cmd.Parameters.AddWithValue("LAST_NAME", lastName);
            return SQLCommandReadData(cmd);
        }
        public List<Student> GetByParent(Guid groupId)
        {
            SqlCommand cmd = new SqlCommand("StudentsGetByParent", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GROUP_ID", groupId);
            return SQLCommandReadData(cmd);
        }

        private List<Student> SQLCommandReadData(SqlCommand cmd)
        {
            List<Student> students = new List<Student>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Guid studentId = reader.GetGuid(reader.GetOrdinal("STUDENT_ID"));
                    Guid groupId = reader.GetGuid(reader.GetOrdinal("GROUP_ID"));
                    string? studentFirstName = reader.GetString(reader.GetOrdinal("FIRST_NAME"));
                    string? studentLastName = reader.GetString(reader.GetOrdinal("LAST_NAME"));
                    Student student = new Student(studentId, studentFirstName, studentLastName, groupId);
                    students.Add(student);
                }
            }
            return IncludeOtherReference(students);
        }
        private List<Student> IncludeOtherReference(List<Student> students)
        {
            foreach (var student in students)
            {
                if (student == null)
                {
                    continue;
                }
                student.Group = _groupRepository?.Get(student.GroupId).FirstOrDefault();
            }
            return students;
        }

        public void Add(Student student)
        {
            SqlCommand cmd = new SqlCommand("StudentsAdd", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("STUDENT_ID", student.Id);
            cmd.Parameters.AddWithValue("GROUP_ID", student.GroupId);
            cmd.Parameters.AddWithValue("FIRST_NAME", student.FirstName);
            cmd.Parameters.AddWithValue("LAST_NAME", student.LastName);
            int result = cmd.ExecuteNonQuery();
        }
        public void AddMany(List<Student> students)
        {
            foreach (var student in students)
            {
                SqlCommand cmd = new SqlCommand("StudentsAdd", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("STUDENT_ID", student.Id);
                cmd.Parameters.AddWithValue("GROUP_ID", student.GroupId);
                cmd.Parameters.AddWithValue("FIRST_NAME", student.FirstName);
                cmd.Parameters.AddWithValue("LAST_NAME", student.LastName);
                int result = cmd.ExecuteNonQuery();
            }
        }
        public void Update(Student student)
        {
            SqlCommand cmd = new SqlCommand("StudentsUpdate", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("STUDENT_ID", student.Id);
            cmd.Parameters.AddWithValue("GROUP_ID", student.GroupId);
            cmd.Parameters.AddWithValue("FIRST_NAME", student.FirstName);
            cmd.Parameters.AddWithValue("LAST_NAME", student.LastName);
            int result = cmd.ExecuteNonQuery();
        }

        public void Delete(Student student)
        {
            SqlCommand cmd = new SqlCommand("StudentsDeleteById", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("STUDENT_ID", student.Id);
            int result = cmd.ExecuteNonQuery();
        }
        public void Delete()
        {
            SqlCommand cmd = new SqlCommand("StudentsDeleteAll", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            int result = cmd.ExecuteNonQuery();
        }
    }
}
