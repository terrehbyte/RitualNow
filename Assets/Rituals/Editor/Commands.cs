using LunarPlugin;

[CCommand("test")]
class Cmd_test : CCommand
{
    void Execute()
    {
        PrintIndent("Hello, Unity!");
    }
}