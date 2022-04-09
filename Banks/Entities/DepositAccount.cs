using System;
using System.Collections.Generic;
using Banks.Tools;

namespace Banks.Entities
{
    public class DepositAccount : Account
    {
        private double _sumPercent;
        private double _percent;
        private DateTime _dateNow;
        private DateTime _date;
        private Dictionary<double, double> _deposit;

        public DepositAccount(Bank bank, Client client, Dictionary<double, double> deposit, double resSum, DateTime dat)
            : base(bank, client)
        {
            _deposit = deposit;
            _dateNow = DateTime.Now;
            _date = dat;
            ResSum = resSum;
            SetSumma(ResSum);
        }

        public override Transaction Withdrawals(Client client, double sum)
        {
            if (sum <= ResSum && client.GetReliable() && _dateNow > _date)
            {
                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                GetBank().GetTransactions().Add(transaction);
                IdLastTransaction = transaction.Id;
                return transaction;
            }

            if (sum <= ResSum && sum <= MaxTransSum && _dateNow > _date)
            {
                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                GetBank().GetTransactions().Add(transaction);
                IdLastTransaction = transaction.Id;
                return transaction;
            }

            throw new BanksException("sum > ResSum or person not reliable");
        }

        public override Transaction Refill(double sum)
        {
            ResSum += sum;
            var transaction = new TransactionOperation(this, sum);
            GetBank().GetTransactions().Add(transaction);
            IdLastTransaction = transaction.Id;
            return transaction;
        }

        public override Transaction Transfer(Client client, double sum, int accountId)
        {
            Transaction transaction = null;
            if (sum <= ResSum && client.GetReliable() && _dateNow > _date)
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
                if (sum < MaxTransSum && _dateNow > _date)
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
            _sumPercent += (ResSum * _percent) / 100;
            return this;
        }

        public override Account ShowMonthPercent()
        {
            ResSum += _sumPercent;
            _sumPercent = 0;
            return this;
        }

        private void SetSumma(double resSum)
        {
            foreach (KeyValuePair<double, double> deposit in _deposit)
            {
                if (!(resSum < deposit.Key)) continue;

                _percent = deposit.Value;
                break;
            }
        }
    }
}