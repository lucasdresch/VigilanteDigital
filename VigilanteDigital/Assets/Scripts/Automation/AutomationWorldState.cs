using System.Collections.Generic;

public static class AutomationWorldState
{
    static HashSet<string> flags = new HashSet<string>();

    public static void SetFlag(string flag) => flags.Add(flag);
    public static void ClearFlag(string flag) => flags.Remove(flag);
    public static bool HasFlag(string flag) => flags.Contains(flag);
}
