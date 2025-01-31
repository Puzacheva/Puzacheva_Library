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
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Page
    {
        private Book _currentBook = new Book();

        public AddBook(Book selectedBook)
        {
            InitializeComponent();

            ComboBoxGenre.ItemsSource = Entities.GetContext().Genre.ToList();
            ComboBoxGenre.DisplayMemberPath = "Genre1";
            ComboBoxGenre.SelectedValuePath = "ID";

            if (selectedBook != null)
                _currentBook = selectedBook;

            DataContext = _currentBook;
        }

        private void TextBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintTitle.Visibility = string.IsNullOrEmpty(TextBoxTitle.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TextBoxAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintAuthor.Visibility = string.IsNullOrEmpty(TextBoxAuthor.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TextBoxYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintYear.Visibility = string.IsNullOrEmpty(TextBoxYear.Text) ? Visibility.Visible : Visibility.Collapsed;

        }

        private void TextBoxCopies_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintCopies.Visibility = string.IsNullOrEmpty(TextBoxCopies.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxGenre.SelectedItem == null || string.IsNullOrEmpty(TextBoxTitle.Text)
                || string.IsNullOrEmpty(TextBoxAuthor.Text) || string.IsNullOrEmpty(TextBoxYear.Text)
                || string.IsNullOrEmpty(TextBoxCopies.Text))

            {
                MessageBox.Show("Все поля являются обязательными!");
                return;
            }

            if (_currentBook.ID == 0)
                Entities.GetContext().Book.Add(_currentBook);

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
