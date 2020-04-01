using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public static bool done = false;
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
