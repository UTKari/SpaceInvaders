using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazePointer : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;
    private GameObject currentObject;
    private void Start()
    {
        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystem = eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();
        }
        pointerEventData = new PointerEventData(eventSystem);
    }
    private void Update()
    {
        pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        GameObject hitObject = GetFirstValid(raycastResults);
        if (currentObject != hitObject)
        {
            if (currentObject != null)
            {
                currentObject.SendMessage("OnPointerExit", pointerEventData, SendMessageOptions.DontRequireReceiver);
            }
            if (hitObject != null)
            {
                hitObject.SendMessage("OnPointerEnter", pointerEventData, SendMessageOptions.DontRequireReceiver);
            }
            currentObject = hitObject;
        }
        if (Input.GetMouseButtonDown(0) && currentObject != null)
        {
            currentObject.SendMessage("OnPointerClick", pointerEventData, SendMessageOptions.DontRequireReceiver);
        }
    }
    GameObject GetFirstValid(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (((1 << result.gameObject.layer) & interactableLayer) != 0)
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
