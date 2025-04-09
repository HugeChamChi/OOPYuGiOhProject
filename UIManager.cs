namespace OOPYuGiOhProject
{


    public class UIManager
    {
        private List<Card> playerHand;
        private List<Card> monsterZone;
        private List<Card> magicZone;

        public UIManager(List<Card> playerHand,List<Card> monsterZone, List<Card> magicZone)
        {
            this.playerHand = playerHand;
            this.monsterZone = monsterZone;
            this.magicZone = magicZone;
        }

        public void UpdateCursor(ConsoleKey key)
        {
            // 키 입력에 따라 커서 이동 처리 (예제)
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    Console.WriteLine("왼쪽으로 이동");
                    break;

                case ConsoleKey.RightArrow:
                    Console.WriteLine("오른쪽으로 이동");
                    break;

                case ConsoleKey.UpArrow:
                    Console.WriteLine("위로 이동");
                    break;

                case ConsoleKey.DownArrow:
                    Console.WriteLine("아래로 이동");
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine("카드 선택");
                    break;

                case ConsoleKey.S:
                    Console.WriteLine("카드 소환");
                    break;

                default:
                    Console.WriteLine("알 수 없는 입력");
                    break;
            }
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
            Console.WriteLine("+-----------------------------------------------------------+--------------------------------------------------------------+");
            Console.WriteLine("|                                                           |                                                              |");
            Console.WriteLine("|                                                           |                                                              |");

            // 몬스터 존 출력
            for (int i = 0; i < monsterZone.Count; i++)
            {
                string display = monsterZone[i] != null ? monsterZone[i].Name : "[Empty]";
                Console.Write($"{display,-12}"); 
                if (i < monsterZone.Count - 1) Console.Write("  ");
            }

            // 빈 줄 추가
            Console.WriteLine("|                                                           |                                                              |");
            Console.WriteLine("+-----------------------------------------------------------+--------------------------------------------------------------+");

            // 마법/함정 존 출력
            
            for (int i = 0; i < magicZone.Count; i++)
            {
                string display = magicZone[i] != null ? magicZone[i].Name : "[Empty]";
                Console.Write($"{display,-12}"); 
                if (i < magicZone.Count - 1) Console.Write(" ");
            }

            // 하단 경계선 추가
            Console.WriteLine("+-----------------------------------------------------------+--------------------------------------------------------------+");
        }

        private void DrawCardDescription()
        {
            Card card = GetSelectedCard();

            if (card != null)
            {
                // 카드 설명 출력
                Console.SetCursorPosition(33, 3); // 카드 설명 위치 지정
                Console.WriteLine($"카드 이름: {card.Name}");

                Console.SetCursorPosition(33, 5);
                Console.WriteLine($"카드 타입: {card.Type}");

                Console.SetCursorPosition(33, 7);
                Console.WriteLine("효과:");

                Console.SetCursorPosition(33, 8);
                Console.WriteLine(card.Description);

                if (card.Type == "몬스터")
                {
                    Console.SetCursorPosition(33, 10);
                    Console.WriteLine($"공격력: {card.Attack}");

                    Console.SetCursorPosition(33, 12);
                    Console.WriteLine($"방어력: {card.Defense}");
                }
            }
        }

        private void DrawInstructions()
        {
            // 하단 안내 메시지 표시
            int instructionTop = 40; // 하단 위치 조정
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
