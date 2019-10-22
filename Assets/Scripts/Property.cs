using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Property 
{

    //씬이 달라져도 유지되는 것들
    public static bool tutorialClear = false;
    public static bool stage1Clear = false;
    public static bool stage2Clear = false;
    public static bool stage3Clear = false;

    public static float tissue = 0;
    public static float money = 0;

    public static float bgmvolume = 1;
    public static float fxvolume = 1;

    //public GameObject property;

    //void Start()
    //{
    //    DontDestroyOnLoad(property);
    //}

}
