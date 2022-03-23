namespace Shops.Entities
{
    public class Person
    {
        private string _name;
        private double _money;

        public Person(string name, double moneyBefore)
        {
            _name = name;
            _money = moneyBefore;
        }

        public void Withdrawal(double money)
        {
            _money -= money;
        }

        public double GetMoney()
        {
            return _money;
        }
    }
}