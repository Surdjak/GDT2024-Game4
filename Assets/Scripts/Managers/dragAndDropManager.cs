using System;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    private GameObject _draggedObject;
    private Action<GameObject, Vector2> _dragAction;
    private Action<GameObject, Vector2> _dropAction;

    public void StartDrag(GameObject gameObject, Action<GameObject, Vector2> dropAction, Action<GameObject, Vector2> dragAction)
    {
        _draggedObject = gameObject;
        _dropAction = dropAction;
        _dragAction = dragAction;
    }

    private void Reset()
    {
        _draggedObject = null;
        _dropAction = null;
    }

    void Update()
    {
        if (_draggedObject == null)
            return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _draggedObject.transform.position = mousePosition;
        _dragAction?.Invoke(_draggedObject, mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            _dropAction?.Invoke(_draggedObject, mousePosition);
            Reset();
        }
    }
}
