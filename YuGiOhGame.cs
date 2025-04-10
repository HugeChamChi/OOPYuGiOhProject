namespace OOPYuGiOhProject
{
    public class YuGiOhGame
    {
        private Card selectedCard;
        private List<Card> playerHand = new();
        private List<Card> monsterZone = new();
        private List<Card> magicZone = new();
        private int cursorPosition = 0;


        public GameContext Context { get; private set; }

        public YuGiOhGame()
        {
            InitializeCards();
            InitializeContext();
        }

        public void InitializeGame()
        {
            InitializeCards();

            var player = new Player
            {
                LifePoints = 8000,
                Hand = new List<Card>(5)
                    { null, new BlueEyesWhiteDragon(), new SummonedSkull(), new BlackPantherWarrior(), null },
                MonsterZone =
                    new List<Card>(5) { null, new IaitoDragonSamurai(), new IaitoDragonSamurai(), null, null },
                MagicZone = new List<Card>(5)
                {
                    new BlackPendant(), new BlackPendant(), new DarknessApproaches(), new NightmareChamber(),
                    new BlockAttack()
                }
            };

            var opponent = new Player
            {
                LifePoints = 5100,
                Hand = new List<Card>(),
                MonsterZone = new List<Card>(5) { null, null, new BlueEyesWhiteDragon(), null, null },
                MagicZone = new List<Card>(5) { null, null, null, null, null }
            };

            // 컨텍스트 설정
            Context = new GameContext
            {
                CurrentPlayer = player,
                Opponent = opponent
            };


            Context.UI = new UIManager(Context);
        }


        private void InitializeCards()
        {
            playerHand.Add(new IaitoDragonSamurai());
            playerHand.Add(new BlackPantherWarrior());
            playerHand.Add(new BlueEyesWhiteDragon());

// 몬스터존, 마법존 초기화
            for (int i = 0; i < 5; i++)
            {
                monsterZone.Add(null);
                magicZone.Add(null);
            }
        }


        private void InitializeContext()
        {
            Context = new GameContext
            {
                CurrentPlayer = new Player
                {
                    LifePoints = 8000,
                    Hand = new List<Card>(5) { null, null, null, null, null },
                    MonsterZone = new List<Card>(5) { null, null, null, null, null },
                    MagicZone = new List<Card>(5) { null, null, null, null, null }
                },
                Opponent = new Player()
                {
                    LifePoints = 8000,
                    Hand = new List<Card>(),
                    MonsterZone = new List<Card>(5) { null, null, null, null, null },
                    MagicZone = new List<Card>(5) { null, null, null, null, null }
                }
            };

            Context.UI = new UIManager(Context);
        }

        public void UseCard(int handIndex)
        {
            if (handIndex < 0 || handIndex >= playerHand.Count) return;

            var card = playerHand[handIndex];

            if (card is SpellCard spell)
            {
                spell.Activate(Context);
                MoveToMagicZone(spell);
                playerHand.RemoveAt(handIndex);
            }
        }

        private void MoveToMagicZone(Card card)
        {
            int emptyIndex = magicZone.FindIndex(c => c == null);
            if (emptyIndex != -1)
            {
                magicZone[emptyIndex] = card;
            }
        }
    }

    public class GameContext
    {
        public Player CurrentPlayer { get; set; }
        public Player Opponent { get; set; }
        public UIManager UI { get; set; }


        // 델리게이트 선언
        public delegate void DamageEventHandler(Card source, int amount);

        // 비전투 데미지 이벤트
        public event DamageEventHandler NonBattleDamageOccured;

        public void RaiseNonBattleDamage(Card source, int damage)
        {
            NonBattleDamageOccured?.Invoke(source, damage);
        }
    }

    public class Player
    {
        public int LifePoints { get; set; }

        public void SetLifePoint(int value)
        {
            LifePoints = value;
        }

        public List<Card> Hand { get; set; } = new List<Card>(Enumerable.Repeat<Card>(null, 5));
        public List<Card> MonsterZone { get; set; } = new List<Card>(Enumerable.Repeat<Card>(null, 5));
        public List<Card> MagicZone { get; set; } = new List<Card>(Enumerable.Repeat<Card>(null, 5));
    }
    
}