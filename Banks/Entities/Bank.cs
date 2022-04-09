using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.Entities
{
    public class Bank
    {
        private List<Client> _subscribers;
        private List<Client> _clients;
        private List<Account> _accounts;
        private List<Transaction> _transactions;
        private double _percent;
        private double _limit;

        public Bank(double commission, double percent, double limit, double maxTransSum, Dictionary<double, double> deposit)
        {
            _subscribers = new List<Client>();
            _clients = new List<Client>();
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
            Deposit = new Dictionary<double, double>();
            Deposit = deposit;
            Commission = commission;
            Percent = percent;
            Limit = limit;
            MaxTransSum = maxTransSum;
        }

        public string Name { get; set; }

        public double Percent
        {
            get => _percent;
            set
            {
                foreach (Client subscriber in _subscribers)
                {
                    subscriber.CallChange("In the " + Name + " bank, the credit limit changed from " + _percent + " to " +
                                          value);
                }

                _percent = value;
            }
        }

        public double Commission { get; set; }
        public double Limit
        {
            get => _limit;
            set
            {
                foreach (Client subscriber in _subscribers)
                {
                    subscriber.CallChange("In the " + Name + " bank, the percentage changed from " + _limit + " to " +
                                          value);
                }

                _limit = value;
            }
        }

        public double MaxTransSum { get; set; }
        public Dictionary<double, double> Deposit { get; set; }

        public Client AddClient(Client client)
        {
            // Console.WriteLine("Do you want to subscribe to changes in interest on debit and credit? \n1. Yes \n2. No");
            // string answer = Console.ReadLine();
            // switch (answer)
            // {
            //     case "1":
            //         _subscribers.Add(client);
            //         break;
            // }
            _clients.Add(client);
            return client;
        }

        public Account CreateDebitAccount(Client client)
        {
            Client a = _clients.Find(x => x.Id == client.Id);
            if (a == null)
            {
                throw new BanksException("Client not found");
            }

            var debit = new DebitAccount(this, client, Percent, MaxTransSum);
            client.GetAccounts().Add(debit);
            _accounts.Add(debit);
            debit.SetBank(this);
            return debit;
        }

        public Account CreateCreditAccount(Client client)
        {
            Client a = _clients.Find(x => x.Id == client.Id);
            if (a == null)
            {
                throw new BanksException("Client not found");
            }

            var credit = new CreditAccount(this, client, Commission, Limit);
            client.GetAccounts().Add(credit);
            _accounts.Add(credit);
            credit.SetBank(this);
            return credit;
        }

        public Account CreateDepositAccount(Client client, double sum, DateTime date)
        {
            Client a = _clients.Find(x => x.Id == client.Id);
            if (a == null)
            {
                throw new BanksException("Client not found");
            }

            var deposit = new DepositAccount(this, client, Deposit, sum, date);
            client.GetAccounts().Add(deposit);
            _accounts.Add(deposit);
            deposit.SetBank(this);
            return deposit;
        }

        public void UndoTransaction(int id)
        {
            Transaction a = _transactions.Find(x => x.Id == id);
            if (a != null)
            {
                a.UndoTransaction();
            }
            else
            {
                throw new BanksException("transaction not found");
            }
        }

        public Transaction Withdrawals(Client client, Account account, double sum)
        {
            Client a = _clients.Find(x => x.Id == client.Id);
            if (a == null)
            {
                throw new BanksException("Client not found");
            }

            Account acc = a.GetAccounts().Find(x => x.Id == account.Id);
            if (acc == null)
            {
                throw new BanksException("Account not found");
            }

            return acc.Withdrawals(client, sum);
        }

        public Transaction Refill(Client client, Account account, double sum)
        {
            Client a = _clients.Find(x => x.Id == client.Id);
            if (a == null)
            {
                throw new BanksException("Client not found");
            }

            a.CheckReliable();
            if (!a.GetReliable())
            {
                throw new BanksException("Client not Reliable");
            }

            Account acc = a.GetAccounts().Find(x => x.Id == account.Id);
            if (acc == null)
            {
                throw new BanksException("Account not found");
            }

            return acc.Refill(sum);
        }

        public KeyValuePair<Transaction, Transaction> Transfer(Client client1, Client client2, Account account1, Account account2, double sum)
        {
            Client a1 = _clients.Find(x => x.Id == client1.Id);
            Client a2 = _clients.Find(x => x.Id == client2.Id);
            if (a1 == null || a2 == null)
            {
                throw new BanksException("Client not found");
            }

            Account acc1 = a1.GetAccounts().Find(x => x.Id == account1.Id);
            Account acc2 = a2.GetAccounts().Find(x => x.Id == account2.Id);
            if (acc1 == null || acc2 == null)
            {
                throw new BanksException("Account not found");
            }

            Transaction transaction1 = acc1.Withdrawals(a1, sum);
            Transaction transaction2 = acc2.Withdrawals(a2, sum);
            return new KeyValuePair<Transaction, Transaction>(transaction1, transaction2);
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public Client FindClient(string name, string surname)
        {
            foreach (Client client in _clients.Where(client => client.Name == name && client.SecondName == surname))
            {
                return client;
            }

            return null;
        }
    }
}