
using UnityEngine;
using System.Collections;

public class WalkOnSphere : MonoBehaviour 
{
	#region vars
	public float rotSpeed = 50;
	public float moveSpeed;
	public float rotDamp;
	public float moveDamp;
	public float height;
	public float jumpHeight;
	[Range(.1f, 10f)]
	public float gravity;
	public float radius;

	public Transform planet;
	protected Transform trans;
	protected Transform parent;

	protected float angle = 90f;
	protected float curJumpHeight = 0;
	protected float jumpTimer;
	protected bool jumping;



	protected Vector3 direction;
	protected Quaternion rotation = Quaternion.identity;

	#endregion

	#region Unity methods
	void Start () 
	{	
		trans = transform;
		parent = transform.parent;
	}

	void Update () 
	{

		//parent.position = planet.position; // If you want to have a moving planet

		direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

		if(Input.GetKey(KeyCode.LeftShift))
			Position(Input.GetAxis("Horizontal") * -moveSpeed, 0);
		else
			Rotation(Input.GetAxis("Horizontal") * -rotSpeed);

		if(Input.GetButtonDown("Jump") && !jumping)
		{
			jumping = true;
			jumpTimer = Time.time;
		}

		if(jumping)
		{
			curJumpHeight = Mathf.Sin((Time.time - jumpTimer) * gravity) * jumpHeight; 
			if(curJumpHeight <= -.01f)
			{
				curJumpHeight = 0;
				jumping = false;
			}
		}

		Position (0, Input.GetAxis("Vertical") * moveSpeed);
		Movement();
	}
	#endregion

	#region Actions
	protected void Rotation(float amt)
	{
		angle += amt * Mathf.Deg2Rad * Time.fixedDeltaTime;
	}

	protected void Position(float x, float y)
	{
		Vector2 perpendicular = new Vector2(-direction.y, direction.x);
		Quaternion vRot = Quaternion.AngleAxis(y, perpendicular);
		Quaternion hRot = Quaternion.AngleAxis(x, direction);
		rotation *= hRot * vRot;
	}

	protected void Movement()
	{
		trans.localPosition = Vector3.Lerp(trans.localPosition, rotation * Vector3.forward * GetHeight(), Time.fixedDeltaTime * moveDamp);
		trans.rotation = Quaternion.Lerp(trans.rotation, rotation * Quaternion.LookRotation(direction, Vector3.forward), Time.fixedDeltaTime * rotDamp);
	}

	protected float GetHeight()
	{
		Ray ray = new Ray(trans.position, planet.position - trans.position);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit))
			radius = Vector3.Distance(planet.position, hit.point) + height + curJumpHeight;

		return radius;
	}

	#endregion
}