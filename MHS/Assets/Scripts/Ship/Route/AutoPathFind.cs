using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoPathFind : MonoBehaviour
{
    public Transform startPos;
    public Transform[] gridCenterPositions = new Transform[24];

    // �׽�Ʈ�� �ӽ÷� �־��
    public Transform[] obstacleCenterPositions = new Transform[24];
    public Transform start;
    public Transform end;

    public Button aiButton;
    private List<Transform> shortestPath;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ������ ��ǥ ���� ����
        Transform startTransform = startPos;
        //Debug.Log(startPos.position);
        Transform goalTransform = gridCenterPositions[gridCenterPositions.Length - 1]; // ��ǥ ���� ��ǥ
        //Debug.Log(goalTransform.position);

        // �ִ� ��� ã��
        shortestPath = FindShortestPath(startTransform, goalTransform);

        /*
        Debug.Log("Shortest Path:");

        foreach (Transform node in shortestPath)
        {
            Debug.Log("Node Position: " + node.position);
        }*/

    }

    public void Btn_AutoPathDraw()
    {
        // �ִ� ��θ� ������ �׸���
        DrawPath(shortestPath);
    }

    List<Transform> FindShortestPath(Transform start, Transform goal)
    {
        List<Transform> path = new List<Transform>();
        HashSet<Transform> openSet = new HashSet<Transform>();
        HashSet<Transform> closedSet = new HashSet<Transform>();
        Dictionary<Transform, Transform> cameFrom = new Dictionary<Transform, Transform>();

        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Transform current = GetLowestFScoreNode(openSet, goal);

            if (current == goal)
            {
                path = ReconstructPath(cameFrom, current);
                break;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Transform neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || IsObstacle(neighbor))
                    continue;

                float tentativeGScore = Vector3.Distance(start.position, current.position) +
                                        Vector3.Distance(current.position, neighbor.position);

                if (!openSet.Contains(neighbor) || tentativeGScore < Vector3.Distance(start.position, neighbor.position))
                {
                    cameFrom[neighbor] = current;
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return path;
    }

    // �־��� ����� ��ġ�� ��ֹ� ��ġ���� Ȯ���ϴ� �Լ�
    bool IsObstacle(Transform node)
    {
        foreach (Transform obstacle in obstacleCenterPositions)
        {
            if (Vector3.Distance(node.position, obstacle.position) < 0.1f)
            {
                return true; // ��ֹ� ��ġ�̸�
            }
        }

        return false; // ��ֹ� ��ġ �ƴ�
    }

    Transform GetLowestFScoreNode(HashSet<Transform> openSet, Transform goal)
    {
        Transform lowestNode = null;
        float lowestFScore = float.MaxValue;

        foreach (Transform node in openSet)
        {
            float gScore = Vector3.Distance(start.position, node.position); // G ���ھ� ���

            // H ���ھ� ���
            float hScore = Vector3.Distance(node.position, goal.position);

            float fScore = gScore + hScore; // F ���ھ� ���

            if (fScore < lowestFScore)
            {
                lowestFScore = fScore;
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    List<Transform> GetNeighbors(Transform node)
    {
        List<Transform> neighbors = new List<Transform>();

        // �����¿� ������ �̿� �߰�
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPosition = node.position + direction;

            // �̿� ��尡 ��ֹ��̰ų� �� ������ ������ ��ŵ
            if (!IsPositionValid(neighborPosition))
                continue;

            // ��ȿ�� �̿� ��� ã���� neighbors ����Ʈ�� �߰�
            Transform neighbor = FindClosestNode(neighborPosition);
            if (neighbor != null)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    bool IsPositionValid(Vector3 position)
    {
        // �� ���
        float mapWidth = 100000.0f; // ���� ���� ����
        float mapHeight = 100000.0f; // ���� ���� ����

        // ��ǥ �� ��� ������ Ȯ��(�� ũ�⿡ ���� �����ؾ� ��)
        if (position.x >= -100000.0f && position.x < mapWidth &&
            position.y >= -100000.0f && position.y < mapHeight)
        {
            return true; // ��ȿ�� ��ġ
        }
        else
        {
            return false; // ��� ��ġ
        }
    }

    Transform FindClosestNode(Vector3 position)
    {
        Transform closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (Transform node in gridCenterPositions)
        {
            float distance = Vector3.Distance(node.position, position);

            // ���� ��� <-> �־��� ��ǥ ������ �Ÿ��� ���ݱ��� ã�� �� �� ���� ����� �Ÿ����� ���� ���
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    List<Transform> ReconstructPath(Dictionary<Transform, Transform> cameFrom, Transform current)
    {
        List<Transform> path = new List<Transform>();
        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current); // ��θ� ���� �������� ���� ����
        }

        return path;
    }

    void DrawPath(List<Transform> path)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // LineRenderer �ʱ�ȭ
        lineRenderer.positionCount = path.Count;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        // ��� ��� LineRenderer�� �߰�
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i].position);
        }
    }
}
