using App1.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToDoAddPage : ContentPage
	{
		public ToDoAddPage()
		{
			InitializeComponent ();
		}

        private async void OnEnterButtonClicked(object sender, EventArgs e)
        {
			string title = EntryTitle.Text ?? "";
			string desc = EntryDesc.Text ?? "";

            if (title.Length > 0)
			{
				ToDoServer todo = ToDoServer.GetInstance();
				todo.Add(title, desc);
                await Navigation.PopModalAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = new Color(0, 0, 0, 0.5);
        }
    }
}