using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeLapse : MonoBehaviour
{
    public Action OnPlayerEnter;
    public Action OnPlayerExit;

    [SerializeField]
    GameObject tutorialImage;

    [SerializeField]
    GameObject punchButton;

    private void Start()
    {
        if(tutorialImage != null)
            tutorialImage.SetActive(false);

        PlayerController.instance.playerFail += ExitTutorial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
            if (tutorialImage != null)
                tutorialImage.SetActive(true);

            if (punchButton != null) punchButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit?.Invoke();
            if (tutorialImage != null)
                tutorialImage.SetActive(false);


        }
    }

    public void ExitTutorial()
    {
        OnPlayerExit?.Invoke();
        if (tutorialImage != null)
            tutorialImage.SetActive(false);
    }
}
