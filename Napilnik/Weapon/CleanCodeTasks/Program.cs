namespace CleanCodeHW1
{
    //1. 2.Даже простой алгоритм можно угробить тупым названием метода Задание Переименуйте метод -
    class Program
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
    }
}

namespace CleanCodeHW2
{
    //2. 7.При именовании имеет смысл использовать упрощенный английский Задание
    //Используйте более распространённое и простое слово в название метода  
    class Program
    {
        public static int FindIndex(int[] array, int element)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == element)
                    return i;

            return -1;
        }
    }
}

namespace CleanCodeHW3
{
    //3. 10. Магические числа нужно всегда заменять на константы Задание. Измените код
    class Weapon
    {
        private const int BulletPerShoot = 1;
        private const int Zero = 0;//Думал еще в сторону EmptyCount и вообе целесообразности этой константы, но все так все.

        private int _bulletsCount;

        public bool CanShoot() => _bulletsCount > Zero;

        public void Shoot() => _bulletsCount -= BulletPerShoot;
    }
}

namespace CleanCodeHW4
{
    //4. 11. Даже деды против венгерской нотации Задание. Перепишите код
    class Program
    {
        public void Main()
        {
            int armySize = 10;
            int coinsCount = 10;
            string name = "Vladislav";
        }
    }
}

namespace CleanCodeHW5
{
    //5. 14. Берите имена из предметной области. Не мяукало, а кошка. Задание. Перед вами оружие
    class Shooter//или Weapon
    {
        public void Shoot() { }
    }
}

namespace CleanCodeHW6
{
    //6. 15. Имена классов и объектов должны предоставлять собой существительные Задание. Переименуйте классы
    class Player { }
    class Attacker { }
    class TargetFollower { }
    class UnitFactory
    {
        public IReadOnlyCollection<Unit> Units { get; private set; }
    }

    public class Unit { }
}

namespace CleanCodeHW7
{
    //7. 17. Методы set должны устанавливать значение из параметра Задание.
    //Метод Set устанавливает значение через свой параметр, плохая история когда вы под Set
    //маскируете более информативный глагол. Т.е если вы через этот метод не устанавливаете значение
    //(например метод SetMoney(int amount) который установит значение), то придумайте информативный глагол.
    //Попробуйте переназвать эти методы
    class Program
    {
        private static object _chance;
        private static int _hourlyRate;

        public static void CreateMapObject()
        {
            //Создание объекта на карте
        }

        public static void CalculateChance()
        {
            _chance = Random.Range(0, 100);
        }

        public static int GetSalary(int hoursWorked)
        {
            return _hourlyRate * hoursWorked;
        }
    }
}

namespace CleanCodeHW8
{
    //8. 20. Группировка полей по префиксу Задание. Поправьте код
    public class Player
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public Mover Mover { get; private set; }
        public Weapon Weapon { get; private set; }
    }

    public class Mover
    {
        public float DirectionX { get; private set; }
        public float DirectionY { get; private set; }
        public float Speed { get; private set; }

        public void Move() { }
    }

    public class Weapon
    {
        public int Damage { get; private set; }
        public float Cooldown { get; private set; }

        public void Attack() { }

        public bool IsReloading()
        {
            throw new NotImplementedException();
        }
    }
}

namespace CleanCodeHW9
{
    //9. 27. В функции можно использовать функции её уровня и на один ниже Задание. Проведите рефакторинг
    using Crestron.SimplSharp.CrestronData;
    using Crestron.SimplSharp.SQLite;
    using System.Reflection;
    using System.Windows.Forms;

    class VoitingDataBase
    {
        private TextBox passportTextbox;
        private Label textResult;

        public class Form1 : Form
        {
            internal static byte[] ComputeSha256Hash(string rawData)
            {
                throw new NotImplementedException();
            }
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            string passportData = passportTextbox.Text.Trim().Replace(" ", string.Empty);

            if (InputIsValid(passportData) == false)
                return;

            if (TryGetData(passportData, out DataTable dataTable))
                ShowResult(dataTable);
        }

        private bool TryGetData(string passportData, out DataTable dataTable)
        {
            SQLiteConnection connection = CreateConnection("db");
            dataTable = null;

            try
            {
                connection.Open();
                string commandText = $"select * from passports where num='{Form1.ComputeSha256Hash(passportData)}' limit 1;";
                dataTable = GetDataFromDB(commandText, connection);
                connection.Close();
            }
            catch (SQLiteException ex)
            {
                const int MissingDbCode = 1;

                if (ex.ErrorCode == MissingDbCode)
                    MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }

            return dataTable != null;
        }

