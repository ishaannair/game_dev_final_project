using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public  Transform player;
    public  Transform boss; // Mario's Transform
    public  Transform endLimit; // GameObject that indicates end of map
    private  float offsetX; // initial x-offset between camera and Mario
    private  float offsetY; // initial x-offset between camera and Mario
    private  float startX; // smallest x-coordinate of the Camera
    private  float endX; // largest x-coordinate of the camera
    private  float viewportHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft =  Camera.main.ViewportToWorldPoint(new  Vector3(0, 0, 0));
        viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x);

        offsetX  =  this.transform.position.x  -  player.position.x;
        offsetY  =  this.transform.position.y  -  player.position.y;
        startX  =  this.transform.position.x;
        endX  =  endLimit.transform.position.x  -  viewportHalfWidth;

    }

    // Update is called once per frame
    void Update()
    {
        float desiredX =  player.position.x  +  offsetX;
        float desiredY =  player.position.y  +  offsetY;
        // check if desiredX is within startX and endX
        if (desiredX  >  startX  &&  desiredX  <  endX){
            this.transform.position  =  new  Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
        //this.transform.position  =  new  Vector3(this.transform.position.x, (player.position.y+2*boss.position.y)/2+7, this.transform.position.z);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    public void Shake()
    {
        StartCoroutine(ShakingCamera());
    }

    private IEnumerator ShakingCamera(float magnitude = 0.25f)
    {
        Vector3 originalPos = transform.position;
        float t = 0f, x, y;
        while (t < 5f)
        {
            x = Random.Range(-1f, 1f) * magnitude;
            y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPos + new Vector3(x, y, 0);

            t += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}
