using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MinimapShader : MonoBehaviour
{

    public Material postprocessMaterial;
    public Color colorSet;

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        postprocessMaterial.SetTexture("_MainTex", src);
        //postprocessMaterial.SetVector("_Color", colorSet);
        //BLIT: Agarra la textura source, la procesa en el material
        //y la agrega al destination. 
        //http://docs.unity3d.com/ScriptReference/Graphics.Blit.html
        Graphics.Blit(src, dest, postprocessMaterial);
    }
}