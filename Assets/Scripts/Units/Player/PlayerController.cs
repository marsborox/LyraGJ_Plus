using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Player player;
    private Vector2 _rawInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player.Move(_rawInput);
        player.input = _rawInput;
    }
    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
        //Debug.Log(_rawInput);
        if (_rawInput.x == -1)
        {
            //player.isMovingLeft = true;
            player.currentDirection = Player.Direction.LEFT;
        }
        else if (_rawInput.x == 1)
        {
            player.currentDirection = Player.Direction.RIGHT;
            //player.isMovingLeft = false;
        }
        else if (_rawInput.y == -1)
        {
            player.currentDirection = Player.Direction.DOWN;
        }
        else if (_rawInput.y == 1)
        {
            player.currentDirection = Player.Direction.UP;
        }
    }
    void OnWeapon1()
    {
        //Debug.Log("weapon1");
        player.AttackWeapon1();
    }
    void OnWeapon2()
    {
        //Debug.Log("weapon2");
        player.AttackWeapon2();
    }
    void OnWeapon3()
    {
        //Debug.Log("weapon3");
        player.AttackWeapon3();
    }
}
