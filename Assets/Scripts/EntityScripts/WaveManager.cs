using UnityEngine;
using System.Collections;
using TMPro;

[System.Serializable]
public struct WaveGroup
{
    public EntityType entityType;
    public int count;
    public float spawnInterval;
}

[System.Serializable]
public struct Wave
{
    public WaveGroup[] waveGroups;
}

public class WaveManager : MonoBehaviour
{
    public PathManager pathManager;
    public EntitySpawn entitySpawner;
    public Wave[] waves;
    public TextMeshProUGUI waveText;

    private int waveIndex = 0;
    private bool waveStarting = false;

    private void Start()
    {
        WaveText();
    }

    public void NextWave()
    {
        if (waveStarting || waveIndex >= waves.Length)
            return;

        waveIndex++;
        WaveText();
        StartCoroutine(WaveRoutine(waves[waveIndex - 1]));
    }

    private IEnumerator WaveRoutine(Wave wave)
    {
        waveStarting = true;
        foreach (WaveGroup group in wave.waveGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                entitySpawner.SpawnEntity(group.entityType, pathManager.cachedPath);
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }
        waveStarting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWave();
        }
    }

    private void WaveText()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave: {waveIndex} / {waves.Length}";
        }
    }
}