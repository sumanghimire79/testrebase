var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUser, UserService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/rebase", () => "this will be my second commit to test 'git rebase -i HEAD~X'  ");
app.MapPost("/write", (IUser userService, User bodytext) =>
{
  Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(bodytext));
  userService.Writeinfolder(bodytext);
});

app.Run();

public interface IUser
{
  void Writeinfolder(User bodytext);
}

public class UserService : IUser
{
  public void Writeinfolder(User bodytext)
  {
    if (!File.Exists("test.json"))
    {
      // File.Create("test.json");
      File.WriteAllText("test.json", "[]");
    }

    var gettextfile = File.ReadAllText(@"test.json");
    var deserialized = System.Text.Json.JsonSerializer.Deserialize<List<User>>(gettextfile);
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(bodytext));
    deserialized.Add(bodytext);

    var derialized = System.Text.Json.JsonSerializer.Serialize(deserialized);
    File.WriteAllText("test.json", derialized);
    Console.WriteLine(gettextfile);
    Console.WriteLine(derialized);
  }
}
public class User
{
  public string? name { get; set; }
  public string? email { get; set; }
}