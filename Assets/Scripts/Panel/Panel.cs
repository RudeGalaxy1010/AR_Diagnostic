using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Class for switching pages
[RequireComponent(typeof(GestureInput), typeof(LookAtTarget))]
public class Panel : MonoBehaviour
{
    private const int StartPageIndex = 0;

    [SerializeField] private TMP_Text _testText;

    [SerializeField] private float _pagesSwipeSpeed = 2000f;
    [SerializeField] private Page[] _pages;
    [SerializeField] private RectTransform _pagesContainer;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _previousPageButton;

    private GestureInput _gestureInput;
    private LookAtTarget _lookAtTarget;

    private Page _currentPage;
    private int _currentPageIndex;

    private float _pageWidth;
    private Vector2 _pagesContainerOffset;
    private Coroutine _swipePageCoroutine;

    private void Awake()
    {
        _gestureInput = GetComponent<GestureInput>();
        _lookAtTarget = GetComponent<LookAtTarget>();
        _lookAtTarget.SetTarget(Camera.main.transform);
        _pageWidth = _pages[0].GetComponent<RectTransform>().rect.width;
        _pagesContainerOffset = _pagesContainer.anchoredPosition;

        _currentPageIndex = StartPageIndex;
        SetPage(StartPageIndex);
    }

    // Subscribe
    private void OnEnable()
    {
        _gestureInput.RightSwipeReceived += PreviousPage;
        _gestureInput.LeftSwipeReceived += NextPage;

        _nextPageButton.onClick.AddListener(NextPage);
        _previousPageButton.onClick.AddListener(PreviousPage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousPage();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            NextPage();
        }
    }

    // Unsubscribe
    private void OnDisable()
    {
        _gestureInput.RightSwipeReceived -= PreviousPage;
        _gestureInput.LeftSwipeReceived -= NextPage;

        _nextPageButton.onClick.RemoveListener(NextPage);
        _previousPageButton.onClick.RemoveListener(PreviousPage);
    }

    // Switch to next page
    private void NextPage()
    {
        _currentPageIndex++;

        if (_currentPageIndex >= _pages.Length)
        {
            _currentPageIndex = 0;
            SetPage(_currentPageIndex, false);
            return;
        }

        SetPage(_currentPageIndex);
    }

    // Switch to previous page
    private void PreviousPage()
    {
        _currentPageIndex--;

        if (_currentPageIndex < 0)
        {
            _currentPageIndex = _pages.Length - 1;
            SetPage(_currentPageIndex, false);
            return;
        }

        SetPage(_currentPageIndex);
    }

    private void SetPage(int index, bool isSmoothSwipe = true)
    {
        if (_swipePageCoroutine != null)
        {
            return;
        }

        _currentPage = _pages[index];
        _swipePageCoroutine = StartCoroutine(SwipePage(isSmoothSwipe));
    }

    // Coroutine for smooth swiping (or hard swiping !only for the first page and the last page due to the gap)
    private IEnumerator SwipePage(bool isSmoothSwipe)
    {
        float xContainerPosition = -(_currentPageIndex * _pageWidth);
        Vector2 newContainerPosition = new Vector2(xContainerPosition, 0) + _pagesContainerOffset;

        if (isSmoothSwipe == false)
        {
            _pagesContainer.anchoredPosition = newContainerPosition;
        }

        while (_pagesContainer.anchoredPosition.x != newContainerPosition.x)
        {
            _pagesContainer.anchoredPosition = Vector2.MoveTowards(_pagesContainer.anchoredPosition, newContainerPosition, Time.deltaTime * _pagesSwipeSpeed);
            yield return new WaitForEndOfFrame();
        }

        _swipePageCoroutine = null;
    }
}
