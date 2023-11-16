using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QHI7OE_HFT_2022232.WpfClient.ViewModels
{
    public class AuthorWindowViewModel
    {
        public Author Actual { get; set; }

        public void SetUp(Author author)
        {
            Actual = author;
        }

        public AuthorWindowViewModel()
        {
         
        }
    }

}
