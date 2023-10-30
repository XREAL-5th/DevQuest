using UnityEngine;
using System.Collections.Generic;

public class GameScene : BaseScene
{
    public static GameScene _game;

    [HideInInspector] public PlayerControl player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public CameraControl camc;
    [HideInInspector] public PlayTime timer;

    public Transform playerSpawn;
    public GameObject quitMenu;
    
    private bool _cleared = false;
    private bool _paused = false;

    public bool Paused { get; }

    private Dictionary<string, float> _targets = null;

    public Dictionary<string, float> Targets { get { return _targets; } }

    public void AddTarget(string target) { _targets.Add(target, timer.Timer); }

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        _game = this;
        player = GameObject.FindObjectOfType<PlayerControl>();
        cam = Camera.main;
        camc = GameObject.FindObjectOfType<CameraControl>();
        timer = GameObject.FindObjectOfType<PlayTime>();
    }

    void EndLevel()
    {
        timer.Stop(_cleared);
        //Debug.Log(Targets.ToString());
        Managers.Scene.LoadScene(Define.Scene.End);
    }

    private void Update()
    {
        if (player.clearedGame())
        {
            _cleared = true;
            EndLevel();
        }
        else if (timer.timeover())
        {
            _cleared = false;
            EndLevel();
        }
    }


    public override void Clear()
    {
        Debug.Log("Game Scene Clear!");
    }

    public void Pause(){
        quitMenu.SetActive(true);
        Time.timeScale = 0f;
        _paused = true;
    }

    public void Resume()
    {
        quitMenu.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool DialogOpen()
    {
        return quitMenu.activeSelf;
    }
}
