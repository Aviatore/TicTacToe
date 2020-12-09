namespace TicTacToe
{
    public class Game
    {
        private Board board;
        
        public Game(int row, int col)
        {
            board = new Board(row, col);
        }

        public void ShowBoard()
        {
            board.Print("John", "Mike", 2, 5);
        }
    }
}