using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using HarmonyLib;

#if IL2CPP
using BepInEx.Unity.IL2CPP;
#endif

#if MONO
using BepInEx.Unity.Mono;
#endif

namespace KingdomAp;

[BepInPlugin("com.N4o.kingdomlogger", "Kingdom Logger", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private readonly Dictionary<string, GameObject> trackedObjects = new();

    private Harmony _harmony;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin KingdomAp is loaded!");

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
{
    foreach (var type in asm.GetTypes())
    {
        if (type.Name == "Chest")
        {
            base.Logger.LogInfo($"[FIND] Möglicher Chest-Typ gefunden: {type.FullName} in {asm.FullName}");
        }
    }
}


        _harmony = new Harmony("com.N4o.kingdomlogger");
        _harmony.PatchAll(); // Alle [HarmonyPatch] anwenden
    }

    void Update()
    {
        var allObjects = GameObject.FindObjectsOfType<GameObject>();

        HashSet<string> currentNames = new();

        foreach (var obj in allObjects)
        {
            string name = obj.name.ToLower();

            if (IsInteresting(name))
            {
                currentNames.Add(obj.name);

                if (!trackedObjects.ContainsKey(obj.name))
                {
                    //DisableChestDrops();
                    trackedObjects[obj.name] = obj;
                    Logger.LogInfo($"🆕 Entdeckt: {obj.name} @ {obj.transform.position}");
                }
            }
        }

        // Überprüfe auf zerstörte Objekte
        var vanished = trackedObjects.Keys.Except(currentNames).ToList();
        foreach (var gone in vanished)
        {
            Logger.LogInfo($"❌ Verschwunden: {gone}");
            trackedObjects.Remove(gone);
        }
    }

    bool IsInteresting(string name)
    {
        return name.Contains("hermit") ||
               name.Contains("mount") ||
               name.Contains("statue") ||
               name.Contains("mine") ||
               name.Contains("chest") ||
               name.Contains("dog") ||
               name.Contains("portal");
    }

    //void DisableChestDrops()
    //{
    //    foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
    //    {
    //        if (obj.name.ToLower().Contains("Player Opens Chest"))
    //        {
    //            var chest = obj.GetComponent("Player Opens Chest");
    //            if (chest != null)
    //            {
    //                Logger.LogInfo($"🚫 Deaktiviere Chest-Komponente: {obj.name}");
    //                ((MonoBehaviour)chest).enabled = false;
    //            }
    //        }
    //    }
    //}

    //void DisableChestDrops()
    //{
    //    foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
    //    {
    //        if (obj.name.ToLower().Contains("chest"))
    //        {
    //            Logger.LogInfo($"[CHEST] {obj.name} at {obj.transform.position}");
    //
    //            var components = obj.GetComponents<Component>();
    //            if (components.Length == 0)
    //            {
    //                Logger.LogWarning($"[CHEST] ⚠️ {obj.name} hat KEINE Komponenten.");
    //            }
    //
    //            foreach (var comp in components)
    //            {
    //                Logger.LogInfo($"[CHEST] ➕ Component: {comp.GetType().FullName}");
    //            }
    //        }
    //    }
    //}
}
