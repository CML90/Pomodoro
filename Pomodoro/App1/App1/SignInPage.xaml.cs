using App1.Server;
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

            await SignInLogo.TranslateTo(SignInLogo.TranslationX, 15, 400, Easing.SpringOut);
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            if (ProfileServer.GetInstance().AccountExists(UsernameEntry.Text, PasswordEntry.Text))
            {
                await SignInLogo.TranslateTo(SignInLogo.TranslationX, -350, 400, Easing.SpringIn);
                await GoToMainPage();
            }
        }

        private async Task GoToMainPage()
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        Easing SillyEase = new Easing(x =>
        {
            double res = Math.Sin(x * Math.PI);
            res *= res * (Math.Sin(x * Math.PI * 5.0) + 1.0) * 0.5;
            return res;
        });

        private async void OnSignInLogoTapped(object sender, EventArgs e)
        {
            await SignInLogo.TranslateTo(SignInLogo.TranslationX, -100, 1000, SillyEase);
            await SignInLogo.TranslateTo(SignInLogo.TranslationX, 15, 0);
        }
    }
}
