namespace OhaseApi.Game
{
  public class Player
  {
    public string PlayerId { get; private set; }
    public string Nick { get; set; }
    public string? NameToGuess { get; set; }
    public string Icon { get; set; }
    public bool ActiveTurn { get; set; }

    private string[] emojiWhitelist = new [] {"👴", "🤡", "😏", "👸", "👮‍♀️", "💂‍♀️", "🧑‍🌾", "🧑‍🚀", "🫃", "🤢", "💃", "🧘", "🦼", "🌚", "👽"};
    public Player(string nick, string playerid)
    {
      this.Nick = nick;
      this.PlayerId = playerid;
      this.ActiveTurn = false;
      this.Icon = this.emojiWhitelist[new Random().Next(0, this.emojiWhitelist.Length)];
    }


  }
}
