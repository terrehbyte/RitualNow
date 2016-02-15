// inspired by: http://codereview.stackexchange.com/questions/1142/checking-if-an-event-is-not-null-before-firing-it-in-c

using System;
using System.Collections;

public static class EventExtensions
{
    public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
    {
        var handlerCpy = handler;   // make a copy for race-condition stuff
        if (handlerCpy != null)
        {
            handlerCpy(sender, args);
        }
    }
}