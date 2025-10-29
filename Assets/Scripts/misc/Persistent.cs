using UnityEngine;

public class Persistent : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
