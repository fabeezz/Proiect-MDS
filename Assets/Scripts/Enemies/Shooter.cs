﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy type that shoots projectiles in customizable bursts and spread angles.
/// Supports oscillation, staggering, and multi-directional cone attacks.
/// </summary>
public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [SerializeField] private bool oscillate;
    [Tooltip("Stagger must be enabled for oscilate to function properly.")]
    private bool isShooting = false;

    /// <summary>
    /// Ensures sensible values in the Inspector when editing.
    /// </summary>
    private void OnValidate()
    {
        if(oscillate) { stagger = true; }
        if(!oscillate) { stagger = false; }
        if(projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if(burstCount < 1) { burstCount = 1; }
        if(timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if(restTime < 0.1f) { restTime = 0.1f; }
        if(startingDistance < 0.1f) { startingDistance = 0.1f; }
        if(angleSpread == 0) { angleSpread = 1; }
        if(bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }

    /// <summary>
    /// Starts attack routine after player is available in the scene.
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(WaitForPlayerAndAttack());
    }

    private IEnumerator WaitForPlayerAndAttack()
    {
        // așteaptă până când PlayerController.Instance e inițializat
        yield return new WaitUntil(() => PlayerController.Instance != null);
        Attack();
    }

    /// <summary>
    /// Triggers the shooting routine if not already firing.
    /// </summary>
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    /// <summary>
    /// Handles the full projectile burst logic, with optional oscillation and staggered firing.
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        
        if(stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst;}
        
        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            } else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;


                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (stagger) { yield return new WaitForSeconds(timeBetweenBursts); }

        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    /// <summary>
    /// Calculates the angular cone from which projectiles will be fired.
    /// </summary>
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        startAngle = 0;
        currentAngle = 0;
        angleStep = 0;

        if (PlayerController.Instance == null)
        {
            Debug.LogWarning("Shooter: PlayerController.Instance is null. Aborting cone calculation.");
            endAngle = 0;
            return;
        }

        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;

        if (angleSpread != 0 && projectilesPerBurst > 1)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    /// <summary>
    /// Determines the exact world position where a bullet should be spawned,
    /// based on current angle and configured offset.
    /// </summary>
    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
