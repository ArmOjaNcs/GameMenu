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
    private Tween _animation;
    private WaitForSeconds _wait;
    private bool _isFirstStart = true;

    private void Awake()
    {
        if (_isFirstStart)
        {
            _targetPosition = _rectTransform.anchoredPosition;
            _startShift = new Vector2(_targetPosition.x, -Screen.height);
            _endShift = new Vector2(_targetPosition.x, Screen.height);
            _wait = new WaitForSeconds(Delay);
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
        _animation = _loadingImage.transform.DORotate(new Vector3(0, 0, 360), 1.5f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void Show()
    {
        _rectTransform.DOAnchorPos(_targetPosition, 0.5f).From(_startShift);
    }

    private void Hide()
    {
        if(_animation != null)
            _animation.Kill();

        _rectTransform.DOAnchorPos(_endShift, 0.5f).From(_targetPosition).OnComplete(() => gameObject.SetActive(false));
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