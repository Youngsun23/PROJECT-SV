using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    private static List<string> AutoBootStrappedScenes = new List<string>()
        {
            // "Main",
            // "Title",
            "Ingame",
        };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void SystemBoot()
    {
#if UNITY_EDITOR
        var activeScene = EditorSceneManager.GetActiveScene();
        for (int i = 0; i < AutoBootStrappedScenes.Count; i++)
        {
            if (activeScene.name.Equals(AutoBootStrappedScenes[i]))
            {
                InternalBoot();
            }
        }
#else
            InternalBoot();
#endif
    }

    private static void InternalBoot()
    {
        UIManager.Singleton.Initialize();
        // SoundManager.Singleton.Initialize();
    }
}