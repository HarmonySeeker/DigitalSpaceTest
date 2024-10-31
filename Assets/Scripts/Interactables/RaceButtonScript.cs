using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceButtonScript : MonoBehaviour, IInteractableObject
{
    enum ButtonType {StartButton, FinishButton}
    [SerializeField] private ButtonType buttonType;

    public void InteractableAction()
    {
        if (buttonType == ButtonType.StartButton)
        {
            AudioManager.Instance.PlaySound3D(AudioNames.Sound.StartRace, transform.position);
            TimerEventManager.OnTimerStart();
        }
        else
        {
            AudioManager.Instance.PlaySound3D(AudioNames.Sound.FinishRace, transform.position);
            TimerEventManager.OnTimerEnd();
        }
    }
}
