using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DebugUIBuilder;

public class PathDrawing : MonoBehaviour
{
    public LineRenderer lineRendererPrefab;
    private bool isDragging = false;
    private LineRenderer currentLineRenderer;
    private float zCoordinate = 100f;
    private Transform nearestGridCenter;
    private Vector3 lastPoint; // 이전 점 저장

    public Transform targetObject; // 도착 목적지
    private float distanceThreshold = 0.5f;

    public Transform[] gridCenterPositions = new Transform[24];

    private float maxDistanceForDrawing = 2.5f; // 대각선으로 못그리게
    private float dragStartTime = 0f; // 드래그 시작 시간
    private float minDragDuration = 0.2f; // 최소 드래그 지속 시간
    private int minDragging = 0;

    public Button penButton; // 펜 버튼
    public GameObject eraserButton; // 지우개 버튼

    void Start()
    {
        // 펜 버튼 클릭 이벤트 연결
        penButton.onClick.AddListener(StartDrawing);

        // 지우개 버튼 클릭 이벤트 연결
        eraserButton.SetActive(true);
        eraserButton.GetComponent<Button>().onClick.AddListener(EraseLines);

        // 처음 라인을 그릴 때 시작 위치를 고정
        Vector3 startPosition = new Vector3(0.9375f, -7.71875f, zCoordinate);
        currentLineRenderer = Instantiate(lineRendererPrefab);
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.SetPosition(0, startPosition);
        lastPoint = startPosition;
        SaveRoutePos.RoutePos.Add(startPosition);
    }

    private void StartDrawing()
    {
        EraseLines();

        UniteData.isPen = true;
        eraserButton.SetActive(true);
    }

    private void EraseLines()
    {
        // 모든 라인 렌더러를 삭제
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();

        //GameObject lineRenderer = GameObject.Find("Line(Clone)");
        //Destroy(lineRenderer);
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.positionCount = 0;
            //Destroy(lineRenderer.gameObject);
        }
        UniteData.isPen = false;

        

        // 그려진 경로 데이터 삭제
        SaveRoutePos.RoutePos.Clear();

        // 새로운 라인 렌더러 생성
        Vector3 startPosition = new Vector3(0.9375f, -7.71875f, zCoordinate);
        currentLineRenderer = Instantiate(lineRendererPrefab);
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.SetPosition(0, startPosition);
        lastPoint = startPosition;
        SaveRoutePos.RoutePos.Add(startPosition);
    }

    void Update()
    {
        if (UniteData.isPen)
        {
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                isDragging = true;
                minDragging = 0;

                dragStartTime = Time.time; // 드래그 시작 시간 기록
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                SaveRoutePos.RoutePos.Add(currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1));
                isDragging = false;
            }

            if (minDragging == 1)
            {
                currentLineRenderer.positionCount++; // 새로운 점 추가
                minDragging++;
            }

            if (isDragging)
            {
                float elapsedTime = Time.time - dragStartTime; // 드래그 경과 시간 계산

                if (elapsedTime >= minDragDuration)
                {
                    minDragging++;

                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    nearestGridCenter = FindNearestGridCenter(mousePosition);
                    if (currentLineRenderer.positionCount > 1)
                    {
                        float distance = Vector3.Distance(nearestGridCenter.position, currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 2));

                        // 대각선 방향으로 그리지 않도록 수정
                        if (distance <= maxDistanceForDrawing)
                        {
                            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, nearestGridCenter.position);
                        }

                        // 특정 위치에 도달하면 씬 전환
                        //if (Vector3.Distance(currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1), targetObject.position) < distanceThreshold)
                        //{
                        //    SaveRoutePos.RoutePos.Add(currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1));
                        //    SceneManager.LoadScene("Ship_Notice");
                        //}
                    }
                }
            }
        }
    }

    private Transform FindNearestGridCenter(Vector3 position)
    {
        Transform nearestGridCenter = null;
        float minDistance = float.MaxValue;

        foreach (Transform centerPosition in gridCenterPositions)
        {
            float distance = Vector3.Distance(position, centerPosition.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestGridCenter = centerPosition;
            }
        }

        return nearestGridCenter;
    }
}
