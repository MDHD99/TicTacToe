using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIColliderScript : MonoBehaviour
{
    public AIGameController gc;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        int name = int.Parse(gameObject.name);
        int parentName = int.Parse(transform.parent.name);
        int[] gridPosition = new int[2];
        int miniboardx = parentName / 10;
        int miniboardy = parentName % 10;
        string miniBoard = miniboardx.ToString() + miniboardy.ToString();

        gridPosition[0] = name / 10;
        gridPosition[1] = name % 10;

        gc.PlayTurn(gameObject, gridPosition, miniBoard);
    }
}
