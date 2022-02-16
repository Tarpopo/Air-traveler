using System;
using UnityEngine;
using System.Collections;
public static class CorroutinesKid
{
    public static IEnumerator ScaleAnimation(Transform transform,Vector3 scale,int upSpeed,int downSpeed,Action onAnimationEnd)
    {
        var delta = (scale-transform.localScale)/upSpeed;
        var startScale = transform.localScale;
        for (int i = 0; i < upSpeed; i++)
        {
            transform.localScale += delta;
            yield return null;
        }
        delta = (startScale - transform.localScale)/downSpeed;
        for (int i = 0; i < downSpeed; i++)
        {
            transform.localScale += delta;
            yield return null;
        }
        onAnimationEnd?.Invoke();
    }
    public static IEnumerator MoveLocalAndBack(Transform transform,Vector3 target,int speed,Action onAnimationEnd)
    {
        var delta = (target-transform.localPosition)/speed;
        var startPosition = transform.localPosition;
        for (int i = 0; i < speed; i++)
        {
            transform.localPosition += delta;
            yield return null;
        }
        delta = (startPosition - transform.localPosition)/speed;
        for (int i = 0; i < speed; i++)
        {
            transform.localPosition += delta;
            yield return null;
        }
        onAnimationEnd?.Invoke();
    }
    public static IEnumerator EulerRotate(Transform transform, Vector3 rotateTo,int frames,Action onEndAnimation=null)
    {
        var delta = Quaternion.Euler((rotateTo-transform.eulerAngles)/frames);
        for (int i = 0;i<frames; i++)
        {
            transform.rotation *= delta;
            yield return null;
        }
        onEndAnimation?.Invoke();
    }
    public static IEnumerator TryEulerRotate(Transform transform, Vector3 rotateTo,int frames,Action onEndAnimation=null)
    {
        var delta = (rotateTo-transform.eulerAngles)/frames;
        for (int i = 0;i<frames; i++)
        {
            transform.eulerAngles += delta;
            yield return null;
        }
        onEndAnimation?.Invoke();
    }
    public static IEnumerator LocalEulerRotate(Transform transform, Vector3 rotateTo,int frames,Action onEndAnimation=null)
    {
        var delta = Quaternion.Euler((rotateTo-transform.localEulerAngles)/frames);
        for (int i = 0;i<frames; i++)
        {
            transform.localRotation *= delta;
            yield return null;
        }
        onEndAnimation?.Invoke();
    }
    
    public static IEnumerator MoveLocal(Transform transform,Vector3 target,int speed,Action onAnimationEnd)
    {
        var delta = (target-transform.localPosition)/speed;
        for (int i = 0; i < speed; i++)
        {
            transform.localPosition += delta;
            yield return null;
        }
        onAnimationEnd?.Invoke();
    }
    public static IEnumerator MoveFromTarget(Transform transform,Vector3 target,int speed,Action onAnimationEnd)
    {
        var startPosition = transform.position;
        transform.position = target;
        var delta = (startPosition-transform.position)/speed;
        for (int i = 0; i < speed; i++)
        {
            transform.position += delta;
            yield return null;
        }
        onAnimationEnd?.Invoke();
    }
    public static IEnumerator Move(Transform transform,Vector3 target,int speed,Action onAnimationEnd,float delay=0)
    {
        yield return new WaitForSeconds(delay);
        var delta = (target-transform.position)/speed;
        for (int i = 0; i < speed; i++)
        {
            transform.position += delta;
            yield return null;
        }
        onAnimationEnd?.Invoke();
    }
}
