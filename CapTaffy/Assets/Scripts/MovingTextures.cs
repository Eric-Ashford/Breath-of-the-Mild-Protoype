using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTextures : MonoBehaviour {

    [SerializeField]
    private float scrollX = 0.5f,
        scrollY = 0.5f;

    Renderer rndr;

	void Start ()
    {
        rndr = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;

        rndr.material.mainTextureOffset = new Vector2(offsetX, offsetY);
	}
}
