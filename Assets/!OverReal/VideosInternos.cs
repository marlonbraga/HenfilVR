using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideosInternos:MonoBehaviour{
	public GameObject telaDeProjecao;
	public GameObject tv;
	public float frameAparecerTela;
	public float frameAparecerTV;
	public Animator animator;
	[Space(20)]
	public long sphericalPlayerCurrentFrame;
	public long sphericalPlayerFrameCount;
	private EfeitosSonoros efeitosSonoros;
	void Start(){
		StartCoroutine(AnimacaoTela());
		StartCoroutine(AnimacaoTV());
		StartCoroutine(FinishExperience());
		efeitosSonoros = GetComponent<EfeitosSonoros>();
	}
	void Update(){
		sphericalPlayerCurrentFrame = GetComponent<VideoPlayer>().frame;
		sphericalPlayerFrameCount = Convert.ToInt64(GetComponent<VideoPlayer>().frameCount);
	}
	IEnumerator AnimacaoTela(){
		while(sphericalPlayerCurrentFrame < frameAparecerTela)
			yield return new WaitForSeconds(0.1f);
		telaDeProjecao.SetActive(true);
		while(sphericalPlayerCurrentFrame < frameAparecerTela+79)
			yield return new WaitForSeconds(0.1f);
		StartCoroutine(efeitosSonoros.SpatialSFX_ExternalControll(efeitosSonoros.projetor, true));
		telaDeProjecao.GetComponentInChildren<VideoPlayer>().Play();
		GetComponent<VideoPlayer>().Pause();
		while(!CheckFinishVideo(telaDeProjecao.GetComponentInChildren<VideoPlayer>()))
			yield return new WaitForSeconds(0.1f);
		GetComponent<VideoPlayer>().Play();
		yield return new WaitForSeconds(0.1f);
		animator.Play("cair");
		StartCoroutine(efeitosSonoros.SpatialSFX_ExternalControll(efeitosSonoros.projetor,false));
		StartCoroutine(efeitosSonoros.SpatialSFX_ExternalControll(efeitosSonoros.telaDeProjecao, true));
		StartCoroutine(efeitosSonoros.SpatialSFX_ExternalControll(efeitosSonoros.cavada,true));
		while(sphericalPlayerCurrentFrame < frameAparecerTela + 300)
			yield return new WaitForSeconds(0.1f);
		telaDeProjecao.SetActive(false);
	}
	IEnumerator AnimacaoTV(){
		while(sphericalPlayerCurrentFrame < frameAparecerTV)
			yield return new WaitForSeconds(0.1f);
		tv.SetActive(true);
		tv.GetComponentInChildren<VideoPlayer>().Play();
		while(sphericalPlayerCurrentFrame < frameAparecerTV + 97)
			yield return new WaitForSeconds(0.1f);
		GetComponent<VideoPlayer>().Pause();
		while(!CheckFinishVideo(tv.GetComponentInChildren<VideoPlayer>()))
			yield return new WaitForSeconds(0.1f);
		tv.GetComponentInChildren<VideoPlayer>().Play();
		tv.GetComponentInChildren<VideoPlayer>().gameObject.GetComponent<AudioSource>().mute = true;
		GetComponent<VideoPlayer>().Play();
		while(sphericalPlayerCurrentFrame < frameAparecerTV + 250)
			yield return new WaitForSeconds(0.1f);
		tv.SetActive(false);
	}
	IEnumerator FinishExperience() {
		while(!CheckFinishVideo(GetComponent<VideoPlayer>()))
			yield return new WaitForSeconds(0.1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	//checkOver function will use current frame and total frames of video player video
	//to determine if the video is over.
	private bool CheckFinishVideo(VideoPlayer videoPlayer) {
		long playerCurrentFrame = videoPlayer.frame;
		long playerFrameCount = Convert.ToInt64(videoPlayer.frameCount);
		if(playerFrameCount == 0) {
			return false;
		} else {
			if(playerCurrentFrame < playerFrameCount) {
				return false;
			} else {
				return true;
			}
		}
	}
}