namespace OOPYuGiOhProject;

public abstract class MonsterCard : Card
    {
        
        public enum BattlePosition {FaceUpAttack, FaceUpDefense, FaceDownDefense}
        public BattlePosition Position { get; protected set; }
        public int Level { get; private set; }
        
        public void ModifyAttack(int amount)
        {
            Attack += amount;
        }
        
        public int GetRequiredTributes()
        {
            return Level switch {
                >= 7 => 2,
                >= 5 => 1,
                _ => 0
            };
        }
        
      

        protected MonsterCard(
            string name, 
            int level,  // ✅ 레벨 추가
            int attack, 
            int defense, 
            string description)
            : base(name, "몬스터", attack, defense, description)
        {
            Level = level;
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
    }


    public class IaitoDragonSamurai : MonsterCard
    {
        public IaitoDragonSamurai()
            : base("일도양단 사무라이", 4,2000, 1700,
                "이 카드가 뒷면 수비표시 몬스터를 공격했을 경우, 데미지 계산을 실행하지 않고 뒷면 수비표시인 채로 그 몬스터를 파괴한다.")
        {
        }
    }


    public class BlackPantherWarrior : MonsterCard
    {
        public BlackPantherWarrior()
            : base("칠흑의 표범 전사 팬서 워리어", 4,2000, 1600,
                "이 카드가 몬스터 존에 존재하는 한, 이카드의 공격 선언 시기에, 자신은 이 카드 이외의 자신 필드의 몬스터 1장을 릴리스해야한다.")
        {
        }
    }


    public class BlueEyesWhiteDragon : MonsterCard
    {
        public BlueEyesWhiteDragon()
            : base("푸른 눈의 백룡",8, 3000, 2500,
                "파괴적인 공격력을 지닌 전설의 드래곤")
        { }
    }

    public class SummonedSkull : MonsterCard
    {
        public SummonedSkull()
            : base("데몬 소환", 5,2500, 1200,
                "어둠의 힘을 사용하여, 사람의 마음을 유혹하는 데몬. 악마족 중에서도 상당히 강력한 힘을 자랑한다.")
        { }
    }