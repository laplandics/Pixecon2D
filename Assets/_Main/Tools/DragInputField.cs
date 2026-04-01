using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tools
{
    public class DragInputField : TMP_InputField
    {
        public ScrollRect ScrollRect { private get; set; }
        private bool _disableInput;

        public override void OnPointerDown(PointerEventData eventData)
        {
            _disableInput = false;
            interactable = true;
            ScrollRect.OnInitializePotentialDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _disableInput = true;
            interactable = false;
            ScrollRect.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            ScrollRect.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            ScrollRect.OnEndDrag(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!_disableInput) interactable = true;
        }
    }
}