using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private List<GameObject> friends;
    [Header("Map Spawn Friend Config")][SerializeField] private bool isGridBased = true;
    [SerializeField] private Bounds mapBoundaries;
    [SerializeField] private LayerMask nonSpawnableArea;
    private AudioSource audioSource;
    private int maxMissingFriend = 3;
    private int friendsFound = 0;
    private UIController ui;

    private void OnEnable() {
        Checkpoint.PlayerEnterCheckpoint += CheckWinRound;
        Friend.PlayerFoundFriend += FoundFriend;
    }

    private void OnDisable() {
        Checkpoint.PlayerEnterCheckpoint -= CheckWinRound;
        Friend.PlayerFoundFriend -= FoundFriend;
    }

    void Awake()
    {
        maxMissingFriend = friends.Count;
        ui = GameObject.Find("UI").GetComponent<UIController>();
        ui.UpdateFriendCounter(maxMissingFriend);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        StartGame();
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        Debug.Log("LOAD SCNE");
        SceneManager.LoadScene(sceneName);
    }

    private void StartGame()
    {
        ui.ShowStartLabel();

        while (friends.Count > 0) {
            if (isGridBased) {
                GridBasedSpawn();
            } else {
                ColliderBasedSpawn();
            }
        }
    }

    private void GridBasedSpawn()
    {
        Vector3 randomPoint = GetRandomPointInBounds(mapBoundaries);
        Vector3Int gridPosition = wallTilemap.WorldToCell(randomPoint);

        if (!wallTilemap.GetTile(gridPosition)) {
            Vector3 realWorldPosition = wallTilemap.GetCellCenterLocal(gridPosition);
            GameObject friend = friends[0];
            Instantiate(friend, realWorldPosition, Quaternion.identity);
            friends.RemoveAt(0);
        }
    }

    private void ColliderBasedSpawn()
    {
        bool hasFoundLocation = false;
        GameObject friend = Instantiate(friends[0], Vector2.zero, Quaternion.identity);
        friends.RemoveAt(0);
        BoxCollider2D collider = friend.GetComponent<BoxCollider2D>();

        while (!hasFoundLocation) {
            Vector3 randomPoint = GetRandomPointInBounds(mapBoundaries);
            Vector3Int gridPosition = wallTilemap.WorldToCell(randomPoint);
            Vector3 realWorldPosition = wallTilemap.GetCellCenterLocal(gridPosition);
            friend.transform.position = realWorldPosition;
            Collider2D hit = Physics2D.OverlapBox(realWorldPosition, collider.bounds.size /2, nonSpawnableArea);

            if (!hit) {
                hasFoundLocation = true;
            }
        }
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void CheckWinRound()
    {
        if (friendsFound == maxMissingFriend) {
            ui.ToggleWinLabel();
            audioSource.Play();
        }
    }

    private void FoundFriend()
    {
        friendsFound++;
        ui.UpdateFriendCounter(maxMissingFriend - friendsFound);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + mapBoundaries.center, mapBoundaries.size);
    }

}
