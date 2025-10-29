using UnityEngine;

public class SingletonPersistent<T> : Singleton<T> where T : Singleton<T>
{

    protected override void Awake()
    {
        base.Awake();
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
