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

    /*
     Не нужно всю логику запихивать в класс модели. В реализациях паттернов семейства MVx под "моделью" может пониматься целая система классов. Не забываем про SRP. Один класс отвечает за получение DataTable из БД (можно назвать DatabaseContext), второй за обработку этих данных (работу с DataTable, можно назвать Repository). Он, если строк нет, то возвращает null. Третий, обработку результата (Service) если результат null, бросает какое то исключение по типу "PassportNotFoundException" и передает в конструктор исключения данные паспорта чтобы в блоке catch к ним обратиться. 

Сервис как раз будет лежать в презентере и к нему будут обращаться за результатом. Результатом выполнения метода может быть либо объект, который содержит Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]) (Объект можно назвать Citizen), либо PassportNotFoundException. 

Слой View не должен проводить никаких операций над данными, которые передаются в презентер.
Для логики string passportData = _passportTextbox.Text.Trim().Replace(" ", string.Empty); и проверок if (string.IsNullOrWhiteSpace(rawData)) и if (rawData.Length < PassportDataLength) лучше создать класс Passport. Он эти проверки будет делать в конструкторе и, если данные не подходят, выбрасывать исключения. Таким образом, если есть экземпляр класса Passport, у него всегда будут корректные данные. Создаваться паспорт будет в презентере в методе GetData

Метод SetPresenter лучше заменить на фабрику в конструктор. Да, в юнити возможен только такой подход, но в других местах это плохая практика. 
class PresenterFactory
{
    private HashGenerator _hashGenerator;
    public PresenterFactory(HashGenerator hashGenerator)
    {
        _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));
    }
    public VoitingPresenter Create(IVoitingView voitingView)
    {
        VoitingModel model = new VoitingModel(_hashGenerator);
        return new VoitingPresenter(voitingView, model);
    }
}

Вызывать ее нужно будет так:
 public VoitingView(PresenterFactory presenterFactory) =>
     _presenter = presenterFactory.Create(this);


Строки с результатом для отображения в модели присутствовать не должны. Они определяются в презентере. (Например, "По паспорту «" + passportData + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";)

В этой реализации появляется несколько классов и некоторые из них существуют только для того чтобы передать данные в какой то обертке (класс Citizen содержит только bool) или для проверки и содержания корректных данных (класс Passport)

Общий код метода презентера в блоке try:
Создание паспорта
Обращение к сервису и получение Citizen
По свойству ситизена определение сообщения, предоставлялся ли доступ
Отображение сообщения

Дополнительно для репозитория было бы хорошо сделать интерфейс чтобы полностью отвязаться от БД на основе SQL 
Хэширование может происходить на уровне репозитория или сервиса (в зависимости от того, получает ли ситизен в конструткор паспорт)
Презентер ничего не должен знать о БД (классе DataTable). Ему все равно откуда появляется Citizen, из БД, обращения к удаленному серверу или рандомом.
     */

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
            new VoitingPresenter(new VoitingView());
        }
    }

    interface IVoitingView
    {
        void ShowResult(string result);
        void SetPresenter(VoitingPresenter voitingPresenter);
        void ShowMessage(string text);
    }

    class VoitingView : Form, IVoitingView
    {
        private TextBox _passportTextbox;
        private Label _textResult;
        private VoitingPresenter _presenter;

        public void SetPresenter(VoitingPresenter presenter) => _presenter = presenter;

        public void CheckButtonClick(object sender, EventArgs e)
        {
            string passportData = _passportTextbox.Text.Trim().Replace(" ", string.Empty);

            if (InputIsValid(passportData) == false)
                return;

            _presenter.GetData(passportData);
        }

        public void ShowResult(string result) => _textResult.Text = result;

        public void ShowMessage(string text) => MessageBox.Show(text);

        private bool InputIsValid(string rawData)
        {
            const int PassportDataLength = 10;

            if (string.IsNullOrWhiteSpace(rawData))
            {
                ShowMessage("Введите серию и номер паспорта");
                return false;
            }

            if (rawData.Length < PassportDataLength)
            {
                ShowResult("Неверный формат серии или номера паспорта");
                return false;
            }

            return true;
        }
    }

    class VoitingPresenter
    {
        private readonly IVoitingView _view;
        private readonly VoitingModel _model;

        public VoitingPresenter(IVoitingView view)
        {
            _view = view ?? throw new ArgumentException(nameof(_view));
            _model = new VoitingModel(new SHA256HashGenerator());

            _view.SetPresenter(this);
        }

        public void GetData(string passportData)
        {
            try
            {
                _view.ShowResult(_model.TryGetData(passportData, out DataTable dataTable));
            }
            catch (FileNotFoundException exception)
            {
                _view.ShowMessage(exception.Message);
            }
        }
    }

    class VoitingModel
    {
        private readonly IHashGenerator _hashGenerator;
        private string _dbName = "db";

        public VoitingModel(IHashGenerator hashGenerator) =>
            _hashGenerator = hashGenerator ?? throw new ArgumentNullException(nameof(hashGenerator));

        public string TryGetData(string passportData, out DataTable dataTable)
        {
            SQLiteConnection connection = CreateConnection(_dbName);
            dataTable = null;

            try
            {
                connection.Open();
                string commandText = $"select * from passports where num='{_hashGenerator.GetHash(passportData)}' limit 1;";
                dataTable = GetDataFromDB(commandText, connection);
                connection.Close();
            }
            catch (SQLiteException ex)
            {
                const int MissingDbCode = 1;

                if (ex.ErrorCode == MissingDbCode)
                    throw new FileNotFoundException($"Файл {_dbName}.sqlite не найден. Положите файл в папку вместе с exe.");
            }

            return GetResult(dataTable, passportData);
        }

        private string GetResult(DataTable dataTable, string passportData)
        {
            if (dataTable.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]))
                    return "По паспорту «" + passportData + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                else
                    return "По паспорту «" + passportData + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            }
            else
            {
                return "Паспорт «" + passportData + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
            }
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