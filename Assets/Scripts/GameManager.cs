using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string rooms;
    
    // Start is called before the first frame update

    private int[,] floorPlan;
    
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
                
    }
}