        private static SQLiteConnection CreateConnection(string dbName)
        {
            string connectionString = $"Data Source={GetLocation()}\\{dbName}.sqlite";

            return new SQLiteConnection(connectionString);
        }

        private static string GetLocation() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private void ShowResult(DataTable dataTable1)
        {
            if (dataTable1.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                    textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                else
                    textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            }
            else
            {
                textResult.Text = "Паспорт «" + passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
            }
        }

        private DataTable GetDataFromDB(string commandText, SQLiteConnection connection)
        {
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);

            return dataTable;
        }

        private bool InputIsValid(string rawData)
        {
            const int PassportDataLength = 10;

            if (string.IsNullOrWhiteSpace(rawData))
            {
                MessageBox.Show("Введите серию и номер паспорта");
                return false;
            }

            if (rawData.Length < PassportDataLength)
            {
                textResult.Text = "Неверный формат серии или номера паспорта";
                return false;
            }

            return true;
        }
    }
}

namespace CleanCodeHW10
{
    //10. Замена условной логики полиморфизмом
    //Задание Сейчас система не готова к расширению.
    //К сожалению при добавление нового способа оплаты, нам нужно модифицировать все ифы которые совершают те или
    //иные действия с разными системами.Необходимо зафиксировать интерфейс платежёной системы и сокрыть их многообразие
    //под какой-нибудь сущностью. Например фабрикой (или фабричным методом).Важное условие: пользователь вводит именно
    //идентификатор платёжной системы.

    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var systemId = orderForm.ShowForm();
            var paymentHandler = PaymentSystemFactory.Create(systemId);

            paymentHandler?.ShowPaymentResult();
        }
    }

    public class PaymentSystemFactory
    {
        private static Dictionary<string, Func<PaymentSystem>> _paymentSystemsCreators;

        static PaymentSystemFactory()
        {
            _paymentSystemsCreators = new Dictionary<string, Func<PaymentSystem>>() {
                { "QIWI", CreateQIWI },
                { "WebMoney", CreateWebMoney },
                { "Card", CreateCard}
            };
        }

        public static string GetCatalogue()
        {
            return string.Join(", ", _paymentSystemsCreators.Keys);
        }

        public static PaymentSystem Create(string systemId)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentException("systemId is null or empty");

            foreach (var creator in _paymentSystemsCreators)
                if (creator.Key == systemId)
                    return creator.Value.Invoke();

            throw new ArgumentException("unknown systemId");
        }

        private static PaymentSystem CreateCard()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            return new CardPaymentSystem();
        }

        private static PaymentSystem CreateWebMoney()
        {
            Console.WriteLine("Вызов API WebMoney...");
            return new WebMoneyPaymentSystem();
        }

        private static PaymentSystem CreateQIWI()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
            return new QiwiPaymentSystem();
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine($"Мы принимаем: {PaymentSystemFactory.GetCatalogue()}");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public abstract class PaymentSystem
    {
        public readonly string Id;

        protected PaymentSystem(string id) => Id = id;

        public void ShowPaymentResult()
        {
            Console.WriteLine($"Вы оплатили с помощью {Id}");

            if (PaymentVerification())
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Что-то пошло не так...");
        }

        protected abstract bool PaymentVerification();
    }

    public class WebMoneyPaymentSystem : PaymentSystem
    {
        public WebMoneyPaymentSystem() : base("WebMoney") { }

        protected override bool PaymentVerification()
        {
            Console.WriteLine($"Проверка платежа через {Id}...");
            return true;
        }
    }

    public class CardPaymentSystem : PaymentSystem
    {
        public CardPaymentSystem() : base("Card") { }

        protected override bool PaymentVerification()
        {
            Console.WriteLine($"Проверка платежа через {Id}...");
            return true;
        }
    }

    public class QiwiPaymentSystem : PaymentSystem
    {
        public QiwiPaymentSystem() : base("QIWI") { }

        protected override bool PaymentVerification()
        {
            Console.WriteLine($"Проверка платежа через {Id}...");
            return true;
        }
    }
}

namespace CleanCodeHW11
{
    //11. 30. Аргументы-флаги - это плохо Задание. Попробуйте заменить метод на пару Включить\Выключить 
    class Program
    {
        public void Activate()
        {
            _enable = true;
            _effects.StartEnableAnimation();
        }

        public void Deactivate()
        {
            _enable = false;
            _pool.Free(this);
        }
    }
}

namespace CleanCodeHW12
{
    //12. 34. Имя параметра дублируется в имени метода Задание. Отрефакторите
    class Program
    {
        public void Shoot(Player target) { }
        public string GetElement(int index) => string.Empty;
    }

    class Player
    { }
}