using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class Logger
{
    // 빌드를 하면 이걸 사용하고 있는 함수들은 컴파일 과정에서 빠짐
    [Conditional("UNITY_EDITOR")]
    public static void Log(string msg)
    {
        Debug.Log($"<color=#ffffff>{msg}</color>");
    }
}