using JustCli;
using JustCli.Attributes;
using System.Reflection;

namespace ConsoleApp.Commands;

[Command("version", "Prints app version & author.")]
public class VersionCommand : ICommand
{
    [CommandOutput]
    public IOutput Output { get; set; } = default!;

    public int Execute()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version!.ToString();

        var author = "Cool Guy";

        Output.WriteInfo($"{version} {author}");
        return ReturnCode.Success;
    }
}
