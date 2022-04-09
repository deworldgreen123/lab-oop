using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static CentralBank _centralBank = new CentralBank();
        private static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Сhoose an answer \n1. Add client \n2. Add bank \n3. AddAccount \n4. Exit");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        AnswerClient();
                        break;
                    case "2":
                        AnswerBank();
                        break;
                    case "3":
                        AnswerAccount();
                        break;
                    case "4":
                        return;
                }
            }
        }

        private static void AnswerClient()
        {
            Console.WriteLine("Which bank do you want to register with?");
            string nameBank = Console.ReadLine();
            Bank bank = _centralBank.GetBanks().FirstOrDefault(bank => bank.Name == nameBank);

            var clientBuilder = new ClientBuilder();
            Console.WriteLine("What is your name?");
            clientBuilder.BuildName(Console.ReadLine());
            Console.WriteLine("What is your surname?");
            clientBuilder.BuildSecondName(Console.ReadLine());
            Console.WriteLine("Do you want to send the address and passport details?\n1. Yes\n2. No");
            string ans = Console.ReadLine();
            if (ans == "1")
            {
                Console.WriteLine("What is your Address?");
                clientBuilder.BuildAddress(Console.ReadLine());
                Console.WriteLine("What is your PassportId?");
                clientBuilder.BuildPassportId(Console.ReadLine());
            }

            _centralBank.AddClientToBank(bank, clientBuilder.GetClient());
        }

        private static void AnswerBank()
        {
            var bankBuilder = new BankBuilder();
            Console.WriteLine("What is the name of the bank?");
            bankBuilder.BuildName(Console.ReadLine());
            Console.WriteLine("What is the bank's commission?");
            bankBuilder.BuildCommission(Convert.ToDouble(Console.ReadLine()));
            Console.WriteLine("What is the bank's percentage?");
            bankBuilder.BuildPercent(Convert.ToDouble(Console.ReadLine()));
            Console.WriteLine("What is the bank's credit limit?");
            bankBuilder.BuildLimit(Convert.ToDouble(Console.ReadLine()));
            Console.WriteLine("What is the maximum amount of withdrawal and transfer from the account?");
            bankBuilder.BuildMaxTransSum(Convert.ToDouble(Console.ReadLine()));
            Console.WriteLine("How many levels of interest on the deposit?");

            int countLevel = Convert.ToInt32(Console.ReadLine());
            var depositBank = new Dictionary<double, double>();
            for (int i = 1; i < countLevel; i++)
            {
                Console.WriteLine("What is the percentage for the deposit " + i + "?");
                double percentDeposit = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("What is the limit for the deposit " + i + "?");
                double limitDeposit = Convert.ToDouble(Console.ReadLine());
                depositBank.Add(percentDeposit, limitDeposit);
            }

            Console.WriteLine("What is the percentage for the last deposit?");
            double lastPercent = Convert.ToDouble(Console.ReadLine());
            depositBank.Add(lastPercent, -1);
            bankBuilder.BuildDeposit(depositBank);
            Bank bewBank = _centralBank.NewBank(bankBuilder);

            Console.WriteLine("Received information about the bank");
            string bankInfo = "Bank name: ";
            bankInfo += bewBank.Name + " \nPercent: " + bewBank.Percent + "% \nCommission: " + bewBank.Commission + "% \nCredit limit: ";
            bankInfo += bewBank.Limit + "rub \nTransfer and Withdrawals limit: " + bewBank.MaxTransSum + " rub\nDeposit: \n";
            double low = 0;
            foreach (KeyValuePair<double, double> deposit in bewBank.Deposit)
            {
                if (deposit.Value == -1)
                {
                    bankInfo += "(" + low + "; inf): " + deposit.Key + "\n";
                    break;
                }

                bankInfo += "(" + low + "; " + deposit.Value + "): " + deposit.Key;
                low = deposit.Value;
            }

            Console.WriteLine(bankInfo);
        }

        private static void AnswerAccount()
        {
            while (true)
            {
                Console.WriteLine("Сhoose an answer \n1. Add Debit account \n2. Add Deposit account \n3. Add Credit account \n4. Back");
                string answerAccount = Console.ReadLine();
                switch (answerAccount)
                {
                    case "1":
                        AnswerDebit();
                        break;
                    case "2":
                        AnswerDeposit();
                        break;
                    case "3":
                        AnswerCredit();
                        break;
                    case "4":
                        return;
                }
            }
        }

        private static void AnswerCredit()
        {
            Console.WriteLine("What is the name of the bank?");
            string nameBank = Console.ReadLine();
            foreach (Bank bank in _centralBank.GetBanks().Where(bank => bank.Name == nameBank))
            {
                Console.WriteLine("Write your name");
                string nameClient = Console.ReadLine();
                Console.WriteLine("Write your surname");
                string surnameClient = Console.ReadLine();
                Client client = bank.FindClient(nameClient, surnameClient);
                if (client == null)
                {
                    Console.WriteLine("This client is not in the bank");
                }

                Console.WriteLine("Write the sum");
                double sum = Convert.ToDouble(Console.ReadLine());
                _centralBank.NewCreditAccountInBank(bank, client, sum);
                return;
            }

            Console.WriteLine("There is no bank with that name");
        }

        private static void AnswerDeposit()
        {
            Console.WriteLine("What is the name of the bank?");
            string nameBank = Console.ReadLine();
            foreach (Bank bank in _centralBank.GetBanks().Where(bank => bank.Name == nameBank))
            {
                Console.WriteLine("Write your name");
                string nameClient = Console.ReadLine();
                Console.WriteLine("Write your surname");
                string surnameClient = Console.ReadLine();
                Client client = bank.FindClient(nameClient, surnameClient);
                if (client == null)
                {
                    Console.WriteLine("This client is not in the bank");
                }

                Console.WriteLine("Write the sum");
                double sum = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Write the date. \nExample: dd.mm.yyyy");
                string date = Console.ReadLine();
                if (date == null || date.Length != 10)
                {
                    Console.WriteLine("Invalid date");
                }

                int day = Convert.ToInt32(date.Substring(0, 2));
                int mount = Convert.ToInt32(date.Substring(3, 2));
                int year = Convert.ToInt32(date.Substring(6, 4));
                _centralBank.NewDepositAccountInBank(bank, client, sum, new DateTime(year, mount, day));
                return;
            }

            Console.WriteLine("There is no bank with that name");
        }

        private static void AnswerDebit()
        {
            Console.WriteLine("What is the name of the bank?");
            string nameBank = Console.ReadLine();
            foreach (Bank bank in _centralBank.GetBanks().Where(bank => bank.Name == nameBank))
            {
                Console.WriteLine("Write your name");
                string nameClient = Console.ReadLine();
                Console.WriteLine("Write your surname");
                string surnameClient = Console.ReadLine();
                Client client = bank.FindClient(nameClient, surnameClient);
                if (client == null)
                {
                    Console.WriteLine("This client is not in the bank");
                }

                Console.WriteLine("Write the sum");
                double sum = Convert.ToDouble(Console.ReadLine());
                _centralBank.NewDebitAccountInBank(bank, client, sum);
                return;
            }

            Console.WriteLine("There is no bank with that name");
        }
    }
}
