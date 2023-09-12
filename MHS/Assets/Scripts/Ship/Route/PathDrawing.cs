using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        // ó�� ������ �׸� �� ���� ��ġ�� ����
        Vector3 startPosition = new Vector3(0.9375f, -7.71875f, zCoordinate);
        currentLineRenderer = Instantiate(lineRendererPrefab);
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.SetPosition(0, startPosition);
        lastPoint = startPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            isDragging = true;
            currentLineRenderer.positionCount++; // ���ο� �� �߰�
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            nearestGridCenter = FindNearestGridCenter(mousePosition);
            float distance = Vector3.Distance(nearestGridCenter.position, currentLineRenderer.GetPosition(currentLineRenderer.positionCount - 2));

            // �밢�� �������� �׸��� �ʵ��� ����
            if (distance <= maxDistanceForDrawing)
            {
                currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, nearestGridCenter.position);
            }

            // Ư�� ��ġ�� �����ϸ� �� ��ȯ
            if (Vector3.Distance(currentLineRenderer.GetPosition(currentLineRenderer.positionCount-1), targetObject.position) < distanceThreshold)
            {
                SceneManager.LoadScene("Ship_Notice");
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
