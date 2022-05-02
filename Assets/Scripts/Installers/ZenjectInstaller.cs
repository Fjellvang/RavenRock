using Assets.Scripts.Game;
using Assets.Scripts.GameInput;
using Assets.Scripts.Player;
using Assets.Scripts.Signals;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<string>().FromInstance("Hello World!");
        //Container.Bind<Greeter>().AsSingle().NonLazy();

        SignalBusInstaller.Install(Container);

        //Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle();// Bound via component
        Container.Bind<MenuManager>().AsSingle();

        Container.DeclareSignal<GamePausedSignal>();

        Container.Bind<PlayerSettings>().AsSingle();
        Container.Bind<PlayerStamina>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStaminaManager>().AsSingle();

        Container.Bind<InputState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle();

    }
}

