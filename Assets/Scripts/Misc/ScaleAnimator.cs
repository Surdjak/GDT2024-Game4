using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
    public AnimationCurve Curve;

    void Start()
    {
        if (Curve == null || Curve.keys.Length == 0)
        {
            Debug.LogError("Missing Curve!", gameObject);
        }
    }

    //private float _time = 0.0f;
    //private void Update()
    //{
    //    _time -= Time.deltaTime;
    //    if (_time < 0)
    //    {
    //        Animate();
    //        _time = 3.0f;
    //    }
    //}

    public void Animate()
    {
        StartCoroutine(AnimateCoroutine());
    }

    private IEnumerator AnimateCoroutine()
    {
        var targetScale = transform.localScale;
        var curTime = 0.0f;
        transform.localScale = targetScale * Curve.Evaluate(curTime);
        yield return new WaitForFixedUpdate();

        while (curTime < Curve.keys.Last().time)
        {
            curTime += Time.deltaTime;
            transform.localScale = targetScale * Curve.Evaluate(curTime);
            yield return new WaitForFixedUpdate();
        }

        transform.localScale = targetScale;
    }
}
