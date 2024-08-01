using System.Collections.ObjectModel;
using System.Windows;
using University.Model;
using University.View;

namespace University.ViewModel
{
    public partial class DataManageVM
    {
        public ObservableCollection<Group> AllGroups { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public Course? GroupCourse { get; set; } = null;

        #region Group properties for edit

        private Group? _selectedGroup;
        public Group? SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
                    UpdateSelectedGroup(value);
                }
            }
        }
        public int SelectedGroupCourseInd { get; set; }
        public string SelectedGroupName { get; set; } = string.Empty;
        public Course? SelectedGroupCourse { get; set; } = null;

        #endregion

        #region Group commands

        public RelayCommand AddGroup { get; set; }
        public RelayCommand EditGroup { get; set; }
        private void CreateGroupRelayCommands()
        {
            CreateCommandAddGroup();
            CreateCommandEditGroup();
        }
        private void CreateCommandAddGroup()
        {
            AddGroup = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyAddGroup(wnd))
                {
                    resultStr = DataWorker.AddGroup(GroupName, GroupCourse.Id);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }
        private void CreateCommandEditGroup()
        {
            EditGroup = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyEditGroup(wnd))
                {
                    resultStr = DataWorker.UpdateGroup(SelectedGroup.Id, SelectedGroupName, SelectedGroupCourse.Id);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }

        #endregion

        private void OpenAddGroupWindowMethod()
        {
            AddNewGroupWindow addNewGroupWindow = new AddNewGroupWindow();
            SetCenterPositionAndOpen(addNewGroupWindow);
        }
        private void OpenEditGroupWindowMethod()
        {
            EditGroupWindow editGroupWindow = new EditGroupWindow();
            SetCenterPositionAndOpen(editGroupWindow);
        }
        private void UpdateSelectedGroup(Group? selectedGroup)
        {
            SelectedGroupName = selectedGroup?.Name ?? "";
            SelectedGroupCourse = selectedGroup?.Course;
            SelectedGroupCourseInd = -1;
            for (int i = 0; i < AllCourses.Count; i++)
            {
                if (AllCourses[i].Id == selectedGroup?.CourseId)
                {
                    SelectedGroupCourseInd = i;
                    break;
                }
            }
        }
        private bool IsReadyAddGroup(Window? wnd)
        {
            if (GroupName == null || GroupName.Replace(" ", "") == "" || GroupCourse == null)
            {
                if (GroupName == null || GroupName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "NameBlock");
                }
                if (GroupCourse == null)
                {
                    ShowMessageToUser("Select course");
                }
                return false;
            }
            return true;
        }
        private bool IsReadyEditGroup(Window? wnd)
        {
            if (SelectedGroup == null || SelectedGroupName == null || SelectedGroupName.Replace(" ", "") == "" || SelectedGroupCourse == null)
            {
                if (SelectedGroupName == null || SelectedGroupName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "NameBlock");
                }
                if (SelectedGroupCourse == null)
                {
                    ShowMessageToUser("Select course");
                }

                return false;
            }
            return true;
        }
    }
}
