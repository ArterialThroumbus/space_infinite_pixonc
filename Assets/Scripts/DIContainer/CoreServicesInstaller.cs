using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using Assets.Scripts.Views.Interfaces;
using Zenject;
using UnityEngine;
using Assets.Scripts;

public class CoreServicesInstaller : MonoInstaller {

    [SerializeField]
    private Configuration _configuration;

    public override void InstallBindings()
    {
        //utilites
        Container.Bind<Configuration>().FromInstance(_configuration).AsSingle();
        Container.BindInterfacesTo<SimpleCoordinateConverter>().AsSingle().NonLazy();
        Container.Bind<SpaceInfo>().FromMethod(TempSpaceInfo).AsSingle();

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
    }

    private SpaceInfo TempSpaceInfo(InjectContext context)
    {
        return new SpaceInfo { CurrentScale = 5 };
    }
}
