using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public float MaxMoveSpeed = 8;
    public AudioSource StepSound;

    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.2f;

    private CharacterController controllerComponent;
    private Animator animatorComponent;
    private PlayerGrabTrigger grabTrigger;
    private Vector3 moveSpeed;
    private float grabCooldown;
    private static readonly int GrabParam = Animator.StringToHash("grab");
    private static readonly int WalkSpeedParam = Animator.StringToHash("walk speed");
    private GameObject fire;
    private PlayerHealth firehp;
    private void Start()
    {
        animatorComponent = GetComponent<Animator>();
        controllerComponent = GetComponent<CharacterController>();
        grabTrigger = GetComponentInChildren<PlayerGrabTrigger>();
        fire = GameObject.FindGameObjectWithTag("Fire");
        firehp = fire.GetComponent<PlayerHealth>();
    }
	
	private void Update()
    {
        if (!firehp.stopped) 
        {
            UpdateWalk();
            if (Button.Pressed) { if(Input.GetMouseButtonDown(0)) Grab(); }
            
            if (Input.GetKeyDown(KeyCode.Space)) Grab();//pc debug 
           
            grabCooldown -= Time.deltaTime;
 
        }
        animatorComponent.SetFloat(WalkSpeedParam, new Vector3(moveSpeed.x, 0, moveSpeed.z).magnitude);
    }

    public void StepAnimationCallback()
    {
        if (StepSound.pitch < 1) StepSound.pitch = Random.Range(1.05f, 1.15f);
        else StepSound.pitch = Random.Range(0.9f, 0.95f);
        StepSound.Play();
    }

    private void UpdateWalk()
    {
        float ySpeed = moveSpeed.y;
        moveSpeed.y = 0;
        float horizontal = LeftJoystick.input.x;
        float vertical = LeftJoystick.input.y;

        Vector3 target = MaxMoveSpeed * new Vector3(horizontal, 0, vertical).normalized;
        moveSpeed = Vector3.MoveTowards(moveSpeed, target, Time.deltaTime * 300);

        if (moveSpeed.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveSpeed),
                Time.deltaTime * 720);
        }
        moveSpeed.y = ySpeed + Physics.gravity.y * Time.deltaTime;
        controllerComponent.Move(moveSpeed * Time.deltaTime);
    }
    private void Grab()
    {
        if (grabCooldown > 0) return;

        if (grabTrigger.GrabbedObject != null)
        {
            grabTrigger.Release();
            return;
        }
        animatorComponent.SetTrigger(GrabParam);

        grabCooldown = .5f;
    }

    public void GrabAnimationCallback()
    {
        grabTrigger.Grab();
    }
}
