using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public RenderTexture PortalTexture;
    public Camera PortalCamera;
    public MeshRenderer PortalMesh;
    public Material PortalMaterial;
    private SpriteRenderer PortalSprite;
    void Start()
    {
        PortalCamera = transform.GetComponentInChildren<Camera>();
        PortalTexture = new RenderTexture(512, 512, 32);
        PortalCamera.targetTexture = PortalTexture;
        PortalMesh = transform.GetComponentInChildren<MeshRenderer>();
        PortalMaterial = new Material(Shader.Find("Standard"));
        PortalMaterial.SetTexture(Shader.PropertyToID("_MainTex"), PortalTexture);
        PortalMesh.material = PortalMaterial;
        PortalSprite = transform.GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        PortalSprite.transform.Rotate(0, 0, 60*Time.deltaTime);
    }
}
