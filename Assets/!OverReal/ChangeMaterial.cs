using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {
	[Range(0,30)]
	public float slideSpeed;
	[Range(0, 2)]
	public float fadeTime = 0.5f;

	public Material blackPanel;

	public Material[] photo;
	void Start (){
		blackPanel.color = new Color(0, 0, 0, 0);
		StartCoroutine(SphericalSlider());
	}
	IEnumerator SphericalSlider(){
		int i = 0;
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		while(true){
			yield return new WaitForSeconds(slideSpeed);
			if(i+1 < photo.Length)
				i++;
			 else
				i = 0;
			StartCoroutine(ChangeShericalPhoto(meshRenderer, i));
        }
	}
	IEnumerator ChangeShericalPhoto(MeshRenderer meshRenderer, int i) {
		int r=0; int g=0; int b=0;
		
		float limit = 100;
		float alpha = 0;
		float count =1;
		while(count < limit){
			alpha = (count / limit);
			yield return new WaitForSeconds(fadeTime/limit);
			Color color = new Color(r,g,b,alpha);
			blackPanel.color = color;

			count++;
		}
		
        meshRenderer.material = photo[i];
		yield return new WaitForSeconds(fadeTime);
		
		limit = 100;
		alpha = 1;
		count = 100;
		while(count > 0) {
			alpha = count / limit;
			yield return new WaitForSeconds(fadeTime / limit);
			Color color = new Color(r, g, b, alpha);
			blackPanel.color = color;

			count--;
		}
	}
}