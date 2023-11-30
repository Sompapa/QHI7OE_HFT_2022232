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
    public class MangaWindowViewModel : ObservableRecipient
    {
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

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MangaWindowViewModel()
        {
            //NonCruds = new RestCollection<object>("http://localhost:59073/", "stat", "hub");
            Mangas = new RestCollection<Manga>("http://localhost:59073/", "manga", "hub");

            //Create:
            CreateMangaCommand = new RelayCommand(() =>
            {
                Mangas.Add(new Manga()
                {
                    Title = SelectedManga.Title,
                    Genre = SelectedManga.Genre,
                    Author = SelectedManga.Author,
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
            () =>
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

            SelectedManga = new Manga();

        }
    }
}
