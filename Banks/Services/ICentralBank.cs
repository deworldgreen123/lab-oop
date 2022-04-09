using System;
using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Services
{
    public interface ICentralBank
    {
        Bank NewBank(BankBuilder builder);

        Account NewDebitAccountInBank(Bank bank, Client client, double sum);
        Account NewDepositAccountInBank(Bank bank, Client client, double sum, DateTime dateTime);
        Account NewCreditAccountInBank(Bank bank, Client client, double sum);

        void ShowPercentOverTime(int days);

        void SetCreditLimit(Bank bank, double newLimit);
        void SetPercent(Bank bank, double newPercent);

        Client AddClientToBank(Bank bank, Client client);

        Transaction Refill(Bank bank, Client client, Account account, double sum);
        Transaction Withdrawal(Bank bank, Client client, Account account, double sum);
        KeyValuePair<Transaction, Transaction> Transfer(Bank bank, Client client1, Client client2, Account account1, Account account2, double sum);
        void UndoTransaction(Account account);
        List<Bank> GetBanks();
    }
}