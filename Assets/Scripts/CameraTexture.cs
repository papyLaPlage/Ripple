using UnityEngine;
using System.Collections;

public class CameraTexture : MonoBehaviour {

	public static Camera cam;
	public static RenderTexture renderTexture;

	// Use this for initialization
	void Awake () {
		cam = GetComponent<Camera>();
		renderTexture = new RenderTexture(320, 320, 16);
		cam.targetTexture = renderTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public Texture2D GetTexture2D()
	{
		// Remember currently active render texture
		RenderTexture currentActiveRT = RenderTexture.active;

		// Set the supplied RenderTexture as the active one
		RenderTexture.active = renderTexture;

		// Create a new Texture2D and read the RenderTexture image into it
		Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
		tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

		// Restorie previously active render texture
		RenderTexture.active = currentActiveRT;
		return tex;
	}


}
