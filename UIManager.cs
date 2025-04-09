namespace OOPYuGiOhProject
{
    public class UIManager
    {
        private GameContext Context;
        private Card selectedCard;
       
        public UIManager(GameContext context)
        {
            this.Context = context;
        }

        

        // 2D 커서 시스템 (zone: 0=핸드, 1=몬스터존, 2=마법존 / position: 해당 존 내 위치)
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
                    cursor = (Math.Max(0, cursor.zone - 1), cursor.position);
                    break;
                
                case ConsoleKey.DownArrow:
                    cursor = (Math.Min(2, cursor.zone + 1), cursor.position);
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
                bool isSelected = cursor.zone == 2 && cursor.position == i;
                string display = i < Context.Opponent.MagicZone.Count 
                    ? (Context.Opponent.MagicZone[i]?.Name ?? "[Empty]") 
                    : "[Empty]";
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
                bool isSelected = cursor.zone == 1 && cursor.position == i;
                string display = i < Context.Opponent.MonsterZone.Count 
                    ? (Context.Opponent.MonsterZone[i]?.Name ?? "[Empty]") 
                    : "[Empty]";
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
                bool isSelected = cursor.zone == 1 && cursor.position == i;
                string display = i < Context.CurrentPlayer.MonsterZone.Count 
                    ? (Context.CurrentPlayer.MonsterZone[i]?.Name ?? "[Empty]") 
                    : "[Empty]";
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
                bool isSelected = cursor.zone == 2 && cursor.position == i;
                string display = i < Context.CurrentPlayer.MagicZone.Count 
                    ? (Context.CurrentPlayer.MagicZone[i]?.Name ?? "[Empty]") 
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
            int baseLine = 3; // 기본 시작 위치
    
            // 콘솔 높이 확인
            int maxLine = Console.WindowHeight - 5; 

            // 카드 이름 출력 (최대 줄 번호를 초과하지 않도록 제한)
            Console.SetCursorPosition(33, Math.Min(baseLine, maxLine));
            if (card != null)
            {
                Console.WriteLine($"카드 이름: {card.Name}");
        
                // 카드 타입 출력 (최대 줄 번호를 초과하지 않도록 제한)
                Console.SetCursorPosition(33, Math.Min(baseLine + 2, maxLine));
                Console.WriteLine($"카드 타입: {card.Type}");
        
                // 카드 효과 출력 (최대 줄 번호를 초과하지 않도록 제한)
                Console.SetCursorPosition(33, Math.Min(baseLine + 4, maxLine));
                Console.WriteLine("효과:");
        
                Console.SetCursorPosition(33, Math.Min(baseLine + 5, maxLine));
                Console.WriteLine(card.Description);

                if (card.Type == "몬스터")
                {
                    
                    Console.SetCursorPosition(33, Math.Min(baseLine + 7, maxLine));
                    Console.WriteLine($"공격력: {card.Attack}");

                    Console.SetCursorPosition(33, Math.Min(baseLine + 9, maxLine));
                    Console.WriteLine($"방어력: {card.Defense}");
                }
            }
            else
            {
                // 빈 슬롯 선택 시 메시지 출력
                Console.SetCursorPosition(33, Math.Min(baseLine, maxLine));
                Console.WriteLine("카드 정보 없음");
            }
        }


        private void DrawInstructions()
        {
            // 하단 안내 메시지 표시
            int instructionTop = Math.Max(Console.WindowHeight - 3, 0);
            Console.SetCursorPosition(0, instructionTop);
            Console.WriteLine("이동: ←→, 선택: Enter, 소환: S, 종료: ESC");
        }

        private Card GetSelectedCard()
        {
            // 선택된 카드 반환 로직 구현
            return null; // 예시로 null 반환
        }
    }
}