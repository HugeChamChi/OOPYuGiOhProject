namespace OOPYuGiOhProject
{
    public abstract class Card
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        // 통합 생성자 재구성했다예요
        protected Card(
            string name, 
            string type, 
            int attack = 0, 
            int defense = 0, 
            string description = "")
        {
            Name = name;
            Type = type;
            Description = description;
            Attack = attack;
            Defense = defense;
        }

        public virtual void Activate(GameContext context) {}
    }

}