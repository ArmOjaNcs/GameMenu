using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    private const float Delay = 5;

    [SerializeField] private UIAnimator _gameWindow;
    [SerializeField] private Image _loadingImage;
    [SerializeField] private RectTransform _rectTransform;

    private Vector2 _targetPosition;
    private Vector2 _startShift;
    private Vector2 _endShift;
    private Tween _imageAnimation;
    private Tween _show;
    private Tween _hide;
    private WaitForSeconds _wait;
    private bool _isFirstStart = true;

    private void Awake()
    {
        if (_isFirstStart)
        {
            _targetPosition = _rectTransform.anchoredPosition;
            _startShift = new Vector2(_targetPosition.x, -Screen.height * 2);
            _endShift = new Vector2(_targetPosition.x, Screen.height * 2);
            _wait = new WaitForSeconds(Delay);
            _show = _rectTransform.DOAnchorPos(_targetPosition, 0.5f).From(_startShift).SetAutoKill(false);
            _hide = _rectTransform.DOAnchorPos(_endShift, 0.5f).From(_targetPosition)
                .OnComplete(() => gameObject.SetActive(false)).SetAutoKill(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(BeginLoading());
    }

    private void Start()
    {
        _isFirstStart = false;
    }

    private void AnimateImage()
    {
        if (_imageAnimation != null)
            _imageAnimation.Restart();
        else
            _imageAnimation = _loadingImage.transform.DORotate(new Vector3(0, 0, 360), 1.5f, RotateMode.FastBeyond360)
                .SetLoops(-1).SetEase(Ease.Linear).Play();
    }

    private void Show()
    {
        _show.Restart();
    }

    private void Hide()
    {
        if (_imageAnimation != null)
            _imageAnimation.Pause();
        
        _hide.Restart();
    }

    private void OffLoading()
    {
        _gameWindow.gameObject.SetActive(true);
        _gameWindow.Show();
        Hide();
    }

    private IEnumerator BeginLoading()
    {
        Show();
        AnimateImage();
        yield return _wait;
        OffLoading();
    }
}