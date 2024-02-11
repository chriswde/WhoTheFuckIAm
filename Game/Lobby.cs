namespace OhaseApi.Game
{
  public class Lobby
  {
    public string LobbyId { get; private set; }
    public List<Player> Players {get; private set; }
    public DateTimeOffset CreationTime {get; private set; }

    public Lobby()
    {
      this.LobbyId = Guid.NewGuid().ToString("N");
      this.Players = new List<Player>();
      this.CreationTime = DateTimeOffset.Now;
    }
  }
}