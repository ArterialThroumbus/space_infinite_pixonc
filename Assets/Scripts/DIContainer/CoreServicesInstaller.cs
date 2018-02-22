using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using Assets.Scripts.Views.Interfaces;
using Zenject;
using UnityEngine;
using Assets.Scripts;
using System;
using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Signals;

public class CoreServicesInstaller : MonoInstaller {

    [SerializeField]
    private Configuration _configuration;

    public override void InstallBindings()
    {
        //signals
        Container.DeclareSignal<SpecialViewChanged>();

        //utilites
        Container.Bind<Configuration>().FromInstance(_configuration).AsSingle();
        Container.BindInterfacesTo<SimpleCoordinateConverter>().AsSingle().NonLazy();
        Container.Bind<SpaceInfo>().FromMethod(TempSpaceInfo).AsSingle();
        
        //Input
        Container.BindInterfacesTo<UnityInputSystem>().AsSingle();
        Container.BindInterfacesTo<CustomInputSystem>().AsSingle();
        Container.Bind<IInputSubscriber>().WithId("input_manager").To<SimpleInputManager>().AsSingle();
        Container.BindInterfacesTo<CustomInputPresenter>().AsSingle();

        //Ship
        Container.BindInterfacesTo<SimpleShip>().AsSingle();
        Container.BindInterfacesAndSelfTo<SimpleShipController>().AsSingle().NonLazy();
        
        Container.BindInterfacesTo<VShip>().FromComponentInNewPrefabResource("Prefabs/Ship").AsSingle();

        Container.BindInterfacesTo<ShipPresenter>().AsSingle().NonLazy();

        //camera following
        Container.BindInterfacesTo<SimpleCameraFollowingModel>().AsSingle();
        Container.BindInterfacesTo<VSimpleCameraFollowing>().AsSingle();
        Container.BindInterfacesTo<CameraFollowingPresenter>().AsSingle().NonLazy();

        //scaling
        Container.BindInterfacesTo<SimpleScaling>().AsSingle();
        Container.BindInterfacesTo<SimpleScaleManipulationView>().AsSingle();
        Container.BindInterfacesTo<ScalingPresenter>().AsSingle().NonLazy();

        //space
        Container.Bind<int>().FromInstance(Guid.NewGuid().GetHashCode());
        Container.BindInterfacesTo<SimpleSpecialViewChecker>().AsSingle();
        Container.BindInterfacesTo<SimpleSpecialView>().AsSingle();
        Container.BindInterfacesTo<SimpleMap>().AsSingle();
        Container.BindInterfacesTo<SimpleSpace>().AsSingle();
        Container.BindInterfacesTo<SimpleExpansionChecker>().AsSingle().NonLazy();
        Container.BindInterfacesTo<SimpleExpansionStrategy>().AsSingle().NonLazy();
        Container.BindInterfacesTo<VSpaceView>().AsSingle();
        Container.BindMemoryPool<VPlanet, VPlanet.Pool>()
            .WithInitialSize(_configuration.CountPlanetInSpecialView)
            .FromComponentInNewPrefabResource("Prefabs/VPlanet").
            UnderTransformGroup("Game");

        Container.BindInterfacesTo<SpacePresenter>().AsSingle().NonLazy();

        //special view
        Container.BindInterfacesTo<SpecialViewPresenter>().AsSingle();

        //GamePresenter
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
    }

    private SpaceInfo TempSpaceInfo(InjectContext context)
    {
        return new SpaceInfo { CurrentScale = 5 };
    }
}
