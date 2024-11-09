using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Stage1MiniGame3Goal : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _miniGame3State.isBabyActive)
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();
        
        if (raycaster != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObject)
                {
                    _miniGame3State.isMouseOpen = !_miniGame3State.isMouseOpen;
                }
            }
        }
    }
}
