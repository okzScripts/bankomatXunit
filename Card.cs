namespace banko;

public class Card
{

    public string pin = "0123";
    public int pinTries = 0;

    public Card(string _pin, Account _account, int _pintries)
    {

        pin = _pin;
        account = _account;
        pinTries = _pintries;
    }
    public Account account;

    public Card(Account account)
    {
        this.account = account;
    }

}
