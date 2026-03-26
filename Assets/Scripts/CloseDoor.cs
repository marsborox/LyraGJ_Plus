using System.Collections;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    [SerializeField] private Animator leftDoorAnimator;
    [SerializeField] private Animator rightDoorAnimator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        leftDoorAnimator.SetTrigger("HasPlayer");
        rightDoorAnimator.SetTrigger("HasPlayer");

        StartCoroutine(ChangeScene(0.5f));
    }

    private IEnumerator ChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        MySceneManager.instance.OpenElevator();
    }
}
