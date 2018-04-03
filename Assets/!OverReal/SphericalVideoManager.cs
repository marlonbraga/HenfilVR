using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine.VR;
using UnityEngine.Video;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SphericalVideoManager:MonoBehaviour {

	public AudioSource[] audioSource;
	public TextMesh textMesh;

	public float limitAngle = 45f;
	[Range(0, 10)]
	public float speedRotation = 1;
	private Vector3 angulacao;
	private bool mouted = true;
	private int count=0;
    void AutoRotation() {
		float Y = transform.rotation.eulerAngles.y;
		if((!((Y >= 0) && (Y <= limitAngle))) && (!((Y >= 360 - limitAngle) && (Y <= 360)))) {
			if((Y < 360 - limitAngle) && (Y > 180)) {
				transform.eulerAngles = new Vector3(0, -limitAngle, 0);
			} else if((Y > limitAngle) && (Y < 180)) {
				transform.eulerAngles = new Vector3(0, limitAngle, 0);
			}
			speedRotation = -speedRotation;
		}
		angulacao = new Vector3(0f, speedRotation * Time.deltaTime, 0f);
		transform.Rotate(angulacao);
	}
	void Update() {
		AutoRotation();
	}
	//void Awake() {
 //       OVRManager.HMDMounted += Mount;
	//	OVRManager.HMDUnmounted += Unmount;
	//}
	//void Disable() {
	//	OVRManager.HMDMounted -= Mount;
	//	OVRManager.HMDUnmounted -= Unmount;
	//}
	void Unmount() {
		if(mouted)
			textMesh.text += "\n HMD Unmounted";
		mouted = false;
		audioSource[0].Stop();
		audioSource[1].Stop();
		GetComponent<VideoPlayer>().Stop();
	}
	void Mount() {
		if(!mouted)
			textMesh.text += "\n HMD Mounted";
		mouted = true;
		audioSource[0].Play();
		audioSource[1].Play();
		GetComponent<VideoPlayer>().Play();
	}
}
