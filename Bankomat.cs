using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Interfaces;

namespace banko;

public class Bankomat
{

    bool cardInserted = false;
    Card card;
    int amount;
    int machineBalance = 11000;
    List<string> msgs = new List<string>();

    bool CardPinOK = false;

    public string getMessage()
    {
        var msg = "";
        if (msgs.Count > 0)
        {
            // return from shift left:
            msg = msgs[0]; // pick
            msgs.RemoveAt(0); // clear
        }
        return msg; // return 
    }

    public void insertCard(Card card)
    {
        cardInserted = true;
        this.card = card;
        msgs.Add("Card inserted");
    }

    public void ejectCard()
    {
        cardInserted = false;
        msgs.Add("Card removed, don't forget it!");
    }

    public bool enterPin(string pin)
    {
        card.pinTries++;
        if (card.pinTries < 3)
        {

            if (card.pin == pin)
            {
                msgs.Add("Correct pin");
                CardPinOK = true;
                return true;
            }
            else
            {
                msgs.Add("Incorrect pin");
                return false;
            }
        }
        else
        {
            msgs.Add("3 faulty pin entries, card is blocked");
            return false;
        }
    }

    public int withdraw(int amount)
    {
        if (cardInserted != false)
        {
            if (CardPinOK == enterPin(card.pin))
            {

                if (amount > 0 && amount <= machineBalance && amount <= card.account.getBalance())
                {
                    machineBalance -= amount;
                    card.account.withdraw(amount);
                    msgs.Add("Withdrawing " + amount);
                    return amount;
                }
                else
                {
                    if (amount > machineBalance)
                    {
                        msgs.Add("Machine has insufficient funds");
                    }
                    else if (amount > card.account.getBalance())
                    {
                        msgs.Add("Card has insufficient funds");
                    }
                    else if (amount <= 0)
                    {
                        msgs.Add("You can not withdraw 0 or less money");
                    }
                    return 0;
                }
            }
            else
            {
                msgs.Add("Enter PIN before withdrawal");
                return 0;
            }
        }
        else
        {
            msgs.Add("Card needed for withdrawal");
            return 0;
        }
    }

    //Ny funktion!
    public int GetBalance(Account account)
    {

        msgs.Add("You have " + account.getBalance() + " funds");
        return 0;
    }

}
