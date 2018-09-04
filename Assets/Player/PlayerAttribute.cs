namespace Assets.Player
{
    public class PlayerAttribute
    {
        public PlayerAttribute(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Mage:
                    Strength = 8;
                    Dexterity = 10;
                    Intelligence = 13;
                    SkillSpeed = 0.5f;
                    _d = 0.2f;
                    _s = 10.0f;
                    _i = 13.0f;
                    Update(true);
                    break;
                default:
                    break;
            }
        }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }

        //calculated
        private readonly float _s, _d, _i;
        public float TotalLife { get; set; }
        public float TotalMana { get; set; }
        public float MovementSpeed { get; set; }
        public float SkillSpeed { get; set; }
        //
        public float CurrentLife { get; set; }
        public float CurrentMana { get; set; }

        public void Add(int strength, int dexterity, int intelligence)
        {
            Strength += strength;
            Dexterity += dexterity;
            Intelligence += intelligence;
            Update(false);
        }

        public void Update(bool updateLevel)
        {
            MovementSpeed = Dexterity * _d;
            SkillSpeed = SkillSpeed > 0.01f ? SkillSpeed - _d : 0.01f; 
            TotalLife = Strength * _s;
            TotalMana = Intelligence * _i;
            if (!updateLevel) return;
            CurrentLife = TotalLife;
            CurrentMana = TotalMana;
        }

        public bool UseManaLife(float mana, float life)
        {
            if (CurrentMana < mana || CurrentLife < life)
                return false;
            CurrentMana -= mana;
            CurrentLife -= life;
            return true;
        }
    }
}