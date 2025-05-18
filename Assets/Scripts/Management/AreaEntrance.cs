using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        Debug.Log($"TransitionName: {transitionName}, SceneTransitionName: {SceneManagement.Instance.SceneTransitionName}");
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
        else
        {
            Debug.LogWarning("Transition name does not match. FadeToClear will not be called.");
        }
    }
}
