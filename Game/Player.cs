namespace OhaseApi.Game
{
  public class Player
  {
    public string PlayerId { get; private set; }
    public string Nick { get; set; }
    public string? NameToGuess { get; set; }
    public bool ActiveTurn { get; set; }
    public Player(string nick, string playerid)
    {
      this.Nick = nick;
      this.PlayerId = playerid;
      
      this.ActiveTurn = false;
    }
  }
}
