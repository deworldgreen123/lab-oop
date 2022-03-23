using System.Collections.Generic;

namespace Banks.Entities
{
    public abstract class Account : IAccount
    {
        private static int _nextId;
        private Bank _bank;

        private List<Account> _accounts;

        protected Account(Bank banks, Client client)
        {
            _accounts = new List<Account>();
            _bank = banks;
            Id = _nextId;
            _nextId++;
            _accounts.Add(this);
            IdLastTransaction = 0;
        }

        public int IdLastTransaction { get; set; }
        public int Id { get; }
        public double ResSum { get; set; }
        protected double MaxTransSum { get; set; }
        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public Bank GetBank()
        {
            return _bank;
        }

        public Bank SetBank(Bank bank)
        {
            _bank = bank;
            return _bank;
        }

        public abstract Transaction Withdrawals(Client client, double sum);
        public abstract Transaction Refill(double sum);
        public abstract Transaction Transfer(Client client, double sum, int accountId);
        public abstract Account ShowDayPercent();
        public abstract Account ShowMonthPercent();
    }
}