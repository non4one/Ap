using HarmonyLib;
using System;
using System.Reflection;

namespace KingdomAp
{
    [HarmonyPatch]
    public static class ChestOpenPatch
    {
        // Dynamische Zielmethode: Chest.Open()
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method("Chest:Open");
        }

        [HarmonyPrefix]
        public static bool BlockChestOpen()
        {
            UnityEngine.Debug.Log("[Harmony] 🚫 Chest.Open blockiert!");
            return false; // verhindert Ausführung der Originalmethode
        }
    }
}
