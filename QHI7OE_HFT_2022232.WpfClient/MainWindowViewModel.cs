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
        private RestCollection<KeyValuePair<string, double>> StDbnoncruds { get; set; }

        public RestCollection<KeyValuePair<string, double>> StDbNonCruds
        {
            get { return StDbnoncruds; }
            set
            {
                StDbnoncruds = value;
                OnPropertyChanged();
            }
        }

        private RestCollection<KeyValuePair<string, DateTime>> StDtnoncruds { get; set; }

        public RestCollection<KeyValuePair<string, DateTime>> StDtNonCruds
        {
            get { return StDtnoncruds; }
            set
            {
                StDtnoncruds = value;
                OnPropertyChanged();
            }
        }

        public ICommand AVGPriceByGenre { get; set; }
        public ICommand AVGPriceByAuthor { get; set; }
        public ICommand AllPriceByGenre { get; set; }
        public ICommand AllpriceByYears { get; set; }
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

            AVGPriceByAuthor = new RelayCommand( async () =>
            {
                StDbNonCruds = new RestCollection<KeyValuePair<string, double>>("http://localhost:59073/", "stat/avgpricebyauthor", "hub");
            });

            AVGPriceByGenre = new RelayCommand(async () =>
            {
                StDbNonCruds = new RestCollection<KeyValuePair<string, double>>("http://localhost:59073/", "stat/avgpricebygenre", "hub");
            });

            AllPriceByGenre = new RelayCommand(async () =>
            {
                StDbNonCruds = new RestCollection<KeyValuePair<string, double>>("http://localhost:59073/", "stat/allpricebygenre", "hub");
            });

            AllpriceByYears = new RelayCommand(async () =>
            {
                StDtNonCruds = new RestCollection<KeyValuePair<string, DateTime>>("http://localhost:59073/", "stat/allpricebyyear", "hub");
            });

        }
    }
}
