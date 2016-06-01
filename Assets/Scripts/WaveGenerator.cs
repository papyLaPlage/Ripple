using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour
{

	private Texture2D sourceimage;
	private Texture2D targettexture;

	private int size;
	private int hwidth;
	private int hheight;
	public int riprad = 5;

	private int[] ripplemap;
	private int data;

	private Color[] ripple;
	private Color[] texture;
	private int[] pixels;

	private int oldind;
	private int newind;
	private int mapind;

	private int i;
	private int a;
	private int b;

	private int width;
	private int height;

	public int disturbsize = 128;

	[SerializeField]
	private float raycastDistance;
	[SerializeField]
	private LayerMask raycastMask;

	// init 
	void Awake()
	{
		//sourceimage = CameraTexture.GetTexture2D();
		width = 320;
		height = 320;

		targettexture = new Texture2D(width, height);
		GetComponent<Renderer>().material.mainTexture = targettexture;
		GetComponent<Renderer>().material.SetTexture("_ParallaxMap", targettexture);

		hwidth = width >> 1;
		hheight = height >> 1;

		size = width * (height + 2) * 2;

		ripplemap = new int[size];
		ripple = new Color[width * height];
		texture = new Color[width * height];
		pixels = new int[width * height];

		oldind = width;
		newind = width * (height + 3);


		/* int counter = 0;
		 for (int y = 0; y < height; y++) 
		 {
			 for (int x = 0; x < width; x++) 
			 {
				 texture[counter] = sourceimage.GetPixel(x, y);
				 counter++;
			 }
		 }*/
	}

	void Update()
	{
		//  image(img, 0, 0); //Displays images to the screen
		//  loadPixels(); // Loads the pixel data for the display window into the pixels[] array
		//  texture = pixels;

		sourceimage = CameraTexture.GetTexture2D();
		//texture = new Color[width * height];
		int counter = 0;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				texture[counter] = sourceimage.GetPixel(x, y);
				counter++;
			}
		}

		Newframe();
		int px = 0;
		int py = 0;



		for (int i = 0; i < pixels.Length; i++)
		{
			//	todo: use Texture2D.SetPixels instead..
			targettexture.SetPixel(px, py, ripple[i]);
			px++;
			if (px >= width)
			{
				px = 0;
				py++;
			}
		}

		//updatePixels(); //Updates the display window with the data in the pixels[] array
		targettexture.Apply();

		// left mouse button is pressed down
		if (Input.GetMouseButton(0))
		{
			// raycast to mousecursor location
			/* 
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastDistance, raycastMask.value))
				return;
			*/

			// get real coordinates
			//Vector2 pixelUV = hit.textureCoord;
			Vector3 pixelUV = Camera.main.WorldToViewportPoint(Input.mousePosition) * 10;
			pixelUV.x -= 5;
			pixelUV.y -= 5;

			// then apply waves on that position
			Disturb((int)pixelUV.x, (int)pixelUV.y);
		}

	}

	// ripples 
	void Disturb(int dx, int dy)
	{
		for (int j = dy - riprad; j < dy + riprad; j++)
		{
			for (int k = dx - riprad; k < dx + riprad; k++)
			{
				if (j >= 0 && j < height && k >= 0 && k < width)
				{
					ripplemap[oldind + (j * width) + k] += disturbsize;
				}
			}
		}
	}


	// processing
	void Newframe()
	{
		//Toggle maps each frame
		i = oldind;
		oldind = newind;
		newind = i;

		i = 0;
		mapind = oldind;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				data = (ripplemap[mapind - width] + ripplemap[mapind + width] + ripplemap[mapind - 1] + ripplemap[mapind + 1]) >> 1;
				data -= ripplemap[newind + i];
				data -= data >> 5;
				ripplemap[newind + i] = data;

				//where data=0 then still, where data>0 then wave
				data = (1024 - data);

				//offsets
				a = ((x - hwidth) * data / 1024) + hwidth;
				b = ((y - hheight) * data / 1024) + hheight;

				//bounds check
				if (a >= width)
					a = width - 1;
				if (a < 0)
					a = 0;
				if (b >= height)
					b = height - 1;
				if (b < 0)
					b = 0;

				ripple[i] = texture[a + (b * width)];
				mapind++;
				i++;
			}
		}
	}
}
