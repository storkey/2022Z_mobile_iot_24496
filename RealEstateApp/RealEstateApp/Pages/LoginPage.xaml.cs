using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    async private void BtnLogin_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.Login(Email.Text, Password.Text);
        if (response)
        {
            Application.Current.MainPage = new CustomTabbedPage();
        }
        else
        {
            await DisplayAlert("", "Coœ posz³o nie tak...", "Anuluj");
        }
    }

    async private void TapRegister_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}