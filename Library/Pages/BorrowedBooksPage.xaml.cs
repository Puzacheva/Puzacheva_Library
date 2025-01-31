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
    /// Логика взаимодействия для BorrowedBooksPage.xaml
    /// </summary>
    public partial class BorrowedBooksPage : Page
    {
        public BorrowedBooksPage()
        {
            InitializeComponent();

            DataGridBorroweBooks.ItemsSource = Entities.GetContext().BorrowedBook
                .Select(BorBook => new
                {
                    Book = BorBook.Book.Title,
                    User = BorBook.User.FIO,
                    BorBook.Borrow_Date,
                    BorBook.Return_Date
                }).ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddBorrowedBookPage(null));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var booksForRemoving = DataGridBorroweBooks.SelectedItems.Cast<BorrowedBook>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {booksForRemoving.Count()} элементов?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entities.GetContext().BorrowedBook.RemoveRange(booksForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridBorroweBooks.ItemsSource = Entities.GetContext().BorrowedBook.ToList();
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
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridBorroweBooks.ItemsSource = Entities.GetContext().BorrowedBook
                .Select(BorBook => new
                {
                    Book = BorBook.Book.Title,
                    User = BorBook.User.FIO,
                    BorBook.Borrow_Date,
                    BorBook.Return_Date
                }).ToList();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddBorrowedBookPage((sender as Button).DataContext as BorrowedBook));
        }
    }
}
