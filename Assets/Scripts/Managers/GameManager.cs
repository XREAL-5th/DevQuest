using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public PlayerControl player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public CameraControl camc;

    public static GameManager _main;

    public Transform playerSpawn;

    //non-lazy, non-DDOL
    private void Awake()
    {
        _main = this;
        player = GameObject.FindObjectOfType<PlayerControl>();
        cam = Camera.main;
        camc = GameObject.FindObjectOfType<CameraControl>();
    }
}