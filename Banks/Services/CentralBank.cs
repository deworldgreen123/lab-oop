using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks;

        public CentralBank()
        {
            _banks = new List<Bank>();
        }

        public Bank NewBank(BankBuilder builder)
        {
            Bank newBank = builder.GetBank();
            if (_banks.Any(bank => bank.Name == newBank.Name))
            {
                throw new BanksException("A bank with this name already exists");
            }

            _banks.Add(newBank);
            return newBank;
        }

        public Account NewDebitAccountInBank(Bank bank, Client client, double sum = 0)
        {
            Account newDebitAccount = bank.CreateDebitAccount(client);
            newDebitAccount.Refill(sum);
            return newDebitAccount;
        }

        public Account NewDepositAccountInBank(Bank bank, Client client, double sum, DateTime dateTime)
        {
            Account newDepositAccount = bank.CreateDepositAccount(client, sum, dateTime);
            return newDepositAccount;
        }

        public Account NewCreditAccountInBank(Bank bank, Client client, double sum = 0)
        {
            Account newCreditAccount = bank.CreateCreditAccount(client);
            newCreditAccount.Refill(sum);
            return newCreditAccount;
        }

        public void ShowPercentOverTime(int days)
        {
            foreach (Account account in _banks.SelectMany(bank => bank.GetAccounts()))
            {
                for (int i = 0; i < days; i++)
                {
                    account.ShowDayPercent();
                }

                account.ShowMonthPercent();
            }
        }

        public void SetCreditLimit(Bank bank, double newLimit)
        {
            bank.Limit = newLimit;
        }

        public void SetPercent(Bank bank, double newPercent)
        {
            bank.Percent = newPercent;
        }

        public Client AddClientToBank(Bank bank, Client client)
        {
            return bank.AddClient(client);
        }

        public Transaction Withdrawal(Bank bank, Client client, Account account, double sum)
        {
            return bank.Withdrawals(client, account, sum);
        }

        public Transaction Refill(Bank bank, Client client, Account account, double sum)
        {
            return bank.Refill(client, account, sum);
        }

        public KeyValuePair<Transaction, Transaction> Transfer(Bank bank, Client client1, Client client2, Account account1, Account account2, double sum)
        {
            return bank.Transfer(client1, client2, account1, account2, sum);
        }

        public void UndoTransaction(Account account)
        {
            foreach (Bank bank in _banks)
            {
                if (bank.GetAccounts().Contains(account))
                {
                    bank.UndoTransaction(account.IdLastTransaction);
                }
            }
        }

        public List<Bank> GetBanks()
        {
            return _banks;
        }
    }
}