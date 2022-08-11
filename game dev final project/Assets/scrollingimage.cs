using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingimage : MonoBehaviour
{
    public float speed = 0.5f;
    Renderer m_ObjectRenderer;
    // Use this for initialization
    void Start(){
        m_ObjectRenderer = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update(){
        Vector2 offset=new Vector2(Time.time*speed,0);
        m_ObjectRenderer.material.mainTextureOffset=offset;
    }
}
