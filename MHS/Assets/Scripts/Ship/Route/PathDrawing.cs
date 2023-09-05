using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer 컴포넌트를 연결합니다.
    public Transform startMarker; // 출발지 마커(Transform)을 연결합니다.
    public Transform endMarker; // 도착지 마커(Transform)을 연결합니다.

    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼을 클릭하면 드래그 시작합니다.
            isDragging = true;
            lineRenderer.positionCount = 0; // 이전 경로를 지웁니다.
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스 왼쪽 버튼을 놓으면 드래그 종료합니다.
            isDragging = false;
        }

        if (isDragging)
        {
            // 마우스를 클릭하고 있는 동안 드래그 중입니다.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = mousePosition - startMarker.position;

            // 대각선 방향을 판단하여 수평 또는 수직으로만 직선을 그립니다.
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                // 수평으로 그립니다.
                endMarker.position = new Vector3(mousePosition.x, startMarker.position.y, 0f);
            }
            else
            {
                // 수직으로 그립니다.
                endMarker.position = new Vector3(startMarker.position.x, mousePosition.y, 0f);
            }

            // 직선 경로를 그립니다.
            DrawLine(endMarker.position, startMarker.position);
        }
    }

    // 두 점 사이에 직선을 그리는 함수
    private void DrawLine(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
