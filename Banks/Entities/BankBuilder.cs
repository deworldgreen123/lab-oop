using System.Collections.Generic;

namespace Banks.Entities
{
    public class BankBuilder
    {
        private Bank _bank = new Bank(0, 0, 0, 0, new Dictionary<double, double>());

        public BankBuilder()
        {
            Reset();
        }

        public BankBuilder BuildName(string name)
        {
            _bank.Name = name;
            return this;
        }

        public BankBuilder BuildCommission(double commission)
        {
            _bank.Commission = commission;
            return this;
        }

        public BankBuilder BuildPercent(double percent)
        {
            _bank.Percent = percent;
            return this;
        }

        public BankBuilder BuildLimit(double limit)
        {
            _bank.Limit = limit;
            return this;
        }

        public BankBuilder BuildMaxTransSum(double maxTransSum)
        {
            _bank.MaxTransSum = maxTransSum;
            return this;
        }

        public BankBuilder BuildDeposit(Dictionary<double, double> deposit)
        {
            _bank.Deposit = deposit;
            return this;
        }

        public Bank GetBank()
        {
            Bank result = _bank;
            Reset();
            return result;
        }

        private void Reset()
        {
            _bank = new Bank(0, 0, 0, 0, new Dictionary<double, double>());
        }
    }
}