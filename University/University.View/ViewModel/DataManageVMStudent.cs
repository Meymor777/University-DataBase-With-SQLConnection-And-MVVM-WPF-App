using System.Collections.ObjectModel;
using System.Windows;
using University.Model;
using University.View;

namespace University.ViewModel
{
    public partial class DataManageVM
    {
        public ObservableCollection<Student> AllStudents { get; set; }
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;
        public Group? StudentGroup { get; set; } = null;

        #region Student properties for edit

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    UpdateSelectedStudent(value);
                }

            }
        }
        public int SelectedStudentGroupInd { get; set; }
        public string SelectedStudentFirstName { get; set; } = string.Empty;
        public string SelectedStudentLastName { get; set; } = string.Empty;
        public Group? SelectedStudentGroup { get; set; } = null;

        #endregion

        #region Student commands

        public RelayCommand AddStudent { get; set; }
        public RelayCommand EditStudent { get; set; }
        private void CreateStudentRelayCommands()
        {
            CreateCommandAddStudent();
            CreateCommandEditStudent();
        }
        private void CreateCommandAddStudent()
        {
            AddStudent = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyAddStudent(wnd))
                {
                    resultStr = DataWorker.AddStudent(StudentFirstName, StudentLastName, StudentGroup.Id);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }
        private void CreateCommandEditStudent()
        {
            EditStudent = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                Window wnd = obj as Window;
                string resultStr = string.Empty;
                if (IsReadyEditStudent(wnd))
                {
                    resultStr = DataWorker.UpdateStudent(SelectedStudent.Id, SelectedStudentFirstName, SelectedStudentLastName, SelectedStudentGroup.Id);
                    UpdateAllDataView();
                    ShowMessageToUser(resultStr);
                    wnd.Close();
                }
            });
        }

        #endregion

        private void OpenAddStudentWindowMethod()
        {
            AddNewStudentWindow addNewStudentWindow = new AddNewStudentWindow();
            SetCenterPositionAndOpen(addNewStudentWindow);
        }
        private void OpenEditStudentWindowMethod()
        {
            EditStudentWindow editStudentWindow = new EditStudentWindow();
            SetCenterPositionAndOpen(editStudentWindow);
        }
        private void UpdateSelectedStudent(Student? selectedStudent)
        {
            SelectedStudentFirstName = selectedStudent?.FirstName ?? "";
            SelectedStudentLastName = selectedStudent?.LastName ?? "";
            SelectedStudentGroup = selectedStudent?.Group;
            SelectedStudentGroupInd = -1;
            for (int i = 0; i < AllGroups.Count; i++)
            {
                if (AllGroups[i].Id == selectedStudent?.GroupId)
                {
                    SelectedStudentGroupInd = i;
                    break;
                }
            }
        }
        private bool IsReadyAddStudent(Window? wnd)
        {
            if (StudentFirstName == null || StudentFirstName.Replace(" ", "") == "" ||
                    StudentLastName == null || StudentLastName.Replace(" ", "") == "" || StudentGroup == null)
            {
                if (StudentFirstName == null || StudentFirstName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "FirstNameBlock");
                }
                if (StudentLastName == null || StudentLastName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "LastNameBlock");
                }
                if (StudentGroup == null)
                {
                    ShowMessageToUser("Select group");
                }
                return false;
            }
            return true;
        }
        private bool IsReadyEditStudent(Window? wnd)
        {
            if (SelectedStudent == null || SelectedStudentFirstName == null || SelectedStudentFirstName.Replace(" ", "") == "" ||
                    SelectedStudentLastName == null || SelectedStudentLastName.Replace(" ", "") == "" || SelectedStudentGroup == null)
            {
                if (SelectedStudentFirstName == null || SelectedStudentFirstName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "FirstNameBlock");
                }
                if (SelectedStudentLastName == null || SelectedStudentLastName.Replace(" ", "") == "")
                {
                    SetRedBlockControl(wnd, "LastNameBlock");
                }
                if (SelectedStudentGroup == null)
                {
                    ShowMessageToUser("Select group");
                }
                return false;
            }
            return true;
        }
    }
}
