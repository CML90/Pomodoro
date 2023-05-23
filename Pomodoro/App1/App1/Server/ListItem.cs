using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App1.Server
{
    public class ListItem : INotifyPropertyChanged
    {
        public int ID { get; set; }
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }
        public ListItem(string title, string description)
        {
            ID = -1;
            Title = title;
            Description = description;
        }
        public ListItem(int id, string title, string description)
        {
            ID = id;
            Title = title;
            Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}