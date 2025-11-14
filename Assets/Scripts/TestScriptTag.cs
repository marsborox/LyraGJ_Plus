using UnityEngine;

public class TestScriptTag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetTag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetTag()
    {
        this.gameObject.tag = "Player";
    }
}
