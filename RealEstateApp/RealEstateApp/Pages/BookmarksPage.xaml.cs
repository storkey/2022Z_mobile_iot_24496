using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class BookmarksPage : ContentPage
{
    public BookmarksPage()
    {
        InitializeComponent();
        GetPropertiesList();
    }

    private async void GetPropertiesList()
    {
        var properties = await ApiService.GetBookmarkList();
        CvBookmarks.ItemsSource = properties;
    }

    private void CvBookmarks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
        ((CollectionView)sender).SelectedItem = null;
    }

    private void CvBookmarks_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;
        if (currentSelection == null) return;
        Navigation.PushAsync(new PropertyDetailPage(currentSelection.PropertyId));
        ((CollectionView)sender).SelectedItem = null;
    }
}
