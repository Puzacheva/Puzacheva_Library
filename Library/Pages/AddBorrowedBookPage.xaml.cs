using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBorrowedBookPage.xaml
    /// </summary>
    public partial class AddBorrowedBookPage : Page
    {
        private BorrowedBook _currentBook = new BorrowedBook();

        public AddBorrowedBookPage(BorrowedBook selectedBook)
        {
            InitializeComponent();

            ComboBoxBook.ItemsSource = Entities.GetContext().Book.ToList();
            ComboBoxBook.DisplayMemberPath = "Title";
            ComboBoxBook.SelectedValuePath = "ID";

            ComboBoxUser.ItemsSource = Entities.GetContext().User.ToList();
            ComboBoxUser.DisplayMemberPath = "FIO";
            ComboBoxUser.SelectedValuePath = "ID";

            if (selectedBook != null)
                _currentBook = selectedBook;

            DataContext = _currentBook;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxBook.SelectedItem == null || ComboBoxUser.SelectedItem == null
                || BorrowDatePicker.SelectedDate == null || ReturnDatePicker.SelectedDate == null)

            {
                MessageBox.Show("Все поля являются обязательными!");
                return;
            }

            if (_currentBook.ID == 0)
                Entities.GetContext().BorrowedBook.Add(_currentBook);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
