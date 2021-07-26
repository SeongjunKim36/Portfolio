using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    
     public bool showMain = true;
     public bool showSub = false;
 
     public int gridSizeX;
     public int gridSizeY;
     public int gridSizeZ;
 
     public float smallStep;
     public float largeStep;
 
     public float startX;
     public float startY;
     public float startZ;
 
     private Material lineMaterial;
 
     public  Color mainColor = new Color(0f, 1f, 0f, 1f);
     public Color subColor = new Color(0f, 0.5f, 0f, 1f);
 
     private void Start() {
         OnPostRender();
     }
     
     void CreateLineMaterial()
     {
         if (!lineMaterial)
         {
             // 유니티 빌트인 쉐이더
             var shader = Shader.Find("Hidden/Internal-Colored");
             lineMaterial = new Material(shader);
             lineMaterial.hideFlags = HideFlags.HideAndDontSave;
             // 알파블랜딩 켜기
             lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
             lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
             // backface culling 끄기
             lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
             // depth writes 끄기
             lineMaterial.SetInt("_ZWrite", 0);
         }
     }
     
 
     void OnPostRender()
     {
         CreateLineMaterial();
         // 메테리얼 셋팅
         lineMaterial.SetPass(0);
 
         GL.Begin(GL.LINES);
 
         if (showSub)
         {
             GL.Color(subColor);
 
             // 레이어
             for (float j = 0; j <= gridSizeY; j += smallStep)
             {
                 // X 축 라인
                 for (float i = 0; i <= gridSizeZ; i += smallStep)
                 {
                     GL.Vertex3(startX, startY + j , startZ + i);
                     GL.Vertex3(startX + gridSizeX, startY + j , startZ + i);
                 }
 
                 // Z 축 라인
                 for (float i = 0; i <= gridSizeX; i += smallStep)
                 {
                     GL.Vertex3(startX + i, startY + j , startZ);
                     GL.Vertex3(startX + i, startY + j , startZ + gridSizeZ);
                 }
             }
 
             // Y 축 라인
             for (float i = 0; i <= gridSizeZ; i += smallStep)
             {
                 for (float k = 0; k <= gridSizeX; k += smallStep)
                 {
                     GL.Vertex3(startX + k, startY , startZ + i);
                     GL.Vertex3(startX + k, startY + gridSizeY , startZ + i);
                 }
             }
         }
 
         if (showMain)
         {
             GL.Color(mainColor);
 
             // 레이어
             for (float j = 0; j <= gridSizeY; j += largeStep)
             {
                 // X 축 라인
                 for (float i = 0; i <= gridSizeZ; i += largeStep)
                 {
                     GL.Vertex3(startX, startY + j, startZ + i);
                     GL.Vertex3(startX + gridSizeX, startY + j , startZ + i);
                 }
 
                 //Z 축 라인
                 for (float i = 0; i <= gridSizeX; i += largeStep)
                 {
                     GL.Vertex3(startX + i, startY + j , startZ);
                     GL.Vertex3(startX + i, startY + j , startZ + gridSizeZ);
                 }
             }
 
             //Y 축 라인
             for (float i = 0; i <= gridSizeZ; i += largeStep)
             {
                 for (float k = 0; k <= gridSizeX; k += largeStep)
                 {
                     GL.Vertex3(startX + k, startY , startZ + i);
                     GL.Vertex3(startX + k, startY + gridSizeY , startZ + i);
                 }
             }
         }
 
 
         GL.End();
     }
}
