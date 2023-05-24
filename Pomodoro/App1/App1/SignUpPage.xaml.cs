using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
           
            await SignUpLogo.RotateTo(360,240);
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            if (ProfileServer.GetInstance().AddAccount(UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text))
            {
                await Application.Current.SavePropertiesAsync();
                await Navigation.PopAsync();
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}