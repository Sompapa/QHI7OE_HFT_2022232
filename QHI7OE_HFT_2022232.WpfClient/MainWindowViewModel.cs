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
using System.Windows.Controls;
using System.Windows.Input;

namespace QHI7OE_HFT_2022232.WpfClient
{
    public  class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<KeyValuePair<string, double>> NonCruds { get; set; }

        private RestCollection<KeyValuePair<string, double>> avgPriceByAuthor;
        public RestCollection<KeyValuePair<string, double>> AVGPriceByAuthor 
        { 
            get { return avgPriceByAuthor; }
            set { }

        }

        public ICommand AVGPrice_Author { get; set; }
        public ICommand OpenMangaWindow { get; set; }
        public ICommand OpenAuthorWindow { get; set; }
        public ICommand OpenGenreWindow { get; set; }

        public static bool IsInDesignMode 
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {

            OpenMangaWindow = new RelayCommand(() =>
            {
                new MangaWindow().ShowDialog();
            });

            OpenAuthorWindow = new RelayCommand(() =>
            {
                new AuthorWindow().ShowDialog();
            });

            OpenGenreWindow = new RelayCommand(() =>
            {
                new GenreWindow().ShowDialog();
            });

            AVGPrice_Author = new RelayCommand(async() =>
            {
                AVGPriceByAuthor = NonCruds.GetNonCrudData("http://localhost:59073/Stat/AVGPriceByAuthor");
            });

        }
    }
}
