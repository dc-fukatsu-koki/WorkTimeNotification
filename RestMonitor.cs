using System.Runtime.InteropServices;

namespace WorkTimeNotification;

public partial class InputMonitor
{
    #region �t�B�[���h



    #endregion

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetLastInputInfo(ref LASTINPUTINFO plii);

    /// <summary>
    /// �Ō�̓��͂���̌o�ߎ��Ԃ��擾����
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