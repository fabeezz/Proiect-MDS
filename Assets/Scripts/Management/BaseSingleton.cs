using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Example base class that uses the generic Singleton pattern.
/// Can be extended to create globally accessible systems with ease.
/// </summary>
public class BaseSingleton : Singleton<BaseSingleton>
{
    // This class currently doesn't have additional functionality.
    // It's ready to be extended by other managers or systems if needed.
}
