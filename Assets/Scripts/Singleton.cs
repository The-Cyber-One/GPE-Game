using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this as T;
    }
}

public class SingletonDontDestroy<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
        _instance = this as T;
    }
}