using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector3 startRotation;
    private Vector2 lastTapPos;
    private float helixDistance;

    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private List<GameObject> spawnedLevels = new List<GameObject>();

    private int currentStage = 0;

    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);

        if (float.IsNaN(helixDistance) || float.IsInfinity(helixDistance))
        {
            return;
        }

        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            { lastTapPos = curTapPos; }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        StartCoroutine(LoadStageCoroutine(stageNumber));
    }

    private IEnumerator LoadStageCoroutine(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        
        Debug.Log($"Loading stage: {stageNumber}");

        // Set the new background color
        Camera.main.backgroundColor = stage.stageBackgroundColor;

        BallControllerX ballController = FindObjectOfType<BallControllerX>();
        if (ballController != null)
        {
            ballController.GetComponent<Renderer>().material.color = stage.stageBallColor;
        }

        // Calculate spawn position
        Vector3 spawnPosition = CalculateSpawnPosition(stageNumber);

        if (IsInvalidPosition(spawnPosition))
        {
            Debug.LogError("Calculated spawn position is invalid!");
            yield break; // Exit if invalid
        }

        // Instantiate the helix level prefab
        GameObject instance = Instantiate(helixLevelPrefab, spawnPosition, Quaternion.identity);
        
        SetupHelixLevel(instance);

        // Check the helixLevelPrefab
        if (helixLevelPrefab == null)
        {
            Debug.LogError("helixLevelPrefab is not assigned.");
            yield break;
        }

        // Get the correct stage
        
        if (stage == null)
        {
            Debug.LogError("No stage found in allStages list (HelixController).");
            yield break;
        }

        
        // Reset the helix rotation
        transform.localEulerAngles = startRotation;

        // Destroy the old levels
        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
            yield return null; // Allow Unity to process
        }
        spawnedLevels.Clear();

        // Create the new levels
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);
            Debug.Log("Spawned Level");

            // Disable parts
            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            // Mark parts as death parts
            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = stage.stageLevelPartColor; // Set color
                if (t.gameObject.activeInHierarchy)
                    leftParts.Add(t.gameObject);
            }

            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.levels[i].deathPartCount && leftParts.Count > 0)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }
            yield return null; // Allow Unity to process
        }
    }

    private Vector3 CalculateSpawnPosition(int stageNumber)
    {
        return new Vector3(0, stageNumber * 5.0f, 0); // Example: stacks levels vertically
    }

    private bool IsInvalidPosition(Vector3 position)
    {
        return float.IsNaN(position.x) || float.IsNaN(position.y) || float.IsNaN(position.z) ||
               float.IsInfinity(position.x) || float.IsInfinity(position.y) || float.IsInfinity(position.z);
    }

    private void SetupHelixLevel(GameObject instance)
    {
        Debug.Log($"Setting up helix level: {instance.name}");
        if (instance.GetComponent<MeshFilter>() == null)
        {
            Debug.LogError("Missing MeshFilter component on the helix level prefab.");
        }
    }
}



