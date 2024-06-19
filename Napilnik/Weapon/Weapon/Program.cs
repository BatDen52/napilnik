namespace HW1.Weapon
{
    class Weapon
    {
        public int Damage { get; private set; }
        public int Bullets { get; private set; }
        public int BulletsPerShot { get; private set; }

        public void Fire(Player player)
        {
            Bullets -= BulletsPerShot;
            player.TakeDamage(Damage);
        }
    }

    class Player
    {
        public Health Health { get; private set; }

        public void TakeDamage(int damage) => Health.ApplyDamage(damage);
    }

    class Health
    {
        public int MinValue { get; private set; } = 0;
        public int MaxHealth { get; private set; } = 100;
        public int Value { get; private set; }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException();

            if (damage <= Value)
                Value -= damage;
            else
                Value = MinValue;
        }
    }

    class Bot
    {
        public Weapon Weapon { get; private set; }

        public void OnSeePlayer(Player player) => Weapon.Fire(player);
    } 
}