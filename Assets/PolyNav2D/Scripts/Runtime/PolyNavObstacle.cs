using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("Navigation/PolyNavObstacle")]
///Place on a game object to act as an obstacle
public class PolyNavObstacle : MonoBehaviour {

	///Inverts the polygon (done automatically if collider already exists due to a sprite)
	public bool invertPolygon = false;

	private Vector3 lastPos;
	private Quaternion lastRot;
	private Vector3 lastScale;
	private Transform _transform;
	private PolygonCollider2D _polyCollider;

	private PolygonCollider2D polyCollider{
		get
		{
			if (_polyCollider == null)
				_polyCollider = GetComponent<PolygonCollider2D>();
			return _polyCollider;
		}
	}

	///The polygon points of the obstacle
	public Vector2[] points{
		get
		{
			Vector2[] tempPoints = polyCollider.points;

			if (invertPolygon)
				System.Array.Reverse(tempPoints);

			return tempPoints;			
		}
	}

	private PolyNav2D polyNav{
		get {return PolyNav2D.current;}
	}

	void Reset(){
		
		if (GetComponent<SpriteRenderer>() != null)
			invertPolygon = true;
	}

	void OnEnable(){

		if (polyNav)
			polyNav.AddObstacle(this);
		
		polyCollider.isTrigger = true;
		lastPos = transform.position;
		lastRot = transform.rotation;
		lastScale = transform.localScale;
		_transform = transform;
	}

	void OnDisable(){

		if (polyNav)
			polyNav.RemoveObstacle(this);
	}

	void Update(){
		
		if (!Application.isPlaying || !polyNav || !polyNav.generateOnUpdate)
			return;

		if (_transform.position != lastPos || _transform.rotation != lastRot || _transform.localScale != lastScale)
			polyNav.GenerateMap();

		lastPos = _transform.position;
		lastRot = _transform.rotation;
		lastScale = _transform.localScale;
	}
}
