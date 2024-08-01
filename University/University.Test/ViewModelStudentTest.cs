using University.ViewModel;
using University.Model;

namespace University.Test
{
    public class ViewModelStudentTest
    {
        public DataManageVM DataManageVM { get; set; }
        [SetUp]
        public void Setup()
        {
            DataManageVM = new DataManageVM();
        }

        [Test]
        public void VmShouldInitializeStudentCommandsExpectNotNull()
        {
            Assert.IsNotNull(DataManageVM.AllStudents, "AllStudents is null");
            Assert.IsNotNull(DataManageVM.AddStudent, "AddStudent is null");
            Assert.IsNotNull(DataManageVM.EditStudent, "EditStudent is null");
        }
        [Test]
        public void SelectStudentExpectEqualProperties()
        {
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.SelectedStudentFirstName == student.FirstName);
            Assert.IsTrue(DataManageVM.SelectedStudentLastName == student.LastName);
        }
        [Test]
        public void AddStudentCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.AddStudent.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.AddStudent.CanExecute(null));
        }
        [Test]
        public void EditStudentCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.EditStudent.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.EditStudent.CanExecute(null));
        }
    }
}
