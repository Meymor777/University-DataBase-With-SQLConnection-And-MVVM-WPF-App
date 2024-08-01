using University.ViewModel;
using University.Model;

namespace University.Test
{
    public class ViewModelCourseTest
    {
        public DataManageVM DataManageVM { get; set; }
        [SetUp]
        public void Setup()
        {
            DataManageVM = new DataManageVM();
        }

        [Test]
        public void VmShouldInitializeCourseCommandsExpectNotNull()
        {
            Assert.IsNotNull(DataManageVM.AllCourses, "AllCourses is null");
            Assert.IsNotNull(DataManageVM.AddCourse, "AddCourse is null");
            Assert.IsNotNull(DataManageVM.EditCourse, "EditCourse is null");
        }
        [Test]
        public void SelectCourseExpectEqualProperties()
        {
            Course course = new Course("TestName", "TestDescription");
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            Assert.IsTrue(DataManageVM.SelectedCourseName == course.Name);
            Assert.IsTrue(DataManageVM.SelectedCourseDescription == course.Description);
        }
        [Test]
        public void AddCourseCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.AddCourse.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            Assert.IsTrue(DataManageVM.AddCourse.CanExecute(null));
        }
        [Test]
        public void EditCourseCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.EditCourse.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            Assert.IsTrue(DataManageVM.EditCourse.CanExecute(null));
        }
    }
}