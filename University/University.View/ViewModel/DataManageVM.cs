using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using University.Model;
using University.View;

namespace University.ViewModel
{
    public partial class DataManageVM : INotifyPropertyChanged
    {
        public DataManageVM()
        {
            AllCommonValues = new ObservableCollection<CommonValues>();
            AllCourses = new ObservableCollection<Course>();
            AllGroups = new ObservableCollection<Group>();
            AllStudents = new ObservableCollection<Student>();
            CreateMainRelayCommands();
            CreateCommonRelayCommands();
            CreateCourseRelayCommands();
            CreateGroupRelayCommands();
            CreateStudentRelayCommands();
        }

        private DataWorker? dataWorker;
        public DataWorker? DataWorker
        {

            get => dataWorker;

            set
            {
                if (value != null)
                {
                    dataWorker = value;
                    UpdateAllDataView();
                }
            }
        }
        public FileController? FileController { get; set; }
        public TabItem? SelectedTabItem { get; set; }
        public string ResultOperation { get; set; } = string.Empty;

        private void UpdateAllDataView()
        {
            if (DataWorker == null)
            {
                return;
            }
            AllCommonValues.Clear();
            foreach (var value in DataWorker.GetCommonValues(FilterLessThanStudent, FilterCourse))
            {
                AllCommonValues.Add(value);
            }
            AllCourses.Clear();
            foreach (var value in DataWorker.GetCourse())
            {
                AllCourses.Add(value);
            }
            AllGroups.Clear();
            foreach (var value in DataWorker.GetGroup())
            {
                AllGroups.Add(value);
            }
            AllStudents.Clear();
            foreach (var value in DataWorker.GetStudent())
            {
                AllStudents.Add(value);
            }
            UpdateSelectedCourse(SelectedCourse);
            UpdateSelectedGroup(SelectedGroup);
            UpdateSelectedStudent(SelectedStudent);
        }
        private void SetEmptyValuesToProperties()
        {
            CourseName = string.Empty;
            CourseDescription = string.Empty;
            GroupName = string.Empty;
            GroupCourse = null;
            StudentFirstName = string.Empty;
            StudentLastName = string.Empty;
            StudentGroup = null;
        }
        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void ShowMessageToUser(string message)
        {
            ResultOperation = message;
            MessageView messageView = new MessageView();
            SetCenterPositionAndOpen(messageView);
            ResultOperation = string.Empty;
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.DataContext = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SetEmptyValuesToProperties();
            window.ShowDialog();
            SetEmptyValuesToProperties();
        }

        #region Main commands

        public RelayCommand AddItem { get; set; }
        public RelayCommand AddItemFromFile { get; set; }
        public RelayCommand EditItem { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand DeleteAllItem { get; set; }
        public RelayCommand ExitMessageWnd { get; set; }

        private void CreateMainRelayCommands()
        {
            CreateCommandAddItem();
            CreateCommandAddItemFromFile();
            CreateCommandEditItem();
            CreateCommandDeleteItem();
            CreateCommandDeleteAllItem();
            CreateCommandExitMessageWndItem();

        }
        private void CreateCommandAddItem()
        {
            AddItem = new RelayCommand(obj =>
            {
                string resultStr = "Object not selected";
                if (SelectedTabItem?.Name == "CourseTab")
                {
                    OpenAddCourseWindowMethod();
                }
                else if (SelectedTabItem?.Name == "GroupTab")
                {
                    OpenAddGroupWindowMethod();
                }
                else if (SelectedTabItem?.Name == "StudentTab")
                {
                    OpenAddStudentWindowMethod();
                }
                else
                {
                    ShowMessageToUser(resultStr);
                }
            });
        }
        private void CreateCommandAddItemFromFile()
        {
            AddItemFromFile = new RelayCommand(obj =>
            {
                if (FileController == null || DataWorker == null)
                {
                    return;
                }
                string resultStr = "";
                string? filePath = FileController.GetFilePathToOpen();
                if (SelectedTabItem?.Name == "CourseTab" && filePath != null)
                {
                    var courses = FileController.GetCoursesFromFile(filePath);
                    resultStr = DataWorker.AddCourses(courses);
                }
                else if (SelectedTabItem?.Name == "GroupTab" && filePath != null)
                {
                    var groups = FileController.GetGroupsFromFile(filePath);
                    resultStr = DataWorker.AddGroups(groups);
                }
                else if (SelectedTabItem?.Name == "StudentTab" && filePath != null)
                {
                    var students = FileController.GetStudentsFromFile(filePath);
                    resultStr = DataWorker.AddStudents(students);
                }
                else
                {
                    return;
                }
                UpdateAllDataView();
                ShowMessageToUser(resultStr);
            });
        }
        private void CreateCommandEditItem()
        {
            EditItem = new RelayCommand(obj =>
            {
                string resultStr = "Object not selected";
                if (SelectedTabItem?.Name == "CourseTab" && SelectedCourse?.Id != null)
                {
                    OpenEditCourseWindowMethod();
                }
                else if (SelectedTabItem?.Name == "GroupTab" && SelectedGroup?.Id != null)
                {
                    OpenEditGroupWindowMethod();
                }
                else if (SelectedTabItem?.Name == "StudentTab" && SelectedStudent?.Id != null)
                {
                    OpenEditStudentWindowMethod();
                }
                else
                {
                    ShowMessageToUser(resultStr);
                }
            });
        }
        private void CreateCommandDeleteItem()
        {
            DeleteItem = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                string resultStr = "Object not selected";
                if (SelectedTabItem?.Name == "CourseTab" && SelectedCourse?.Id != null)
                {
                    resultStr = DataWorker.DeleteCourse(SelectedCourse.Id);
                }
                else if (SelectedTabItem?.Name == "GroupTab" && SelectedGroup?.Id != null)
                {
                    resultStr = DataWorker.DeleteGroup(SelectedGroup.Id);
                }
                else if (SelectedTabItem?.Name == "StudentTab" && SelectedStudent?.Id != null)
                {
                    resultStr = DataWorker.DeleteStudent(SelectedStudent.Id);
                }
                else
                {
                    return;
                }
                UpdateAllDataView();
                ShowMessageToUser(resultStr);
            });
        }
        private void CreateCommandDeleteAllItem()
        {
            DeleteAllItem = new RelayCommand(obj =>
            {
                if (DataWorker == null)
                {
                    return;
                }
                string resultStr = "Object not selected";
                if (SelectedTabItem?.Name == "CourseTab")
                {
                    resultStr = DataWorker.DeleteCourses();
                }
                else if (SelectedTabItem?.Name == "GroupTab")
                {
                    resultStr = DataWorker.DeleteGroups();
                }
                else if (SelectedTabItem?.Name == "StudentTab")
                {
                    resultStr = DataWorker.DeleteStudents();
                }
                else
                {
                    return;
                }
                UpdateAllDataView();
                ShowMessageToUser(resultStr);
            });
        }
        private void CreateCommandExitMessageWndItem()
        {
            ExitMessageWnd = new RelayCommand(obj =>
            {
                Window wnd = obj as Window;
                wnd.Close();
            });
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
