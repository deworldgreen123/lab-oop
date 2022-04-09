namespace Banks.Entities
{
    public abstract class Transaction
    {
        private static int _nextId;

        protected Transaction(double sum)
        {
            Sum = sum;
            Id = _nextId;
            _nextId++;
            IsCanceled = false;
        }

        public int Id { get; }
        protected double Sum { get; }
        protected bool IsCanceled { get; set; }

        public abstract void UndoTransaction();
    }
}