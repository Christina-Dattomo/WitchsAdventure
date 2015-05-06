#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class UIPreview : EditorWindow
{
    Vector2 scrollPosition = Vector2.zero;
    public int scrollY = 600;

    void OnGUI()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(10, 10, 785,585), scrollPosition, new Rect(0, 0, 8000, 6000));
        GeneratePreview(PlayerPrefs.GetInt("tempSlots"), PlayerPrefs.GetInt("tempRawLength"), PlayerPrefs.GetFloat("sizex"), PlayerPrefs.GetFloat("sizey"));
        GUI.EndScrollView();
       
    }
 
    public void GeneratePreview(int tempSlots,int tempRowLength,float slotSizeX,float slotSizeY)
    {
        for (int r = 0; r < tempSlots / tempRowLength + 1; r++)
        {
            for (int s = 0; s < tempRowLength; s++)
            {
                int thisIndex = r * tempRowLength + s;
                if (thisIndex < tempSlots)
                {
                    if (GUI.Button(new Rect(s * slotSizeX,r * slotSizeY,slotSizeX,slotSizeY),""))
                    {
                    }
                }
            }
        }
    }

}
#endif