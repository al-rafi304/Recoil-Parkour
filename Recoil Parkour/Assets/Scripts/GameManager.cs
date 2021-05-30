using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform respawnPos;

    public void Respawn(GameObject _player)
    {
        _player.transform.position = respawnPos.position;
    }
}
