namespace RitualWarehouse
{
    public interface ICvarStorable
    {
        string GetValue(string cvarName, string defaultValue = "");
        int GetInt(string cvarName, int defaultValue = -1);
        float GetFloat(string cvarName, float defaultValue = -1.0f);
        double GetDouble(string cvarName, double defaultValue = -1.0f);
    }
}