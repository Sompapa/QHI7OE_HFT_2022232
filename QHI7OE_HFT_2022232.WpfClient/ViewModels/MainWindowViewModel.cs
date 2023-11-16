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

namespace QHI7OE_HFT_2022232.WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Manga> Mangas { get; set; }
        public RestCollection<Author> Authors { get; set; }
        public RestCollection<Genre> Genres { get; set; }

        public ICommand CreateMangaCommand { get; set; }
        public ICommand DeleteMangaCommand { get; set; }
        public ICommand UpdateMangaCommand { get; set; }
        public ICommand CreateAuthorCommand { get; set; }
        public ICommand DeleteAuthorCommand { get; set; }
        public ICommand UpdateAuthorCommand { get; set; }
        public ICommand CreateGenreCommand { get; set; }
        public ICommand DeleteGenreCommand { get; set; }
        public ICommand UpdateGenreCommand { get; set; }

        private Manga selectedManga;

        public Manga SelectedManga
        {
            get { return selectedManga; }
            set
            {
                if (value != null)
                {
                    selectedManga = new Manga()
                    {
                        MangaId = value.MangaId,
                        Title = value.Title,
                        Author = value.Author,
                        Genre = value.Genre,
                        Price = value.Price,
                        Release = value.Release,
                        Rating = value.Rating
                    };
                }
                OnPropertyChanged();
                (DeleteMangaCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Manga newManga;

        public Manga NewManga
        {
            get { return newManga; }
            set
            {
                if (value != null)
                {
                    newManga = new Manga()
                    {
                        MangaId = value.MangaId,
                        Title = value.Title,
                        Author = value.Author,
                        Genre = value.Genre,
                        Price = value.Price,
                        Release = value.Release,
                        Rating = value.Rating
                    };
                }
                OnPropertyChanged();
                (DeleteMangaCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Author selectedAuthor;

        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                if (value != null)
                {
                    selectedAuthor = new Author()
                    {
                        AuthorId = value.AuthorId,
                        AuthorName = value.AuthorName,
                        Mangas = value.Mangas,

                    };
                }
                OnPropertyChanged();
                (DeleteAuthorCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

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
                        Mangas = value.Mangas,

                    };
                }
                OnPropertyChanged();
                (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public static bool IsInDesingnMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public int IdCounter (int id)
        {
            return id++;
        }

        public MainWindowViewModel()
        {
            if (!IsInDesingnMode)
            {
                Mangas = new RestCollection<Manga>("http://localhost:59073/", "manga", "hub");
                Authors = new RestCollection<Author>("http://localhost:59073/", "author", "hub");
                Genres = new RestCollection<Genre>("http://localhost:59073/", "genre", "hub");

                //Manga
                CreateMangaCommand = new RelayCommand(() =>
                {
                    Mangas.Add(new Manga()
                    {
                        Title = newManga.Title

                    });
                });

                UpdateMangaCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Mangas.Update(SelectedManga);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteMangaCommand = new RelayCommand(() =>
                {
                    Mangas.Delete(SelectedManga.MangaId);
                },
                () =>
                {
                    return SelectedManga != null;
                });

                //Author
                CreateAuthorCommand = new RelayCommand(() =>
                {
                    Authors.Add(new Author()
                    {
                        AuthorName = SelectedAuthor.AuthorName,
                    });
                });

                UpdateAuthorCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Authors.Update(SelectedAuthor);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteAuthorCommand = new RelayCommand(() =>
                {
                    Authors.Delete(SelectedAuthor.AuthorId);
                },
                () =>
                {
                    return SelectedAuthor != null;
                });

                //Genre
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
