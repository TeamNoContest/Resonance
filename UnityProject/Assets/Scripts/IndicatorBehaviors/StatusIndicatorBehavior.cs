using UnityEngine;
using System.Collections;

public class StatusIndicatorBehavior : MonoBehaviour
{

    public Material matRed;
    public Material matNone;
    public Material matBlue;
    public Material matYellow;
    public Material matPurple;
    public Material matWhite;

    public bool gotMessage = false;

    // Use this for initialization
    void Start()
    {
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    //And this is all of those useful utility/modifier methods.
    public void SetMaterialRed()
    {
        renderer.material = matRed;
    }
	
    public void SetMaterialNone()
    {
        renderer.material = matNone;
    }

    public void SetMaterialBlue()
    {
        renderer.material = matBlue;
    }
	
    public void SetMaterialYellow()
    {
        renderer.material = matYellow;
    }

    public void SetMaterialPurple()
    {
        renderer.material = matPurple;
    }
	
    public void SetMaterialWhite()
    {
        renderer.material = matWhite;
    }

    public void getMessage()
    {
        gotMessage = true;
    }
}
