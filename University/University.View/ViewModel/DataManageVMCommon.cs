using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using University.Model;
using University.View;

namespace University.ViewModel
{
    public partial class DataManageVM : INotifyPropertyChanged
    {
        public ObservableCollection<CommonValues> AllCommonValues { get; set; }

        public int? FilterLessThanStudent { get; set; } = null;
        public Course? FilterCourse { get; set; } = null;

        public string? SelectedFilterLessThanStudent { get; set; }
        public Course? SelectedFilterCourse { get; set; } = null;
        public int SelectedFilterCourseInd { get; set; } = -1;

        #region Common values commands

        public RelayCommand UseFilter { get; set; }
        public RelayCommand RefreshFilter { get; set; }
        public RelayCommand SaveResult { get; set; }
        public RelayCommand OpenFilter { get; set; }
        private void CreateCommonRelayCommands()
        {
            CreateCommandUseFilter();
            CreateCommandRefreshFilter();
            CreateCommandSaveResult();
            CreateCommandOpenFilter();
        }
        private void CreateCommandUseFilter()
        {
            UseFilter = new RelayCommand(obj =>
            {
                Window wnd = obj as Window;
                if (SelectedFilterLessThanStudent != null && SelectedFilterLessThanStudent.Replace(" ", "") != "")
                {
                    int? lessThanStudents = null;
                    if (int.TryParse(SelectedFilterLessThanStudent, out int parsingResult))
                    {
                        lessThanStudents = parsingResult;
                        FilterLessThanStudent = lessThanStudents;
                        FilterCourse = SelectedFilterCourse;
                        UpdateAllDataView();
                        wnd.Close();
                    }
                    else
                    {
                        SetRedBlockControl(wnd, "LessThanStudentBlock");
                    }
                }
                else
                {
                    FilterLessThanStudent = null;
                    FilterCourse = SelectedFilterCourse;
                    UpdateAllDataView();
                    wnd.Close();
                }
            });
        }
        private void CreateCommandRefreshFilter()
        {
            RefreshFilter = new RelayCommand(obj =>
            {
                Window wnd = obj as Window;
                FilterCourse = null;
                FilterLessThanStudent = null;
                UpdateAllDataView();
                wnd.Close();
            });
        }
        private void CreateCommandSaveResult()
        {
            SaveResult = new RelayCommand(obj =>
            {
                if (FileController == null || DataWorker == null)
                {
                    return;
                }
                List<CommonValues> AllCommonValuesList = DataWorker.GetCommonValues(FilterLessThanStudent, FilterCourse);
                if (AllCommonValuesList.Count == 0)
                {
                    return;
                }
                string? filePath = FileController.GetFilePathToCreate();
                if (filePath != null)
                {
                    FileController.CreateCSVFile(filePath, AllCommonValuesList);
                }
            });
        }
        private void CreateCommandOpenFilter()
        {
            OpenFilter = new RelayCommand(obj => OpenFilterWindowMethod());
        }

        #endregion

        private void OpenFilterWindowMethod()
        {
            SelectedFilterCourse = FilterCourse;
            SelectedFilterLessThanStudent = FilterLessThanStudent.ToString();
            SelectedFilterCourseInd = -1;
            for (int i = 0; i < AllCourses.Count; i++)
            {
                if (AllCourses[i].Id == FilterCourse?.Id)
                {
                    SelectedFilterCourseInd = i;
                    break;
                }
            }
            FilterWindow filterWindow = new FilterWindow();
            SetCenterPositionAndOpen(filterWindow);
            SelectedFilterCourse = null;
            SelectedFilterLessThanStudent = null;
            SelectedFilterCourseInd = -1;
        }
    }
}
