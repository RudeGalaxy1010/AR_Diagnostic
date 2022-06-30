using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Class for switching pages
[RequireComponent(typeof(GestureInput), typeof(LookAtTarget))]
public class Panel : MonoBehaviour
{
    private const int TextPagesCount = 5;
    private const int ImagePagesCount = 1;
    private const int StartPageIndex = 0;

    [SerializeField] private float _pagesSwipeSpeed = 2000f;
    [SerializeField] private Page _textPagePrefab;
    [SerializeField] private Page _imagePagePrefab;
    [SerializeField] private RectTransform _pagesContainer;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _previousPageButton;

    private GestureInput _gestureInput;
    private LookAtTarget _lookAtTarget;

    private Page[] _pages;
    private int _currentPageIndex;

    private float _pageWidth;
    private Vector2 _pagesContainerOffset;
    private Coroutine _swipePageCoroutine;

    private void Awake()
    {
        _gestureInput = GetComponent<GestureInput>();
        _lookAtTarget = GetComponent<LookAtTarget>();
        _lookAtTarget.SetTarget(Camera.main.transform);

        CreatePages(TextPagesCount, ImagePagesCount);

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

    public void SetInfo(cDataHolder data)
    {
        if (data == null)
        {
            Reset();

            return;
        }

        string cpuInfo = "CPU" + Environment.NewLine + data.dCPUName + Environment.NewLine + data.dCPUManufacturer + Environment.NewLine +
            data.dCPUWidth + Environment.NewLine + data.dCPUNumberOfCores + Environment.NewLine +
            data.dCPUNumberOfThreads + Environment.NewLine + data.dCPUCurrentClockSpeed;

        string videoAdapterInfo = "Video" + Environment.NewLine + data.dGPUName + Environment.NewLine + data.dGPUVideoProcessor + Environment.NewLine +
            data.dGPUStatus + Environment.NewLine + data.dGPURAM + Environment.NewLine + data.dGPUDriverVersion;

        string RAMInfo = "RAM" + Environment.NewLine + data.dRAMSize + Environment.NewLine + data.dRAMFree;

        string windowsInfo = "Windows" + Environment.NewLine + data.dSystemName + Environment.NewLine + data.dSystemVersion + Environment.NewLine +
            data.dSystemSerialNumber + Environment.NewLine + data.dSystemDirectory;

        string diskInfo = "Drive" + Environment.NewLine + data.dDiskParams;

        ((TextPage)_pages[0]).SetText(cpuInfo);
        ((TextPage)_pages[1]).SetText(videoAdapterInfo);
        ((TextPage)_pages[2]).SetText(RAMInfo);
        ((TextPage)_pages[3]).SetText(windowsInfo);
        ((TextPage)_pages[4]).SetText(diskInfo);
    }

    public void Reset()
    {
        ((TextPage)_pages[0]).SetText("Updating...");
        ((TextPage)_pages[1]).SetText("Updating...");
        ((TextPage)_pages[2]).SetText("Updating...");
        ((TextPage)_pages[3]).SetText("Updating...");
        ((TextPage)_pages[4]).SetText("Updating...");
    }

    private void CreatePages(int textPagesCount, int imagePagesCount)
    {
        _pages = new Page[textPagesCount + imagePagesCount];

        for (int i = 0; i < textPagesCount; i++)
        {
            _pages[i] = Instantiate(_textPagePrefab, _pagesContainer);
        }

        for (int i = textPagesCount; i < textPagesCount + imagePagesCount; i++)
        {
            _pages[i] = Instantiate(_imagePagePrefab, _pagesContainer);
        }
    }

    // Switch to next page
    private void NextPage()
    {
        if (_swipePageCoroutine != null)
        {
            return;
        }

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
        if (_swipePageCoroutine != null)
        {
            return;
        }

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
