using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndScene : BaseScene
{

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.End;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Managers.Scene.LoadScene(Define.Scene.Game);
        if (Input.GetKeyDown(KeyCode.E))
            QuitGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public override void Clear()
    {
        Debug.Log("End Scene Clear!");
    }
}
