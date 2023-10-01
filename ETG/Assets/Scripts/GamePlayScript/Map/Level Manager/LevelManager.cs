using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }
    public List<LevelData> levelDatas;
    // Start is called before the first frame update
    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
