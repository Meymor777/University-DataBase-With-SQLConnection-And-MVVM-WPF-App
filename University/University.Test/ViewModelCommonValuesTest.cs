using University.Model;
using University.ViewModel;

namespace University.Test
{
    public class ViewModelCommonValuesTest
    {
        public DataManageVM DataManageVM { get; set; }
        [SetUp]
        public void Setup()
        {
            DataManageVM = new DataManageVM();
        }

        [Test]
        public void VmShouldInitializeCommonCommandsExpectNotNull()
        {
            Assert.IsNotNull(DataManageVM.AllCommonValues, "AllCommonValues is null");
            Assert.IsNotNull(DataManageVM.UseFilter, "UseFilter is null");
            Assert.IsNotNull(DataManageVM.RefreshFilter, "RefreshFilter is null");
            Assert.IsNotNull(DataManageVM.SaveResult, "SaveResult is null");
            Assert.IsNotNull(DataManageVM.OpenFilter, "OpenFilter is null");
        }
        [Test]
        public void UseFilterCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.UseFilter.CanExecute(null));
            DataManageVM.FilterLessThanStudent = 10;
            Course course = new Course("TestName", "TestDescription");
            DataManageVM.AllCourses.Add(course);
            DataManageVM.SelectedCourse = course;
            DataManageVM.FilterCourse = course;
            Assert.IsTrue(DataManageVM.UseFilter.CanExecute(null));
        }

    }
}