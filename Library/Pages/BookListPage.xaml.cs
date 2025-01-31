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
    /// Логика взаимодействия для BookListPage.xaml
    /// </summary>
    public partial class BookListPage : Page
    {
        public BookListPage()
        {
            InitializeComponent();

            DataGridBooks.ItemsSource = Entities.GetContext().Book
                .Select(book => new
                {
                    book.Title,
                    book.Author,
                    Genre = book.Genre1.Genre1,
                    book.Year,
                    book.CopiesAvailable
                }).ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddBook(null));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var booksForRemoving = DataGridBooks.SelectedItems.Cast<Book>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {booksForRemoving.Count()} элементов?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entities.GetContext().Book.RemoveRange(booksForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridBooks.ItemsSource = Entities.GetContext().Book.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DataGridBooks.ItemsSource = null; DataGridBooks.ItemsSource = Entities.GetContext().Book.ToList();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddBook((sender as Button).DataContext as Book));
        }
    }
}
