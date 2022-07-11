using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MazeStageMove
{
    public float rot = 30f;

    protected override void MoveStage(Vector3 pos)
    {
        if (pos.x >= 30f) pos.x = 30f;
        else if (pos.x <= -30f) pos.x = -30f;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(rot, 0f, -pos.x), 1f * Time.deltaTime);
    }
}
