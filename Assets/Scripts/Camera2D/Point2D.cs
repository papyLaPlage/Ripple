using UnityEngine;
using System.Collections;

public class Point2D : MonoBehaviour
{
	public EnumCameraPlan enumCameraPlan;
	public Vector2 position = Vector2.zero;
	public Vector2 decalage = Vector2.zero;
	private Vector2 cameraPosition;

	public Point2D()
	{
	}

	public Point2D(float X, float Y)
	{
		position = new Vector2(X, Y);
	}

	public Point2D(float X, float Y, float DX, float DY)
	{
		position = new Vector2(X, Y);
		decalage = new Vector2(DX, DY);
	}

	public Vector2 CameraPosition
	{
		get
		{
			return position + decalage;
		}
	}

	public static Point2D Zero
	{
		get
		{
			return new Point2D();
		}
	}
}
