using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace App1.Server
{
    public class PomodoroServer
    {
        private static PomodoroServer _instance;

        public static PomodoroServer GetInstance()
        {
            if (_instance == null)
                _instance = new PomodoroServer();
            return _instance;
        }

        public enum Mode
        {
            Pomodoro,
            ShortBreak,
            LongBreak,
        }

        public delegate void ModeChanger(Mode mode);
        public delegate void IntChanger(int newInt);
        public delegate void BoolChanger(bool newBool);

        public event BoolChanger OnRunningChanged = delegate { };
        public event ModeChanger OnModeChanged = delegate { };
        public event IntChanger OnTimeChanged = delegate { };

        private Timer timer;
        private Mode CurrentMode = Mode.Pomodoro;
        private bool IsRunning = false;
        private int CurrentTime = 0;
        private int Pomodoros = 0;

        public void Reset()
        {
            timer?.Dispose();
            CurrentMode = Mode.Pomodoro;
            IsRunning = false;
            CurrentTime = GetTimeByMode(CurrentMode);
            Pomodoros = 0;

            OnModeChanged(CurrentMode);
            OnTimeChanged(CurrentTime);
            OnRunningChanged(IsRunning);
        }

        private static int GetTimeByMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.Pomodoro: return 1500;
                case Mode.LongBreak: return 900;
                case Mode.ShortBreak: return 300;
                default: return 0;
            }
        }

        public Mode GetCurrentMode() => CurrentMode;
        public int GetCurrentTime() => CurrentTime;

        public void ChangeMode(Mode mode)
        {
            CurrentMode = mode;
            CurrentTime = GetTimeByMode(mode);
            Stop();
            OnModeChanged(CurrentMode);
            OnTimeChanged(CurrentTime);
        }

        public void Toggle()
        {
            if (IsRunning)
                timer.Dispose();
            else
                timer = new Timer(_timerElapsed, null, 1000, 1000);
            IsRunning = !IsRunning;
            OnRunningChanged(IsRunning);
        }

        private void Stop()
        {
            timer.Dispose();
            IsRunning = false;
            OnRunningChanged(IsRunning);
        }

        public void Skip()
        {
            if (!IsRunning)
                return;
            UpdateMode();
        }

        private void UpdateMode()
        {
            if (CurrentMode == Mode.Pomodoro)
            {
                ChangeMode((Pomodoros == 3) ? Mode.LongBreak : Mode.ShortBreak);
                Pomodoros = (++Pomodoros) % 4;
            }
            else
                ChangeMode(Mode.Pomodoro);
        }

        private void _timerElapsed(object state)
        {
            if (CurrentTime <= 0)
                UpdateMode();
            else
            {
                CurrentTime--;
                OnTimeChanged(CurrentTime);
            }
        }
    }
}
