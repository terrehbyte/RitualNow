using UnityEngine;
using System.Collections;

using LunarPlugin;
using LunarPluginInternal;
using System;

public class LunarCvars : ICvarStorable
{
    public bool GetBool(string cvarName, bool defaultValue = false)
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.IsBool)
        {
            Debug.LogError(cvarName + " is not a bool!");
            return defaultValue;
        }

        return cmd.BoolValue;
    }

    public Color GetColor(string cvarName, Color defaultValue = default(Color))
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.IsColor)
        {
            Debug.LogError(cvarName + " is not a color!");
            return defaultValue;
        }

        return cmd.cvar.ColorValue;
    }

    public float GetFloat(string cvarName, float defaultValue = -1F)
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.IsFloat)
        {
            Debug.LogError(cvarName + " is not a float!");
            return defaultValue;
        }

        return cmd.FloatValue;
    }

    public int GetInt(string cvarName, int defaultValue = -1)
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.IsInt)
        {
            Debug.LogError(cvarName + " is not an int!");
            return defaultValue;
        }

        return cmd.IntValue;
    }

    public Rect GetRect(string cvarName, Rect defaultValue = default(Rect))
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.IsRect)
        {
            Debug.LogError(cvarName + " is not a Rect!");
            return defaultValue;
        }

        return cmd.cvar.RectValue;
    }

    public string GetValue(string cvarName, string defaultValue = "")
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }

        return cmd.Value;
    }

    public Vector2 GetVector2(string cvarName, Vector2 defaultValue = default(Vector2))
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.cvar.IsVector2)
        {
            Debug.LogError(cvarName + " is not a Vector2!");
            return defaultValue;
        }

        return cmd.cvar.Vector2Value;
    }

    public Vector3 GetVector3(string cvarName, Vector3 defaultValue = default(Vector3))
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.cvar.IsVector3)
        {
            Debug.LogError(cvarName + " is not a Vector3!");
            return defaultValue;
        }

        return cmd.cvar.Vector3Value;
    }

    public Vector4 GetVector4(string cvarName, Vector4 defaultValue = default(Vector4))
    {
        CVarCommand cmd = CRegistery.FindCvarCommand(cvarName);

        if (cmd == null)
        {
            Debug.LogWarning("Can't find cvar '" + cvarName + "'");
            return defaultValue;
        }
        else if (!cmd.cvar.IsVector4)
        {
            Debug.LogError(cvarName + " is not a Vector4!");
            return defaultValue;
        }

        return cmd.cvar.Vector4Value;
    }
}