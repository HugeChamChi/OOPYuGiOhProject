using OOPYuGiOhProject;
class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(120, 50);
        Console.SetBufferSize(120, 50);
        
        YuGiOhGame game = new YuGiOhGame();
        game.InitializeGame();
        
        
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