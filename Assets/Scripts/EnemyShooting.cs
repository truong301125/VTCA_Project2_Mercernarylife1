﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform player; // Tham chiếu đến Transform của player
    [SerializeField] private float shootingRange = 4f; // Tầm bắn của enemy
    [SerializeField] private float fireRate = 1f; // Tốc độ bắn (đạn/giây)
    [SerializeField] private GameObject bulletPrefab; // Prefab của viên đạn
    [SerializeField] private float bulletSpeed = 10f; // Tốc độ của viên đạn

    private float fireCooldown = 0f; // Biến kiểm soát thời gian hồi chiêu

    void Update()
    {
        // Tính khoảng cách giữa enemy và player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Giảm thời gian hồi chiêu
        fireCooldown -= Time.deltaTime;

        // Kiểm tra nếu player trong tầm bắn
        if (distanceToPlayer <= shootingRange)
        {
            // Nếu đã hết thời gian hồi chiêu, bắn đạn
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate; // Đặt lại thời gian hồi chiêu
            }
        }
    }

    private void Shoot()
    {
        // Tạo viên đạn từ prefab tại vị trí hiện tại của enemy
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Gắn Rigidbody2D (hoặc Rigidbody nếu 3D) để điều khiển viên đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Đẩy viên đạn sang trái
            rb.velocity = Vector2.left * bulletSpeed;
        }

        // Hủy viên đạn sau 5 giây để tránh tràn bộ nhớ
        Destroy(bullet, 5f);
    }

    public void SetSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }

    void OnDrawGizmosSelected()
    {
        // Hiển thị tầm bắn trong Scene
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}