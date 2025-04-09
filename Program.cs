using OOPYuGiOhProject;
class Program
{
    static void Main(string[] args)
    {
        YuGiOhGame game = new YuGiOhGame();
        game.InitializeGame();

        Console.WriteLine("유희왕 게임에 오신 것을 환영합니다!");
        
        while (true)
        {
            // UI 갱신
            game.Context.UI.DrawUI();
            
            // 입력 처리
            var key = Console.ReadKey(true).Key;
            
            if (key == ConsoleKey.Escape) break;
            
            // 커서 업데이트
            game.Context.UI.UpdateCursor(key);
        }
    }
}