namespace OOPYuGiOhProject;

public abstract class MonsterCard : Card
    {
        public enum BattlePosition {FaceUpAttack, FaceUpDefense, FaceDownDefense}
        public BattlePosition Position { get; protected set; }
        
        
        public override string Type { get; protected set; } = "몬스터";

        protected MonsterCard(string name, int attack, int defense, string description)
            : base(name, "몬스터", attack, defense, description)
        {
            Position = BattlePosition.FaceUpAttack;
        }

        public void SetPosition(BattlePosition position)
        {
            Position = position;
        }
        public virtual void Summon()
        {
            Console.WriteLine($"{Name}을(를) 소환했습니다!");
        }

        public virtual void AttackTarget(MonsterCard target)
        {
            Console.WriteLine($"{Name}이(가) {target.Name}을(를) 공격합니다!");
            if (this.Attack > target.Defense)
            {
                Console.WriteLine($"{target.Name}이(가) 파괴되었습니다!");
            }
            else
            {
                Console.WriteLine($"{Name}의 공격이 실패했습니다!");
            }
        }
    }


    public class IaitoDragonSamurai : MonsterCard
    {
        public IaitoDragonSamurai()
            : base("일도양단 사무라이", 2000, 1700,
                "이 카드가 뒷면 수비표시 몬스터를 공격했을 경우, 데미지 계산을 실행하지 않고 뒷면 수비표시인 채로 그 몬스터를 파괴한다.")
        {
        }

        public override void Activate()
        {
            Console.WriteLine($"{Name}의 효과가 발동됩니다!");
            Console.WriteLine("뒷면 수비표시 몬스터를 파괴합니다.");
        }
    }


    public class BlackPantherWarrior : MonsterCard
    {
        public BlackPantherWarrior()
            : base("칠흑의 표범 전사 팬서 워리어", 2000, 1600,
                "이 카드가 몬스터 존에 존재하는 한, 이카드의 공격 선언 시기에, 자신은 이 카드 이외의 자신 필드의 몬스터 1장을 릴리스해야한다.")
        {
        }

        public override void Activate()
        {
            Console.WriteLine($"{Name}의 효과가 발동됩니다!");
            Console.WriteLine("공격을 하기위하여 몬스터 1장을 릴리스합니다.");
        }
        
    }


    public class BlueEyesWhiteDragon : MonsterCard
    {
        public BlueEyesWhiteDragon()
            : base("푸른 눈의 백룡", 3000, 2500,
                "파괴적인 공격력을 지닌 전설의 드래곤")
        { }
    }