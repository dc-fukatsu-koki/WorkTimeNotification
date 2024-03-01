using System.Runtime.InteropServices;

namespace WorkTimeNotification;

public partial class InputMonitor
{
    #region フィールド



    #endregion

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetLastInputInfo(ref LASTINPUTINFO plii);

    /// <summary>
    /// 最後の入力からの経過時間を取得する
    /// </summary>
    /// <returns></returns>
    private static TimeSpan GetInputSpan()
    {
        var lastInputInfo = new LASTINPUTINFO();
        lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
        GetLastInputInfo(ref lastInputInfo);

        var lastInputTick = lastInputInfo.dwTime;

        var currentTick = Environment.TickCount;

        var idleTick = currentTick - lastInputTick;

        var idleTime = TimeSpan.FromMilliseconds(idleTick);

        return idleTime;
    }
}

struct LASTINPUTINFO
{
    public uint cbSize;
    public uint dwTime;
}