using UnityEngine;
using UnityEngine.AI;

namespace UnityTutorial.EnemyBotControl
{
    public class EnemyBotController : MonoBehaviour
    {
        [SerializeField] private GameObject playerObject;
        [SerializeField] private float stoppingDistance = 2.0f;
        [SerializeField] private float chaseRadius = 10.0f;
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float Dis2Ground = 0.8f;

        private NavMeshAgent _navMeshAgent;
        private Rigidbody _playerRigidbody;
        private Animator _animator;
        private bool _grounded = false;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private int _groundHash;
        private int _fallingHash;
        private Vector2 _currentVelocity;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.updateRotation = false;
            _navMeshAgent.stoppingDistance = stoppingDistance;

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");
        }

        private void FixedUpdate()
        {
            SampleGround();
            Move();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

            if (distanceToPlayer <= chaseRadius)
            {
                _navMeshAgent.SetDestination(playerObject.transform.position);
            }
            else
            {
                _navMeshAgent.ResetPath();
            }

            if (_grounded)
            {
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _navMeshAgent.velocity.x, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _navMeshAgent.velocity.z, AnimBlendSpeed * Time.fixedDeltaTime);

                if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) // Check if the agent is moving
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * AnimBlendSpeed);
                }
            }

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void SampleGround()
        {
            if (!_hasAnimator) return;

            RaycastHit hitInfo;
            if (Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                _grounded = true;
                SetAnimationGrounding();
                return;
            }
            _grounded = false;
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }
    }
}
