using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;  // Corrected typo from playerPrehub to playerPrefab
    public GameObject boxPrefab;     // Corrected typo from boxPrehub to boxPrefab
    public GameObject goalPrefab;    // Corrected typo from goalPrehub to goalPrefab
    int[,] map;
    GameObject[,] field;

    public GameObject clearText;

    bool MoveNumber(Vector2Int moveTo, Vector2Int moveFrom)
    {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (map[moveTo.y, moveTo.x] == 2)
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo + velocity, moveTo);

            if (!success) { return false; }
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo + velocity, moveTo);
            if (!success)
            {
                return false;
            }
        }

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x].GetComponent<MoveScript>().MoveTo(moveToPosition);
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }

    private Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }

                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    void Start()
    {
        UnityEngine.Device.Screen.SetResolution(1280, 720, false);

        map = new int[,]
        {
            {1,0,0,2,3 },
            {0,0,0,2,3 },
            {0,0,0,2,3 },
            {0,0,0,2,0 },
            {0,0,0,2,0 },
            {0,0,0,2,0 },
        };

        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    GameObject instance = Instantiate(playerPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                    field[y, x] = instance;
                }
                if (map[y, x] == 2)
                {
                    GameObject instance = Instantiate(boxPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                    field[y, x] = instance;
                }
            }
        }
    }

    bool IsCleared()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        foreach (var goal in goals)
        {
            GameObject f = field[goal.y, goal.x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }

        return true;
    }

    void Update()
    {
        Vector2Int playerIndex = GetPlayerIndex();
        if (playerIndex == new Vector2Int(-1, -1)) return;

        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow)) direction = new Vector2Int(1, 0);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = new Vector2Int(-1, 0);
        if (Input.GetKeyDown(KeyCode.UpArrow)) direction = new Vector2Int(0, -1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) direction = new Vector2Int(0, 1);

        if (direction != Vector2Int.zero)
        {
            MoveNumber(playerIndex + direction, playerIndex);

            if (IsCleared())
            {
                Debug.Log("Clear");
                clearText.SetActive(true);
            }
        }
    }
}
