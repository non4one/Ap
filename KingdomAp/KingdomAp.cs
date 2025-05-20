using BepInEx.Logging;
using BepInEx;
using System;
using System.Collections.Generic;
using UnityEngine;
using File = System.IO.File;
using HarmonyLib;
using Coatsink.Common;
using System.Reflection;
using BepInEx.Unity.Mono;
using JetBrains.Annotations;
using System.Text;
using System.Linq;
using System.IO;

namespace KingdomAp.KingdomAp;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]

public class KingdomAp : BaseUnityPlugin
{
    void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");
    }

    private HashSet<string> loggedObj = new HashSet<string>();

    void Update()
    {
        //Event e = Event.current;
        //if (e.isKey && e.type == EventType.KeyDown)           //logging every Button Input
        //{
        //    Logger.LogInfo($"Key {e.keyCode} pressed");
        //}

        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {

            if (!loggedObj.Contains(obj.name))
            {
                Logger.LogInfo($"{obj.name} {obj.transform.position}");
                loggedObj.Add(obj.name);
            }
        }

        //Need to fix logger to get every obj name
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var obj in loggedObj)
            {
                Logger.LogMessage(obj);
            }
            Logger.LogInfo($"Exported unique string log");
        }
    }
}