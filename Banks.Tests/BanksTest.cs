using System;
using System.Collections.Generic;
using NUnit.Framework;
using Banks.Entities;
using Banks.Services;
using Banks.Tools;

namespace Banks.Tests
{
    public class BankTests
    {
        private ICentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void AddClientWithoutReliable()
        {
            BankBuilder bankBuilder = new BankBuilder().BuildMaxTransSum(100);
            Bank bank = _centralBank.NewBank(bankBuilder);
            ClientBuilder clientBuilder = new ClientBuilder().BuildName("Bair").BuildSecondName("Enkeev");
            Client client = _centralBank.AddClientToBank(bank, clientBuilder.GetClient());
            Account account = _centralBank.NewDebitAccountInBank(bank, client, 100);
            Assert.Catch<BanksException>(() =>
            {
                _centralBank.Refill(bank, client, account, 60);
            });
            client.MakeReliable("SPB", "311510");
            _centralBank.Refill(bank, client, account, 60);
        }

        [Test]
        public void CheckShowPercentOverTime()
        {
            BankBuilder bankBuilder = new BankBuilder().BuildPercent(1).BuildMaxTransSum(100);
            Bank bank = _centralBank.NewBank(bankBuilder);
            ClientBuilder clientBuilder = new ClientBuilder().BuildName("Bair").BuildSecondName("Enkeev").BuildAddress("SPB").BuildPassportId("311510");
            Client client = _centralBank.AddClientToBank(bank, clientBuilder.GetClient());
            Account account = _centralBank.NewDebitAccountInBank(bank, client, 100);
            _centralBank.ShowPercentOverTime(30);
            Assert.True(Math.Abs(account.ResSum - 130) < 0.001);
        }

        [Test]
        public void UndoTransaction()
        {
            BankBuilder bankBuilder = new BankBuilder().BuildMaxTransSum(100);
            Bank bank = _centralBank.NewBank(bankBuilder);
            ClientBuilder clientBuilder = new ClientBuilder().BuildName("Bair").BuildSecondName("Enkeev").BuildAddress("SPB").BuildPassportId("311510");
            Client client = _centralBank.AddClientToBank(bank, clientBuilder.GetClient());
            Account account = _centralBank.NewDebitAccountInBank(bank, client, 100);
            _centralBank.Withdrawal(bank, client, account,50);
            Assert.True(Math.Abs(account.ResSum - 50) < 0.001);
            _centralBank.UndoTransaction(account);
            Assert.True(Math.Abs(account.ResSum - 100) < 0.001);
        }
    }
}