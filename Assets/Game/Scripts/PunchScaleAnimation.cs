using DG.Tweening;
using UnityEngine;

public class PunchScaleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    [SerializeField] private int _vibrato = 1;
    [SerializeField] private float _elastisity = 1;
    [SerializeField] private float _duration;
    private bool _isPlay;

    public void PlayAnimation()
    {
        if (_isPlay) return;
        _isPlay = true;
        transform.DOPunchScale(_scale, _duration, _vibrato, _elastisity).onComplete = () => _isPlay = false;
    }
}