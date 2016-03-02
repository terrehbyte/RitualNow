using UnityEngine;
using System;
using System.Collections;

public interface ICvarStorable
{
    string  GetValue(string cvarName,   string defaultValue="");
    int     GetInt(string cvarName,     int defaultValue=-1);
    float   GetFloat(string cvarName,   float defaultValue=-1.0f);
    bool    GetBool(string cvarName,    bool defaultValue = false);
    Color   GetColor(string cvarName,   Color defaultValue = default(Color));
    Rect    GetRect(string cvarName,    Rect defaultValue = default(Rect));
    Vector2 GetVector2(string cvarName, Vector2 defaultValue = default(Vector2));
    Vector3 GetVector3(string cvarName, Vector3 defaultValue = default(Vector3));
    Vector4 GetVector4(string cvarName, Vector4 defaultValue = default(Vector4));
}