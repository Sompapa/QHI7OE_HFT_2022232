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
    public class MangaWindowViewModel : ObservableRecipient
    {

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


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
                        MangaId = value.MangaId,
                        Title = value.Title,
                        Genre = value.Genre,
                        Price = value.Price,
                        Author = value.Author,
                        Release = value.Release
                    };
                    SetProperty(ref selectedManga, value);
                    (DeleteMangaCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateMangaCommand { get; set; }
        public ICommand DeleteMangaCommand { get; set; }
        public ICommand UpdateMangaCommand { get; set; }

        public static bool IsInDesingnMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MangaWindowViewModel()
        {
            SelectedManga = new Manga();
            if (!IsInDesingnMode)
            {
                Mangas = new RestCollection<Manga>("http://localhost:59073/", "manga", "hub");

                CreateMangaCommand = new RelayCommand(() =>
                {
                    Mangas.Add(new Manga()
                    {
                        Title = SelectedManga.Title,
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
            }
        }
    }
}

