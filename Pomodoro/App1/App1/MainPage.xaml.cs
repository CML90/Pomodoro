using App1.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        private ToDoServer ToDo = ToDoServer.GetInstance();
        private PomodoroServer Pomodoro = PomodoroServer.GetInstance();
        public MainPage()
        {
            InitializeComponent();

            ToDo.AssignListView(ToDoListView);

            Pomodoro.OnRunningChanged += OnPomodoroRunningChanged;
            Pomodoro.OnModeChanged += OnPomodoroModeChanged;
            Pomodoro.OnTimeChanged += OnPomodoroTimeChanged;
            Pomodoro.Reset();
        }

        public void OnPomodoroRunningChanged(bool isRunning)
        {
            Device.BeginInvokeOnMainThread(() => {
                PomSkipButton.IsVisible = isRunning;
                PomToggleButton.Text = isRunning ? "PAUSE" : "START";
            });
        }

        public void OnPomodoroModeChanged(PomodoroServer.Mode mode)
        {

        }

        public void OnPomodoroTimeChanged(int time)
        {
            Device.BeginInvokeOnMainThread(() => {
                int secs = time % 60;
                int mins = time / 60;
                PomTimeLabel.Text = mins.ToString().PadLeft(2, '0') + ":" + secs.ToString().PadLeft(2, '0');
            });
        }

        private void OnPomToggleButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.Toggle();
        }

        private void OnPomSkipButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.Skip();
        }

        private void OnPomPomModeButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.ChangeMode(PomodoroServer.Mode.Pomodoro);
        }

        private void OnPomShortModeButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.ChangeMode(PomodoroServer.Mode.ShortBreak);
        }

        private void OnPomLongModeButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.ChangeMode(PomodoroServer.Mode.LongBreak);
        }

        private async void OnSignOutButtonClicked(object sender, EventArgs e)
        {
            Pomodoro.Reset();
            await Navigation.PopAsync();
        }

        private async void OnToDoAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ToDoAddPage());
        }

        private void OnToDoDoneClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.BindingContext as ListItem;
            ToDo.Remove(item);
        }
    }
}