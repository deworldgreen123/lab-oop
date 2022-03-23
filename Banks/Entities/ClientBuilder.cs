namespace Banks.Entities
{
    public class ClientBuilder
    {
        private Client _client;

        public ClientBuilder()
        {
            Reset();
        }

        public ClientBuilder BuildName(string name)
        {
            _client.Name = name;
            return this;
        }

        public ClientBuilder BuildSecondName(string secondName)
        {
            _client.SecondName = secondName;
            return this;
        }

        public ClientBuilder BuildAddress(string address)
        {
            _client.Address = address;
            return this;
        }

        public ClientBuilder BuildPassportId(string passportId)
        {
            _client.PassportId = passportId;
            return this;
        }

        public Client GetClient()
        {
            Client result = _client;
            Reset();
            return result;
        }

        private void Reset()
        {
            _client = new Client();
        }
    }
}