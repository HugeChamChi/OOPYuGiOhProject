namespace OOPYuGiOhProject
{
    public class YuGiOhGame
    {
        private Card selectedCard = null;
        private List<Card> playerHand = new List<Card>();
        private List<Card> monsterZone = new List<Card>();
        private List<Card> magicZone = new List<Card>();
        private int cursorPosition = 0;

        public YuGiOhGame()
        {
            InitializeCards();
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

        
        
    }
}