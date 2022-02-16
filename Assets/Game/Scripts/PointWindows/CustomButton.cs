using DG.Tweening;
using Sirenix.OdinInspector;
using SquareDino;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _disable;
    [SerializeField] private Sprite _enable;
    [SerializeField] private bool _isAnimate;
    [ShowIf("_isAnimate")][SerializeField] private float _duration;
    [ShowIf("_isAnimate")][SerializeField] private int _elastisity;
    [ShowIf("_isAnimate")][SerializeField] private int _vibrato;
    [ShowIf("_isAnimate")][SerializeField] private Vector3 _punchScale;
    private bool _isActive;

    public void SetButton(bool isActive)
    {
        _isActive = isActive;
        _image.sprite = isActive ? _enable : _disable;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isActive == false) return;
        MyVibration.Haptic(MyHapticTypes.LightImpact);
        if (_isAnimate) transform.DOPunchScale(_punchScale,_duration,_vibrato,_elastisity);
        _onClick?.Invoke();
    }
}
