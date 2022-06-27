using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GestureInput), typeof(LookAtTarget))]
public class Panel : MonoBehaviour
{
    private const int StartPageIndex = 0;

    [SerializeField] private TMP_Text _testText;

    [SerializeField] private Page[] _pages;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _previousPageButton;

    private GestureInput _gestureInput;
    private LookAtTarget _lookAtTarget;

    private Page _currentPage;
    private int _currentPageIndex;

    private void Awake()
    {
        _gestureInput = GetComponent<GestureInput>();
        _lookAtTarget = GetComponent<LookAtTarget>();
        _lookAtTarget.SetTarget(Camera.main.transform);

        _currentPageIndex = StartPageIndex;
        SetPage(StartPageIndex);
    }

    private void OnEnable()
    {
        _gestureInput.RightSwipeReceived += PreviousPage;
        _gestureInput.LeftSwipeReceived += NextPage;

        _nextPageButton.onClick.AddListener(NextPage);
        _previousPageButton.onClick.AddListener(PreviousPage);
    }

    private void OnDisable()
    {
        _gestureInput.RightSwipeReceived -= PreviousPage;
        _gestureInput.LeftSwipeReceived -= NextPage;

        _nextPageButton.onClick.RemoveListener(NextPage);
        _previousPageButton.onClick.RemoveListener(PreviousPage);
    }

    private void NextPage()
    {
        _currentPageIndex++;

        if (_currentPageIndex >= _pages.Length)
        {
            _currentPageIndex = 0;
        }

        SetPage(_currentPageIndex);
    }

    private void PreviousPage()
    {
        _currentPageIndex--;

        if (_currentPageIndex < 0)
        {
            _currentPageIndex = _pages.Length - 1;
        }

        SetPage(_currentPageIndex);
    }

    private void SetPage(int index)
    {
        _currentPage = _pages[index];
        _testText.text += "1";
    }
}
