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
    public class GenreWindowViewModel : ObservableRecipient
    {

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

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
                        GenreId = value.GenreId,
                        GenreName = value.GenreName,
                        Mangas = value.Mangas
                    };
                    SetProperty(ref selectedGenre, value);
                    (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }

        public ICommand CreateGenreCommand { get; set; }
        public ICommand DeleteGenreCommand { get; set; }
        public ICommand UpdateGenreCommand { get; set; }

        public static bool IsInDesingnMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public GenreWindowViewModel()
        {
            SelectedGenre = new Genre();
            if (!IsInDesingnMode)
            {
                Genres = new RestCollection<Genre>("http://localhost:59073/", "manga", "hub");

                CreateGenreCommand = new RelayCommand(() =>
                {
                    Genres.Add(new Genre()
                    {
                        GenreName = SelectedGenre.GenreName,
                    });
                });

                UpdateGenreCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Genres.Update(SelectedGenre);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                        throw;
                    }
                });

                DeleteGenreCommand = new RelayCommand(() =>
                {
                    Genres.Delete(SelectedGenre.GenreId);
                },
                () =>
                {
                    return SelectedGenre != null;
                });
            }
        }
    }
}
