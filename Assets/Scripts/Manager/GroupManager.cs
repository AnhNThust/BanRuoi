using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public static GroupManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Only 1 GroupManager allow exist");
        Instance = this;
    }

    public Transform GetGroupByIndex(int index)
    {
        return transform.GetChild(index);
    }

    public Transform GetRandomGroup()
    {
        var randIndex = Random.Range(0, transform.childCount);
        return transform.GetChild(randIndex);
    }
}