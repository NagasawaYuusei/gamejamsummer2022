using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator1 : MonoBehaviour
{
    [SerializeField, Tooltip(""), Min(0)] float _time = 0.05f;
    [SerializeField, Tooltip(""), Min(0)] int _poolsizu = 100;
    [SerializeField, Tooltip("")] Enemy[] _prefab = null;
    [SerializeField, Tooltip("")] Transform _root = null;

    bool spawning = false;

    //GameObject player;
    float _timer = 0.0f;
    [SerializeField] float _cRad = 0.0f;
    [SerializeField] int _pos = 30;
    [SerializeField] Transform _popPos;
    ObjectPool<Enemy> _enemyPool = new ObjectPool<Enemy>();

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        var x = Random.Range(0, _prefab.Length );

        _enemyPool.SetBaseObj(_prefab[x], _root);
        _enemyPool.SetCapacity(_poolsizu);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _time)
        {
            Spawn();
            _timer -= _time;
        }
    }

    void Spawn()
    {

        var script = _enemyPool.Instantiate();
        if (!script)
        {
            return;
        }

        //_popPos.x = player.transform.position.x + _pos * Mathf.Cos(_cRad);
        //_popPos.z = player.transform.position.z + _pos * Mathf.Sin(_cRad);
        script.transform.position = _popPos.position;
        _cRad += 1f;
    }


}
