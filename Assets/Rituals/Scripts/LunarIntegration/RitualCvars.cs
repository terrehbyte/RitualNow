using LunarPlugin;

[CVarContainer]
static class RitualCvars
{
    public static readonly CVar myBool = new CVar("myBool", false);
    public static readonly CVar myInt = new CVar("myInt", 10);
    public static readonly CVar myFloat = new CVar("myFloat", 3.14f);
    public static readonly CVar myString = new CVar("myString", "Hello!");
}