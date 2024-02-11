namespace OhaseApi.Game
{
  public class Lobby
  {
    public string LobbyId { get; private set; }

    public List<Player> Players {get; private set; }

    public Lobby()
    {
      this.LobbyId = Guid.NewGuid().ToString("N");
      this.Players = new List<Player>();
    }

    [System.Diagnostics.ConditionalAttribute("DEBUG")]
    public void DummyData()
    {
      string[] names = {"Klaus", "Renate", "Wolfgang", "Rainer", "Helene", "Gudrun", "Herribert", "Hagen", "Hildegard"};
      var rnd = new Random();
      for (int i = 0; i < 7; i++)
      {
        this.Players.Add(new Player(names[rnd.Next(0, names.Length)], rnd.Next(11111111, 99999999).ToString()));
      }
    }
  }
}