using UnityEngine;
using System;
#if UNITY_EDITOR
    using UnityEditor;
#endif

// 원본 코드
//public class SingletonBase<T> : MonoBehaviour where T : class
//{
//    public static T Singleton
//    {
//        get
//        {
//            return _instance.Value;
//        }
//    }
//    public static bool IsSingletonCreated = _instance?.IsValueCreated ?? false;

//    private static readonly Lazy<T> _instance =
//       new Lazy<T>(() =>
//       {
//           T instance = FindObjectOfType(typeof(T)) as T;

//           if (instance == null)
//           {
//               GameObject obj = new GameObject(typeof(T).ToString());
//               instance = obj.AddComponent(typeof(T)) as T;

//#if UNITY_EDITOR
//               if (EditorApplication.isPlaying)
//               {
//                   DontDestroyOnLoad(obj);
//               }
//#else
//                   DontDestroyOnLoad(obj);
//#endif
//           }

//           return instance;
//       });

//    public virtual bool IsRuntimeInitializeScript { get; set; } = false;

//    protected virtual void Awake()
//    {
//        DontDestroyOnLoad(gameObject);
//        Init();
//    }
//    /// <summary>
//    /// Awake 함수에서 호출 된다.
//    /// </summary>
//    protected virtual void Init()
//    {

//    }
//}

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Singleton
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    //Debug.LogError($"[SingletonBase] {typeof(T)} 오브젝트가 씬에 존재하지 않습니다.");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 방지
        }

        Init();
    }

    protected virtual void Init() { }
}
