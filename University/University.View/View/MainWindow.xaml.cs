using System.Windows;
using System.Windows.Controls;
using University.ViewModel;
using University.Model;

namespace University.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllCommonValuesView { get; set; }
        public static ListView AllCoursesView { get; set; }
        public static ListView AllGroupsView { get; set; }
        public static ListView AllStudentsView { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataManageVM dataManageVM = new DataManageVM();
            dataManageVM.DataWorker = new DataWorker();
            dataManageVM.FileController = new FileController();
            DataContext = dataManageVM;
            AllCommonValuesView = ViewAllCommonValuesView;
            AllCoursesView = ViewAllCoursesView;
            AllGroupsView = ViewAllGroupsView;
            AllStudentsView = ViewAllStudentsView;
        }
    }
}