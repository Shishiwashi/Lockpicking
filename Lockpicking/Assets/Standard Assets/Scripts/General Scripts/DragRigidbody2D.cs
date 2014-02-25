using UnityEngine;
using System.Collections;

public class DragRigidbody2D : MonoBehaviour
{
	public float maxDistance = 2;
	public float distance = 0.2f;
	public float damper = 1.0f;
	public float drag = 30.0f; 
	public float angularDrag = 30.0f;
	public LayerMask layerMask;

	private SpringJoint2D springJoint;
	private Camera mainCamera;
	
	void Start()
	{
		mainCamera = FindCamera ();
	}
	
	void Update()
	{
		if (!Input.GetMouseButtonDown (0))
		{
			rigidbody2D.velocity = Vector2.zero;
			return;
		}

		RaycastHit2D hit = Physics2D.Raycast (mainCamera.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);
		if(hit.transform == transform)
		{
			if (!springJoint)
			{
				GameObject go = new GameObject ("Rigidbody2D Dragger");
				Rigidbody2D body = go.AddComponent ("Rigidbody2D") as Rigidbody2D;
				springJoint = go.AddComponent ("SpringJoint2D") as SpringJoint2D;
				body.isKinematic = true;
				body.mass=0.0001f;
			}
			
			springJoint.transform.position = hit.point;
			springJoint.distance = distance; // there is no distance in SpringJoint2D
			springJoint.dampingRatio = damper;// there is no damper in SpringJoint2D but there is a dampingRatio
			springJoint.connectedBody = hit.rigidbody;
			// maybe check if the 'fraction' is normalised. See http://docs.unity3d.com/Documentation/ScriptReference/RaycastHit2D-fraction.html
			StartCoroutine ("DragObject", hit.fraction);
		}
	}

	IEnumerator DragObject (float distance)
	{	
		float oldDrag = springJoint.connectedBody.drag;
		float oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		Camera mainCamera = FindCamera ();

		while (Input.GetMouseButton (0))
		{
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			springJoint.transform.position = ray.GetPoint (distance);
			if(Vector2.Distance(transform.position, ray.GetPoint(distance)) > maxDistance)
			{
				Break();
			}
			yield return null;
		}

		Break();
	}

	void Break()
	{
		if (springJoint.connectedBody)
		{
			Vector2 power = springJoint.connectedBody.rigidbody2D.velocity;
			power = power/(springJoint.connectedBody.rigidbody2D.mass+1);
			//Debug.Log(power.ToString());
			springJoint.connectedBody.drag = 0.0f;//oldDrag;
			springJoint.connectedBody.angularDrag = 0.05f;//oldAngularDrag;
			springJoint.connectedBody = null;
		}
	}

	Camera FindCamera ()
	{	
		if (camera) return camera;
		else return Camera.main;	
	}
}