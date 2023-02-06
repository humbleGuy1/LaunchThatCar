using Runtime.BaseCar;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Bots
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private CarMover _carMover;
        [SerializeField] private Path _path;
        [SerializeField] private LayerMask _layerMask;

        private float _rotateTime =0.5f;
        private float _pointReachDistance =10f;
        private float _obstacleAvoidDistance = 30f;
        private float _stopSpeed = 100f;
        private float _maxForce = 100f;
        private float _maxDistance = 100f;
        private float _speedGainTime = 0.5f;
        private float _freeRideDuration = 4f;
        private int _curretPathPointIndex;
        private bool _isPointReached;


        private void Start()
        {
            _carMover.IsControlDisabed = true;
            _carMover.Controller.SetStartRotation();
            StartCoroutine(Racing());
        }

        public void SetPointReached(int index)
        {
            _curretPathPointIndex = index;
            _isPointReached = true;
        }

        public IEnumerator Racing()
        {
            yield return new WaitForSeconds(1f);

            PathPoint pathPoint = _path.GetClosestPathPoint(transform.position,out int index);

            _curretPathPointIndex = index;
            while(IsRaceOver() == false)
            {
                yield return new WaitUntil(() => _carMover.IsGrounded);

                yield return Rotating(pathPoint, _rotateTime);
                yield return new WaitUntil(() => HasObstacleInFront(_obstacleAvoidDistance) == false);
                yield return Launch(pathPoint);

                yield return WaitingForBrake();
                yield return Braking();

                if(_isPointReached)
                    pathPoint = GetNextPathPoint();
            }
        }

        private IEnumerator Launch(PathPoint pathPoint)
        {
            _carMover.Wheels.Resume();
            yield return new WaitForSeconds(_speedGainTime);

            float distance = Vector3.Distance(transform.position, pathPoint.transform.position);

            float force = Mathf.Lerp(0, _maxForce, distance / _maxDistance);

            StartCoroutine(_carMover.MovingForward(force));
        }

        private PathPoint GetNextPathPoint()
        {
            PathPoint pathPoint;

            if (_path.TryGetNextPathPoint(_curretPathPointIndex, out PathPoint nextPathPoint))
            {
                pathPoint = nextPathPoint;
                _curretPathPointIndex++;
            }
            else
            {
                _curretPathPointIndex = -1;
                _path.TryGetNextPathPoint(_curretPathPointIndex, out PathPoint firstPathPoint);
                pathPoint = firstPathPoint;
            }

            _isPointReached = false;
            return pathPoint;
        }

        private bool IsRaceOver()
        {
            return false;
        }

        private IEnumerator WaitingForBrake()
        {
            float elapsedTime = 0;

            while (_isPointReached || HasObstacleInFront(_obstacleAvoidDistance) || elapsedTime< _freeRideDuration)
            {
                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }


        private bool HasObstacleInFront(float distance)
        {
           return Physics.Raycast(transform.position, transform.forward, distance, _layerMask);
        }

        private IEnumerator Rotating(PathPoint pathPoint, float rotatingTime)
        {
            float elapsedTime = 0f;
            var direction = pathPoint.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            Quaternion rot = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            while (elapsedTime < rotatingTime)
            {
                elapsedTime += Time.fixedDeltaTime;
                _carMover.Controller.Rotate(rot, elapsedTime / rotatingTime);
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator Braking()
        {
            while (_carMover.RBVelocity>0.1f)
            {
                _carMover.Brake(_stopSpeed);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
