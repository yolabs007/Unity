using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
	LEFT,
	RIGHT
}

public class PlayerScript : MonoBehaviour
{

	private Animator anim;
	private AudioSource audioManager;
	private Direction dir = Direction.RIGHT;

	public GameObject missile;
	public ParticleSystem rightMuzzle, leftMuzzle, rightFire, leftFire, boost;

	public Transform leftArm, rightArm;
	public Transform misslePoint;

	public Light leftLight, rightLight;

	public float speed = 4f;

	private ParticleSystem.EmissionModule right_Muzzle_Emission, left_Muzzle_Emission,
		right_Fire_Emission, left_Fire_Emission;

	private ParticleSystem.MainModule boostMain;

	private Rigidbody myBody;
	private ConstantForce constForce;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();

		audioManager = GetComponent<AudioSource>();

		myBody = GetComponent<Rigidbody>();
		constForce = myBody.GetComponent<ConstantForce>();

		right_Muzzle_Emission = rightMuzzle.emission;
		left_Muzzle_Emission = leftMuzzle.emission;
		right_Fire_Emission = rightFire.emission;
		left_Fire_Emission = leftFire.emission;

		right_Muzzle_Emission.rateOverTime = 0f;
		left_Muzzle_Emission.rateOverTime = 0f;
		right_Fire_Emission.rateOverTime = 0f;
		left_Fire_Emission.rateOverTime = 0f;

		boostMain = boost.main;

	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			if (!LeanTween.isTweening(gameObject))
			{

				if (isGrounded())
					anim.Play("Walk");
				else
					anim.Play("Idle");

				if (dir != Direction.LEFT)
				{
					LeanTween.rotateAroundLocal(gameObject, Vector3.up, 180f, 0.3f).setOnComplete(TurnLeft);
				}
				else
				{
					transform.Translate(Vector3.forward * speed * Time.deltaTime);
				}
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			if (!LeanTween.isTweening(gameObject))
			{

				if (isGrounded())
					anim.Play("Walk");
				else
					anim.Play("Idle");

				if (dir != Direction.RIGHT)
				{
					LeanTween.rotateAroundLocal(gameObject, Vector3.up, -180f, 0.3f).setOnComplete(TurnRight);
				}
				else
				{
					transform.Translate(Vector3.forward * speed * Time.deltaTime);
				}
			}
		}
		else
		{
			anim.Play("Idle");
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			rightArm.Rotate(Vector3.back * 200f * Time.deltaTime);
			leftArm.Rotate(Vector3.back * 200f * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			rightArm.Rotate(Vector3.forward * 200f * Time.deltaTime);
			leftArm.Rotate(Vector3.forward * 200f * Time.deltaTime);

		}

		if (Input.GetKey(KeyCode.Z))
		{
			constForce.force = Vector3.zero;

			if (myBody.velocity.y < 4f)
				myBody.AddRelativeForce(Vector3.up * 20f);

			if (!boostMain.loop)
			{
				boost.Play();
				boostMain.loop = true;
			}
		}
		else
		{
			constForce.force = new Vector3(0f, -10f, 0f);
			boostMain.loop = false;
		}

	}

	bool isGrounded()
	{
		return Physics.Raycast(transform.position + transform.forward * 0.4f +
			transform.up * 0.1f, Vector3.down, 0.1f);
	}

	void TurnLeft()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		dir = Direction.LEFT;
	}

	void TurnRight()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		dir = Direction.RIGHT;
	}


}
