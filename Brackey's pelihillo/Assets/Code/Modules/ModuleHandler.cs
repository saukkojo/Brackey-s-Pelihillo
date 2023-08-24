using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHandler : MonoBehaviour
{
    public const int MODULES_PER_DEPTH = 4;
    public Module[] shallowModules = null;
    public Module[] deepModules = null;
    public Module[] abyssModules = null;

    private void Awake()
    {
        Debug.Assert(shallowModules != null);
        Debug.Assert(deepModules != null);
        Debug.Assert(abyssModules != null);
    }

    public void PlaceModules()
    {
        float increase = Module.HEIGHT * 0.5f;
        float depth = increase;
        for (int i = 0; i < System.Enum.GetNames(typeof(ModuleType)).Length; i++)
        {
            for (int j = 0; j < MODULES_PER_DEPTH; j++)
            {
                PlaceModule(depth, (ModuleType)i);
                depth += increase;
            }
        }
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

        var module = Instantiate(modulePrefab, transform);
        module.Setup(depth);
    }

    public void CleanUp()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
