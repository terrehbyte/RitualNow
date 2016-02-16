using UnityEngine;
using System.Collections;

using Zenject;

public class RitualInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ITickable>().ToSingle<RitualRunner>();
        Container.Bind<IInitializable>().ToSingle<RitualRunner>();

        Container.Bind<ITickable>().ToSingle<Statistics>();
        Container.Bind<IInitializable>().ToSingle<Statistics>();
    }
}

public class RitualRunner : ITickable, IInitializable
{
    public void Initialize()
    {
        Debug.Log("Runner Initializing...");
    }

    public void Tick()
    {
        
    }
}