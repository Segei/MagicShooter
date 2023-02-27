using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Platforms
{
    public class Router : MonoBehaviour
    {
        [SerializeField] private List<Segment> route = new List<Segment>();
        [SerializeField] private Color colorLineGizmo = Color.black;
        [SerializeField] private int countSegmentInLineGizmo = 50;
        private Segment currentSegment;
        private PathPoint currentPoint;
        private float timeLastCalls, currentTimeInSegment;
        private bool startPath, endPath;

        [Tooltip("The starting path of the direction. False - from start to finish.")] public bool Direction;

        private bool StartPath
        {
            get => startPath;
            set
            {
                startPath = value;
                if (value)
                {
                    OnStartPath?.Invoke();
                }
            }
        }

        private bool EndPath
        {
            get => endPath;
            set
            {
                endPath = value;
                if (value)
                {
                    OnEndPath?.Invoke();
                }
            }
        }

        public UnityEvent OnStartPath, OnEndPath;

        [Server]
        public PathPoint Wait()
        {
            timeLastCalls = Time.time;
            return currentPoint;
        }
        
        [Server]
        public PathPoint GetNextPoint()
        {
            if (EndPath)
            {
                return currentPoint;
            }

            StartPath = false;
            currentTimeInSegment += Time.time - timeLastCalls;

            if (currentTimeInSegment > currentSegment.Time)
            {
                int indexSegment = route.IndexOf(currentSegment);
                if (indexSegment < route.Count - 1)
                {
                    currentSegment = route[++indexSegment];
                    currentTimeInSegment = 0;
                }
                else
                {
                    EndPath = true;
                    return currentPoint;
                }
            }

            SetCurrentPoint();
            return currentPoint;
        }
        
        [Server]
        public PathPoint GeеPreliminaryPoint()
        {
            if (StartPath)
            {
                return currentPoint;
            }

            EndPath = false;
            currentTimeInSegment -= Time.time - timeLastCalls;

            if (currentTimeInSegment < 0)
            {
                int indexSegment = route.IndexOf(currentSegment);
                if (indexSegment > 0)
                {
                    currentSegment = route[--indexSegment];
                    currentTimeInSegment = currentSegment.Time;
                }
                else
                {
                    StartPath = true;
                    return currentPoint;
                }
            }

            SetCurrentPoint();
            return currentPoint;
        }
        
        [Server]
        public void Instance()
        {
            if (Direction)
            {
                StartPath = true;
                currentSegment = route.First();
                currentPoint.Position = currentSegment.Start.transform.position;
                currentPoint.Rotation = Bezier.GetDirection(currentSegment, 0);
            }
            else
            {
                StartPath = false;
                currentSegment = route.Last();
                currentPoint.Position = currentSegment.End.transform.position;
                currentPoint.Rotation = Bezier.GetDirection(currentSegment, 1);
                currentTimeInSegment = currentSegment.Time;
            }

            EndPath = !StartPath;
            timeLastCalls = Time.time;
        }
        
        [Server]
        private void SetCurrentPoint()
        {
            timeLastCalls = Time.time;
            currentPoint.Position = Bezier.GetPoint(currentSegment, currentTimeInSegment / currentSegment.Time);
            currentPoint.Rotation = Bezier.GetDirection(currentSegment, currentTimeInSegment / currentSegment.Time);
        }
        
        [Server]
        private void Awake()
        {
            
            foreach (var segment in route)
            {
                if (CheckSegment(segment))
                {
                    Debug.LogError(
                        "The router has a null waypoints in the route in segment " + route.IndexOf(segment) + ".",
                        gameObject);
                    return;
                }
            }
        }
        
        private static bool CheckSegment(Segment segment)
        {
            return segment.Start == null || segment.End == null;
        }
        
        private void OnValidate()
        {
            foreach (var segment in route)
            {
                segment.name = nameof(Segment) + " " + route.IndexOf(segment);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = colorLineGizmo;
            foreach (Segment segment in route)
            {
                if (CheckSegment(segment))
                {
                    return;
                }

                Vector3 preliminaryPoint = segment.Start.transform.position;

                for (int i = 0; i <= countSegmentInLineGizmo; i++)
                {
                    var parameter = (float)i / countSegmentInLineGizmo;

                    Vector3 point = Bezier.GetPoint(segment, parameter);

                    Gizmos.DrawLine(preliminaryPoint, point);

                    preliminaryPoint = point;
                }
            }
        }
    }
}