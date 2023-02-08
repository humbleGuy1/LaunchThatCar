using Dreamteck;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour, IRespawnable
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField] private WheelsHandler _wheels;
        [SerializeField] private CenterOfMassPosition _centerOfMassPosition = new CenterOfMassPosition();
        [SerializeField] private PositionProperty _positionProperty;
        [SerializeField] private AnimationCurve _angularDragCurve;
        [SerializeField] private CarController _carController;
        [SerializeField] private AnimationCurve _acceleratingCurve;
        [SerializeField] private CarAnimator _carAnimator;
        [SerializeField] private Transform _target;
        [SerializeField] private float _motorTorque;

        [field: SerializeField] public float StopSpeed { get; private set; } = 200f;
        [field: SerializeField] public float MaxStopSpeed { get; private set; } = 75f;

        public bool IsControlDisabed;

        private float _acceleratingTime = 0.3f;
        private AngularVelocityCalculator _angularDragCalculator;
        private CarRespawn _carRespawn;

        private readonly float _relaxTime = 0.5f;

        public float MaxFlySpeed { get; private set; } = 50f;
        public float ChargedSpeed { get; private set; }
        public float LastChargedSpeed { get; private set; }
        public bool IsBraking { get; private set; }
        public bool IsGrounded => _wheels.IsGearWheelGrounded;
        public CarController Controller => _carController;
        public float RBVelocity => _rigidBody.velocity.magnitude;
        public float MaxSpeed => _converter.MaxForce;
        public float ChargedPercent => ChargedSpeed / MaxSpeed;
        public bool IsMoving => _rigidBody.velocity.magnitude > 0.01f;
        public bool CanWRUMWRUM => _rigidBody.velocity.magnitude < 10f && _wheels.IsGearWheelGrounded;

        public WheelsHandler Wheels => _wheels;

        private void Awake()
        {
            _carRespawn = new CarRespawn(_rigidBody,transform, _wheels);
            _converter.Init(_playerInput);
        }

        private void LateUpdate()
        {
            if (_carRespawn.IsRespawning)
                return;

            MaxFlySpeed = _wheels.MaxFlySpeed;
            _wheels.Update();
            _wheels.DisableWheelColiderControll(false);

            if (IsControlDisabed)
                return;


            if (_playerInput.IsButtonDown)
            {
                if (_positionProperty.IsOnCarcase)
                    Respawn(transform.position + Vector3.up * 2, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));

                _carController.SetStartRotation();
            }


            if(_playerInput.IsButtonHold && _wheels.IsGearWheelGrounded)
            {
                if(CanWRUMWRUM)
                    _carController.Rotate(_playerInput, ChargedSpeed, MaxSpeed);
                else if(IsMoving)
                    _carController.Rotate(_playerInput, _rigidBody.velocity.magnitude, MaxSpeed);

            }

            if (IsMoving)
            {
                float speed = MaxSpeed / _relaxTime;
                ChargedSpeed = Mathf.MoveTowards(ChargedSpeed, 0, speed * Time.deltaTime);
            }

            //if(CheckPointReached)
            //    transform.rotation = Quaternion.LookRotation(_projector.result.forward, transform.up);

        }

        private void FixedUpdate()
        {
            var dir = _target.position - _rigidBody.position;
            dir.y = 0;
            var targetRotation = Quaternion.LookRotation(dir, Vector3.up);

            if(_wheels.IsSteerWheelGrounded)
                _rigidBody.rotation = Quaternion.RotateTowards(_rigidBody.rotation, targetRotation, Mathf.Rad2Deg * Time.fixedDeltaTime);

            if (_wheels.IsGearWheelGrounded)
            {
                //_rigidBody.AddForce(transform.forward * _motorTorque, ForceMode.Acceleration);
                _rigidBody.velocity = transform.forward * _motorTorque;
            }

            if (_wheels.IsGearWheelGrounded == false)
            {
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, MaxFlySpeed);
            }

            if (_wheels.IsGearWheelGrounded)
            {
                float gripForce = Mathf.Lerp(0, _wheels.MaxGrip, RBVelocity / MaxSpeed);
                _rigidBody.AddForce(-transform.up * gripForce, ForceMode.Acceleration);
            }
        }

        public void SetMaxForce(float value)
        {
            _converter.SetMaxForce(value);
        }
        
        public void SetMaxStopSpeed(float value)
        {
            MaxStopSpeed = value;
        }

        public void SetStopSpeed(float value)
        {
            StopSpeed = value;
        }

        public void MoveForward(float force)
        {
            if (_wheels.IsGearWheelGrounded)
            {
                _rigidBody.velocity = transform.forward* force;

            }
        }

        public IEnumerator MovingForward(float force)
        {
            float elapsedTime = 0;
            
            while(_wheels.IsGearWheelGrounded && elapsedTime < _acceleratingTime && IsBraking == false &&force >0)
            {
                elapsedTime += Time.deltaTime;
                _rigidBody.velocity = transform.forward * _acceleratingCurve.Evaluate(Mathf.Lerp(_rigidBody.velocity.magnitude, force, elapsedTime/_acceleratingTime)/force)*force;
                yield return new WaitForFixedUpdate();
            }
        }

        public void Brake(float brakeSpeed)
        {
            IsBraking = true;
            Vector3 targetVelocity = _rigidBody.velocity;
            targetVelocity.x = 0;
            targetVelocity.z = 0;
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, targetVelocity, brakeSpeed * Time.fixedDeltaTime);
            
            if (_rigidBody.velocity.magnitude < brakeSpeed * Time.fixedDeltaTime)
            {
                _rigidBody.velocity = targetVelocity;
            }
        }

        public void Respawn(Transform point)
        {
            Respawn(point.position, point.rotation);
        }

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            StartCoroutine(_carRespawn.Respawn(position, rotation));
            _carController.SetStartRotation();
        }
    }
}
