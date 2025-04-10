namespace OOPYuGiOhProject
{
    public class UIManager
    {
        private GameContext Context;
        private Card selectedCard;
        
        // 존 인덱스 정의
        private const int PlayerHandZone = 0;
        private const int PlayerMonsterZone = 1;
        private const int PlayerMagicZone = 2;
        private const int OpponentMonsterZone = 3;
        private const int OpponentMagicZone = 4;
        
        private readonly int[] zoneOrder = new int[] {
            OpponentMagicZone,    // 4: 상대 마법 존
            OpponentMonsterZone,  // 3: 상대 몬스터 존
            PlayerMonsterZone,    // 1: 플레이어 몬스터 존
            PlayerMagicZone,      // 2: 플레이어 마법 존
            PlayerHandZone        // 0: 플레이어 핸드
        };
        
        private IEnumerable<string> SplitText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) yield break;

            for (int i = 0; i < text.Length; i += maxLength)
            {
                yield return text.Substring(i, Math.Min(maxLength, text.Length - i));
            }
        }
       
        public UIManager(GameContext context)
        {
            this.Context = context;
        }

        

        // zone: 0=핸드, 1=몬스터존, 2=마법존 / position: 해당 존 내 위치)
        private (int zone, int position) cursor = (0, 0); 
    
        public void UpdateCursor(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    cursor = (cursor.zone, Math.Max(0, cursor.position - 1));
                    break;
                
                case ConsoleKey.RightArrow:
                    int maxPosition = GetCurrentZoneMax();
                    cursor = (cursor.zone, Math.Min(maxPosition, cursor.position + 1));
                    break;
                
               
                case ConsoleKey.UpArrow:
                    int currentUpIndex = Array.IndexOf(zoneOrder, cursor.zone);
                    if (currentUpIndex < zoneOrder.Length - 1)
                    {
                        cursor.zone = zoneOrder[currentUpIndex + 1];
                    }
                    break;
                
                case ConsoleKey.DownArrow:
                    int currentDownIndex = Array.IndexOf(zoneOrder, cursor.zone);
                    if (currentDownIndex > 0)
                    {
                        cursor.zone = zoneOrder[currentDownIndex - 1];
                    }
                    break;
                case ConsoleKey.Enter:
                    SelectCard();
                    break;
            
                case ConsoleKey.S:
                    SummonCard();
                    break;
            }
        }
        private void SelectCard()
        {
            selectedCard = GetSelectedCard();
        }
        private void SummonCard()
        {
            if (selectedCard == null || cursor.zone != 0) return;

            // 몬스터 카드 소환
            if (selectedCard is MonsterCard)
            {
                for (int i = 0; i < Context.CurrentPlayer.MonsterZone.Count; i++)
                {
                    if (Context.CurrentPlayer.MonsterZone[i] == null)
                    {
                        Context.CurrentPlayer.MonsterZone[i] = selectedCard;
                        Context.CurrentPlayer.Hand.Remove(selectedCard);
                        selectedCard = null;
                        return;
                    }
                }
            }
            // 마법 카드 발동
            else if (selectedCard is SpellCard)
            {
                for (int i = 0; i < Context.CurrentPlayer.MagicZone.Count; i++)
                {
                    if (Context.CurrentPlayer.MagicZone[i] == null)
                    {
                        Context.CurrentPlayer.MagicZone[i] = selectedCard;
                        Context.CurrentPlayer.Hand.Remove(selectedCard);
                        selectedCard = null;
                        return;
                    }
                }
            }
        }
        
        private int GetCurrentZoneMax()
        {
            return cursor.zone switch
            {
                0 => Context.CurrentPlayer.Hand.Count - 1,
                1 => Context.CurrentPlayer.MonsterZone.Count - 1,
                2 => Context.CurrentPlayer.MagicZone.Count - 1,
                _ => 0
            };
        }

        public void DrawUI()
        {
            Console.Clear();
            DrawField();
            DrawCardDescription();
            DrawInstructions();
        }

        private void DrawField()
        {
            const int zoneSize = 5;
            
            Console.WriteLine(
                "┌─────────────────────────────────────────────────────────────────────┬───────────────────────────────────────────────────────────────┐");
            Console.WriteLine(
                "│                                                                     │ Card Description                                              │");
            // 상대방 마법 존 출력
            Console.Write("│    ");
            for (int i = 0; i < zoneSize; i++)
            {
                bool isSelected = cursor.zone == OpponentMagicZone && cursor.position == i;
                string display = Context.Opponent.MagicZone[i] switch
                {
                    SpellCard card => "[Spell]", 
                    null => "[Empty]",             
                    _ => "[Unknown]"               
                };
                Console.Write(isSelected ? $"▶{display,-11}◀" : $"{display,-13}");
            }

            Console.WriteLine("│                                                               │");

            //상대방 중앙 존 출력
            Console.WriteLine(
                "│                                                                     │                                                               │");
            Console.WriteLine(
                "│                                                                     │                                                               │");


            // 상대방 몬스터 존 출력
            Console.Write("│    ");
            for (int i = 0; i < zoneSize; i++)
            {
                bool isSelected = cursor.zone == OpponentMonsterZone && cursor.position == i;
                
                
                string display = Context.Opponent.MonsterZone[i] switch
                {
                    MonsterCard card => "[Monster]", 
                    null => "[Empty]",             
                    _ => "[Unknown]"               
                };

                Console.Write(isSelected ? $"▶{display,-11}◀" : $"{display,-13}");
            }

            Console.WriteLine("│                                                               │");


            Console.WriteLine(
                "│                                                                     │                                                               │");
            Console.WriteLine(
                "└─────────────────────────────────────────────────────────────────────┴───────────────────────────────────────────────────────────────┘");
            Console.WriteLine(
                "      [D  P]       ▶       [S  P]       ▶        [M  P  1]         ▶        [B  P]       ▶       [M  P  2]        ▶        [E  P]     ");
            Console.WriteLine(
                "┌─────────────────────────────────────────────────────────────────────┬───────────────────────────────────────────────────────────────┐");
            Console.WriteLine(
                "│                                                                     │                                                               │");

            // 플레이어 몬스터 존 출력
            Console.Write("│    ");
            for (int i = 0; i < zoneSize; i++)
            {
                bool isSelected = cursor.zone == PlayerMonsterZone && cursor.position == i;
                string display = Context.CurrentPlayer.MonsterZone[i] switch
                {
                    MonsterCard card => "[Monster]", 
                    null => "[Empty]",             
                    _ => "[Unknown]"               
                };
                Console.Write(isSelected ? $"▶{display,-11}◀" : $"{display,-13}");
            }

            Console.WriteLine("│                                                               │");
            //플레이어 중앙 존 출력
            Console.WriteLine(
                "│                                                                     │                                                               │");
            Console.WriteLine(
                "│                                                                     │                                                               │");

            // 플레이어 마법 존 출력
            Console.Write("│    ");
            for (int i = 0; i < zoneSize; i++)
            {
                bool isSelected = cursor.zone == PlayerMagicZone && cursor.position == i;
                string display = Context.CurrentPlayer.MagicZone[i] switch
                {
                    SpellCard card => "[Spell]", 
                    null => "[Empty]",             
                    _ => "[Unknown]"               
                };
                Console.Write(isSelected ? $"▶{display,-11}◀" : $"{display,-13}");
            }

            Console.WriteLine("│                                                               │");
            Console.WriteLine(
                "│                                                                     │                                                               │");

            Console.WriteLine(
                "└─────────────────────────────────────────────────────────────────────┴───────────────────────────────────────────────────────────────┘");
            
            // 플레이어 핸드 출력

            Console.WriteLine("                 [ P  L  A  Y  E  R  H  A  N  D ]                                                                        ");
            Console.WriteLine(
                "┌─────────────────────────────────────────────────────────────────────┬───────────────────────────────────────────────────────────────┐");
            Console.WriteLine(
                "│                                                                     │                                                               │");
            Console.Write("│    ");
            for (int i = 0; i < Context.CurrentPlayer.Hand.Count; i++)
            {
                bool isSelected = cursor.zone == PlayerHandZone && cursor.position == i;
                string display = Context.CurrentPlayer.Hand[i] != null 
                
                    ?  "[Card]"
                    
                    : "[Empty]";
                
                Console.Write(isSelected ? $"▶{display,-11}◀" : $"{display,-13}");
            }
            
            Console.WriteLine("│                                                               │");
            Console.WriteLine(
                "│                                                                     │                                                               │");
            Console.WriteLine(
                "└─────────────────────────────────────────────────────────────────────┴───────────────────────────────────────────────────────────────┘");
        }


        private void DrawCardDescription()
        {
            Card card = GetSelectedCard();
            int descriptionLeft = 72;
            

            if (card != null)
            {
                // 카드 이름 출력
                Console.SetCursorPosition(descriptionLeft, 4);
                Console.Write($"카드 이름: {card.Name}");

                // 카드 타입 출력
                Console.SetCursorPosition(descriptionLeft, 5);
                Console.Write($"카드 타입: {card.Type}");

                // 공격력 및 방어력 출력 (몬스터 카드인 경우)
                if (card is MonsterCard monster)
                {
                    Console.SetCursorPosition(descriptionLeft, 6);
                    Console.Write($"공격력: {monster.Attack}");
                    Console.SetCursorPosition(descriptionLeft, 7);
                    Console.Write($"방어력: {monster.Defense}");
                }
                
                
                Console.SetCursorPosition(descriptionLeft, 20);
                Console.Write("효과: ");
                string[] wrappedDescription = SplitText(card.Description, 30).ToArray();


                if (wrappedDescription.Length > 0)
                {
                    Console.Write(wrappedDescription[0]); 
                }


                for (int i = 1; i < wrappedDescription.Length; i++)
                {
                    Console.SetCursorPosition(descriptionLeft + 4, 20 + i); 
                    Console.Write(wrappedDescription[i]);
                }
            }
            else
            {
                // 빈 슬롯 선택 시 메시지 출력
                Console.SetCursorPosition(descriptionLeft, 3);
                Console.WriteLine("카드 정보 없음");
            }
        }


        private void DrawInstructions()
        {
            // 하단 안내 메시지 표시
            int instructionTop = Math.Max(Console.WindowHeight - 4, 0);
            Console.SetCursorPosition(0, instructionTop);
            Console.WriteLine("이동: ←→, 선택: Enter, 소환: S, 종료: ESC");
        }

        private Card GetSelectedCard()
        {
            List<Card> targetZone = cursor.zone switch {
                PlayerHandZone => Context.CurrentPlayer.Hand,
                PlayerMonsterZone => Context.CurrentPlayer.MonsterZone,
                PlayerMagicZone => Context.CurrentPlayer.MagicZone,
                OpponentMonsterZone => Context.Opponent.MonsterZone,
                OpponentMagicZone => Context.Opponent.MagicZone,
                _ => new List<Card>()
            };
    
            return (cursor.position >= 0 && cursor.position < targetZone.Count) 
                ? targetZone[cursor.position] ?? new EmptyCard()
                : new EmptyCard();
        }

// 빈 카드를 나타내는 클래스
        

    }
}