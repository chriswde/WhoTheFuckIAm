namespace OhaseApi
{
  public class ConfigParser
  {
    public static Dictionary<string, string> LoadConfigFromFile(string filepath)
      {
        var conf = new Dictionary<string, string>();

        if (System.IO.File.Exists(filepath))
        {
          foreach (string line in File.ReadAllLines(filepath))
          {
            if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
            {
              continue;
            }
            string[] k_and_v = line.Split(" = ");
            if (k_and_v.Length == 2)
            {
              conf.Add(k_and_v[0], k_and_v[1]);
            }
            else
            {
              return new Dictionary<string, string>();
            }
          }
          return conf;
        }
        return new Dictionary<string, string>();
      }
  }
}