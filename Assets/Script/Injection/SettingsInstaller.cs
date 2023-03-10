using Mirror;
using Newtonsoft.Json;
using Script.Tools;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField] private GameSettings settings;

    private void OnValidate()
    {
        if (settings.PrefabPlayer == null)
        {
            Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
            return;
        }

        if (!settings.PrefabPlayer.TryGetComponent(out NetworkIdentity _))
        {
            Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
            return;
        }
    }

    public override void InstallBindings()
    {
        string result = PlayerPrefs.GetString(nameof(GameSettings));
        if (string.IsNullOrEmpty(result))
        {
            Save();
        }
        else
        {
           settings.Load(JsonConvert.DeserializeObject<GameSettings>(result));
        }
        settings.OnChangeGameSettings.AddListener(Save);
        Container.Bind<GameSettings>().FromInstance(settings).AsSingle().NonLazy();
    }

    private void Save()
    {
        string name = nameof(GameSettings);
        string data = JsonConvert.SerializeObject(settings);
        Debug.Log(data);
        PlayerPrefs.SetString(name, data);
    }
}