using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Stage1MiniGame3Goal : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _miniGame3State.isBabyActive)
        {
            _miniGame3State.isMouseOpen = !_miniGame3State.isMouseOpen;
            if (_miniGame3State.isMouseOpen)
            {
                anim.Play("AH");
            }
            else
            {
                anim.Play("UM");
            }
            // OnClick();
        }
    }
    public void PlayEat()
    {
        anim.Play("UMUL");
    }
    public void PlayAH()
    {
        anim.Play("AH");
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
