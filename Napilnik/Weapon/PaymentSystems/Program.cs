using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        IPaymentSystem[] paymentSystems =
        {
            new PaymentSystemOrder(new MD5HashGenerator(), "pay.system1.ru"),
            new PaymentSystemPay(new MD5HashGenerator(), "order.system2.ru"),
            new PaymentSystemPayWhithAmount(new Sha1HashGenerator(), "system3.com", "XXXXXXX")
        };

        Order order = new Order(1, 12000, "RUB");

        foreach (var paymentSystem in paymentSystems)
            paymentSystem.GetPayingLink(order);
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;
    public readonly string Curency;

    public Order(int id, int amount, string curency) => (Id, Amount, Curency) = (id, amount, curency);
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public abstract class PaymentSystemOrderBase : IPaymentSystem
{
    private IHashGenerator _hashGenerator;

    public PaymentSystemOrderBase(IHashGenerator hashGenerator, string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException();

        Url = url;
        _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));
    }

    public string Url { get; }

    public abstract string GetPayingLink(Order order);

    protected string GetHash(string input) => _hashGenerator.GetHash(input);
}

public class PaymentSystemOrder : PaymentSystemOrderBase
{
    public PaymentSystemOrder(IHashGenerator hashGenerator, string url) : base(hashGenerator, url) { }

    public override string GetPayingLink(Order order) =>
        $"{Url}/order?amount={order.Amount + order.Curency}&hash={GetHash(order.Id.ToString())}";
}

public class PaymentSystemPay : PaymentSystemOrderBase
{
    public PaymentSystemPay(IHashGenerator hashGenerator, string url) : base(hashGenerator, url) { }

    public override string GetPayingLink(Order order) =>
        $"{Url}/pay?hash={GetHash(order.Id.ToString() + order.Amount.ToString())}";
}

public class PaymentSystemPayWhithAmount : PaymentSystemOrderBase
{
    private readonly string _key;

    public PaymentSystemPayWhithAmount(IHashGenerator hashGenerator, string url, string key) 
        : base(hashGenerator, url) 
        => _key = key;

    public override string GetPayingLink(Order order) =>
        $"{Url}/pay?amount={order.Amount}&curency={order.Curency}" +
        $"&hash={GetHash(order.Amount.ToString() + order.Id.ToString() + _key)}";
}

public interface IHashGenerator
{
    public string GetHash(string input);
}

public abstract class HashGenerator : IHashGenerator
{
    public string GetHash(string input)
    {
        return Convert.ToHexString(HashData(Encoding.UTF8.GetBytes(input)));
    }

    protected abstract byte[] HashData(byte[] bytes);
}

public class MD5HashGenerator : HashGenerator
{
    protected override byte[] HashData(byte[] bytes)
    {
        return MD5.HashData(bytes);
    }
}

public class Sha1HashGenerator : HashGenerator
{
    protected override byte[] HashData(byte[] bytes)
    {
        return SHA1.HashData(bytes);
    }
}