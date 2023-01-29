using RealEstateApp.Services;
using Plugin.LocalNotification;

namespace RealEstateApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	async private void BtnRegister_Clicked(object sender, EventArgs e)
	{
		var response = await ApiService.RegisterUser(FullName.Text, Email.Text, Password.Text, Phone.Text);

		if (response)
		{

			await DisplayAlert("", "Twoje konto zosta�o utworzone", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
		else
		{
			await DisplayAlert("", "Co� posz�o nie tak...", "Anuluj");
		}
	}

    async private void TapSignIn_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}