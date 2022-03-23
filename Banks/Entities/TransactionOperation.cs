using Banks.Tools;

namespace Banks.Entities
{
    public class TransactionOperation : Transaction
    {
        private Account account;

        public TransactionOperation(Account one, double sum)
            : base(sum)
        {
            account = one;
            one.GetBank().GetTransactions().Add(this);
        }

        public override void UndoTransaction()
        {
            if (IsCanceled)
            {
                throw new BanksException("Transaction cancelled");
            }

            Account acc = account.GetAccounts().Find(x => x.Id == account.Id);

            if (acc != null)
            {
                acc.ResSum -= Sum;
            }

            IsCanceled = true;
        }
    }
}