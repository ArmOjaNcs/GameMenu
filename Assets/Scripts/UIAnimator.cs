using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button[] _buttons;
    
    private Vector2 _targetPosition;
    private Vector2 _startShift;
    private Vector2 _endShift;
    private Vector2 _defaultButtonScale;
    private bool _isFirstStart = true;

    private void Awake()
    {
        if (_isFirstStart)
        {
            _targetPosition = _rectTransform.anchoredPosition;
            _startShift = new Vector2(_targetPosition.x, -Screen.height);
            _endShift = new Vector2(_targetPosition.x, Screen.height);
            _defaultButtonScale = _buttons[0].transform.localScale;
        }
    }

    private void Start()
    {
        _isFirstStart = false;
    }

    public void Show()
    {
        _rectTransform.DOAnchorPos(_targetPosition, 0.5f).From(_startShift);

        foreach (var button in _buttons)
            button.transform.DOScale(_defaultButtonScale, 1f).From(0).SetEase(Ease.OutBounce);
    }

    public void Hide()
    {
        foreach (var button in _buttons)
            button.transform.DOScale(0, 1f).From(_defaultButtonScale).SetEase(Ease.OutBounce);

        _rectTransform.DOAnchorPos(_endShift, 0.5f).From(_targetPosition).OnComplete(() => gameObject.SetActive(false));
    }
}