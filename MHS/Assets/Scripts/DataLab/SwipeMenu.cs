using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 메뉴를 스와이프해서 페이지를 바꾸는 코드
// 닫기 버튼도 이 스크립트에 구현되어 있음
// Canvas의 Tablet 오브젝트에 적용되어 있음
public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] menuPages; // 메뉴 페이지의 Transform 배열
    private int currentPage = 0; // 현재 페이지 인덱스

    private Vector2 mouseDownPos;
    private Vector2 mouseUpPos;
    private float swipeThreshold = 1f; // 스와이프 감지 임계값

    [SerializeField] private Image[] pageIconObject;
    [SerializeField] private Sprite[] pageIconSprite;

    void Start()
    {
        // 처음에는 첫 페이지만 화면에 보이게
        for (int i = 1; i < menuPages.Length; i++)
        {
            menuPages[i].position = new Vector3(Screen.width, 0, 0);
        }

        // 첫 페이지 하단 아이콘 검은색으로 설정
        pageIconObject[currentPage].sprite = pageIconSprite[1];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseUpPos = Input.mousePosition;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        
        float swipeDistance = Vector2.Distance(mouseDownPos, mouseUpPos);

        if (swipeDistance > swipeThreshold)
        {
            if (mouseUpPos.x > mouseDownPos.x)
            {
                MoveToPreviousPage();
            }
            else
            {
                MoveToNextPage();
            }
        }
    }

    void MoveToPreviousPage()
    {
        if (currentPage > 0)
        {
            // 현재 페이지 아이콘 이미지 회색 아이콘으로 변경
            pageIconObject[currentPage].sprite = pageIconSprite[0];

            // 이전 페이지 아이콘 이미지 검은색으로 변경
            pageIconObject[currentPage - 1].sprite = pageIconSprite[1];

            menuPages[currentPage].position = new Vector3(Screen.width, 0, 0); // 현재 페이지 오른쪽 이동
            currentPage--;
            menuPages[currentPage].position = new Vector3(0, 0, 0); // 이전 페이지 중앙 이동
        }
    }

    void MoveToNextPage()
    {
        if (currentPage < menuPages.Length - 1)
        {
            // 현재 페이지 아이콘 이미지 회색 아이콘으로 변경
            pageIconObject[currentPage].sprite = pageIconSprite[0];

            // 다음 페이지 아이콘 이미지 검은색 아이콘으로 변경
            pageIconObject[currentPage + 1].sprite = pageIconSprite[1];

            menuPages[currentPage].position = new Vector3(-Screen.width, 0, 0); // 현재 페이지 왼쪽 이동
            currentPage++;
            menuPages[currentPage].position = new Vector3(0, 0, 0); // 다음 페이지 중앙 이동
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Camp");
    }
}
