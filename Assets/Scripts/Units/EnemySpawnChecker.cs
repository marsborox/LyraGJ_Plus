
using NUnit.Framework;
using UnityEngine;

public class EnemySpawnChecker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider2D;
    public bool isColliding = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("spawn not valid"); 
        isColliding = true;    
        Debug.Log("coliding with "+collision.gameObject.name);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;

    }

    public bool IsSpawnPosValid(Vector2 position)
    {
        this.transform.position = position;
        /*_collider2D.enabled = false;
        _collider2D.enabled = true;*/
        _spriteRenderer.color = Color.white;
        //isColliding = false;

        Debug.Log("spawnPoint checker should be at: "+position);
        
        if(!isColliding) {_spriteRenderer.color = Color.green;}
        else {_spriteRenderer.color = Color.red;}

        return isColliding;
        
    }

}
