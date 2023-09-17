using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �޴��� ���������ؼ� �������� �ٲٴ� �ڵ�
// �ݱ� ��ư�� �� ��ũ��Ʈ�� �����Ǿ� ����
// Canvas�� Tablet ������Ʈ�� ����Ǿ� ����
public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] menuPages; // �޴� �������� Transform �迭
    private int currentPage = 0; // ���� ������ �ε���

    private Vector2 mouseDownPos;
    private Vector2 mouseUpPos;
    private float swipeThreshold = 1f; // �������� ���� �Ӱ谪

    [SerializeField] private Image[] pageIconObject;
    [SerializeField] private Sprite[] pageIconSprite;

    void Start()
    {
        // ó������ ù �������� ȭ�鿡 ���̰�
        for (int i = 1; i < menuPages.Length; i++)
        {
            menuPages[i].position = new Vector3(Screen.width, 0, 0);
        }

        // ù ������ �ϴ� ������ ���������� ����
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
            // ���� ������ ������ �̹��� ȸ�� ���������� ����
            pageIconObject[currentPage].sprite = pageIconSprite[0];

            // ���� ������ ������ �̹��� ���������� ����
            pageIconObject[currentPage - 1].sprite = pageIconSprite[1];

            menuPages[currentPage].position = new Vector3(Screen.width, 0, 0); // ���� ������ ������ �̵�
            currentPage--;
            menuPages[currentPage].position = new Vector3(0, 0, 0); // ���� ������ �߾� �̵�
        }
    }

    void MoveToNextPage()
    {
        if (currentPage < menuPages.Length - 1)
        {
            // ���� ������ ������ �̹��� ȸ�� ���������� ����
            pageIconObject[currentPage].sprite = pageIconSprite[0];

            // ���� ������ ������ �̹��� ������ ���������� ����
            pageIconObject[currentPage + 1].sprite = pageIconSprite[1];

            menuPages[currentPage].position = new Vector3(-Screen.width, 0, 0); // ���� ������ ���� �̵�
            currentPage++;
            menuPages[currentPage].position = new Vector3(0, 0, 0); // ���� ������ �߾� �̵�
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Camp");
    }
}
