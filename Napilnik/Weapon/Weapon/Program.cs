namespace HW1.Weapon
{
    class Weapon
    {
        public Weapon(int damage = 1, int bullets = 1, int bulletsPerShot = 1)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage) + " is not valid");

            if (bullets < 0)
                throw new ArgumentOutOfRangeException(nameof(bullets) + " is not valid");

            if (bulletsPerShot < 0)
                throw new ArgumentOutOfRangeException(nameof(bulletsPerShot) + " is not valid");

            Damage = damage;
            Bullets = bullets;
            BulletsPerShot = bulletsPerShot;
        }

        public int Damage { get; private set; }
        public int Bullets { get; private set; }
        public int BulletsPerShot { get; private set; }

        public void Fire(Player player)
        {
            if (Bullets <= BulletsPerShot)
                throw new InvalidOperationException();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Bullets -= BulletsPerShot;
            player.TakeDamage(Damage);
        }
    }

    class Player
    {
        private readonly Health _health;

        public Player(Health health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public Player() : this(new Health()) { }

        public Health Health => _health;

        public void TakeDamage(int damage) => Health.ApplyDamage(damage);
    }

    class Health
    {
        public Health(int value, int minValue = 0, int maxValue = 100)
        {
            if (minValue < 0)
                throw new ArgumentOutOfRangeException(nameof(minValue) + " is not valid");

            if (maxValue < minValue)
                throw new ArgumentOutOfRangeException(nameof(maxValue) + " is not valid");

            if (value < minValue || value > maxValue)
                throw new ArgumentOutOfRangeException(nameof(value) + " is not valid");

            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }

        public Health(int minValue = 0, int maxValue = 100) : this(maxValue, minValue, maxValue) { }

        public int MinValue { get; private set; } = 0;
        public int MaxValue { get; private set; } = 100;
        public int Value { get; private set; }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage) + " is not valid");

            Value = Math.Max(Value - damage, MinValue);
        }
    }

    class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public Bot() : this(new Weapon()) { }

        public Weapon Weapon => _weapon;

        public void OnSeePlayer(Player player) => Weapon.Fire(player);
    }
}