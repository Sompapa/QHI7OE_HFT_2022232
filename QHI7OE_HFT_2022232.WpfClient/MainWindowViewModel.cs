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
        //Manga:
        public RestCollection<Manga> Mangas { get; set; }

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
                        Title = value.Title,
                        MangaId = value.MangaId,
                        Genre = value.Genre,
                        Author = value.Author,
                        Rating = value.Rating,
                        Release = value.Release,
                        Price = value.Price
                    };
                    OnPropertyChanged();
                    (DeleteMangaCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                
            }
            
        }

        public ICommand CreateMangaCommand { get; set; }
        public ICommand DeleteMangaCommand { get; set; }
        public ICommand UpdateMangaCommand { get; set; }
        public ICommand OpenMangaWindow { get; set; }

        //Author:
        public RestCollection<Author> Authors { get; set; }

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
                        AuthorName = value.AuthorName,
                        AuthorId = value.AuthorId,
                    };
                    OnPropertyChanged();
                    (DeleteAuthorCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
            
        }

        public ICommand CreateAuthorCommand { get; set; }
        public ICommand DeleteAuthorCommand { get; set; }
        public ICommand UpdateAuthorCommand { get; set; }


        //Genre:
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


        //DesignMode:
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
            //RestCollections:
            Mangas = new RestCollection<Manga>("http://localhost:59073/", "manga");
            Authors = new RestCollection<Author>("http://localhost:59073/", "author");
            Genres = new RestCollection<Genre>("http://localhost:59073/", "genre");
            

            //Manga Commands:
            //Create:
            CreateMangaCommand = new RelayCommand(()=>
            {
                Mangas.Add(new Manga()
                {
                    Title = SelectedManga.Title,
                    GenreId = SelectedManga.GenreId,
                    AuthorId = SelectedManga.AuthorId,
                    Rating = SelectedManga.Rating,
                    Release = SelectedManga.Release,
                    Price = SelectedManga.Price
                });

            });

            //Delete:
            DeleteMangaCommand = new RelayCommand(() =>
            {
                Mangas.Delete(SelectedManga.MangaId);
            },
            ()=>
            {
                return SelectedManga != null;
            });

            //Update:
            UpdateMangaCommand = new RelayCommand(() =>
            {
                try
                {
                    Mangas.Update(SelectedManga);
                }
                catch (Exception)
                {

                    throw;
                }
            });

            OpenMangaWindow = new RelayCommand(() =>
            {
                new MangaWindow().ShowDialog();
            });

            SelectedManga = new Manga();

            //Author Commands:
            //Create:
            CreateAuthorCommand = new RelayCommand(() =>
            {
                Authors.Add(new Author()
                {
                    AuthorName = selectedAuthor.AuthorName
                });
            });

            //Delete:
            DeleteAuthorCommand = new RelayCommand(() =>
            {
                Authors.Delete(selectedAuthor.AuthorId);
            },
            ()=>
            {
                return selectedAuthor != null;
            });

            //Update:
            UpdateAuthorCommand = new RelayCommand(() =>
            {
                try
                {
                    Authors.Update(selectedAuthor);
                }
                catch (Exception)
                {

                    throw;
                }
            });

            SelectedAuthor = new Author();

            //Genre Commands:
            //Create:
            CreateGenreCommand = new RelayCommand(() =>
            {
                Genres.Add(new Genre()
                {
                    GenreName=SelectedGenre.GenreName
                });
            });

            //Delete:
            DeleteGenreCommand = new RelayCommand(() =>
            {
                Genres.Delete(SelectedGenre.GenreId);
            },
            ()=>
            {
                return SelectedGenre != null;
            });

            //Update:
            UpdateGenreCommand = new RelayCommand(() =>
            {
                try
                {
                    Genres.Update(SelectedGenre);
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
