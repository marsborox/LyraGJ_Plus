using System.Collections;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public bool makeLookBroken = true;

    [SerializeField] private Sprite brokenDoor;
    [SerializeField] private Sprite door;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    private Animator _leftDoorAnimator;
    private Animator _rightDoorAnimator;
    private SpriteRenderer _leftDoorRenderer;
    private SpriteRenderer _rightDoorRenderer;

    void Start()
    {
        _leftDoorAnimator = leftDoor.GetComponentInChildren<Animator>();
        _leftDoorRenderer = leftDoor.GetComponentInChildren<SpriteRenderer>();
        _leftDoorRenderer.sprite = makeLookBroken ? brokenDoor : door;

        _rightDoorAnimator = rightDoor.GetComponentInChildren<Animator>();
        _rightDoorRenderer = rightDoor.GetComponentInChildren<SpriteRenderer>();
        _rightDoorRenderer.sprite = makeLookBroken ? brokenDoor : door;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        _leftDoorAnimator.SetTrigger("HasPlayer");
        _rightDoorAnimator.SetTrigger("HasPlayer");

        StartCoroutine(ChangeScene(0.5f));
    }

    private IEnumerator ChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        MySceneManager.instance.OpenElevator();
    }
}
