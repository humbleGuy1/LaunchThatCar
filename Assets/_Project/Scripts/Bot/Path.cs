using System.Linq;
using UnityEngine;

namespace Bots
{
    public class Path: MonoBehaviour
    {
        [SerializeField] private PathPoint[] _pathPoints;

        public PathPoint GetClosestPathPoint(Vector3 position, out int index)
        {
            PathPoint closestPathPoint = null;
            float minDistance = Mathf.Infinity;
            index = -1;

            foreach (var pathPoint in _pathPoints)
            {
                float dist = Vector3.Distance(pathPoint.transform.position, position);

                if (dist < minDistance)
                {
                    closestPathPoint = pathPoint;
                    minDistance = dist;
                }
            }

            index = closestPathPoint.Index;
            return closestPathPoint;
        }

        public bool TryGetNextPathPoint(int currentIndex, out PathPoint pathPoint)
        {
            int nextIndex = currentIndex + 1;
            pathPoint = _pathPoints.FirstOrDefault(point => point.Index == nextIndex);

            return pathPoint != null;
        }

        public static Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
        {
            lineDir.Normalize();//this needs to be a unit vector
            var v = pnt - linePnt;
            var d = Vector3.Dot(v, lineDir);
            return linePnt + lineDir * d;
        }
    }
}
