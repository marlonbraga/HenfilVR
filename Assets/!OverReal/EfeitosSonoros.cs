using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class EfeitosSonoros:MonoBehaviour{
	
	public AudioSource megafone;
	public AudioSource barata;
	public AudioSource projetor;
	public AudioSource telaDeProjecao;
	public AudioSource cavada;
	public AudioSource freiada;
	
	public float frameMegafoneBegin;//835
	public float frameMegafoneEnd;//1150
	public float frameBarataBegin;//1200
	public float frameBarataEnd;//1532
	public float frameCavadaEnd;//2262
	public float frameFreiadaBegin;//2890
	public float frameFreiadaEnd;//2919

	[Space(25)]
	public long sphericalPlayerCurrentFrame;
	public long sphericalPlayerFrameCount;
	void Start(){
		StartCoroutine(SpatialSFX(megafone, frameMegafoneBegin,frameMegafoneEnd));
		StartCoroutine(SpatialSFX(barata, frameBarataBegin, frameBarataEnd));
		StartCoroutine(SpatialSFX(cavada, frameCavadaEnd));
		StartCoroutine(SpatialSFX(freiada, frameFreiadaBegin, frameFreiadaEnd));
	}
	void Update(){
		sphericalPlayerCurrentFrame = GetComponent<VideoPlayer>().frame;
		sphericalPlayerFrameCount = Convert.ToInt64(GetComponent<VideoPlayer>().frameCount);
	}
	IEnumerator SpatialSFX(AudioSource audiosource, float initialFrame, float finalFrame) {
		while(sphericalPlayerCurrentFrame < initialFrame)
			yield return new WaitForSeconds(0.1f);
		audiosource.Play();
		while(sphericalPlayerCurrentFrame < finalFrame)
			yield return new WaitForSeconds(0.1f);
		audiosource.Stop();
	}
	IEnumerator SpatialSFX(AudioSource audiosource, float finalFrame) {
		while(sphericalPlayerCurrentFrame < finalFrame)
			yield return new WaitForSeconds(0.1f);
		audiosource.Stop();
	}
	public IEnumerator SpatialSFX_ExternalControll(AudioSource audioSource, bool TurnCanvasSFX){
		//Debug.Log(audioSource.name+"= " + TurnCanvasSFX);
		if(TurnCanvasSFX)
			audioSource.Play();
		else
			audioSource.Stop();
		yield return null;
	}
}