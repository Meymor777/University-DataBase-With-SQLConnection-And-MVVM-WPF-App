using System.Collections.ObjectModel;
using System.Windows;
using University.Model;
using University.View;

namespace University.ViewModel
{
    public partial class DataManageVM
    {
        public ObservableCollection<Course> AllCourses { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseDescription { get; set; } = string.Empty;

        #region Course properties for edit

        private Course? _selectedCourse;
        public Course? SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                if (_selectedCourse != value)
                {
                    _selectedCourse = value;
                    UpdateSelectedCourse(value);
                }

            }
        }
        public string SelectedCourseName { get; set; } = string.Empty;
        public string SelectedCourseDescription { get; set; } = string.Empty;

        #endregion

        #region Course commands

        public RelayCommand AddCourse { get; set; }
        public RelayCommand EditCourse { get; set; }
        private void CreateCourseRelayCommands()
        {
            CreateCommandAddCourse();
            CreateCommandEditCourse();
        }
        private void CreateCommandAddCourse()
        {
            AddCourse = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyAddCourse(wnd))
                {
                    resultStr = DataWorker.AddCourse(CourseName, CourseDescription);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }
        private void CreateCommandEditCourse()
        {
            EditCourse = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyEditCourse(wnd))
                {
                    resultStr = DataWorker.UpdateCourse(SelectedCourse.Id, SelectedCourseName, SelectedCourseDescription);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }

        #endregion

        private void OpenAddCourseWindowMethod()
        {
            AddNewCourseWindow addNewCourseWindow = new AddNewCourseWindow();
            SetCenterPositionAndOpen(addNewCourseWindow);
        }
        private void OpenEditCourseWindowMethod()
        {
            EditCourseWindow editCourseWindow = new EditCourseWindow();
            SetCenterPositionAndOpen(editCourseWindow);
        }
        private void UpdateSelectedCourse(Course? selectedCourse)
        {
            SelectedCourseName = selectedCourse?.Name ?? "";
            SelectedCourseDescription = selectedCourse?.Description ?? "";
        }
        private bool IsReadyAddCourse(Window? wnd)
        {
            if (CourseName == null || CourseName.Replace(" ", "") == "" ||
                    CourseDescription == null || CourseDescription.Replace(" ", "") == "")
            {
                if (CourseName == null || CourseName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "NameBlock");
                }
                if (CourseDescription == null || CourseDescription.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "DescriptionBlock");
                }
                return false;
            }
            return true;
        }
        private bool IsReadyEditCourse(Window? wnd)
        {
            if (SelectedCourse == null || SelectedCourseName == null || SelectedCourseName.Replace(" ", "") == "" ||
                    SelectedCourseDescription == null || SelectedCourseDescription.Replace(" ", "") == "")
            {
                if (SelectedCourseName == null || SelectedCourseName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "NameBlock");
                }
                if (SelectedCourseDescription == null || SelectedCourseDescription.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "DescriptionBlock");
                }

                return false;
            }
            return true;
        }
    }
}
