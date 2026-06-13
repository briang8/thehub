using UnityEngine;
using System.Collections.Generic;

public class JourneyLine : MonoBehaviour
{
    public static JourneyLine Instance;

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    [SerializeField] private Transform playerStart;

    private void Awake()
    {
        Instance = this;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if (playerStart != null)
        {
            points.Add(playerStart.position);
            UpdateLine();
        }
    }

    public void AddPoint(Vector3 worldPosition)
    {
        worldPosition.z = -1f;
        points.Add(worldPosition);
        UpdateLine();
    }

    private void UpdateLine()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}