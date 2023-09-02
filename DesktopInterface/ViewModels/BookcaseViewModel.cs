using Caliburn.Micro;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using DesktopInterface.Models;
using DesktopInterface.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopInterface.ViewModels
{
    public class BookcaseViewModel : Screen
    {
        // Variables
        private readonly IBookDataConnector bookDataConnector;

        private bool tempBookIsEdit = false;

        // Backing fields
        private BookDisplayModel _tempBookItem = new BookDisplayModel();
        private bool _displayAddBook;
        private BindingList<BookDisplayModel> _bookcaseItems = new BindingList<BookDisplayModel>();
        private BookDisplayModel? _selectedBookcaseItem = null;

        // Bound Properties
        /// <summary>
        /// A temporary placeholder for the book to be added to collection
        /// Or the book in the collection that should be edited 
        /// </summary>
        public BookDisplayModel TempBookItem
        {
            get { return _tempBookItem; }
            set
            {
                _tempBookItem = value;
                NotifyOfPropertyChange(() => TempBookItem);
            }
        }

        public string ControlPanelBookLabel
        {
            get { return $"{(tempBookIsEdit ? "Edit" : "Add")} Book"; }
            set
            {
                NotifyOfPropertyChange(() => ControlPanelBookLabel);
            }
        }

        public bool DisplayAddBook
        {
            get { return _displayAddBook; }
            set
            {
                _displayAddBook = value;
                NotifyOfPropertyChange(() => DisplayAddBook);
                NotifyOfPropertyChange(() => DisplayAddBookInverted);
            }
        }

        public bool DisplayAddBookInverted => !DisplayAddBook;

        public BindingList<BookDisplayModel> BookcaseItems
        {
            get { return _bookcaseItems; }
            set
            {
                _bookcaseItems = value;
                NotifyOfPropertyChange(() => BookcaseItems);
                NotifyOfPropertyChange(() => SelectedBookcaseItem);
                NotifyOfPropertyChange(() => CanClearBooks);
            }
        }

        public BookDisplayModel SelectedBookcaseItem
        {
            get { return _selectedBookcaseItem; }
            set
            {
                _selectedBookcaseItem = value;
                NotifyOfPropertyChange(() => SelectedBookcaseItem);
                NotifyOfPropertyChange(() => CanDeleteBook);
                NotifyOfPropertyChange(() => CanEditBook);
            }
        }

        public BookcaseViewModel(IBookDataConnector bookDataConnector)
        {
            this.bookDataConnector = bookDataConnector;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            await LoadBookcase();
        }

        private async Task LoadBookcase()
        {
            try
            {
                List<BookDataModel> result = await bookDataConnector.GetAll();

                List<BookDisplayModel> displayModels = new List<BookDisplayModel>();

                foreach (var item in result)
                {
                    displayModels.Add(new BookDisplayModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ISBN = item.ISBN,
                        Author = item.Author,
                        Description = item.Description,
                    });
                }

                BookcaseItems = new BindingList<BookDisplayModel>(displayModels);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ResetTempBookFields()
        {
            TempBookItem = new BookDisplayModel();
        }

        #region Action Conditions

        public bool CanAddBook
        {
            get
            {
                return true;
            }
        }

        public bool CanEditBook
        {
            get
            {
                if (BookcaseItems.Count <= 0) return false;
                if (SelectedBookcaseItem == null) return false;

                return true;
            }
        }

        public bool CanDeleteBook
        {
            get
            {
                if (BookcaseItems.Count <= 0) return false;
                if (SelectedBookcaseItem == null) return false;

                return true;
            }
        }

        public bool CanClearBooks
        {
            get
            {
                if (BookcaseItems.Count <= 0) return false;

                return true;
            }
        }

        public bool CanCancelAddBook
        {
            get { return true; }
        }

        public bool CanConfirmAddBook
        {
            get { return true; }
        }

        #endregion

        #region Action Invokers

        public void AddBook()
        {
            tempBookIsEdit = false;
            DisplayAddBook = true;

            NotifyOfPropertyChange(() => ControlPanelBookLabel);
        }

        public void EditBook()
        {
            TempBookItem = SelectedBookcaseItem;

            tempBookIsEdit = true;
            DisplayAddBook = true;

            NotifyOfPropertyChange(() => ControlPanelBookLabel);
        }

        public async Task DeleteBookAsync()
        {
            MessageBoxResult firstResult = MessageBox.Show($"Delete '{SelectedBookcaseItem.Title}' from storage? This cannot be undone.", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

            if (firstResult == MessageBoxResult.Yes)
            {
                try
                {
                    await bookDataConnector.Delete(SelectedBookcaseItem.Id);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    await LoadBookcase();
                }
            }
        }

        public async Task ClearBooks()
        {
            MessageBoxResult firstResult = MessageBox.Show("You are about to delete every book from your database. Continue?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

            if (firstResult == MessageBoxResult.Yes)
            {
                MessageBoxResult secondResult = MessageBox.Show("Really delete all the data from this app?", "Last Chance!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

                if (secondResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        await bookDataConnector.DeleteAll();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    finally
                    {
                        await LoadBookcase();
                    }
                }
            }
        }

        public void CancelAddBook()
        {
            ResetTempBookFields();
            DisplayAddBook = false;
        }

        public async Task ConfirmAddBook()
        {
            if (string.IsNullOrWhiteSpace(TempBookItem.Title))
            {
                MessageBox.Show("Please enter a book title", "Syntax Error");
                FocusTitleField();
                return;
            }

            if (tempBookIsEdit)
            {
                try
                {
                    await bookDataConnector.Update(new BookDataModel
                    {
                        Id = TempBookItem.Id,
                        Title = TempBookItem.Title,
                        ISBN = TempBookItem.ISBN,
                        Author = TempBookItem.Author,
                        Description = TempBookItem.Description,
                    });

                    FocusTitleField();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                try
                {
                    await bookDataConnector.Insert(new BookDataModel
                    {
                        Title = TempBookItem.Title,
                        ISBN = TempBookItem.ISBN,
                        Author = TempBookItem.Author,
                        Description = TempBookItem.Description,
                    });

                    FocusTitleField();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            ResetTempBookFields();
            await LoadBookcase();
        }

        private void FocusTitleField()
        {
            this.SetFocus("tbTempBookItemTitle");
        }

        #endregion
    }
}
