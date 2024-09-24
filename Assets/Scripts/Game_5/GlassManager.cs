using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    [SerializeField] GameObject glassFolder;

    public void SetGlass()
    {
        Transform leftFolder = glassFolder.transform.Find("Left");
        Transform rightFolder = glassFolder.transform.Find("Right");

        for (int i = 0; i < leftFolder.transform.childCount; i++)
        {
            Glass_script glassScript = leftFolder.transform.GetChild(i).GetComponent<Glass_script>();
            glassScript.ishard = Random.Range(0, 2) == 0;

            Glass_script rightGlassScript = rightFolder.GetChild(i).GetComponent<Glass_script>();
            rightGlassScript.ishard = !glassScript.ishard;
        }
    }
}
