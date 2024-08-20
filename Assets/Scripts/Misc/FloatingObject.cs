using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [HideInInspector] public bool AutoTick;
    [HideInInspector] public float AutoTickTime;

    public GameObject TextMeshPrefab;
    public Vector3 Direction;
    public float Speed;
    public float Duration;

    private float _autoTickTimer = 0.0f;

    private TextMeshPro _textMesh;
    private float _textSize;
    private Vector3 _textPosition;

    void Start()
    {
        if (TextMeshPrefab == null)
        {
            Debug.LogError("Missing Text Mesh Prefab!", gameObject);
        }
        else
        {
            var childObject = Instantiate(TextMeshPrefab);
            childObject.transform.parent = transform;
            childObject.transform.localPosition = Vector3.zero;
            _textMesh = childObject.GetComponentInChildren<TextMeshPro>();
            _textSize = _textMesh.fontSize;
            _textPosition = _textMesh.transform.localPosition;
            _textMesh.fontSize = 0f;
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

    public void Animate(string newText = null)
    {
        StartCoroutine(AnimateCoroutine(newText));
    }

    private IEnumerator AnimateCoroutine(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            _textMesh.text = newText;
        }

        _textMesh.fontSize = _textSize;

        var curDuration = 0f;
        while (curDuration < Duration)
        {
            _textMesh.transform.localPosition += Direction * Speed;
            yield return new WaitForFixedUpdate();
            curDuration += Time.deltaTime;
        }

        // reset
        _textMesh.fontSize = 0f;
        _textMesh.transform.localPosition = _textPosition;
    }
}

[CustomEditor(typeof(FloatingText))]
public class FloatingTextCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var source = (FloatingText)target;

        source.AutoTick = EditorGUILayout.Toggle("Auto Tick", source.AutoTick);
        if (source.AutoTick)
        {
            source.AutoTickTime = EditorGUILayout.FloatField("Tick Time", source.AutoTickTime);
        }
    }
}