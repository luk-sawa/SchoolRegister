using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolRegister
{
    public partial class AddGroupWindow : Window
    {
        public AddGroupWindow()
        {
            InitializeComponent();            
        }

        private void WindowAddGroupButtonClick(object sender, RoutedEventArgs e)
        {
            string groupName = this.GroupNameBox.Text;
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("The 'group name' field cannot be empty or filled with white-space characters", "Incorrect name input");
                return;
            }
            string lector = this.LectorBox.Text;
            if (string.IsNullOrWhiteSpace(lector))
            {
                MessageBox.Show("The 'lector' field cannot be empty or filled with white-space characters", "Incorrect last name input");
                return;
            }

            Group group = new Group(groupName, lector);
            ((MainWindow)Application.Current.MainWindow).GroupList.Add(group);
            this.Close();
        }

        private void WindowCancelGroupButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
