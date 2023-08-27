using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaterDepthChanger : MonoBehaviour
{
    public Material _mat;
    private Player _player;
    private ModuleHandler _handler;
    public Light2D _light;

    private void Awake()
    {
        if (_light == null)
        {
            _light = GetComponent<Light2D>();
        }
        Debug.Assert(_mat != null);
        Debug.Assert(_light != null);
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
        _light.intensity = 1 - t;
    }
}
