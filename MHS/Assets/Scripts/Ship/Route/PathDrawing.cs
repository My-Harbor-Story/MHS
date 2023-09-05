using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer ������Ʈ�� �����մϴ�.
    public Transform startMarker; // ����� ��Ŀ(Transform)�� �����մϴ�.
    public Transform endMarker; // ������ ��Ŀ(Transform)�� �����մϴ�.

    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ư�� Ŭ���ϸ� �巡�� �����մϴ�.
            isDragging = true;
            lineRenderer.positionCount = 0; // ���� ��θ� ����ϴ�.
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ���콺 ���� ��ư�� ������ �巡�� �����մϴ�.
            isDragging = false;
        }

        if (isDragging)
        {
            // ���콺�� Ŭ���ϰ� �ִ� ���� �巡�� ���Դϴ�.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = mousePosition - startMarker.position;

            // �밢�� ������ �Ǵ��Ͽ� ���� �Ǵ� �������θ� ������ �׸��ϴ�.
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                // �������� �׸��ϴ�.
                endMarker.position = new Vector3(mousePosition.x, startMarker.position.y, 0f);
            }
            else
            {
                // �������� �׸��ϴ�.
                endMarker.position = new Vector3(startMarker.position.x, mousePosition.y, 0f);
            }

            // ���� ��θ� �׸��ϴ�.
            DrawLine(endMarker.position, startMarker.position);
        }
    }

    // �� �� ���̿� ������ �׸��� �Լ�
    private void DrawLine(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
