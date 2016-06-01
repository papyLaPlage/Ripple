using UnityEngine;
using System.Collections;

public class Camera2DLogic : MonoBehaviour
{
	static int IDs = -1;

	[SerializeField]
	protected int iD;

	public bool startFollow = false;
	public EnumCameraPlan plan = EnumCameraPlan.XY;
	public Vector2 decalage = Vector2.zero;

	protected Transform _transform;

	public int ID
	{
		get
		{
			return iD;
		}
	}

	virtual protected void Awake()
	{
		_transform = transform;
		iD = ++IDs;
	}

	virtual protected void Start()
	{
		if (startFollow)
		{
			Camera2D camera2D = FindObjectOfType<Camera2D>();
			if (camera2D != null)
				camera2D.SetTarget(transform);
		}
	}

	virtual public void UpdatePoint(ref Point2D point2D)
	{
		switch (plan)
		{
			case EnumCameraPlan.XY:
			point2D.position = _transform.position;
			break;
			case EnumCameraPlan.X:
			point2D.position.x = _transform.position.x;
			break;
			case EnumCameraPlan.Y:
			point2D.position.y = _transform.position.y;
			break;
		}
	}
}
