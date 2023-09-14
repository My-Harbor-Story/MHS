using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoPathFind : MonoBehaviour
{
    public Transform startPos;
    public Transform[] gridCenterPositions = new Transform[24];

    // 테스트로 임시로 넣어둠
    public Transform[] obstacleCenterPositions = new Transform[24];
    public Transform start;
    public Transform end;

    public Button aiButton;
    private List<Transform> shortestPath;

    // Start is called before the first frame update
    void Start()
    {
        // 시작 지점과 목표 지점 설정
        Transform startTransform = startPos;
        //Debug.Log(startPos.position);
        Transform goalTransform = gridCenterPositions[gridCenterPositions.Length - 1]; // 목표 지점 좌표
        //Debug.Log(goalTransform.position);

        // 최단 경로 찾기
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
        // 최단 경로를 선으로 그리기
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

    // 주어진 노드의 위치가 장애물 위치인지 확인하는 함수
    bool IsObstacle(Transform node)
    {
        foreach (Transform obstacle in obstacleCenterPositions)
        {
            if (Vector3.Distance(node.position, obstacle.position) < 0.1f)
            {
                return true; // 장애물 위치이면
            }
        }

        return false; // 장애물 위치 아님
    }

    Transform GetLowestFScoreNode(HashSet<Transform> openSet, Transform goal)
    {
        Transform lowestNode = null;
        float lowestFScore = float.MaxValue;

        foreach (Transform node in openSet)
        {
            float gScore = Vector3.Distance(start.position, node.position); // G 스코어 계산

            // H 스코어 계산
            float hScore = Vector3.Distance(node.position, goal.position);

            float fScore = gScore + hScore; // F 스코어 계산

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

        // 상하좌우 방향의 이웃 추가
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPosition = node.position + direction;

            // 이웃 노드가 장애물이거나 맵 밖으로 나가면 스킵
            if (!IsPositionValid(neighborPosition))
                continue;

            // 유효한 이웃 노드 찾으면 neighbors 리스트에 추가
            Transform neighbor = FindClosestNode(neighborPosition);
            if (neighbor != null)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    bool IsPositionValid(Vector3 position)
    {
        // 맵 경계
        float mapWidth = 100000.0f; // 맵의 가로 길이
        float mapHeight = 100000.0f; // 맵의 세로 길이

        // 좌표 맵 경계 내인지 확인(맵 크기에 맞춰 설정해야 함)
        if (position.x >= -100000.0f && position.x < mapWidth &&
            position.y >= -100000.0f && position.y < mapHeight)
        {
            return true; // 유효한 위치
        }
        else
        {
            return false; // 벗어난 위치
        }
    }

    Transform FindClosestNode(Vector3 position)
    {
        Transform closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (Transform node in gridCenterPositions)
        {
            float distance = Vector3.Distance(node.position, position);

            // 현재 노드 <-> 주어진 좌표 사이의 거리가 지금까지 찾은 것 중 가장 가까운 거리보다 작은 경우
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
            path.Insert(0, current); // 경로를 시작 지점부터 역순 저장
        }

        return path;
    }

    void DrawPath(List<Transform> path)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // LineRenderer 초기화
        lineRenderer.positionCount = path.Count;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        // 경로 노드 LineRenderer에 추가
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i].position);
        }
    }
}
