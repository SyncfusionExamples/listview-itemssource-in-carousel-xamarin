using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewCarousel : CarouselPage
    {
        PagesViewModel pagesViewModel;

        public ListViewCarousel()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (height > 0 && BindingContext == null)
            {
                pagesViewModel = new PagesViewModel(height);
                BindingContext = pagesViewModel;
                ItemsSource = pagesViewModel.CarouselPages;
            }
            base.OnSizeAllocated(width, height);
        }
    }
}