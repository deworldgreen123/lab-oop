using Banks.Tools;

namespace Banks.Entities
{
     public class DebitAccount : Account
    {
        private double _sumPercent;
        private double _percent;

        public DebitAccount(Bank bank, Client client, double percent, double maxTransSum)
            : base(bank, client)
        {
            _sumPercent = 0;
            _percent = percent;
            MaxTransSum = maxTransSum;
        }

        public override Transaction Withdrawals(Client client, double sum)
        {
            if (sum <= ResSum && client.GetReliable())
            {
                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                GetBank().GetTransactions().Add(transaction);
                IdLastTransaction = transaction.Id;
                return transaction;
            }
            else
            {
                if (!(sum <= ResSum) || !(sum <= MaxTransSum))
                {
                    throw new BanksException("sum > ResSum or person not reliable");
                }

                ResSum -= sum;
                var transaction = new TransactionOperation(this, -sum);
                GetBank().GetTransactions().Add(transaction);
                IdLastTransaction = transaction.Id;
                return transaction;
            }
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
            if (sum <= ResSum && client.GetReliable())
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
                if (sum <= MaxTransSum && sum <= ResSum)
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
    }
}