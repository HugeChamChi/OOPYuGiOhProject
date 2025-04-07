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

    public SpellCard(string name, SpellType spellType, string description)
        : base(name, "마법", description)
    {
        SpellCardType = spellType;
    }
}

// 악몽의 고문실 카드 구현
public class NightmareChamber : SpellCard
{
    public NightmareChamber()
        : base("악몽의 고문실", SpellType.Continuous,
            "상대 라이프에 전투 데미지 이외의 데미지를 줄 때마다, 상대 라이프에 300 포인트 데미지를 준다. '악몽의 고문실'의 효과로는 이 카드의 효과는 적용되지 않는다.")
    {
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

    public override void Activate();
    {
        Console.WriteLine($"{Name}이(가) 발동.");

        // 패 2장이 아닐경우.
        if (player.Hand.Count < 2)
        {
            Console.WriteLine("패가 2장 미만이라 발동할 수 없습니다.");
            return;
        }

        // 패 2장 선택해서 버리기
        for (int i = 0; i < 2; i++)
        {
            //패 2장 선택 버리기.
        }


        // 앞면표시몬스터 예외처리.
        if (faceUpMonsters.Count == 0)
        {
            Console.WriteLine("앞면 표시 몬스터가 없어 효과를 발동할 수 없습니다.");
            return;
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

    public override void Activate()
    {
        Console.WriteLine($"{Name}이(가) 발동.");

        // 장착할 몬스터가 없을경우 예외처리.
        if (monsters.Count == 0)
        {
            Console.WriteLine("장착할 몬스터가 없습니다.");
            return;
        }


        // 공격력 증가
        equippedMonster.Attack += 500;
    }
}

// 공격 봉인 카드 구현
public class SwordsOfRevealingLight : SpellCard
{
    private int remainingTurns = 3;

    public SwordsOfRevealingLight()
        : base("공격 봉인", SpellType.Continuous,
            "이 카드는 발동 후, 필드에 계속해서 남고, 상대 턴에서 세어서 3턴 후의 상대 엔드 페이즈에 파괴된다. 이 카드의 발동 시, 상대 필드에 뒷면 표시 몬스터가 존재할 경우, 그 몬스터를 전부 앞면 표시로 한다. 이 카드가 마법 & 함정 존에 존재하는 한, 상대 몬스터는 공격 선언할 수 없다.")
    {
    }

    public override void Activate()
    {
        Console.WriteLine($"{Name}이(가) 발동.");
    }
}