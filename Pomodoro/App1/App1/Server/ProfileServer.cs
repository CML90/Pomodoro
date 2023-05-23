using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Server
{
    public sealed class ProfileServer
    {
        private static ProfileServer _instance;

        public static ProfileServer GetInstance()
        {
            if (_instance == null)
                _instance = new ProfileServer();
            return _instance;
        }

        private struct Account
        {
            public string Username;
            public string Email;
            public string Password;

            public Account(string username, string email, string password)
            {
                Username = username;
                Email = email;
                Password = password;
            }
        }

        private List<Account> accounts = new List<Account>();

        public bool AddAccount(string username, string email, string password)
        {
            if (username.Length == 0 || email.Length == 0 || password.Length == 0)
                return false;
            int usedAccountsCount = accounts
                .Where((account) => account.Username == username || account.Email == email)
                .Count();
            if (usedAccountsCount > 0)
                return false;
            accounts.Add(new Account(username, email, password));
            return true;
        }

        public bool AccountExists(string username, string password)
        {
            return accounts
                .Where((account) => account.Username == username && account.Password == password)
                .Count() > 0;
        }
    }
}
