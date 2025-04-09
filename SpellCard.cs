namespace OOPYuGiOhProject;

public abstract class SpellCard : Card
{
    public enum SpellType
    {
        Normal,
        Continuous,
        Equip,
    }

    public SpellType SpellCardType { get; protected set; }

    protected SpellCard(string name, SpellType spellType, string description)
        : base
        (
            name: name, 
            type: "마법", 
            description: description
        )
    {
        SpellCardType = spellType;
    }
}

// 악몽의 고문실 카드 구현
public class NightmareChamber : SpellCard
{
    private GameContext activeContext;

    public NightmareChamber()
        : base("악몽의 고문실", SpellType.Continuous,
            "상대 라이프에 전투 데미지 이외의 데미지를 줄 때마다, 상대 라이프에 300 포인트 데미지를 준다.")
    {
    }

    public override void Activate(GameContext context)
    {
        activeContext = context;
        context.NonBattleDamageOccured += HandleNonBattleDamage;
        Console.WriteLine($"{Name} 발동!");
    }

    private void HandleNonBattleDamage(Card damageSource, int originalDamage)
    {
        // 자신이 발생시킨 데미지는 무시
        if (damageSource == this) return;

        // 추가 데미지 적용
        activeContext.Opponent.LifePoints -= 300;
        Console.WriteLine($"[{Name}] {damageSource.Name}의 데미지에 300 추가");
    }
}


// 어둠의 엄습 카드 구현
public class DarknessApproaches : SpellCard
{
    public DarknessApproaches()
        : base("어둠의 엄습", SpellType.Normal,
            "패를 2장 버린다. 앞면 표시의 몬스터 1장을 선택하여, 뒷면 수비 표시로 한다.")
    {
    }

    public override void Activate(GameContext context)
    {
        Console.WriteLine($"{Name}이(가) 발동.");

        if (context.CurrentPlayer.Hand.Count < 2) 
        {
            Console.WriteLine("패가 부족합니다.");
            return;
        }

        context.CurrentPlayer.Hand = context.CurrentPlayer.Hand
            .Skip(2)
            .ToList();

        // 상대 몬스터 뒤집기
        MonsterCard targetMonster = null;
        foreach (Card card in context.Opponent.MonsterZone)
        {
            if (card is MonsterCard monster && 
                monster.Position == MonsterCard.BattlePosition.FaceUpAttack)
            {
                targetMonster = monster;
                break;
            }
        }

        if (targetMonster != null)
        {
            targetMonster.SetPosition(MonsterCard.BattlePosition.FaceDownDefense);
            Console.WriteLine($"{targetMonster.Name}을 뒷면 표시로 변경");
        }
        else
        {
            Console.WriteLine("대상 몬스터가 없습니다!");
        }
        
    }
}
// 검은 펜던트 카드 구현
public class BlackPendant : SpellCard
{
    private MonsterCard equippedMonster;

    public BlackPendant()
        : base("검은 펜던트", SpellType.Equip,
            "몬스터에 장착하여 장착 몬스터의 공격력을 500 포인트 올린다. 이 카드가 필드에서 묘지로 보내졌을 때, 상대 플레이어에게 500 포인트 데미지를 준다.")
    {
    }


    public override void Activate(GameContext context)
    {
        Console.WriteLine($"{Name}이(가) 발동.");

        // 장착할 몬스터가 없을경우 예외처리.
        MonsterCard target = null;
        foreach (Card card in context.CurrentPlayer.MonsterZone)
        {
            if (card is MonsterCard monster && 
                monster.Position == MonsterCard.BattlePosition.FaceUpAttack)
            {
                target = monster;
                break;
            }
        }

        if (target == null)
        {
            Console.WriteLine("장착할 몬스터가 없습니다!");
            return;
        }


        // 공격력 증가
        equippedMonster = target;
        equippedMonster.ModifyAttack(500);
        Console.WriteLine($"{equippedMonster.Name} 공격력 500 증가");
    }
}

// 공격 봉인 카드 구현
public class BlockAttack : SpellCard
{
    public BlockAttack()
        : base("공격 봉인", SpellType.Normal,
            "상대 필드의 공격 표시 몬스터 1장을 대상으로 하고 발동할 수 있다. 그 상대의 공격 표시 몬스터를 앞면 수비 표시로 한다.")
    {
    }

    public override void Activate(GameContext context)
    {
        Console.WriteLine($"{Name}이(가) 발동.");

        // 상대 필드에서 공격 표시 몬스터 찾기
        MonsterCard targetMonster = null;
        foreach (Card card in context.Opponent.MonsterZone)
        {
            if (card is MonsterCard monster && 
                monster.Position == MonsterCard.BattlePosition.FaceUpAttack)
            {
                targetMonster = monster;
                break;
            }
        }

        // 대상 몬스터가 없으면 효과 발동 불가
        if (targetMonster == null)
        {
            Console.WriteLine("대상으로 지정할 공격 표시 몬스터가 없습니다.");
            return;
        }

        // 몬스터를 앞면 수비 표시로 변경
        targetMonster.SetPosition(MonsterCard.BattlePosition.FaceUpDefense);
        Console.WriteLine($"{targetMonster.Name}이(가) 앞면 수비 표시로 전환되었습니다.");
    }
}