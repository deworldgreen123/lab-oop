namespace Banks.Entities
{
    public interface IAccount
    {
        Transaction Withdrawals(Client client, double sum);
        Transaction Refill(double sum);
        Transaction Transfer(Client client, double sum, int accountId);
    }
}