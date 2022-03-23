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
            while (true)
            {
                Console.WriteLine("Сhoose an answer \n1. Add client \n2. Add bank \n3. Exit");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
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
                        break;
                    case "2":
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
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
