using Assets.AI.Detection;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Hey developer!
/// If you have any questions, come chat with me on my Discord: https://discord.gg/GqeHHnhHpz
/// If you enjoy the controller, make sure you give the video a thumbs up: https://youtu.be/rJECT58CQHs
/// Have fun!
///
/// Love,
/// Tarodev
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private int UnpauseTimeDelay = 2;
    bool pause = false;
    private float TimePaused;
    private FrameInputs _inputs;
    

    private void Update()
    {
        if (!pause)
        {
            HandleGrounding();

            //HandleWalking(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow));

            //HandleJumping(); //Input.GetKeyDown(KeyCode.Space)

            HandleFalling();

        //    HandleWallSlide();

        //    HandleWallGrab();

        //    HandleDashing();
        }
    }

    #region Inputs

    private bool _facingLeft;

    public void SetInputs(float horizontalRaw, float verticalRaw, float horizontal, float vertical)
    {
        _inputs.RawX = (int)horizontalRaw;
        _inputs.RawY = (int)verticalRaw;
        _inputs.X = horizontal;
        _inputs.Y = vertical;
    }

    private void SetFacingDirection(bool left)
    {
        if (!pause)
        {
            _anim.transform.rotation = left ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
        }
    }
    public void HandlePause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pause && Time.time - TimePaused > UnpauseTimeDelay)
            {
                pause = true;
                Time.timeScale = 0;
            }
            else
            {
                pause = false;
                TimePaused = Time.time;
                Time.timeScale = 1;
            }
        }
    }
    #endregion

    #region Detection
    [SerializeField]
    private ColliderChecker _colliderChecker;
    public bool IsGrounded;
    public static event Action OnTouchedGround;

    private void HandleGrounding()
    {
        // Grounder
        var grounded = _colliderChecker.CollidingBootom;

        if (!IsGrounded && grounded)
        {
            IsGrounded = true;
            _hasJumped = false;
            _currentMovementLerpSpeed = 100;
            PlayRandomClip(_landClips);
            OnTouchedGround?.Invoke();
            //transform.SetParent(_ground[0].transform); - для двигающейся платформы
        }
        else if (IsGrounded && !grounded)
        {
            IsGrounded = false;
            _timeLeftGrounded = Time.time;
            //transform.SetParent(null);
        }

        if (IsGrounded)
        {
            _anim.SetBool("Falling", false);
            _anim.SetBool("Jumped", false);
        }
        else
        {
            _anim.SetBool("Falling", true);
        }

        // Wall detection
    }

    #endregion

    #region Walking

    [Header("Walking")][SerializeField] private float _walkSpeed = 4;
    [SerializeField] private float _acceleration = 2;
    [SerializeField] private float _currentMovementLerpSpeed = 100;

    public void HandleWalking(bool walkLeft, bool walkRight)
    {
        // This can be done using just X & Y input as they lerp to max values, but this gives greater control over velocity acceleration
        var acceleration = IsGrounded ? _acceleration : _acceleration * 0.5f;

        if (walkLeft)
        {
            SetFacingDirection(true);
            if (_rb.velocity.x > 0) _inputs.X = 0; // Immediate stop and turn. Just feels better
            _inputs.X = Mathf.MoveTowards(_inputs.X, -1, acceleration * Time.deltaTime);
            _anim.SetBool("isWalking", true);
        }
        else if (walkRight)
        {
            SetFacingDirection(false);
            if (_rb.velocity.x < 0) _inputs.X = 0;
            _inputs.X = Mathf.MoveTowards(_inputs.X, 1, acceleration * Time.deltaTime);
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _inputs.X = Mathf.MoveTowards(_inputs.X, 0, acceleration * 2 * Time.deltaTime);
        }

        var idealVel = new Vector3(_inputs.X * _walkSpeed, _rb.velocity.y);
        // _currentMovementLerpSpeed should be set to something crazy high to be effectively instant. But slowed down after a wall jump and slowly released
        _rb.velocity = Vector3.MoveTowards(_rb.velocity, idealVel, _currentMovementLerpSpeed * Time.deltaTime);
    }

    #endregion

    #region Jumping

    [Header("Jumping")][SerializeField] private float _jumpForce = 15;
    [SerializeField] private float _fallMultiplier = 7;
    [SerializeField] private float _jumpVelocityFalloff = 8;
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private Transform _jumpLaunchPoof;
    //[SerializeField] private float _wallJumpLock = 0.25f;
    //[SerializeField] private float _wallJumpMovementLerp = 5;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private bool _enableDoubleJump = true;
    private float _timeLeftGrounded = -10;
    private float _timeLastWallJumped;
    private bool _hasJumped;
    private bool _hasDoubleJumped;

    public void HandleJumping()
    {
        if (true)
        {
            //if (_grabbing || !IsGrounded && (_isAgainstLeftWall || _isAgainstRightWall))
            //{
            //    _timeLastWallJumped = Time.time;
            //    _currentMovementLerpSpeed = _wallJumpMovementLerp;
            //    ExecuteJump(new Vector2(_isAgainstLeftWall ? _jumpForce : -_jumpForce, _jumpForce)); // Wall jump
            //}
            if (IsGrounded || Time.time < _timeLeftGrounded + _coyoteTime || _enableDoubleJump && !_hasDoubleJumped)
            {
                if (!_hasJumped || _hasJumped && !_hasDoubleJumped)
                {
                    ExecuteJump(new Vector2(_rb.velocity.x, _jumpForce), _hasJumped);
                }// Ground jump
            }
        }

        void ExecuteJump(Vector3 dir, bool doubleJump = false)
        {
            _rb.velocity = dir;
            //_jumpLaunchPoof.up = _rb.velocity;
            //_jumpParticles.Play();
            _anim.SetBool("Jumped", true);
            _hasDoubleJumped = doubleJump;
            _hasJumped = true;
        }
    }

    private void HandleFalling()
    {
        // Fall faster and allow small jumps. _jumpVelocityFalloff is the point at which we start adding extra gravity. Using 0 causes floating

        if (_rb.velocity.y < _jumpVelocityFalloff || _rb.velocity.y > 0) //  && !Input.GetKey(KeyCode.Space)
            _rb.velocity += _fallMultiplier * Physics.gravity.y * Vector3.up * Time.deltaTime;
    }

    #endregion

    //#region Wall Slide

    //[Header("Wall Slide")]
    //[SerializeField]
    //private ParticleSystem _wallSlideParticles;

    //[SerializeField] private float _slideSpeed = 1;
    //private bool _wallSliding;

    //private void HandleWallSlide()
    //{
    //    var sliding = _pushingLeftWall || _pushingRightWall;

    //    if (sliding && !_wallSliding)
    //    {
    //        transform.SetParent(_pushingLeftWall ? _leftWall[0].transform : _rightWall[0].transform);
    //        _wallSliding = true;
    //        //_wallSlideParticles.transform.position = transform.position + new Vector3(_pushingLeftWall ? -_wallCheckOffset : _wallCheckOffset, 0);
    //        //_wallSlideParticles.Play();

    //        // Don't add sliding until actually falling or it'll prevent jumping against a wall
    //        if (_rb.velocity.y < 0) _rb.velocity = new Vector3(0, -_slideSpeed);
    //    }
    //    else if (!sliding && _wallSliding && !_grabbing)
    //    {
    //        transform.SetParent(null);
    //        _wallSliding = false;
    //        //_wallSlideParticles.Stop();
    //    }
    //}

    #region Impacts

    [Header("Collisions")]
    [SerializeField]
    private ParticleSystem _impactParticles;

    [SerializeField] private GameObject _deathExplosion;
    [SerializeField] private float _minImpactForce = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > _minImpactForce && IsGrounded) 
        {  
            //_impactParticles.Play();                                                                  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Instantiate(_deathExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    #endregion

    #region Audio

    [Header("Audio")][SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _landClips;
    [SerializeField] private AudioClip[] _dashClips;

    private void PlayRandomClip(AudioClip[] clips)
    {
        //_source.PlayOneShot(clips[Random.Range(0, clips.Length)], 0.2f);
    }

    #endregion

    private struct FrameInputs
    {
        public float X, Y;
        public int RawX, RawY;
    }
}