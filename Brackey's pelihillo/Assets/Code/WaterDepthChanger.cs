using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDepthChanger : MonoBehaviour
{
    public Material _mat;
    private Player _player;
    private ModuleHandler _handler;

    private void Awake()
    {
        Debug.Assert(_mat != null);
    }

    private void Start()
    {
        _player = Player.current;
        _handler = GameManager.current.moduleHandler;
    }

    private void OnApplicationQuit()
    {
        _mat.SetFloat("_Depth", 0);
    }

    private void Update()
    {
        float t = _player.depth / _handler.maxDepth;
        _mat.SetFloat("_Depth", t);
    }
}
