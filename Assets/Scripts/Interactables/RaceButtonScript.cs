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
            TimerEventManager.OnTimerStart();
        else
            TimerEventManager.OnTimerEnd();
    }
}
