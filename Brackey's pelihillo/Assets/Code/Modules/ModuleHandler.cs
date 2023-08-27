using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHandler : MonoBehaviour
{
    public const int MODULES_PER_DEPTH = 4;
    public Module[] shallowModules = null;
    public Module[] deepModules = null;
    public Module[] abyssModules = null;
    private float _maxDepth;
    public float maxDepth => _maxDepth;
    public float deepStartDepth;
    private GameObject _modules;

    [SerializeField] private GiantClam _clamPrefab;

    private void Awake()
    {
        Debug.Assert(shallowModules != null);
        Debug.Assert(deepModules != null);
        Debug.Assert(abyssModules != null);
    }

    public void PlaceModules()
    {
        if (!_modules)
        {
            _modules = new GameObject("Modules");
            _modules.transform.parent = transform;
        }

        float increase = Module.HEIGHT;
        float depth = increase * 0.5f;
        for (int i = 0; i < System.Enum.GetNames(typeof(ModuleType)).Length; i++)
        {
            for (int j = 0; j < MODULES_PER_DEPTH; j++)
            {
                PlaceModule(depth, (ModuleType)i);
                depth += increase;

            }
            if ((ModuleType)i == ModuleType.Deep)
            {
                deepStartDepth = depth - increase * 0.5f;
            }
        }
        _maxDepth = depth - increase * 0.5f;
        Instantiate(_clamPrefab, new Vector2(0, -depth), Quaternion.identity, _modules.transform);
    }

    private void PlaceModule(float depth, ModuleType type)
    {
        Module modulePrefab = null;
        switch (type)
        {
            case ModuleType.Shallow:
                modulePrefab = shallowModules[Random.Range(0, shallowModules.Length)];
                break;
            case ModuleType.Deep:
                modulePrefab = deepModules[Random.Range(0, deepModules.Length)];
                break;
            case ModuleType.Abyss:
                modulePrefab = abyssModules[Random.Range(0, abyssModules.Length)];
                break;
        }

        var module = Instantiate(modulePrefab, _modules.transform);
        module.Setup(depth);
    }

    public void CleanUp()
    {
        Destroy(_modules);
    }
}
