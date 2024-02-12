namespace OhaseApi.Game
{
  public class Logic
  {
    private Dictionary<string, Lobby> ActiveLobbies;
    private Thread CleanupLobbiesThread;

    public Logic()
    {
      this.ActiveLobbies = new Dictionary<string, Lobby>();
      this.CleanupLobbiesThread = new Thread(new ThreadStart(this.CleanUpLobbies));
      this.CleanupLobbiesThread.IsBackground = true;
      this.CleanupLobbiesThread.Start();
    }

    public string NewLobby()
    {
      var lobby = new Lobby();
      this.ActiveLobbies.Add(lobby.LobbyId, lobby);
      return lobby.LobbyId;
    }

    public string Getlobbyinfo(string lobby_id)
    {
      try
      {
        return System.Text.Json.JsonSerializer.Serialize(this.ActiveLobbies[lobby_id]);
      }
      catch (KeyNotFoundException)
      {
        return System.Text.Json.JsonSerializer.Serialize("");
      }
    }

    public bool AddPlayerToLobby(string lobby_id, string player_id, string player_nick)
    {
      Lobby lobby;
      try
      {
        lobby = this.ActiveLobbies[lobby_id];
      }
      catch (KeyNotFoundException)
      {
        return false;
      }

      if (!lobby.Players.Any(p => p.PlayerId == player_id))
      {
        lobby.Players.Add(new Player(player_nick, player_id));
        return true;
      }
      return false;
    }

    public bool SetNameToGuess(string name2guess, string lobby_id, string player_id)
    {
      Lobby lobby;
      try
      {
        lobby = this.ActiveLobbies[lobby_id];
      }
      catch (KeyNotFoundException)
      {
        return false;
      }

      foreach (Player p in lobby.Players)
      {
        if (p.PlayerId == player_id)
        {
          p.NameToGuess = name2guess;
          return true;
        }
      }
      return false;
    }

    private void CleanUpLobbies()
    {
      while(true)
      {
        //run every 15 minutes
        Thread.Sleep(1000*60*15);
        foreach (var entry in this.ActiveLobbies)
        {
          var now = DateTimeOffset.Now;
          var expire = entry.Value.CreationTime.AddHours(5);
          if (expire < now)
          {
            this.ActiveLobbies.Remove(entry.Key);
          }
        }
      }
    }
  }
}