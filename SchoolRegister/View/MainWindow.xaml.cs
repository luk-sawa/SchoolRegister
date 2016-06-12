using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolRegister
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Group> GroupList { get; set; }
        public ObservableCollection<Student> StudentList { get; set; }
        public ObservableCollection<Student> GroupStudentList { get; set; }
        public ObservableCollection<Week> WeekGrid { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WeekGrid = new ObservableCollection<Week>();
            for (int dayHour = 0; dayHour < 24; dayHour++)
            {
                Week week = new Week(
                    (dayHour + " - " + (dayHour + 1)), null, null, null, null, null, null, null);
                WeekGrid.Add(week);
            }
            GroupList = new ObservableCollection<Group>();
            StudentList = new ObservableCollection<Student>();
            GroupStudentList = new ObservableCollection<Student>();
            
            GroupComboBox.ItemsSource = CollectionViewSource.GetDefaultView(GroupList);
            WeekPlanGroupComboBox.ItemsSource = CollectionViewSource.GetDefaultView(GroupList);
            WeekPlanHourComboBox.ItemsSource = CollectionViewSource.GetDefaultView(WeekGrid);
            StudentListView.ItemsSource = CollectionViewSource.GetDefaultView(StudentList);
            UngroupStudentListView.ItemsSource = CollectionViewSource.GetDefaultView(StudentList);
            GroupStudentListView.ItemsSource = CollectionViewSource.GetDefaultView(GroupStudentList);
            WeekViewGrid.ItemsSource = CollectionViewSource.GetDefaultView(WeekGrid);
        }

       
        private void AddToPlanClick(object sender, RoutedEventArgs e)
        {
            string monday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Monday;
            string tuesday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Tuesday;
            string wednesday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Wednesday;
            string thursday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Thursday;
            string friday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Friday;
            string saturday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Saturday;
            string sunday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Sunday;
            try
            {
                switch (this.WeekPlanDayComboBox.SelectedIndex)
                {
                    case 0:
                        monday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 1:
                        tuesday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 2:
                        wednesday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 3:
                        thursday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 4:
                        friday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 5:
                        saturday = this.WeekPlanGroupComboBox.Text;
                        break;
                    case 6:
                        sunday = this.WeekPlanGroupComboBox.Text;
                        break;
                }

                Week week = new Week(this.WeekPlanHourComboBox.Text, monday, tuesday, wednesday, thursday, friday, saturday, sunday);

                this.WeekGrid.Insert(this.WeekPlanHourComboBox.SelectedIndex, week);
                this.WeekGrid.RemoveAt(this.WeekPlanHourComboBox.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select group, day and hour of the lessons.", "Selection error");
            }
        }

        private void DeleteFromPlanClick(object sender, RoutedEventArgs e)
        {
            string monday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Monday;
            string tuesday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Tuesday;
            string wednesday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Wednesday;
            string thursday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Thursday;
            string friday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Friday;
            string saturday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Saturday;
            string sunday = this.WeekGrid[this.WeekPlanHourComboBox.SelectedIndex].Sunday;
            try
            {
                switch (this.WeekPlanDayComboBox.SelectedIndex)
                {
                    case 0:
                        monday = null;
                        break;
                    case 1:
                        tuesday = null;
                        break;
                    case 2:
                        wednesday = null;
                        break;
                    case 3:
                        thursday = null;
                        break;
                    case 4:
                        friday = null;
                        break;
                    case 5:
                        saturday = null;
                        break;
                    case 6:
                        sunday = null;
                        break;
                }

                Week week = new Week(this.WeekPlanHourComboBox.Text, monday, tuesday, wednesday, thursday, friday, saturday, sunday);

                this.WeekGrid.Insert(this.WeekPlanHourComboBox.SelectedIndex, week);
                this.WeekGrid.RemoveAt(this.WeekPlanHourComboBox.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select group, day and hour of the lessons.", "Selection error");
            }
        }

        private void AddGroupButtonClick(object sender, RoutedEventArgs e)
        {
            AddGroupWindow groupWindow = new AddGroupWindow();
            groupWindow.Owner = Application.Current.MainWindow;
            groupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            groupWindow.Show();
        }

        private void DeleteGroupButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GroupList.RemoveAt(this.GroupComboBox.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select group from the list.", "Group not selected");
            }
        }

        private void AddToGroupButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Student groupStudent = new Student
                    (this.StudentList[this.UngroupStudentListView.SelectedIndex].Name,
                    this.StudentList[this.UngroupStudentListView.SelectedIndex].LastName,
                    this.StudentList[this.UngroupStudentListView.SelectedIndex].Bid,
                    this.StudentList[this.UngroupStudentListView.SelectedIndex].PhoneNumber,
                    this.StudentList[this.UngroupStudentListView.SelectedIndex].Adress,
                    this.GroupList[this.GroupComboBox.SelectedIndex].GroupName);
                GroupStudentList.Add(groupStudent);
                StudentList.RemoveAt(this.UngroupStudentListView.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select student and group from the lists.", "Student or group not selected");
            }
        }

        private void DeleteFromGroupButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Student student = new Student
                    (this.GroupStudentList[this.GroupStudentListView.SelectedIndex].Name,
                    this.GroupStudentList[this.GroupStudentListView.SelectedIndex].LastName,
                    this.GroupStudentList[this.GroupStudentListView.SelectedIndex].Bid,
                    this.GroupStudentList[this.GroupStudentListView.SelectedIndex].PhoneNumber,
                    this.GroupStudentList[this.GroupStudentListView.SelectedIndex].Adress, null);
                StudentList.Add(student);
                GroupStudentList.RemoveAt(this.GroupStudentListView.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select student from the list.", "Student not selected");
            }
        }

        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            int outBidParse;
            string name = this.NameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("The 'name' field cannot be empty or filled with white-space characters", "Incorrect name input");
                return;
            }
            string lastName = this.LastNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("The 'last name' field cannot be empty or filled with white-space characters", "Incorrect last name input");
                return;
            }
            if (!int.TryParse(this.BidTextBox.Text, out outBidParse))
            {
                MessageBox.Show("The 'bid' field cannot be empty and has to be numeric.", "Incorrect bid input");
                return;
            }
            int bid = int.Parse(this.BidTextBox.Text);
            string phoneNumber = this.PhoneNumberTextBox.Text;
            bool validPhoneNumber = (phoneNumber.Length == 9 && phoneNumber.All(char.IsDigit) || string.IsNullOrEmpty(this.PhoneNumberTextBox.Text));
            if (!validPhoneNumber)
            {
                MessageBox.Show("The 'phone number' field should contain 9 digit phone number or be empty", "Incorrect phone number input");
                return;
            }
            string adress = this.AdressTextBox.Text;

            Student student = new Student(name, lastName, bid, phoneNumber, adress, null);

            StudentList.Add(student);
        }

        private void EditStudentButtonClick(object sender, RoutedEventArgs e)
        {
            int outBidParse;
            string name = this.NameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("The 'name' field cannot be empty or filled with white-space characters", "Incorrect name input");
                return;
            }
            string lastName = this.LastNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("The 'last name' field cannot be empty or filled with white-space characters", "Incorrect last name input");
                return;
            }
            if (!int.TryParse(this.BidTextBox.Text, out outBidParse))
            {
                MessageBox.Show("The 'bid' field cannot be empty and has to be numeric.", "Incorrect bid input");
                return;
            }
            int bid = int.Parse(this.BidTextBox.Text);
            string phoneNumber = this.PhoneNumberTextBox.Text;
            bool validPhoneNumber = (phoneNumber.Length == 9 && phoneNumber.All(char.IsDigit) || string.IsNullOrEmpty(this.PhoneNumberTextBox.Text));
            if (!validPhoneNumber)
            {
                MessageBox.Show("The 'phone number' field should contain 9 digit phone number or be empty", "Incorrect phone number input");
                return;
            }
            string adress = this.AdressTextBox.Text;
            Student student = new Student(name, lastName, bid, phoneNumber, adress, null);
            try
            {
                StudentList.Insert(this.StudentListView.SelectedIndex, student);
                this.StudentList.RemoveAt(this.StudentListView.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select student from the list.", "Student not selected");
            }
        }

        private void DeleteStudentButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {

                this.StudentList.RemoveAt(this.StudentListView.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select student from the list.", "Student not selected");
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.FileName = "WeekPlan";
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = "XML documents (.xml)|*.xml";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = fileDialog.FileName;

                DatasToFile(filePath);
            }
        }

        private void DatasToFile(string filePath)
        {

            //I admit i coudn't make it work at this time
            using (var writingToFile = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                serializer.Serialize(writingToFile, StudentList);
                serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                serializer.Serialize(writingToFile, GroupStudentList);
                writingToFile.WriteLine();
                serializer = new XmlSerializer(typeof(ObservableCollection<Group>));
                serializer.Serialize(writingToFile, GroupList);
                writingToFile.WriteLine();
                serializer = new XmlSerializer(typeof(ObservableCollection<Week>));
                serializer.Serialize(writingToFile, WeekGrid);
                writingToFile.WriteLine();
            }
        }

        private void OpenButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = "XML documents (.xml)|*.xml*";

            Nullable<bool> result = fileDialog.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = fileDialog.FileName;
            }

            if (File.Exists(filename))
            {
                FileToDatas(filename);
            }
        }

        private void FileToDatas(string filename)
        {
            for (int dayHour = 0; dayHour < 24; dayHour++)
            {
                string monday = null;
                string tuesday = null;
                string wednesday = null;
                string thursday = null;
                string friday = null;
                string saturday = null;
                string sunday = null;
                Week week = new Week((dayHour + " - " + (dayHour + 1)), monday, tuesday, wednesday, thursday, friday, saturday, sunday);
                this.WeekGrid.RemoveAt(dayHour);
                this.WeekGrid.Insert(dayHour, week);
            }
            StudentList.Clear();
            GroupStudentList.Clear();
            GroupList.Clear();
            using (var readFromFile = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                ObservableCollection<Student> tmpStudentList = (ObservableCollection<Student>)deserializer.Deserialize(readFromFile);
                foreach (var item in tmpStudentList)
                {
                    StudentList.Add(item);
                }
                deserializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                foreach (var item in tmpStudentList)
                {
                    GroupStudentList.Add(item);
                }
                deserializer = new XmlSerializer(typeof(ObservableCollection<Group>));
                ObservableCollection<Group> tmpGroupList = (ObservableCollection<Group>)deserializer.Deserialize(readFromFile);
                foreach (var item in tmpGroupList)
                {
                    GroupList.Add(item);
                }
                deserializer = new XmlSerializer(typeof(ObservableCollection<Week>));
                ObservableCollection<Week> tmpWeekGrid = (ObservableCollection<Week>)deserializer.Deserialize(readFromFile);
                foreach (var item in tmpWeekGrid)
                {
                    WeekGrid.Add(item);
                }
            }
        }

        private void NewButtonClick(object sender, RoutedEventArgs e)
        {
            for (int dayHour=0; dayHour < 24; dayHour++)
            { 
                string monday =null;
                string tuesday = null;
                string wednesday = null;
                string thursday = null;
                string friday = null;
                string saturday = null;
                string sunday = null;
                Week week = new Week((dayHour + " - "+(dayHour+1)), monday, tuesday, wednesday, thursday, friday, saturday, sunday);
                this.WeekGrid.RemoveAt(dayHour);
                this.WeekGrid.Insert(dayHour, week);
                
            }
            StudentList.Clear();
            GroupStudentList.Clear();
            GroupList.Clear();
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
