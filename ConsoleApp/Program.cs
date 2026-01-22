using BankAccountSystem.ConsoleApp.CompositionRoot;

public class Program
{
    private static void Main(string[] args)
    {
        var app = AppBootstrapper.Build();
        app.Run();
    }
}