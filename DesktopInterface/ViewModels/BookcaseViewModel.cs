using Caliburn.Micro;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using DesktopInterface.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DesktopInterface.ViewModels
{
    public class BookcaseViewModel : Screen
    {
        private readonly IBookDataConnector bookDataConnector;

        private BindingList<BookDisplayModel> _bookcaseItems = new BindingList<BookDisplayModel>();

        public BindingList<BookDisplayModel> BookcaseItems
        {
            get { return _bookcaseItems; }
            set
            {
                _bookcaseItems = value;
                NotifyOfPropertyChange(() => BookcaseItems);
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
    }
}
