using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        IPaymentSystem[] paymentSystems =
        {
            new PaymentSystemOrder(new MD5HashGenerator()),
            new PaymentSystemPay(new MD5HashGenerator()),
            new PaymentSystemPayWhithAmount(new Sha1HashGenerator(), "XXXXXXX")
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
    private readonly IHashGenerator _hashGenerator;

    public PaymentSystemOrderBase(IHashGenerator hashGenerator) => 
        _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));

    public abstract string GetPayingLink(Order order);

    protected string GetHash(string input) => _hashGenerator.GetHash(input);
}

public class PaymentSystemOrder : PaymentSystemOrderBase
{
    public PaymentSystemOrder(IHashGenerator hashGenerator) : base(hashGenerator) { }

    public override string GetPayingLink(Order order) =>
        $"pay.system1.ru/order?amount={order.Amount + order.Curency}&hash={GetHash(order.Id.ToString())}";
}

public class PaymentSystemPay : PaymentSystemOrderBase
{
    public PaymentSystemPay(IHashGenerator hashGenerator) : base(hashGenerator) { }

    public override string GetPayingLink(Order order) =>
        $"order.system2.ru/pay?hash={GetHash(order.Id.ToString() + order.Amount.ToString())}";
}

public class PaymentSystemPayWhithAmount : PaymentSystemOrderBase
{
    private readonly string _key;

    public PaymentSystemPayWhithAmount(IHashGenerator hashGenerator, string key) : base(hashGenerator) 
        => _key = key;

    public override string GetPayingLink(Order order) =>
        $"system3.com/pay?amount={order.Amount}&curency={order.Curency}" +
        $"&hash={GetHash(order.Amount.ToString() + order.Id.ToString() + _key)}";
}

public interface IHashGenerator
{
    public string GetHash(string input);
}

public abstract class HashGenerator : IHashGenerator
{
    public string GetHash(string input) =>
        Convert.ToHexString(HashData(Encoding.UTF8.GetBytes(input)));

    protected abstract byte[] HashData(byte[] bytes);
}

public class MD5HashGenerator : HashGenerator
{
    protected override byte[] HashData(byte[] bytes) => MD5.HashData(bytes);
}

public class Sha1HashGenerator : HashGenerator
{
    protected override byte[] HashData(byte[] bytes) => SHA1.HashData(bytes);
}