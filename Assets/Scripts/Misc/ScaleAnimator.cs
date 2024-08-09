using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
    [HideInInspector] public bool AutoTick;
    [HideInInspector] public float AutoTickTime;

    public AnimationCurve Curve;

    private float _autoTickTimer = 0.0f;

    void Start()
    {
        if (Curve == null || Curve.keys.Length == 0)
        {
            Debug.LogError("Missing Curve!", gameObject);
        }
    }

    void Update()
    {
        if (AutoTick)
        {
            _autoTickTimer -= Time.deltaTime;
            if (_autoTickTimer < 0.0f)
            {
                Animate();
                _autoTickTimer = AutoTickTime;
            }
        }
    }

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

[CustomEditor(typeof(ScaleAnimator))]
public class ScaleAnimatorCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var source = (ScaleAnimator)target;

        source.AutoTick = EditorGUILayout.Toggle("Auto Tick", source.AutoTick);
        if (source.AutoTick)
        {
            source.AutoTickTime = EditorGUILayout.FloatField("Tick Time", source.AutoTickTime);
        }
    }
}