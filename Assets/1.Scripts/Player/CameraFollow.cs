using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    public static CameraFollow Instance => instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        Player player = Player.Instance;

        playerTr = player.transform;

        gapX = transform.position.x - player.transform.position.x;
        gapY = transform.position.y - player.transform.position.y;
        gapZ = transform.position.z - player.transform.position.z;
    }

    float gapX;
    float gapY;
    float gapZ;

    public float smoothness = 0.5f;//부드럽게 따라가기

    public Transform playerTr; //플레이어 Transform 컴포넌트
    Vector3 velocity;

    void FixedUpdate()
    {
        if (!playerTr)
            return;

        Vector3 pos = Vector3.zero;
        pos.x = playerTr.position.x + gapX;
        pos.y = playerTr.position.y + gapY;
        pos.z = playerTr.position.z + gapZ;

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);
    }

    public void ChangeCamera(Vector3 newpos)
    {
        Player player = Player.Instance;

        playerTr = player.transform;

        gapX = newpos.x;
        gapY = newpos.y - player.transform.position.y;
        gapZ = newpos.z;
    }
}
