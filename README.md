# How to share items source among Xamarin.Forms ListView with carousel page (SfListView)

You can programmatically split the collections of items among the Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview) loaded in [CarouselPage](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/carousel-page).

You can also refer the following article.

https://www.syncfusion.com/kb/11789/how-to-share-items-source-among-xamarin-forms-listview-with-carousel-page-sflistview

**XAML**

Define [CarouselPage](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.carouselpage) and load the ListViewPage in [CarouselPage.ItemTemplate](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.multipage-1.itemtemplate#Xamarin_Forms_MultiPage_1_ItemTemplate).

``` xml
<CarouselPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
              xmlns:local="clr-namespace:ListViewXamarin"
              x:Class="ListViewXamarin.ListViewCarousel">
    <CarouselPage.ItemTemplate>
        <DataTemplate>
            <local:ListViewPage/>
        </DataTemplate>
    </CarouselPage.ItemTemplate>
</CarouselPage>
```

**XAML**

**SfListView** is defined in the ListViewPage with binding of (.)

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.ListViewPage">
	 <ContentPage.Content>
        <StackLayout>
            <syncfusion:SfListView x:Name="listView" ItemSize="100" ItemsSource="{Binding .}">
                <syncfusion:SfListView.LayoutManager>
                    <syncfusion:GridLayout SpanCount="3"/>
                </syncfusion:SfListView.LayoutManager>
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <Grid x:Name="grid">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="0.3*" />
                                <ColumnDefinition Width="0.7*" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                            <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center">
                                <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**C#**

In the [OnSizeAllocated](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.page.onsizeallocated) override, send page height to ViewModel class to determine the items count to split the [ItemsSource](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.multipage-1.itemssource#Xamarin_Forms_MultiPage_1_ItemsSource) for the **CarouselPage**.

``` c#
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
```

**C#**

In carousel **PagesViewModel** determines **ItemsPerPage** count based on [ItemSize](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemSize.html) and [SpanCount](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GridLayout~SpanCount.html) which determines the number of pages based on the Items count. **PagesViewModel** defines the collection for each page and add to the collection which bound to the [SfListView.ItemsSource](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemsSource.html).

``` c#
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
```

**Output**

![ListViewInCarouselView](https://github.com/SyncfusionExamples/listview-itemssource-in-carousel-xamarin/blob/master/ScreenShot/ListViewInCarouselView.gif)
