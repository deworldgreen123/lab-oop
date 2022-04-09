using System;
using System.Collections.Generic;

namespace Banks.Entities
{
    public class Client
    {
        private static int _nextId;
        private List<Account> _accounts;
        private bool _reliable;

        public Client()
        {
            Id = _nextId;
            _nextId++;
            _accounts = new List<Account>();
            _reliable = false;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string PassportId { get; set; }

        public Account AddAccount(Account account)
        {
            _accounts.Add(account);
            return account;
        }

        public void MakeReliable(string address, string passportId)
        {
            Address = address;
            PassportId = passportId;
            _reliable = true;
        }

        public void CheckReliable()
        {
            if ((Address == null) || (PassportId == null))
            {
                _reliable = false;
            }
            else
            {
                _reliable = true;
            }
        }

        public void CallChange(string call)
        {
            Console.WriteLine(call);
        }

        public int GetNextId()
        {
            return _nextId;
        }

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public bool GetReliable()
        {
            return _reliable;
        }
    }
}