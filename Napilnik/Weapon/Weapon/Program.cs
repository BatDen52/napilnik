namespace HW1.Weapon
{
    class Weapon
    {
        public Weapon(int damage = 1, int bullets = 1, int bulletsPerShot = 1)
        {
            if (damage < 0)
                throw new ArgumentException(nameof(damage) + " is not valid");

            if (bullets < 0)
                throw new ArgumentException(nameof(bullets) + " is not valid");

            if (bulletsPerShot < 0)
                throw new ArgumentException(nameof(bulletsPerShot) + " is not valid");

            Damage = damage;
            Bullets = bullets;
            BulletsPerShot = bulletsPerShot;
        }

        public int Damage { get; private set; }
        public int Bullets { get; private set; }
        public int BulletsPerShot { get; private set; }

        public void Fire(Player player)
        {
            if (Bullets <= 0)
                throw new InvalidOperationException();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Bullets -= BulletsPerShot;
            player.TakeDamage(Damage);
        }
    }

    class Player
    {
        public Player()
        {
            Health = new Health();
        }

        public Player(Health health)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            Health = health;
        }

        public Health Health { get; private set; }

        public void TakeDamage(int damage) => Health.ApplyDamage(damage);
    }

    class Health
    {
        public Health(int minValue = 0, int maxValue = 100)
        {
            Initialize(maxValue, minValue, maxValue);
        }

        public Health(int value, int minValue = 0, int maxValue = 100)
        {
            Initialize(value, minValue, maxValue);
        }

        public int MinValue { get; private set; } = 0;
        public int MaxValue { get; private set; } = 100;
        public int Value { get; private set; }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException(nameof(damage) + " is not valid");

            if (damage <= Value)
                Value -= damage;
            else
                Value = MinValue;
        }

        private void Initialize(int value, int minValue, int maxValue)
        {
            if (minValue < 0)
                throw new ArgumentException(nameof(minValue) + " is not valid");
            
            if (maxValue < minValue)
                throw new ArgumentException(nameof(maxValue) + " is not valid");

            if (value < minValue || value > maxValue)
                throw new ArgumentException(nameof(value) + " is not valid");

            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }
    }

    class Bot
    {
        public Bot()
        {
            Weapon = new Weapon();
        }

        public Bot(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            Weapon = weapon;
        }

        public Weapon Weapon { get; private set; }

        public void OnSeePlayer(Player player) => Weapon.Fire(player);
    }
}