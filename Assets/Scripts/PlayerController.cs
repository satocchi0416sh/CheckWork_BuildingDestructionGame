using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombForce = 10f;
    [SerializeField] private float rotateSpeed = 100f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowBomb();
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontal * Time.deltaTime * rotateSpeed);
    }
    
    private void ThrowBomb()
    {
        // 生成位置をプレイヤーの前方に設定
        Vector3 spawnPosition = transform.position + transform.forward;
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        
        // 爆弾を飛ばす方向はプレイヤーの前方に上方向を加えたベクトル（斜め上に飛ばす）
        Vector3 bombDirection = transform.forward + Vector3.up;
        bomb.GetComponent<Rigidbody>().AddForce((bombDirection) * bombForce, ForceMode.Impulse);
    }
}
