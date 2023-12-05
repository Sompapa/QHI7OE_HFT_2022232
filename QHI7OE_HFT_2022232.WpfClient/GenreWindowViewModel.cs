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

namespace QHI7OE_HFT_2022232.WpfClient
{
    public class GenreWindowViewModel : ObservableRecipient
    {
        public RestCollection<Genre> Genres { get; set; }

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (value != null)
                {
                    selectedGenre = new Genre()
                    {
                        GenreName = value.GenreName,
                        GenreId = value.GenreId,
                    };
                    OnPropertyChanged();
                    (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateGenreCommand { get; set; }
        public ICommand DeleteGenreCommand { get; set; }
        public ICommand UpdateGenreCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public GenreWindowViewModel()
        {

            Genres = new RestCollection<Genre>("http://localhost:59073/", "genre", "hub");

            //Genre Commands:
            //Create:
            CreateGenreCommand = new RelayCommand(() =>
            {
                Genres.Add(new Genre()
                {
                    GenreId = selectedGenre.GenreId,
                    GenreName = SelectedGenre.GenreName
                }, "http://localhost:59073/genre");
            });

            //Delete:
            DeleteGenreCommand = new RelayCommand(() =>
            {
                Genres.Delete(SelectedGenre.GenreId, "http://localhost:59073/genre");
            },
            () =>
            {
                return SelectedGenre != null;
            });

            //Update:
            UpdateGenreCommand = new RelayCommand(() =>
            {
                try
                {
                    Genres.Update(SelectedGenre, "http://localhost:59073/genre");
                }
                catch (Exception)
                {

                    throw;
                }
            });

            SelectedGenre = new Genre();
        }
    }
}
