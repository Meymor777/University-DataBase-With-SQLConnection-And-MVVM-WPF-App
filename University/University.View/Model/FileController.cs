using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.IO;

namespace University.Model
{
    public class FileController
    {
        public string? GetFilePathToOpen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File (*.csv)|*.csv;";
            openFileDialog.Title = "Please select *.csv file.";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
        public string? GetFilePathToCreate()
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "File (*.csv)|*.csv;";
            openFileDialog.Title = "Please select *.csv file.";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public List<(Guid id, string name, string description)> GetCoursesFromFile(string filePath)
        {
            List<(Guid id, string name, string description)> courses = new List<(Guid id, string name, string description)>();
            List<string[]>? result = GetDataFromFile(filePath, 3);
            if (result == null || result.Count < 1)
            {
                return courses;
            }
            for (int i = 1; i < result.Count; i++)
            {
                if (result[i].Length != 3 || !Guid.TryParse(result[i][0], out Guid courseId))
                {
                    continue;
                }
                string courseName = result[i][1];
                string courseDescription = result[i][2];
                courses.Add((courseId, courseName, courseDescription));
            }
            return courses;
        }
        public List<(Guid id, string name, Guid courseID)> GetGroupsFromFile(string filePath)
        {
            List<(Guid id, string name, Guid courseID)> groups = new List<(Guid id, string name, Guid courseID)>();
            List<string[]>? result = GetDataFromFile(filePath, 3);
            if (result == null || result.Count < 1)
            {
                return groups;
            }
            for (int i = 1; i < result.Count; i++)
            {
                if (result[i].Length != 3 || !Guid.TryParse(result[i][0], out Guid groupId) || !Guid.TryParse(result[i][2], out Guid courseId))
                {
                    continue;
                }
                string groupName = result[i][1];
                groups.Add((groupId, groupName, courseId));
            }
            return groups;
        }
        public List<(Guid id, string firstName, string lastName, Guid groupId)> GetStudentsFromFile(string filePath)
        {
            List<(Guid id, string firstName, string lastName, Guid groupId)> students = new List<(Guid id, string firstName, string lastName, Guid groupId)>();
            List<string[]>? result = GetDataFromFile(filePath, 4);
            if (result == null || result.Count < 1)
            {
                return students;
            }
            for (int i = 1; i < result.Count; i++)
            {
                if (result[i].Length != 4 || !Guid.TryParse(result[i][0], out Guid studentId) || !Guid.TryParse(result[i][1], out Guid groupId))
                {
                    continue;
                }
                string studentFirstName = result[i][2];
                string studentLastName = result[i][3];
                students.Add((studentId, studentFirstName, studentLastName, groupId));
            }
            return students;
        }

        private List<string[]>? GetDataFromFile(string filePath, int colummCount)
        {
            List<string[]>? result = new List<string[]>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int indRow = 0;
                while (!parser.EndOfData)
                {
                    //Processing row
                    indRow++;
                    string[]? fields = parser.ReadFields();
                    if (fields == null)
                    {
                        continue;
                    }
                    if (fields.Length != colummCount)
                    {
                        if (indRow == 1)
                        {
                            return null;
                        }
                        continue;
                    }
                    result.Add(fields.ToArray());
                }
            }
            return result;
        }

        public void CreateCSVFile(string fileName, List<CommonValues> resultCommonValues)
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (var resultCommonValue in resultCommonValues)
            {
                data.Add(new Dictionary<string, object> {
                    { "CourseName", resultCommonValue.CourseName },
                    { "CourseDescription", resultCommonValue.CourseDescription },
                    { "GroupName", resultCommonValue.GroupName },
                    { "StudentFirstName", resultCommonValue.StudentFirstName },
                    { "StudentLastName", resultCommonValue.StudentLastName },
                    { "StudentCount", resultCommonValue.StudentCount } });
            }

            // Write the data to the CSV file
            using (var writer = new StreamWriter(fileName))
            {
                // Write the header row
                string header = string.Join(",", data[0].Keys) + "\n";
                writer.Write(header);

                // Write each data row
                foreach (var row in data)
                {
                    string rowStr = string.Join(",", row.Values) + "\n";
                    writer.Write(rowStr);
                }
            }
        }
    }
}
