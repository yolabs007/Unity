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
	private float jumpTimer = 0f;

	private ParticleSystem.EmissionModule right_Muzzle_Emission, left_Muzzle_Emission,
		right_Fire_Emission, left_Fire_Emission;

	private ParticleSystem.MainModule boostMain;

	private Rigidbody myBody;
	private ConstantForce constForce;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		anim.Play("Walk");

		audioManager = GetComponent<AudioSource>();

		myBody = GetComponent<Rigidbody>();
		constForce = myBody.GetComponent<ConstantForce>();

		right_Muzzle_Emission = rightMuzzle.emission;
		left_Muzzle_Emission = leftMuzzle.emission;
	

		//right_Muzzle_Emission.rateOverTime = 0f;
		//left_Muzzle_Emission.rateOverTime = 0f;
		
	}
}
