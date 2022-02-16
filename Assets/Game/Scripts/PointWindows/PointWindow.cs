using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.PointWindows
{
    public class PointWindow : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private CustomButton _button;

        public void SetActive(bool isActive)
        {
            SetButton();
            gameObject.SetActive(isActive);
        }

        protected virtual bool IsButtonEnable() => false;
        protected void SetButton() => _button.SetButton(IsButtonEnable());
        public void OnPointerDown(PointerEventData eventData) => SetActive(false);
    }
}