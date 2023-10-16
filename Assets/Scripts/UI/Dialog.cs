using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialog : MonoBehaviour
{
    public virtual void Build()
    {
        if (GameScene._game != null) GameScene._game.Pause();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        if (GameScene._game != null && GameScene._game.DialogOpen())
            GameScene._game.Resume();
    }
}
