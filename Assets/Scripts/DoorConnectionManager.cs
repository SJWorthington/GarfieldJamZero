using UnityEngine;

public class DoorConnectionManager : MonoBehaviour
{
    private Vector2? _doorSpawnPosition;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Vector2? useSpawnPosition()
    {
        var position = _doorSpawnPosition;
        _doorSpawnPosition = null;
        return position;
    }

    public void setPos(Vector2 spawnPos)
    {
        _doorSpawnPosition = spawnPos;
    }
}
