using System.Collections.Generic;
using UnityEngine;

public class HelixControllerX : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;
    private float helixDistance;

    public List<StageX> allStages = new List<StageX>();
    private List<GameObject> spawnedLevels = new List<GameObject>();

    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);

        if (float.IsNaN(helixDistance) || float.IsInfinity(helixDistance))
        {
            Debug.LogError("Helix distance is invalid.");
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
            {
                lastTapPos = curTapPos;
            }

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
        StageX stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in allStages list. Are all stages assigned in the list?");
            return;
        }

        Camera.main.backgroundColor = stage.stageBackgroundColor;

        BallControllerX ballController = FindObjectOfType<BallControllerX>();
        if (ballController != null)
        {
            ballController.GetComponent<Renderer>().material.color = stage.stageBallColor;
        }

        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }
        spawnedLevels.Clear();

        if (stage.levels.Count == 0)
        {
            Debug.LogError("Stage has no levels.");
            return;
        }

        float levelDistance = helixDistance / stage.levels.Count;

        if (float.IsNaN(levelDistance) || float.IsInfinity(levelDistance))
        {
            Debug.LogError("Calculated levelDistance is invalid.");
            return;
        }

        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            int partToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform t in level.transform)
            {
                Renderer renderer = t.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = stage.stageLevelPartColor;
                }

                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.AddComponent<DeathPartX>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }
}

