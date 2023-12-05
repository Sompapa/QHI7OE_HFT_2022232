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
    public class AuthorWindowViewModel : ObservableRecipient
    {
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

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public AuthorWindowViewModel()
        {

            //Create
            Authors = new RestCollection<Author>("http://localhost:59073/", "author", "hub");

            CreateAuthorCommand = new RelayCommand(() =>
            {
                Authors.Add(new Author()
                {
                    AuthorId = selectedAuthor.AuthorId,
                    AuthorName = selectedAuthor.AuthorName
                }, "http://localhost:59073/author");
            });

            //Delete:
            DeleteAuthorCommand = new RelayCommand(() =>
            {
                Authors.Delete(selectedAuthor.AuthorId, "http://localhost:59073/author");
            },
            () =>
            {
                return selectedAuthor != null;
            });

            //Update:
            UpdateAuthorCommand = new RelayCommand(() =>
            {
                try
                {
                    Authors.Update(selectedAuthor, "http://localhost:59073/author");
                }
                catch (Exception)
                {

                    throw;
                }
            });

            SelectedAuthor = new Author();
        }
    }
}
