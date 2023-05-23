using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SignInLogo.TranslateTo(SignInLogo.TranslationX, 15, 200);
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            await SignInLogo.TranslateTo(SignInLogo.TranslationX, -350, 120);
            await Navigation.PushAsync(new MainPage());
            //if (ProfileServer.GetInstance().AccountExists(UsernameEntry.Text, PasswordEntry.Text))
            //    await Navigation.PushAsync(new MainPage());
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}
