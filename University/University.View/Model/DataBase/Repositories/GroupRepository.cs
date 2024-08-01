using System.Data;
using System.Data.SqlClient;

namespace University.Model
{
    public class GroupRepository
    {
        private SqlConnection? _sqlConnection { get; set; }
        private CourseRepository? _courseRepository { get; set; }
        private StudentRepository? _studentRepository { get; set; }

        public GroupRepository(SqlConnection? sqlConnection, bool mainTable = true)
        {
            _sqlConnection = sqlConnection;
            if (mainTable)
            {
                _courseRepository = new CourseRepository(sqlConnection, false);
                _studentRepository = new StudentRepository(sqlConnection, false);
            }
        }

        public List<Group> Get()
        {
            SqlCommand cmd = new SqlCommand("GroupsGet", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Group> Get(Guid id)
        {
            SqlCommand cmd = new SqlCommand("GroupsGetById", _sqlConnection);
            cmd.Parameters.AddWithValue("GROUP_ID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Group> Get(string? name)
        {
            SqlCommand cmd = new SqlCommand("GroupsGetByName", _sqlConnection);
            cmd.Parameters.AddWithValue("NAME", name);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }
        public List<Group> GetByParent(Guid coursesId)
        {
            SqlCommand cmd = new SqlCommand("GroupsGetByParent", _sqlConnection);
            cmd.Parameters.AddWithValue("COURSE_ID", coursesId);
            cmd.CommandType = CommandType.StoredProcedure;
            return SQLCommandReadData(cmd);
        }

        private List<Group> SQLCommandReadData(SqlCommand cmd)
        {
            List<Group> groups = new List<Group>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Guid groupId = reader.GetGuid(reader.GetOrdinal("GROUP_ID"));
                    string? groupName = reader.GetString(reader.GetOrdinal("NAME"));
                    Guid coursesId = reader.GetGuid(reader.GetOrdinal("COURSE_ID"));
                    Group group = new Group(groupId, groupName, coursesId);
                    groups.Add(group);
                }
            }
            return IncludeOtherReference(groups);
        }
        private List<Group> IncludeOtherReference(List<Group> groups)
        {
            foreach (var group in groups)
            {
                if (group == null)
                {
                    continue;
                }
                group.Course = _courseRepository?.Get(group.CourseId).FirstOrDefault();
                group.Students = _studentRepository?.GetByParent(group.Id);
            }
            return groups;
        }

        public void Add(Group group)
        {
            SqlCommand cmd = new SqlCommand("GroupsAdd", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GROUP_ID", group.Id);
            cmd.Parameters.AddWithValue("NAME", group.Name);
            cmd.Parameters.AddWithValue("COURSE_ID", group.CourseId);
            int result = cmd.ExecuteNonQuery();
        }
        public void AddMany(List<Group> groups)
        {
            foreach (var group in groups)
            {
                SqlCommand cmd = new SqlCommand("GroupsAdd", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GROUP_ID", group.Id);
                cmd.Parameters.AddWithValue("NAME", group.Name);
                cmd.Parameters.AddWithValue("COURSE_ID", group.CourseId);
                int result = cmd.ExecuteNonQuery();
            }
        }
        public void Update(Group group)
        {
            SqlCommand cmd = new SqlCommand("GroupsUpdate", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GROUP_ID", group.Id);
            cmd.Parameters.AddWithValue("NAME", group.Name);
            cmd.Parameters.AddWithValue("COURSE_ID", group.CourseId);
            int result = cmd.ExecuteNonQuery();
        }

        public void Delete(Group group)
        {
            SqlCommand cmd = new SqlCommand("GroupsDeleteById", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GROUP_ID", group.Id);
            int result = cmd.ExecuteNonQuery();
        }
        public void Delete()
        {
            SqlCommand cmd = new SqlCommand("GroupsDeleteAll", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            int result = cmd.ExecuteNonQuery();
        }
    }
}
