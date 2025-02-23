﻿using System;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
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
    }
}
