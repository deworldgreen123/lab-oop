using Banks.Tools;

namespace Banks.Entities
{
    public class TransactionTransfer : Transaction
    {
        private Account _first;
        private Account _second;

        public TransactionTransfer(Account one, Account two, double sum)
            : base(sum)
        {
            _first = one;
            _second = two;
            one.GetBank().GetTransactions().Add(this);
            two.GetBank().GetTransactions().Add(this);
        }

        public override void UndoTransaction()
        {
            if (IsCanceled)
            {
                throw new BanksException("Transaction cancelled");
            }

            Account accountFirst = _first.GetAccounts().Find(x => x.Id == _first.Id);
            Account accountSecond = _second.GetAccounts().Find(x => x.Id == _second.Id);

            if (accountFirst != null) accountFirst.ResSum += Sum;
            if (accountSecond != null) accountSecond.ResSum -= Sum;

            IsCanceled = true;
        }
    }
}