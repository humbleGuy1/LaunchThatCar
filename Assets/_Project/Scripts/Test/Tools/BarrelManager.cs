using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{
    public static List<Barrel> BarrelList = new List<Barrel>();

    private void OnDrawGizmos()
    {
        foreach(Barrel barrel in BarrelList)
        {
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeight;

            Handles.DrawBezier(transform.position, barrel.transform.position,
                managerPos - offset, barrelPos + offset, Color.white, EditorGUIUtility.whiteTexture, 1f);
            //Handles.DrawAAPolyLine(transform.position, barrel.transform.position);
        }
    }
}
