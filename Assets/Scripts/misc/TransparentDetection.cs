using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine;
public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)] //this will give us slider as serialized field we can 
    //set value in
    [SerializeField] private float _transparencyAmount = 0.8f;
    //how long will it take to fade
    [SerializeField] private float _transparencyFadeTime = 0.4f;

    [SerializeField] private SpriteRenderer _spriteToFade;
    [SerializeField] private int _unitsEnteredCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {//might want to expand on enemies and projectiles
        //Debug.Log("House trigger enter");
        if (other.gameObject.GetComponent<Unit>())
        {   //fade the tree
            _unitsEnteredCollider++;
            //we pass our sprite renderer, time to fade, sprite renderer alpha color, how transparent it will be
            StartCoroutine(FadeRoutine(_spriteToFade, _transparencyFadeTime, _spriteToFade.color.a, _transparencyAmount));
            //Debug.Log("UnitBehindBuilding entering");
        }
    }
    //we want to return to original (no) transparency
    //when moved out
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("House trigger exit");
        if (other.gameObject.GetComponent<Unit>())
        {
            _unitsEnteredCollider--;
            if (_unitsEnteredCollider > 0)
                return;
            StartCoroutine(FadeRoutine(_spriteToFade, _transparencyFadeTime, _spriteToFade.color.a, 1f));
            //Debug.Log("UnitBehindBuilding leaving");
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float transparencyFadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < transparencyFadeTime)
        {
            elapsedTime += Time.deltaTime;
            //we want is take start value go to our target transparency over some amount of time
            //which will be elapsed time divided by fade time
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / transparencyFadeTime);

            //to change alpha value of sprite renderer is we will change entire color
            //we will grab rgb values of our sprite renderer, (RGB) and forth value we are setting transparency calculated before
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            //when condition is not met anymore, just stop 
            yield return null;
        }
    }
}
