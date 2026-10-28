using UnityEngine;
using System.Collections;
using TMPro;

[System.Serializable]
public struct WaveGroup
{
    public EntityType entityType;
    public int count;
    public float spawnInterval;
    public float delay;
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
    public GameObject spacePrompt;

    private int waveIndex = 0;
    private bool waveStarting = false;

    private void Start()
    {
        WaveText();

        if (spacePrompt != null)
        {
            spacePrompt.SetActive(true);
        }
    }

    public void NextWave()
    {
        if (waveStarting || waveIndex >= waves.Length)
            return;

        waveStarting = true;
        waveIndex++;

        WaveText();

        if (spacePrompt != null)
        {
            spacePrompt.SetActive(false);
        }

        StartCoroutine(WaveRoutine(waves[waveIndex - 1]));
    }

    private IEnumerator WaveRoutine(Wave wave)
    {
        int activeGroups = wave.waveGroups.Length;

        foreach (WaveGroup group in wave.waveGroups)
        {
            StartCoroutine(SpawnGroupRoutine(group, () => activeGroups--));
        }

        while (activeGroups > 0)
        {
            yield return null;
        }

        while (AreEntitiesStillActive())
        {
            yield return null;
        }

        waveStarting = false;

        if (spacePrompt != null && waveIndex < waves.Length)
        {
            spacePrompt.SetActive(true);
        }
    }

    private IEnumerator SpawnGroupRoutine(WaveGroup group, System.Action onComplete)
    {
        if (group.delay > 0)
        {
            yield return new WaitForSeconds(group.delay);
        }

        for (int i = 0; i < group.count; i++)
        {
            entitySpawner.SpawnEntity(group.entityType, pathManager.cachedPath);
            yield return new WaitForSeconds(group.spawnInterval);
        }

        onComplete?.Invoke();
    }

    private bool AreEntitiesStillActive()
    {
        EntityMovementManager[] activeEntities = FindObjectsByType<EntityMovementManager>(FindObjectsSortMode.None);
        return activeEntities.Length > 0;
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