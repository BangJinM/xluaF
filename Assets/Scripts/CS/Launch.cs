using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using PureMVC.Patterns.Facade;
public class Launch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Facade facade = new Facade("myApp");
        XluaManager.Instance.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
