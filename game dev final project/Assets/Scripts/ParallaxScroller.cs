using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public  Renderer[] layers;
	public float[] speedMultiplier;
	private float previousXPositionPlayer;
	private float previousXPositionCamera;
	public Transform player;
	public Transform mainCamera;
	private float[] offset;

	void  Start()
	{
		offset = new  float[layers.Length];
		for(int i = 0; i<  layers.Length; i++){
			offset[i] = 0.0f;
		}
		previousXPositionPlayer = player.transform.position.x;
		previousXPositionCamera = mainCamera.transform.position.x;

	}

    // Update is called once per frame
    void  Update()
    {
	// if camera has moved
	if (Mathf.Abs(previousXPositionCamera  -  mainCamera.transform.position.x) >  0.001f){
		for(int i =  0; i <  layers.Length; i++){
			if (offset[i] >  1.0f  ||  offset[i] <  -1.0f)
				offset[i] =  0.0f; //reset offset
			float newOffset =  player.transform.position.x  -  previousXPositionPlayer;
			offset[i] =  offset[i] +  newOffset  *  speedMultiplier[i];
			layers[i].material.mainTextureOffset  =  new  Vector2(offset[i], 0);
		}

	}
	//update previous pos
	previousXPositionPlayer  =  player.transform.position.x;
	previousXPositionCamera  =  mainCamera.transform.position.x;
	}

    
}
