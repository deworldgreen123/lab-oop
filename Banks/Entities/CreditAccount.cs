using System;
using Banks.Tools;

namespace Banks.Entities
{
    public class CreditAccount : Account
    {
        private readonly double _commission;
        private readonly double _creditLimit;

        public CreditAccount(Bank bank, Client person, double commission, double limit)
            : base(bank, person)
        {
            _commission = commission;
            _creditLimit = limit;
        }

        public override Transaction Withdrawals(Client client,  double sum)
        {
            if (sum <= ResSum + _creditLimit && client.GetReliable())
            {
                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                IdLastTransaction = transaction.Id;
                return transaction;
            }
            else
            {
                if (!(sum <= ResSum + _creditLimit) || !(sum <= MaxTransSum))
                {
                    throw new BanksException("sum > ResSum or person not reliable");
                }

                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                IdLastTransaction = transaction.Id;
                return transaction;
            }
        }

        public override Transaction Refill(double sum)
        {
            ResSum += sum;
            var transaction = new TransactionOperation(this, sum);
            IdLastTransaction = transaction.Id;
            return transaction;
        }

        public override Transaction Transfer(Client client, double sum, int accountId)
        {
            Transaction transaction = null;
            if (sum <= ResSum + _creditLimit && client.GetReliable())
            {
                ResSum -= sum;
                Account a = GetAccounts().Find(item => item.Id == accountId);
                if (a != null)
                {
                    a.ResSum += sum;
                    transaction = new TransactionTransfer(this, a, sum);
                }
            }
            else
            {
                if (sum <= ResSum + _creditLimit && sum <= MaxTransSum)
                {
                    ResSum -= sum;
                    Account a = GetAccounts().Find(item => item.Id == accountId);
                    if (a != null)
                    {
                        a.ResSum += sum;
                        transaction = new TransactionTransfer(this, a, sum);
                    }
                }
                else
                {
                    throw new BanksException("sum > ResSum or person not reliable");
                }
            }

            if (transaction != null)
            {
                GetBank().GetTransactions().Add(transaction);
            }

            IdLastTransaction = transaction.Id;
            return transaction;
        }

        public override Account ShowDayPercent()
        {
            return this;
        }

        public override Account ShowMonthPercent()
        {
            if (ResSum < 0)
            {
                ResSum -= Math.Abs(_commission * ResSum) / 100;
            }

            return this;
        }
    }
}