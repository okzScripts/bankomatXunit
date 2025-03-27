namespace banko;

using System.ComponentModel;
using Xunit;
using System;

public class TestClass
{
    static Account testAccount = new Account(5000);
    Card testCard = new Card("0123", testAccount, 0);

    static Account testAccount2 = new Account(20000);
    Card testCard2 = new Card("0123", testAccount2, 0);

    Bankomat atm = new Bankomat();



    [Fact]
    public void TestEnterIncorrectPin()
    {


        atm.insertCard(testCard);

        bool result = atm.enterPin("1111");
        atm.getMessage();

        Assert.False(result);
        Assert.Equal("Incorrect pin", atm.getMessage());

    }

    [Fact]
    public void TestBlockCard()
    {

        atm.insertCard(testCard);
        atm.getMessage();
        atm.enterPin("9999");
        atm.getMessage();
        atm.enterPin("9999");
        atm.getMessage();
        atm.enterPin("9999");
        atm.getMessage();
        atm.enterPin("9999");

        Assert.Equal("3 faulty pin entries, card is blocked", atm.getMessage());
    }

    [Fact]
    public void TestCorrectPin()
    {

        atm.insertCard(testCard);

        bool result = atm.enterPin("0123");
        atm.getMessage();

        Assert.True(result);
        Assert.Equal("Correct pin", atm.getMessage());

    }

    [Fact]
    public void TestInsertCard()
    {

        atm.insertCard(testCard);

        Assert.Equal("Card inserted", atm.getMessage());

    }

    [Fact]
    public void TestejectCard()
    {

        atm.ejectCard();
        Assert.Equal("Card removed, don't forget it!", atm.getMessage());
    }

    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(300)]
    [InlineData(500)]
    public void TestWithdrawSuccess(int amount)
    {
        atm.insertCard(testCard);
        atm.getMessage();
        atm.enterPin("0123");

        atm.getMessage();
        atm.withdraw(amount);
        atm.getMessage();

        Assert.Equal("Withdrawing " + amount, atm.getMessage());

    }

    [Fact]
    public void TestWithDrawTooMuch()
    {

        atm.insertCard(testCard);
        atm.getMessage();
        atm.enterPin("0123");
        atm.getMessage();

        atm.withdraw(10000);
        atm.getMessage();


        Assert.Equal("Card has insufficient funds", atm.getMessage());
    }

    [Fact]
    public void TestWithdrawExceedMachineBalance()
    {

        atm.insertCard(testCard2);
        atm.getMessage();

        atm.enterPin("0123");
        atm.getMessage();

        atm.withdraw(11001);
        atm.getMessage();

        Assert.Equal("Machine has insufficient funds", atm.getMessage());
    }

    [Theory]
    [InlineData(-500)]
    [InlineData(-100)]
    [InlineData(0)]
    public void TestWithDrawLessThanZeroOrZero(int amount)
    {

        atm.insertCard(testCard);
        atm.getMessage();

        atm.enterPin("0123");
        atm.getMessage();

        atm.withdraw(amount);
        atm.getMessage();


        Assert.Equal("You can not withdraw 0 or less money", atm.getMessage());

    }

    [Fact]
    public void TestGetBalance()
    {
        atm.insertCard(testCard);
        atm.getMessage();
        atm.GetBalance(testAccount);

        Assert.Equal("You have " + testAccount.getBalance() + " funds", atm.getMessage());
    }

    [Fact]
    public void TestWithdrawWithoutCard()
    {

        atm.withdraw(100000000);

        Assert.Equal("Card needed for withdrawal", atm.getMessage());
    }

    [Fact]
    public void WithDrawWithoutPin()
    {
        atm.insertCard(testCard);
        atm.getMessage();

        atm.withdraw(250);
        atm.getMessage();

        Assert.Equal("Enter PIN before withdrawal", atm.getMessage());
    }


}