using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Singleton base class for Unity MonoBehaviours.
/// Ensures only one instance of the given type exists in the scene.
/// Can be inherited by any class that needs a global instance (e.g., GameManager, UIManager).
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    /// <summary>
    /// Accessor for the Singleton instance.
    /// Returns the only existing instance of this class in the scene.
    /// </summary>
    private static T instance;
    public static T Instance{ get { return instance; } }
    /// <summary>
    /// Handles instantiation and duplicate prevention logic.
    /// Destroys any extra instance and marks the correct one as persistent across scenes.
    /// </summary>
    protected virtual void Awake()
    {
        // If an instance already exists and this is not the main one, destroy it
        if (instance != null && this.gameObject != null) {
            Destroy(this.gameObject);
        } else {
            instance = (T)this;
        }
        // If the object is not a child of something else, keep it between scenes
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);       
        }
    }
}
