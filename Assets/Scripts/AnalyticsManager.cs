using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private void Awake()
    {
        Application.logMessageReceived += ApplicationOnlogMessageReceived;
    }

    private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                AppMetrica.Instance.ReportErrorFromLogCallback(condition, stacktrace);
                break;
        }
    }
}
