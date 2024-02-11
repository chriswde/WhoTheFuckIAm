using System.Net;
using System.Text;
using System.Net.Mime;

namespace OhaseApi
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      //load config
      Dictionary<string, string> Config = ConfigParser.LoadConfigFromFile("config.cfg");
      if (Config.Count == 0)
      {
        Console.WriteLine("Invalid config file");
        Environment.Exit(-1);
      }

      //initialize game logic
      var Game = new Game.Logic();
  

      var builder = WebApplication.CreateBuilder(args);
      var app = builder.Build();

      app.MapGet("/", (HttpContext context) => 
      {
        context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
        return(readHtml("www/index.html"));
      });
        
      app.MapGet("/lobby", (string lobby_id, string player_id, string nick, HttpContext context) =>
      {
        Game.AddPlayerToLobby(lobby_id, player_id, nick);
        context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
        return(readHtml("www/lobby.html", true, "###server_url_port###", string.Concat(Config["server_url"],Config["server_port"])));
      });

      app.MapGet("/getlobbyinfo", (string lobby_id) =>
      {
        return Game.Getlobbyinfo(lobby_id);
      });

      app.MapGet("/setNameToGuess", (string name2guess, string lobby_id, string player_id) =>
      {
        if(!Game.SetNameToGuess(name2guess, lobby_id, player_id))
        {
          return "Could not set name";
        }
        return "[{updated: \"yes\"}]";
      });

      app.MapGet("/newlobby", () => 
      {
        return Game.NewLobby();
      });

      app.Run();
    }

    private static string readHtml(string filename, bool replaceKey = false, string key = "", string value = "")
    {
      if (!replaceKey)
      {
        return System.IO.File.ReadAllText(filename);
      }
      return System.IO.File.ReadAllText(filename).Replace(key, value);
    }
  }
}
