using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave w = waves[i];

            // spawn power-ups
            for (int p = 0; p < w.numberOfPowerUp; p++)
            {
                int rand = Random.Range(0, spawnPoints.Length);
                Instantiate(powerUpPrefab, spawnPoints[rand].position, Quaternion.identity);
            }

            // รอเวลาตามที่กำหนดก่อนเริ่มสปาวน์ศัตรู
            yield return new WaitForSeconds(w.delayStart);

            // เลือกจุดสปาวน์แบบสุ่มตามจำนวนที่กำหนด
            List<Transform> selectedPoints = new List<Transform>();
            List<Transform> tempPoints = new List<Transform>(spawnPoints);

            for (int j = 0; j < w.numberOfRandomSpawnPoint; j++)
            {
                int rand = Random.Range(0, tempPoints.Count);
                selectedPoints.Add(tempPoints[rand]);
                tempPoints.RemoveAt(rand);
            }

            // สปาวน์ศัตรูตามจำนวนที่กำหนด โดยใช้จุดสปาวน์ที่เลือกไว้
            for (int e = 0; e < w.totalSpawnEnemies; e++)
            {
                int rand = Random.Range(0, selectedPoints.Count);
                Instantiate(enemyPrefab, selectedPoints[rand].position, Quaternion.identity);

                yield return new WaitForSeconds(w.spawnInterval);
            }

            // รอจนกว่าศัตรูทั้งหมดจะถูกทำลายก่อนจะเริ่มเวฟถัดไป
            while (FindObjectsOfType<Enemy>().Length > 0)
            {
                yield return null;
            }
        }
    }
}