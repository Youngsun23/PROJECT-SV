using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : SingletonBase<SceneTransitionManager>
{
    private string currentLevelName;

    // private List<TransitionPoint> points = new List<TransitionPoint>();
    private Vector2 startPoint = new Vector2(-7f, -1f); // 임시 시작 위치 (집 내부 생기기 전까지)

    //public void AddTransitionPoint(TransitionPoint point)
    //{
    //    points.Add(point);
    //}

    private void Start()
    {
        if (string.IsNullOrEmpty(currentLevelName))
        {
            LoadLevel("Farm", startPoint);
        }
    }

    [Button]
    public void LoadLevel(string levelName, Vector2 pos)
    {
        StartCoroutine(LoadLevelAsync(levelName, pos));
    }

    IEnumerator LoadLevelAsync(string levelName, Vector2 pos)
    {
        if (PlayerCharacterController.Singleton != null)
        {
            PlayerCharacterController.Singleton.enabled = false;
            // PlayerCharacterController.Singleton.Move(Vector2.zero);
        }

        var FadeUI = UIManager.Singleton.GetUI<FadeInOutUI>(UIType.FadeInOut);
        if (!string.IsNullOrEmpty(currentLevelName))
        {
            FadeUI.FadeOut();
            yield return new WaitForSeconds(1);

            var unloadAsync = SceneManager.UnloadSceneAsync(currentLevelName);
            while (!unloadAsync.isDone)
            {
                yield return null;
            }
            currentLevelName = string.Empty;
        }

        currentLevelName = levelName;
        var loadAsync = SceneManager.LoadSceneAsync(currentLevelName, LoadSceneMode.Additive);
        while (!loadAsync.isDone)
        {
            yield return null;
        }
        //Scene loadedScene = SceneManager.GetSceneByName(currentLevelName);
        //SceneManager.SetActiveScene(loadedScene);

        var player = FindObjectOfType<PlayerCharacterToolController>();
        var marker = FindObjectOfType<MarkerManager>();
        player?.SetMarkerManager(marker);

        // PlayerCharacterController.Singleton.Move(Vector2.zero);
        PlayerCharacterController.Singleton.transform.position = pos;
        PlayerCharacterController.Singleton.enabled = true;

        FadeUI.FadeIn();
        yield return new WaitForSeconds(1);
    }
}
