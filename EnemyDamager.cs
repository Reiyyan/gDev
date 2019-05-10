using UnityEngine;
using System.Collections;

public class EnemyDamager : MonoBehaviour {

    public int ballDamage;

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponentInParent<Player>();
        if (_player != null)
        {
          //  Debug.LogError("HELLO WE HIT PLAYER");
            _player.DamagePlayer(ballDamage);
        }
    }

    // Use this for initialization
    void Start () {

        ballDamage = gameObject.GetComponentInParent<Enemy>().enemyStats.damage;

    }
}
