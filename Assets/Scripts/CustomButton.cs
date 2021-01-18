using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SOV
{
    public class CustomButton : Button
    {
        public new bool IsPressed { get; private set; }

        public override void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
            base.OnPointerUp(eventData);
        }
    }
}