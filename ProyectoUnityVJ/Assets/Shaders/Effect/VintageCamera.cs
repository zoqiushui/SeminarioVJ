using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class VintageCamera : MonoBehaviour {

	public Material postprocessMaterial;
	public Color colorSet;
	private float _offset;
	public float speed;
	public Camera cam2;
	
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{        

		postprocessMaterial.SetTexture("_MainTex",src);
		postprocessMaterial.SetVector("_Color",colorSet);
		Graphics.Blit(src, dest, postprocessMaterial);
	}
	void Update() {
		_offset = Time.time * speed;
		postprocessMaterial.SetTextureOffset("_NoiseTex", new Vector2(_offset, 0));
	}

}
