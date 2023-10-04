using UnityEngine;

public class GameManger : MonoBehaviour
{
    static GameManger main;

    [HideInInspector] public PlayerControl player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public CameraControl camc;

    public Transform playerSpawn;
    public GameObject[] dialogs = { };

    //non-lazy, non-DDOL
    private void Awake()
    {
        main = this;
        player = GameObject.FindObjectOfType<PlayerControl>();
        cam = Camera.main;
        camc = GameObject.FindObjectOfType<CameraControl>();
    }

    public bool DialogOpen()
    {
        foreach (GameObject dialog in dialogs)
        {
            if (dialog.activeInHierarchy) return true;
        }
        return false;
    }
}