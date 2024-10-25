using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

	public Controller controller;
	private GameObject Shipc;
	private Ship Ship;
	public float runSpeed = 40f;
	bool ControllingShip = false;
	public float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	public bool CanMove = true;
	private Turret Turret = null;	
	public bool OnTurret = false;
	private bool OnStation = false;
	private Station Station;
	public float FootstepCooldown;
	private float FtCharger;
	private Animator Animator;
	private GameObject PlayerMng;
	private Vector2 Move;
	private PlayerMng PlayerMngS;
	public GameObject P1;
	public GameObject P2;
	public GameObject P3;
	public GameObject P4;
	public bool actionPress;
	// Update is called once per frame
	private void Awake()
	{
		PlayerMng = GameObject.Find("PlayerManager");
		PlayerMngS = PlayerMng.GetComponent<PlayerMng>();
		PlayerMngS.PlayerCount += 1;
		if (PlayerMngS.PlayerCount == 1)
		{
			P1.SetActive(true);
		}
		if (PlayerMngS.PlayerCount == 2)
		{
			P2.SetActive(true);
		}
		if (PlayerMngS.PlayerCount == 3)
		{
			P3.SetActive(true);
		}
		if (PlayerMngS.PlayerCount == 4)
		{
			P4.SetActive(true);
		}
		transform.position = new Vector3(2.5f, 228f, 0);
		//controls = new Controllers();
		//controls.Controls.Jump.performed += ctx => Jump1();
		//controls.Controls.Action.started += ctx => Action1();
		//controls.Controls.Action.canceled += ctx => ActionOff();
		//controls.Controls.Escape.performed += ctx => Escape1();
		//controls.Controls.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
		//controls.Controls.Move.canceled += ctx => Move = Vector2.zero;
		Shipc = GameObject.Find("ShipOutside");
		Ship = Shipc.GetComponent<Ship>();
	}

	void OnMove(InputValue value)
	{
		Move = value.Get<Vector2>();
	}
	private void Start()
    {
		
		Animator = GetComponentInChildren<Animator>();
		
		controller.HorizontalMove = Move.x;
		controller.VerticalMove = Move.y;
    }
    void Update()
	{
		if (Turret != null)
		{
			Turret.ActionToggle = actionPress;
			Turret.HorizontalMove = Move.x;
		}
		
		controller.HorizontalMove = Move.x;
		controller.VerticalMove = Move.y;
		if (controller.IsClimbing == true)
        {			
				Animator.SetBool("isClimbing", true);						
			if(Move.y < 0.1 )
            {				
				Animator.speed = 0f;
			}
			
			if (Move.y > 0.1)
			{
				if ((FtCharger < Time.time))
				{
                    FindFirstObjectByType<AudioManager>().PlayP("FootStep");
					FtCharger = Time.time + FootstepCooldown;
				}
				Animator.speed = 1f;
			}
        }
		else
        {
			Animator.SetBool("isClimbing", false);
			Animator.speed = 1f;
		}
		if (horizontalMove > 0.2 & controller.IsClimbing != true)
        {
			Animator.SetFloat("Speed", horizontalMove);
		}
		else if (horizontalMove < -0.2 & controller.IsClimbing != true)
        {
			Animator.SetFloat("Speed", horizontalMove * -1);

		}
		else if (controller.IsClimbing != true)
        {
			Animator.SetFloat("Speed", 0);
		}
	
		
		if (CanMove == false)
		{
			horizontalMove = 0;
		}
		if (CanMove == true)
		{
			horizontalMove = Move.x * runSpeed;
			if (Move.x > 0.2| Move.x < -0.2)
			{
				if((FtCharger < Time.time) & controller.m_Grounded)
				{
                    FindFirstObjectByType<AudioManager>().PlayP("FootStep");
					FtCharger = Time.time + FootstepCooldown;
				}
			}
			

		}
	

	}
	void OnJump()
	{
		if(CanMove)
		{
			if (controller.m_Grounded)
			{
                FindFirstObjectByType<AudioManager>().PlayP("Jump");
			}
			jump = true;
		}

		
	}
	void OnAction()
	{
		actionPress = true;
		if (OnTurret == true)
		{
			if (Turret.IsControlled == false)
			{
				Turret.HorizontalMove = Move.x;
				
				
				CanMove = false;
				Turret.IsControlled = true;
				gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().linearVelocity.y);
			}
		}
		if (OnStation == true)
		{
			if (Station.IsControlledStation == false)
			{
				Station.IsControlledStation = true;
				CanMove = false;
				ControllingShip = true;
			}

		}
	}
	void OnActionOff()
	{
		actionPress = false;
	}
	void OnEscape()
	{
		if (Turret != null)
		{
			
			Turret.IsControlled = false;

		}
		if (Station != null)
		{
			Station.IsControlledStation = false;

		}

		ControllingShip = false;


		CanMove = true;
	}
	void FixedUpdate()
	{
		
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;		
		if (ControllingShip == true)
		{			
			Ship.MoveShip(Move.x, Move.y);
		}

		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		

		if (collision.CompareTag("Ladder") & Move.y > 0f & CanMove == true )
		{
			controller.Climb();
		}
		
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.CompareTag("ShipStation"))
		{			
			OnStation = true;
			Station = collision.GetComponent<Station>();
		}
		if (collision.CompareTag("Station"))
		{			
				Turret = collision.GetComponent<Turret>();
				OnTurret = true;
				controller.IsClimbing = false;
				controller.m_Rigidbody2D.gravityScale = 1;

		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Station"))
		{
			OnTurret = false;
			Turret = null;
		}
		if (collision.CompareTag("ShipStation"))
		{
			OnStation = false;
		}
	}
}