using UnityEngine;

public class Singleton<T>: MonoBehaviour where T : MonoBehaviour {
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static T Instance {
        get {
            if(_applicationIsQuitting) {
                //    return null;
            }
            lock(_lock) {
                if(_instance == null) {
                    _instance = (T)FindFirstObjectByType(typeof(T));
                    if(FindObjectsByType<T>(FindObjectsSortMode.None).Length > 1) {
                        return _instance;
                    }

                    if(_instance == null) {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        if(Application.isPlaying)
                            DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }
    }

    private void OnDestroy() {
        if(_instance == this) {
            _applicationIsQuitting = true;
        }
    }

    private void OnApplicationQuit() {
        _applicationIsQuitting = true;
    }
}