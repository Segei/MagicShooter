using Script.Server;
using Script.Tools;
using Zenject;

namespace Script.Injection
{
    public class PlayerSpawnerInjection : MonoInstaller<PlayerSpawnerInjection>
    {
        [Inject] private GameSettings gameSettings;
        
        
        public override void InstallBindings()
        {
            Container.BindFactory<PlayerInstanceFactory, PlayerInstanceFactory.Factory>()
                .FromComponentInNewPrefab(gameSettings.PrefabPlayer);
        }
    }
}