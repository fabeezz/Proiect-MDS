using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        // Playerul e repoziționat doar când numele tranziției coincide
        this.SceneTransitionName = sceneTransitionName;
    }
}