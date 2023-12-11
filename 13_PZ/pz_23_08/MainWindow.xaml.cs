using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace pz_23_08
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student s = new Student()
            {
                login = login_txt.Text,
                password = password_txt.Password,
                email = email_txt.Text,
                password_again = password_again_txt.Password
            };
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new ValidationContext(s, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(s, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    MessageBox.Show(validationResult.ErrorMessage);
                }
            }
            else
            {
                MessageBox.Show("Операция прошла хорошо!");
            }
        }
        private void login_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (login_txt.Text == "Логин") login_txt.Text = "";
        }

        private void email_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void password_txt_TextChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void password_again_txt_TextChanged(object sender, RoutedEventArgs e)
        {
            
        }
    }
    class Student
    {
        [Required(ErrorMessage = "Имя скажи пжпжпж")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Нормальный логин нужно, понимаешь?")]
        public string login { get; set; }

        [Required(ErrorMessage = "Пароль пора писать")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Слишком легко")]
        public string password { get; set; }

        [EmailAddress(ErrorMessage = "Не похоже на почту!")]
        public string email { get; set; }

        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        public string password_again { get; set; }
    }
}
