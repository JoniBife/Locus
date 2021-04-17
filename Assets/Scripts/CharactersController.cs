using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.GameEvents;
using Assets.Scripts.Audio;

namespace Assets
{
    public class CharactersController : MonoBehaviour
    {
        [Serializable]
        public class CharInfo
        {
            private string _animatorParameter = "Running";
            public float Velocity, AngularVelocity;
            public float HorizontalInput = 0, VerticalInput = 0;
            public Quaternion CurrRotation;
            public GameObject Go;
            public Rigidbody Rb;
            public Renderer Rdr;
            public ParticleSystem BloodParticles;
            public Collider Collider;
            public Animator Animator;
            private bool _running = false;
            //public bool Moving = false;
            public bool IndependentMovement = false;

            private bool _movementEnabled = true;
            public bool MovementEnabled { get { return _movementEnabled;  } set { _movementEnabled = value; } }

            public void CharacterDeath()
            {
                int i = 0;
                foreach(Transform child in Go.transform)
                {
                    if (i == 6) break;
                    child.gameObject.SetActive(false);
                    ++i;
                }
                Rb.velocity = Vector3.zero;
                AudioManager.Instance.PlayAudio("Pop");
                BloodParticles.Play();               
                Collider.enabled = false;
            }

            public void CharacterRevive(Vector3 respawnPosition)
            {
                int i = 0;
                foreach (Transform child in Go.transform)
                {
                    if (i == 6) break;
                    child.gameObject.SetActive(true);
                    ++i;
                }
                Collider.enabled = true;
                MovementEnabled = true;
                Go.transform.rotation = Quaternion.identity;
                Go.transform.position = respawnPosition;
            }

            public bool IsMoving()
            {
                return HorizontalInput != 0 || VerticalInput != 0;
            }
            
            // Default constructor
            public CharInfo(GameObject go) {
                this.Go = go;
                this.Rb = go.GetComponent<Rigidbody>();
                this.Rdr = go.GetComponent<Renderer>();
                this.Collider = go.GetComponent<Collider>();
                this.BloodParticles = go.GetComponentInChildren<ParticleSystem>();
                this.Animator = go.GetComponent<Animator>();
                this.BloodParticles.Stop();
            }

            public void Move(CharInfo otherInfo, float movementForce)
            {
#if DEBUG
                    Velocity = Rb.velocity.magnitude; AngularVelocity = Rb.angularVelocity.magnitude;
#endif
                if (otherInfo.IsMoving()|| IndependentMovement)
                {
                    // If there is any horizontal or vertical input we update the rigidbody's velocity
                    Vector3 newVelocity = new Vector3(HorizontalInput, 0, VerticalInput);

                    if (newVelocity.x != 0 && newVelocity.z != 0)
                        newVelocity.Normalize();

                    newVelocity *= movementForce;
                 
                    newVelocity.y = Rb.velocity.y;
                    Rb.velocity = newVelocity;

                    if (Rb.velocity.sqrMagnitude > 0f)
                    {

                        CurrRotation = Quaternion.LookRotation(Rb.velocity, Vector3.up).normalized;
                        Rb.rotation = CurrRotation;
                    }
                } 
                else
                {
                    // We have to set the velocity back to 0 otherwise it will maintain the previous velocity and slide if
                    // the other is stopped first
                    Rb.velocity = new Vector3(0f, Rb.velocity.y, 0f);
                }

                Rb.angularVelocity = Vector3.zero;
                Debug.Log(Rb.velocity);
            }

            public void Move(CharInfo otherInfo, float desiredSpeed, float movementForce)
            {
#if DEBUG
                Velocity = Rb.velocity.magnitude; AngularVelocity = Rb.angularVelocity.magnitude;
#endif
                if (_movementEnabled)
                {
                    if ((otherInfo.IsMoving() || IndependentMovement) && (Mathf.Abs(VerticalInput) == 1 || Mathf.Abs(HorizontalInput) == 1))
                    {
                        // If there is any horizontal or vertical input we update the rigidbody's velocity
                        Vector3 movementDir = new Vector3(HorizontalInput, 0, VerticalInput).normalized;

                        Rb.ApplyForceToReachVelocity(movementDir * desiredSpeed, movementForce);

                        Go.transform.rotation = Quaternion.LookRotation(movementDir, Vector3.up);

                        AudioManager.Instance.PlayAudioDelayed("Walk", 0.25f);

                        if (!_running)
                        {
                            _running = true;
                            Animator.SetBool(_animatorParameter, true);
                        }
                    } else if (_running) { 
                        
                        _running = false;
                        Animator.SetBool(_animatorParameter, false);
                    }
                } else
                {
                    AudioManager.Instance.StopAudio("Walk");
                    _running = false;
                    Animator.SetBool(_animatorParameter, false);
                }

                if (Go.transform.position.y < 0f)
                {
                    Rb.ApplyForceToReachVelocity(Vector3.down * 60f, movementForce);
                    otherInfo.MovementEnabled = false;
                }
            }
        }

        [Header("Movement settings")]
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _walkForwardForce;
        [SerializeField]
        private GameObject _leftCharacter, _rightCharacter;
        [SerializeField]
        private float _respawnDelay = 3f;
        public IntEvent PlayerDeathEvent;

        public CharInfo LeftCharInfo, RightCharInfo;
        public Vector3 LeftCurrentSpawnPoint, RightCurrentSpawnPoint;

        private bool _paused = false;

        private void Awake()
        {
            LeftCharInfo = new CharInfo(_leftCharacter);
            RightCharInfo = new CharInfo(_rightCharacter);
            LeftCurrentSpawnPoint = _leftCharacter.transform.position;
            RightCurrentSpawnPoint = _rightCharacter.transform.position;
        }

        void Update()
        {
            if (!_paused)
            {
                LeftCharInfo.HorizontalInput = Input.GetAxis("HorizontalLeft"); 
                LeftCharInfo.VerticalInput = Input.GetAxis("VerticalLeft");
                RightCharInfo.HorizontalInput = Input.GetAxis("HorizontalRight");
                RightCharInfo.VerticalInput = Input.GetAxis("VerticalRight");
            }

        }

        private void FixedUpdate()
        {
            if (!_paused)
            {
                LeftCharInfo.Move(RightCharInfo, _moveSpeed, _walkForwardForce);
                RightCharInfo.Move(LeftCharInfo, _moveSpeed, _walkForwardForce);

                if (LeftCharInfo.Go.transform.position.y < -60f)
                {
                    PlayerDeathEvent.Raise(1);
                    CharacterDeath(1);
                } else if (RightCharInfo.Go.transform.position.y < -60f)
                {
                    PlayerDeathEvent.Raise(2);
                    CharacterDeath(2);
                }
            }
        }

        public void CharacterDeath(int characterId)
        {
            _paused = true;
            if (characterId == 1)
            {
                LeftCharInfo.CharacterDeath();
                StartCoroutine(ResetCharactersRoutine());
            }
            else
            {
                RightCharInfo.CharacterDeath();
                StartCoroutine(ResetCharactersRoutine());
            }
        }

        private IEnumerator ResetCharactersRoutine()
        {
            yield return new WaitForSeconds(_respawnDelay);
            ResetCharacters();
            _paused = false;
        }

        public void ResetCharacters()
        {
            LeftCharInfo.CharacterRevive(LeftCurrentSpawnPoint);
            RightCharInfo.CharacterRevive(RightCurrentSpawnPoint);
        }

    }
}