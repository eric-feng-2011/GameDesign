using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScaling;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        previousCamPos = cam.position;
        parallaxScaling = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScaling[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScaling[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, 
                        backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, 
                        backgroundTargetPos, smoothing * Time.deltaTime);
            previousCamPos = cam.position;
        }
	}
}
