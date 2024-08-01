using University.ViewModel;
using University.Model;

namespace University.Test
{
    public class ViewModelGroupTest
    {
        public DataManageVM DataManageVM { get; set; }
        [SetUp]
        public void Setup()
        {
            DataManageVM = new DataManageVM();
        }

        [Test]
        public void VmShouldInitializeGroupCommandsExpectNotNull()
        {
            Assert.IsNotNull(DataManageVM.AllGroups, "AllGroups is null");
            Assert.IsNotNull(DataManageVM.AddGroup, "AddGroup is null");
            Assert.IsNotNull(DataManageVM.EditGroup, "EditGroup is null");
        }
        [Test]
        public void SelectGroupExpectEqualProperties()
        {
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            Assert.IsTrue(DataManageVM.SelectedGroupName == group.Name);
        }
        [Test]
        public void AddGroupCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.AddGroup.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            Assert.IsTrue(DataManageVM.AddGroup.CanExecute(null));
        }
        [Test]
        public void EditGroupCmdCanBeExecutedAlwaysExpectTrue()
        {
            Assert.IsTrue(DataManageVM.EditGroup.CanExecute(null));
            Course course = new Course("TestName", "TestDescription");
            Group group = new Group("TestGroupName", course.Id);
            DataManageVM.AllGroups.Add(group);
            DataManageVM.SelectedGroup = group;
            Assert.IsTrue(DataManageVM.EditCourse.CanExecute(null));
        }
    }
}
