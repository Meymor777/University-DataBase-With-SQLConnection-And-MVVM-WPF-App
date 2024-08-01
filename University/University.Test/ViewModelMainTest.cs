using University.Model;
using University.ViewModel;

namespace University.Test
{
    public class ViewModelMainTest
    {
        public DataManageVM DataManageVM { get; set; }
        [SetUp]
        public void Setup()
        {
            DataManageVM = new DataManageVM();
        }

        [Test]
        public void VmShouldInitializeMainCommandsExpectNotNull()
        {
            Assert.IsNotNull(DataManageVM.AddItem, "AddItem is null");
            Assert.IsNotNull(DataManageVM.AddItemFromFile, "AddItemFromFile is null");
            Assert.IsNotNull(DataManageVM.EditItem, "EditItem is null");
            Assert.IsNotNull(DataManageVM.DeleteItem, "DeleteItem is null");
            Assert.IsNotNull(DataManageVM.DeleteAllItem, "DeleteAllItem is null");
        }
        [Test]
        public void AddItemCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.AddItem.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.AddItem.CanExecute(null));
        }
        [Test]
        public void AddItemFromFileCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.AddItemFromFile.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.AddItemFromFile.CanExecute(null));
        }
        [Test]
        public void EditItemCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.EditItem.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.EditItem.CanExecute(null));
        }
        [Test]
        public void DeleteItemCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.DeleteItem.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.DeleteItem.CanExecute(null));
        }
        [Test]
        public void DeleteAllItemCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.DeleteAllItem.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.DeleteAllItem.CanExecute(null));
        }
        [Test]
        public void ExitMessageWndCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.ExitMessageWnd.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            Student student = new Student("TestFirstName", "TestLastName", group.Id);
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            DataManageVM.AllStudents.Add(student);
            DataManageVM.SelectedStudent = student;
            Assert.IsTrue(DataManageVM.ExitMessageWnd.CanExecute(null));
        }
    }
}