using Unity.VisualScripting;
using UnityEngine.Events;

public static class TimerEventManager
{
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction<float> TimerUpdate;
    public static event UnityAction<float> RecordUpdate;

    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerEnd() => TimerStop?.Invoke();
    public static void OnTimerUpdate(float value) => TimerUpdate?.Invoke(value);
    public static void OnRecordUpdate(float value) => RecordUpdate?.Invoke(value);
}
