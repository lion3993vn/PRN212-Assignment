using Repository.Repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PE_PRN211_SP23_TrialTest_PhamVanTuanHieu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserRoleRepository _userRoleRepository;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Text;

            _userRoleRepository = new();
            var user = _userRoleRepository.Login(username, password);
            if (user != null) 
            {
                if(user.UserRole1 != 2)
                {
                    MessageBox.Show("You have not permission to access this application!", "Error", MessageBoxButton.OK);
                }
                else
                {
                    StudentManagement form = new StudentManagement();
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButton.OK);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}