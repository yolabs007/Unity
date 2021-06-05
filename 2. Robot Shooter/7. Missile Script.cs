using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{

	public GameObject explosion;

	void LaunchMissile(Vector3 tarPos)
	{
		Invoke("SetActive", 0.9f);

		LeanTween.move(gameObject, tarPos, 1.6f).setEase(LeanTweenType.easeInBack).setOnComplete(Explode);

	}

	void SetActive()
	{
		GetComponent<Collider>().enabled = true;
	}

	void Explode()
	{
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider target)
	{
		if (target.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			if (target.tag == "Box")
			{
				target.GetComponent<Rigidbody>().AddExplosionForce(2000f, transform.position, 6f);
			}
			else if (target.tag == "Enemy")
			{
				target.SendMessage("Damage");
			}
			Explode();
		}
	}

} // class
































