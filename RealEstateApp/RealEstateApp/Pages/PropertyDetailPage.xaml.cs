using Plugin.LocalNotification;
using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Security.Cryptography;

namespace RealEstateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private bool IsBookmarkEnabled;
    private int propertyId;
    private int bookmarkId;
    public PropertyDetailPage(int propertyId)
    {
        InitializeComponent();
        GetPropertyDetail(propertyId);
        this.propertyId = propertyId;
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await ApiService.GetPropertyDetail(propertyId);
        LblPrice.Text = "$ " + property.Price;
        LblDescription.Text = property.Detail;
        LblAddress.Text = property.Address;
        ImgProperty.Source = property.FullImageUrl;
        phoneNumber = property.Phone;

        if (property.Bookmark == null)
        {
            ImgbookmarkBtn.Source = "bookmark_empty_icon";
            IsBookmarkEnabled = false;
        }
        else
        {
            ImgbookmarkBtn.Source = property.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
            bookmarkId = property.Bookmark.Id;
            IsBookmarkEnabled = true;
        }
    }
    void ImgbackBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    async void ImgbookmarkBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        if (IsBookmarkEnabled == false)
        {
            var addBookmark = new AddBookmark()
            {
                User_Id = Preferences.Get("userId", 0),
                PropertyId = propertyId
            };
            var response = await ApiService.AddBookmark(addBookmark);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_fill_icon";
                IsBookmarkEnabled = true;
            }
        }
        else
        {
            var response = await ApiService.DeleteBookmark(bookmarkId);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_empty_icon";
                IsBookmarkEnabled = false;
            }

            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "Usuniêto! Szkoda :(",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }
}