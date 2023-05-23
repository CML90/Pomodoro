using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App1.Server
{
    public class ToDoServer
    {
        private static ToDoServer _instance;

        public static ToDoServer GetInstance()
        {
            if (_instance == null)
                _instance = new ToDoServer();
            return _instance;
        }

        private ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();
        private int lastId = 0;

        public void AssignListView(ListView listView)
        {
            listView.ItemsSource = items;
        }

        public void Add(string title, string desc)
        {
            items.Add(new ListItem(lastId++, title, desc));
        }

        public void Remove(ListItem item)
        {
            items.Remove(item);
        }
    }
}
