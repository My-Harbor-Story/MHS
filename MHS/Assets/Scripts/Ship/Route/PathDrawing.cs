using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer 컴포넌트를 연결합니다.
    public Transform startMarker; // 출발지 마커(Transform)을 연결합니다.
    public Transform endMarker; // 도착지 마커(Transform)을 연결합니다.

    private bool isDragging = false;
    private Vector3 previousEndPoint; // 이전 직선의 끝점을 저장합니다.

    void Start()
    {
        // 초기 이전 직선의 끝점은 시작 마커 위치로 설정합니다.
        previousEndPoint = startMarker.position;

        // LineRenderer 초기화
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startMarker.position);
        lineRenderer.SetPosition(1, startMarker.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼을 클릭하면 드래그 시작합니다.
            isDragging = true;
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
            lineRenderer.SetPosition(1, endMarker.position);
        }
    }
}
