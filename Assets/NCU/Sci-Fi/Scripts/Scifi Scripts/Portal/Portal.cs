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
    public ParticleSystem PortalParticleEffect;
    bool falleninPortal;
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
        PortalParticleEffect = transform.GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        PortalSprite.transform.Rotate(0, 0, 60*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && falleninPortal==false)
        {
            falleninPortal = true;
            PlayerInPortal(other.gameObject.transform, true);
        }
    }
    IEnumerator MoveTOPortal(Transform Player,Vector3 Pos, float Duration)
    {
        PortalParticleEffect.Play();
        float t = 0;
        while (t < Duration)
        {
            Player.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        Player.position = PortalCamera.transform.position;
        PlayerInPortal(Player, false);
        falleninPortal = false;
    }
    void PlayerInPortal(Transform Player,bool Status)
    {
        if(Status==true)
        {
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<Collider>().isTrigger = true;
            Player.GetComponent<Player>().enabled = false;
            StartCoroutine(MoveTOPortal(Player, transform.position - new Vector3(0, 10, 0), 1f));
        }
        else
        {
            Player.GetComponent<Rigidbody>().isKinematic = false;
            Player.GetComponent<Collider>().isTrigger = false;
            Player.GetComponent<Player>().enabled = true;

        }
    }
}
