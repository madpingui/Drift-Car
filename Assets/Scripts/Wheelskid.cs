using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelskid : MonoBehaviour {

    [SerializeField] float intensityModifier = 1.5f;

    Skidmarks skidMarkController;
    PlayerCar playerCar;
    ParticleSystem particleSystem;

    int lastSkidId = -1;

	void Start () {
        skidMarkController = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
        particleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        float intensity = playerCar.SideSlipAmount;
        if(intensity < 0)
        {
            intensity = -intensity;
        }

        if(intensity> 0.1f)
        {
            lastSkidId = skidMarkController.AddSkidMark(transform.position, transform.up, intensity * intensityModifier, lastSkidId);
            if(particleSystem != null && !particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            lastSkidId = -1;
            if (particleSystem != null && particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
	}
}
