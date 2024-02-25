using System;
using System.IO;
using System.Reflection;
using BepInEx;
using Photon.Voice;
using ThatGTFrame;
using Unity.Mathematics;
using UnityEngine;
using Utilla;

namespace ThatGTFrame
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {


        public bool active;
        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            AssetObj.SetActive(true);

            active = true;

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            AssetObj.SetActive(false);

            active = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }
        public GameObject AssetObj;
        void OnGameInitialized(object sender, EventArgs e)
        {
            var assetBundle = LoadAssetBundle("ThatGTFrame.thatgt");
            GameObject Obj = assetBundle.LoadAsset<GameObject>("ThatGtFrame");

            AssetObj = Instantiate(Obj);
            AssetObj.transform.position = new Vector3(-68.8294F, 12.382F, -84.1987F);
            AssetObj.transform.rotation = Quaternion.Euler(270f, 151.0634f, 0f);
            AssetObj.transform.localScale = new Vector3(3.427f, 47.7546f, 53.7565f);
        }

        AssetBundle LoadAssetBundle(string path)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                AssetBundle bundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
                Debug.Log("[" + PluginInfo.GUID + "] Success loading asset bundle");
                return bundle;
            }
            catch (Exception e)
            {
                Debug.Log("[" + PluginInfo.Name + "] Error loading asset bundle: " + e.Message + " " + path);
                throw;
            }
        }
    }
}