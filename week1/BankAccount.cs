using System.Security.Cryptography;

internal class BankAccount
{
    private decimal _balance;

    public string HolderName {get;}
    private int _holderAge;

    public BankAccount(string name, int age)
    {
        HolderName = name;
        _holderAge = age;
    }

    public void Deposite(decimal amount)
    {
        if(amount <= 0)
        {
            throw new ArgumentException("Invalid deposit");
        }

        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if(amount <= 0 || amount >= _balance)
            throw new ArgumentException("Invalid withdrawal");

        _balance -= amount;
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public int HolderAge
    {
        get {return _holderAge;}

        set
        {
            if(value >= 18 && value <= 60)
            {
                _holderAge = value;
            }
            else
            {
                throw new ArgumentException("Age must be between 18 and 60.");
            }
        }
    }
}