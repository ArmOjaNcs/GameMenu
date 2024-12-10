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
    private Vector2[] _defaultButtonsScale;
    private Tween _show;
    private Tween[] _buttonsBounceUp;
    private Tween _hide;
    private Tween[] _buttonsBounceDown;
    private bool _isFirstStart = true;

    private void Awake()
    {
        if (_isFirstStart)
        {
            _targetPosition = _rectTransform.anchoredPosition;
            _startShift = new Vector2(_targetPosition.x, -Screen.height * 2);
            _endShift = new Vector2(_targetPosition.x, Screen.height * 2);
            _defaultButtonsScale = new Vector2[_buttons.Length];
            _buttonsBounceUp = new Tween[_buttons.Length];
            _buttonsBounceDown = new Tween[_buttons.Length];

            for (int index = 0; index < _buttons.Length; index++)
            {
                _defaultButtonsScale[index] = _buttons[index].transform.localScale;

                _buttonsBounceUp[index] = _buttons[index].transform.DOScale(_defaultButtonsScale[index], 1f).From(0).SetEase(Ease.OutBounce)
                   .SetAutoKill(false);

                _buttonsBounceDown[index] = _buttons[index].transform.DOScale(0, 1f).From(_defaultButtonsScale[index]).SetEase(Ease.OutBounce)
                    .SetAutoKill(false);
            }

            _show = _rectTransform.DOAnchorPos(_targetPosition, 0.5f).From(_startShift).SetAutoKill(false);
            
            _hide = _rectTransform.DOAnchorPos(_endShift, 0.5f).From(_targetPosition)
                .OnComplete(() => gameObject.SetActive(false)).SetAutoKill(false);
        }
    }

    private void Start()
    {
        _isFirstStart = false;
    }

    public void Show()
    {
        _show.Restart();

        foreach(Tween tween in _buttonsBounceUp)
            tween.Restart();
    }

    public void Hide()
    {
        foreach (Tween tween in _buttonsBounceDown)
            tween.Restart();

        _hide.Restart();
    }
}