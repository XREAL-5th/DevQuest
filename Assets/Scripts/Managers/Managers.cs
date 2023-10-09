using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { init(); return s_instance; } }

    // Contents
    //GameManagerEx _game = new GameManagerEx();

    //public static GameManagerEx Game { get { return Instance._game; } }

    // Core
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene;  } }



    void Start()
    {
        init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
        }    
    }
}
