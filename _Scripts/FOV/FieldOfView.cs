using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameServer;


public delegate void TargetsVisibilityChange(HashSet<Transform> newTargets);

[Serializable]
public class FOVData {
    public float viewRadius;
    [UnityEngine.Range(0, 360)] public float viewAngle;

    public void Update(CharacterSettings characterSettings) {
        viewRadius = characterSettings.FovRadius;
        viewAngle = characterSettings.FovAngle;
    }
}

//[ExecuteInEditMode]
public class FieldOfView : MonoBehaviour {
    public FOVData fovData;

    public float viewDepth;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector] public HashSet<Transform> visibleTargets = new HashSet<Transform>();

    public int meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;


    public MeshFilter viewMeshFilter;
    public bool debug;

    public static event TargetsVisibilityChange OnTargetsVisibilityChange;

    public FogProjector fogProjector;

    public float updateDistance = 1;
    public float fogUpdateDeltaTime = 0.1f;

    private float _lastFogUpdateTime = 0f;
    private Mesh _viewMesh;

    void OnEnable() {
        _viewMesh = new Mesh {name = "View Mesh"};
        viewMeshFilter.mesh = _viewMesh;

        fogProjector = fogProjector ?? FindObjectOfType<FogProjector>();

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void OnDisable() {
        visibleTargets.Clear();
    }


    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void LateUpdate() {
#if UNITY_EDITOR
        if (!Application.isPlaying) {
            return;
        }
#endif
        if (Time.time - _lastFogUpdateTime > fogUpdateDeltaTime) {
            DrawFieldOfView();
            fogProjector.UpdateFog();
            _lastFogUpdateTime = Time.time;
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fovData.viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < fovData.viewAngle / 2) {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) {
                    visibleTargets.Add(target);
                }
            }
        }
        
        OnTargetsVisibilityChange?.Invoke(visibleTargets);
    }

    void DrawFieldOfView() {
        float stepAngleSize = fovData.viewAngle / meshResolution;
        List<Vector3> viewPoints = new List<Vector3>();

        ObstacleInfo oldViewCast = new ObstacleInfo();

        for (int i = 0; i <= meshResolution; ++i) {
            float angle = transform.eulerAngles.y - fovData.viewAngle / 2f + stepAngleSize * i;
            ObstacleInfo newViewCast = FindObstacles(angle);

            if (i > 0) {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit ||
                    (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded)) {
                    // we're going to find the edge
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero) {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero) {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point); // points of collision
            //  Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
            oldViewCast = newViewCast;
        }

        // MESH CREATING
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3]; // 3 points in triangle. v - 2 -- count of triagles
        // triangles is index of points for all triangles. as in the example [0, 1, 2, 0, 2, 3, 0, 3, 4]
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; ++i) {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ObstacleInfo minObstacle, ObstacleInfo maxObstacle) {
        float minAngle = minObstacle.angle;
        float maxAngle = maxObstacle.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++) {
            float angle = (minAngle + maxAngle) / 2;
            ObstacleInfo newObstacle = FindObstacles(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minObstacle.dst - newObstacle.dst) > edgeDstThreshold;
            if (newObstacle.hit == minObstacle.hit && !edgeDstThresholdExceeded) {
                minAngle = angle;
                minPoint = newObstacle.point;
            } else {
                maxAngle = angle;
                maxPoint = newObstacle.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ObstacleInfo FindObstacles(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (DebugRayCast(transform.position, dir, out hit, fovData.viewRadius, obstacleMask)) {
            return new ObstacleInfo(true, hit.point + hit.normal * -viewDepth, hit.distance, globalAngle);
        }

        return new ObstacleInfo(false, transform.position + dir * (fovData.viewRadius - viewDepth), fovData.viewRadius, globalAngle);
    }

    bool DebugRayCast(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDistance, int mask) {
        if (Physics.Raycast(origin, direction, out hit, maxDistance, mask)) {
            if (debug)
                Debug.DrawLine(origin, hit.point);
            return true;
        }

        if (debug)
            Debug.DrawLine(origin, origin + direction * maxDistance);
        return false;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobal) {
        if (!isGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ObstacleInfo {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ObstacleInfo(bool _hit, Vector3 _point, float _dst, float _angle) {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}