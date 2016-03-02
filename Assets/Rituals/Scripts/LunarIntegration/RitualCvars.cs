using LunarPlugin;

[CVarContainer]
static class RitualCvars
{
    // Packer
    public static readonly CVar godmode = new CVar("godmode", false);

    // Assembler
    public static readonly CVar sv_assembler_speed = new CVar("sv_assembler_speed", 2.0f);
}