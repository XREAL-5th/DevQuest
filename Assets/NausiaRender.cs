using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class NauseaEffectRender : MonoBehaviour
{
    public Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial)
            Graphics.Blit(src, dest, effectMaterial);
        else
            Graphics.Blit(src, dest);
    }
}