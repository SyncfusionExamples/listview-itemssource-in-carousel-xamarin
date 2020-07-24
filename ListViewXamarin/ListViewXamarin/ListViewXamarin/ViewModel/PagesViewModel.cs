using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class PagesViewModel
    {
        public ObservableCollection<List<Contacts>> CarouselPages { get; set; }

        public PagesViewModel(double height)
        {
            int j = 0;
            int itemsPerPage = (int)(height / 100) * 3;
            int temp = itemsPerPage;
            CarouselPages = new ObservableCollection<List<Contacts>>();
            var contactsviewmodel = new ContactsViewModel();
          
            var pageCount = Math.Ceiling((double)contactsviewmodel.ContactsInfo.Count / itemsPerPage);

            for (int i = 0; i < pageCount; i++)
            {
                var source = contactsviewmodel.ContactsInfo.Skip(j).Take(temp);
                var items = source.AsEnumerable().ToList();
                CarouselPages.Add(items);
                j += itemsPerPage;
            }
        }
    }
}
