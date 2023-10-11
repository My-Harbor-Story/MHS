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
    private Vector3 lastPoint; // ���� �� ����

    public Transform targetObject; // ���� ������
    private float distanceThreshold = 0.5f;

    public Transform[] gridCenterPositions = new Transform[24];

    private float maxDistanceForDrawing = 2.5f; // �밢������ ���׸���
    private float dragStartTime = 0f; // �巡�� ���� �ð�
    private float minDragDuration = 0.2f; // �ּ� �巡�� ���� �ð�
    private int minDragging = 0;

    public Button penButton; // �� ��ư
    public GameObject eraserButton; // ���찳 ��ư

    void Start()
    {
        // �� ��ư Ŭ�� �̺�Ʈ ����
        penButton.onClick.AddListener(StartDrawing);

        // ���찳 ��ư Ŭ�� �̺�Ʈ ����
        eraserButton.SetActive(true);
        eraserButton.GetComponent<Button>().onClick.AddListener(EraseLines);

        // ó�� ������ �׸� �� ���� ��ġ�� ����
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
        // ��� ���� �������� ����
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();

        //GameObject lineRenderer = GameObject.Find("Line(Clone)");
        //Destroy(lineRenderer);
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.positionCount = 0;
            //Destroy(lineRenderer.gameObject);
        }
        UniteData.isPen = false;

        

        // �׷��� ��� ������ ����
        SaveRoutePos.RoutePos.Clear();

        // ���ο� ���� ������ ����
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

                dragStartTime = Time.time; // �巡�� ���� �ð� ���
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                SaveRoutePos.RoutePos.Add(currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 1));
                isDragging = false;
            }

            if (minDragging == 1)
            {
                currentLineRenderer.positionCount++; // ���ο� �� �߰�
                minDragging++;
            }

            if (isDragging)
            {
                float elapsedTime = Time.time - dragStartTime; // �巡�� ��� �ð� ���

                if (elapsedTime >= minDragDuration)
                {
                    minDragging++;

                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    nearestGridCenter = FindNearestGridCenter(mousePosition);
                    if (currentLineRenderer.positionCount > 1)
                    {
                        float distance = Vector3.Distance(nearestGridCenter.position, currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 2));

                        // �밢�� �������� �׸��� �ʵ��� ����
                        if (distance <= maxDistanceForDrawing)
                        {
                            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, nearestGridCenter.position);
                        }

                        // Ư�� ��ġ�� �����ϸ� �� ��ȯ
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
