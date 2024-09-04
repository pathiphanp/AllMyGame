using UnityEngine;

public class Singleton<T> : MonoBehaviour
where T : Component
{
    public static T _instance;
    public virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = FindAnyObjectByType<T>();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
