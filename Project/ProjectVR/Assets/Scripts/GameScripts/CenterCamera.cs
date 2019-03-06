using UnityEngine;
using UnityEngine.XR;


public class CenterCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            InputTracking.Recenter();
        }

    }
}
