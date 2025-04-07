namespace OOPYuGiOhProject
{
    public abstract class Card
    {
        public string Name { get; protected set; }
        public virtual string Type { get; protected set; }
        public string Description { get; protected set; }
        public virtual int Attack { get; protected set; }
        public virtual int Defense { get; protected set; }

        protected Card(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public virtual void Activate()
        {

        }

        protected Card(string name, String type, int attack, int defense, string description)
        
        {
            Name = name;
            Type = type;
            Attack = attack;
            Defense = defense;
            Description = description;
        }
        
    }
}