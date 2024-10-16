namespace CleanCodeHW1
{
    //1. 2.Даже простой алгоритм можно угробить тупым названием метода Задание Переименуйте метод -
    class Program
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
                throw new ArgumentException("min is greater than max");

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
            if (array == null)
                throw new ArgumentNullException("array");

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
        private const int Zero = 0;//Думал еще в сторону EmptyCount и вообще целесообразности этой константы, но все так все.

        private int _bulletsCount;

        public Weapon(int bulletsCount)
        {
            if (bulletsCount < Zero)
                throw new ArgumentOutOfRangeException(nameof(bulletsCount) + " is not valid");

            _bulletsCount = bulletsCount;
        }

        public bool CanShoot => _bulletsCount > BulletPerShoot;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException();

            _bulletsCount -= BulletPerShoot;
        }
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
            int coins = 10;
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
    class PlayerStats { }
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

        public static void SetRandomChance()
        {
            _chance = Random.Range(0, 100);
        }

        public static int CalculateSalary(int hoursWorked)
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
        public Player(string name, int age, Mover mover, Weapon weapon)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name) + " is not valid");

            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age) + " is not valid");

            Name = name;
            Age = age;
            Mover = mover ?? throw new ArgumentNullException(nameof(mover));
            Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public string Name { get; }
        public int Age { get; }
        public Mover Mover { get; }
        public Weapon Weapon { get; }
    }

    public class Mover
    {
        private const int MinDirectionValue = -1;
        private const int MaxDirectionValue = 1;

        private float _directionX;
        private float _directionY;

        public Mover(float directionX, float directionY, float speed)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed) + " is not valid");

            DirectionX = directionX;
            DirectionY = directionY;
            Speed = speed;
        }

        public float Speed { get; }
        public float DirectionX
        {
            get => _directionX;
            private set
            {
                if (value < MinDirectionValue || value > MaxDirectionValue)
                    throw new ArgumentOutOfRangeException(nameof(DirectionX) + " is not valid");

                _directionX = value;
            }
        }
        public float DirectionY
        {
            get => _directionY;
            private set
            {
                if (value < MinDirectionValue || value > MaxDirectionValue)
                    throw new ArgumentOutOfRangeException(nameof(DirectionY) + " is not valid");

                _directionY = value;
            }
        }

        public void Move() { }
    }

    public class Weapon
    {
        public Weapon(int damage, float cooldown)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage) + " is not valid");

            if (cooldown < 0)
                throw new ArgumentOutOfRangeException(nameof(cooldown) + " is not valid");

            Damage = damage;
            Cooldown = cooldown;
        }

        public int Damage { get; }
        public float Cooldown { get; }

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
    using Crestron.SimplSharp.CrestronIO;
    using Crestron.SimplSharp.SQLite;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    class Program
    {
        private static void Main()
        {
            new VoitingView(new PresenterFactory(new SHA256HashGenerator()));
        }
    }

    interface IVoitingView
    {
        void ShowResult(string result);
        void ShowMessage(string text);
    }

    public interface IHashGenerator
    {
        public string GetHash(string input);
    }

    public interface IDatabaseContext
    {
        DataTable TryGetData(string passportHashData);
    }

    class PresenterFactory
    {
        private IHashGenerator _hashGenerator;

        public PresenterFactory(IHashGenerator hashGenerator)
            => _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));

        public VoitingPresenter Create(IVoitingView voitingView)
        {
            if (voitingView == null)
                throw new ArgumentNullException(nameof(voitingView));

            Service model = new Service(_hashGenerator);
            return new VoitingPresenter(voitingView, model);
        }
    }

    class VoitingView : Form, IVoitingView
    {
        private TextBox _passportTextbox;
        private Label _textResult;
        private VoitingPresenter _presenter;

        public VoitingView(PresenterFactory presenterFactory) => _presenter = presenterFactory.Create(this);

        public void CheckButtonClick(object sender, EventArgs e) => _presenter.GetData(_passportTextbox.Text);

        public void ShowResult(string result) => _textResult.Text = result;

        public void ShowMessage(string text) => MessageBox.Show(text);
    }

    class VoitingPresenter
    {
        private readonly IVoitingView _view;
        private readonly Service _model;

        public VoitingPresenter(IVoitingView view, Service model)
        {
            _view = view ?? throw new ArgumentException(nameof(view));
            _model = model ?? throw new ArgumentException(nameof(model));
        }

        public void GetData(string passportData)
        {
            try
            {
                Citizen citizen = _model.GetCitizen(new Pass(passportData));
                _view.ShowResult(GetMessage(citizen, passportData));
            }
            catch (PassportNotFoundException exception)
            {
                _view.ShowMessage("Passport " + exception.PassportData + " not found");
            }
            catch (Exception exception)
            {
                _view.ShowMessage(exception.Message);
            }
        }

        private string GetMessage(Citizen citizen, string passportData)
        {
            if (citizen == null)
                return "По паспорту «" + passportData + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            else if (citizen.HasAccess)
                return "По паспорту «" + passportData + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            else
                return "Паспорт «" + passportData + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
        }
    }

    class Pass
    {
        private const int PassportDataLength = 10;

        public Pass(string passportData)
        {
            passportData = passportData.Trim().Replace(" ", string.Empty);

            if (string.IsNullOrWhiteSpace(passportData))
                throw new ArgumentException("Введите серию и номер паспорта");

            if (passportData.Length < PassportDataLength)
                throw new ArgumentException("Неверный формат серии или номера паспорта");

            Data = passportData;
        }

        public string Data { get; private set; }
    }

    class Service
    {
        private Repository _repository;
        private readonly IHashGenerator _hashGenerator;

        public Service(IHashGenerator hashGenerator)
        {
            _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));
            _repository = new Repository(new DatabaseContext());
        }

        public Citizen GetCitizen(Pass pass)
        {
            return _repository.GetCitizen(_hashGenerator.GetHash(pass.Data))
                    ?? throw new PassportNotFoundException(pass.Data);
        }
    }

    class DatabaseContext : IDatabaseContext
    {
        private string _dbName = "db";

        public DataTable TryGetData(string passportHashData)
        {
            SQLiteConnection connection = CreateConnection(_dbName);
            DataTable dataTable = null;

            try
            {
                connection.Open();
                string commandText = $"select * from passports where num='{passportHashData}' limit 1;";
                dataTable = GetDataFromDB(commandText, connection);
                connection.Close();
            }
            catch (SQLiteException ex)
            {
                const int MissingDbCode = 1;

                if (ex.ErrorCode == MissingDbCode)
                    throw new FileNotFoundException($"Файл {_dbName}.sqlite не найден. Положите файл в папку вместе с exe.");
            }

            return dataTable;
        }

        private static SQLiteConnection CreateConnection(string dbName)
        {
            string connectionString = $"Data Source={GetLocation()}\\{dbName}.sqlite";

            return new SQLiteConnection(connectionString);
        }

        private static string GetLocation() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private DataTable GetDataFromDB(string commandText, SQLiteConnection connection)
        {
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);

            return dataTable;
        }
    }

    class Repository
    {
        private IDatabaseContext _databaseContext;

        public Repository(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

        public Citizen GetCitizen(string passportData)
        {
            DataTable dataTable = _databaseContext.TryGetData(passportData);

            if (dataTable.Rows.Count == 0)
                return null;

            return new Citizen(Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]));
        }
    }

    class Citizen
    {
        public Citizen(bool hasAccess) => HasAccess = hasAccess;

        public bool HasAccess { get; private set; }
    }

    class PassportNotFoundException : Exception
    {
        public PassportNotFoundException(string passportData) => PassportData = passportData;

        public string PassportData { get; private set; }
    }

    public abstract class HashGenerator : IHashGenerator
    {
        public string GetHash(string input) =>
            Convert.ToHexString(HashData(Encoding.UTF8.GetBytes(input)));

        protected abstract byte[] HashData(byte[] bytes);
    }

    public class SHA256HashGenerator : HashGenerator
    {
        protected override byte[] HashData(byte[] bytes) => SHA256.HashData(bytes);
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
            PaymentSystemFactory systemFactory = new PaymentSystemFactory();

            var orderForm = new OrderForm(systemFactory.GetCatalogue());
            orderForm.ShowForm();
            var paymentHandler = new PaymentHandler(systemFactory.GetCreator(orderForm.SystemId));

            paymentHandler?.ShowPaymentResult();
        }
    }

    public class PaymentSystemFactory
    {
        private Dictionary<string, Func<PaymentSystem>> _paymentSystemsCreators;

        public PaymentSystemFactory()
        {
            _paymentSystemsCreators = new Dictionary<string, Func<PaymentSystem>>() {
                { "QIWI", CreateQIWI },
                { "WebMoney", CreateWebMoney },
                { "Card", CreateCard}
            };
        }

        public IEnumerable<string> GetCatalogue()
        {
            return _paymentSystemsCreators.Keys;
        }

        public Func<PaymentSystem> GetCreator(string systemId)
        {
            if (string.IsNullOrWhiteSpace(systemId))
                throw new ArgumentException("systemId is null or empty");

            if (_paymentSystemsCreators.TryGetValue(systemId, out Func<PaymentSystem> creator))
                return creator;

            throw new ArgumentException("unknown systemId");
        }

        private PaymentSystem CreateCard() => new CardPaymentSystem();

        private PaymentSystem CreateWebMoney() => new WebMoneyPaymentSystem();

        private PaymentSystem CreateQIWI() => new QiwiPaymentSystem();
    }

    public class OrderForm
    {
        private IEnumerable<string> _systemsCatalog;

        public OrderForm(IEnumerable<string> systemsCatalog)
        {
            _systemsCatalog = systemsCatalog;
        }

        public string SystemId { get; private set; }

        public void ShowForm()
        {
            do
            {
                Console.WriteLine($"Мы принимаем: {string.Join(", ", _systemsCatalog)}");

                //симуляция веб интерфейса
                Console.WriteLine("Какое системой вы хотите совершить оплату?");
                SystemId = Console.ReadLine();
            }
            while (_systemsCatalog.Any(i => i == SystemId) == false);
        }
    }

    public class PaymentHandler
    {
        private PaymentSystem _paymentSystem;

        public PaymentHandler(Func<PaymentSystem> systemCreator)
        {
            if (systemCreator == null)
                throw new ArgumentNullException("systemCreator");

            _paymentSystem = systemCreator.Invoke();
        }

        public void ShowPaymentResult()
        {
            Console.WriteLine($"Вы оплатили с помощью {_paymentSystem.Id}");

            if (_paymentSystem.PaymentVerification())
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Что-то пошло не так...");
        }
    }

    public abstract class PaymentSystem
    {
        protected PaymentSystem(string id) => Id = id;

        public string Id { get; }

        public virtual bool PaymentVerification()
        {
            Console.WriteLine($"Проверка платежа через {Id}...");
            return true;
        }
    }

    public class WebMoneyPaymentSystem : PaymentSystem
    {
        public WebMoneyPaymentSystem() : base("WebMoney") { }

        public override bool PaymentVerification()
        {
            Console.WriteLine("Вызов API WebMoney...");
            return base.PaymentVerification();
        }
    }

    public class CardPaymentSystem : PaymentSystem
    {
        public CardPaymentSystem() : base("Card") { }

        public override bool PaymentVerification()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            return base.PaymentVerification();
        }
    }

    public class QiwiPaymentSystem : PaymentSystem
    {
        public QiwiPaymentSystem() : base("QIWI") { }

        public override bool PaymentVerification()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
            return base.PaymentVerification();
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