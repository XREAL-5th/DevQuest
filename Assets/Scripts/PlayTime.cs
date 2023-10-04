using UnityEngine;

public class PlayTime : MonoBehaviour
{
    float _timer;
    bool _hasRecord = false;
    float _fastestPlayTime;
    bool _playTimer = true;
    private float _timeLimit = 30.0f;

    public float Timer { get { return _timer; } }

    void Awake()
    {
        _fastestPlayTime = PlayerPrefs.GetFloat("FastestPlayTime", 0);
    }

    public void Stop(bool clear)
    {
        _playTimer = false;
        if (clear)
        {
            if (_fastestPlayTime > _timer || !_hasRecord)
            {
                Save();
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("FastestPlayTime", _timer);
    }

    public bool timeover()
    {
        return _timer >= _timeLimit - 0.0001;
    }

    void Update()
    {
        if (_playTimer) _timer += Time.deltaTime;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        Rect rect = new Rect(50, 10, w, h * 2 / 50);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(255f, 255f, 255f, 1f);

        string text = "Make Fisrt Record!";
        if (_fastestPlayTime > 0)
        {
            text = string.Format("Best Record: {0:N} s", _fastestPlayTime);
        }
        text += string.Format("\n{0:N}", _timeLimit - _timer);
        GUI.Label(rect, text, style);
    }
}
