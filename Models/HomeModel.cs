namespace TicTacToe.Models
{
    public class HomeModel
    {
        public bool State { get; set; }

        public HomeModel(bool state)
        {
            State = state;
        }
    }
}
