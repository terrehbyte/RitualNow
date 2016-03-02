using UnityEngine;
using System;
using System.Collections;

using Zenject;

public class RitualInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;

    [SerializeField]
    Packer _packerInstance = null;

    public override void InstallBindings()
    {
        InstallSettings();
        InstallRituals();
    }

    void InstallRituals()
    {
        Container.Bind<ITickable>().ToSingle<RitualRunner>();
        Container.Bind<IInitializable>().ToSingle<RitualRunner>();

        Container.Bind<Statistics>().ToSingle();
        Container.Bind<ITickable>().ToSingle<Statistics>();
        Container.Bind<IInitializable>().ToSingle<Statistics>();

        Container.Bind<GameMode>().ToSingle();
        Container.Bind<IInitializable>().ToSingle<GameMode>();

        Container.Bind<AssemblyLine>().ToSingle();
        Container.Bind<ITickable>().ToSingle<AssemblyLine>();

        Container.Bind<Packer>().ToInstance(_packerInstance);
    }

    void InstallSettings()
    {
        Container.Bind<AssemblyLine.Settings>().ToSingleInstance(_settings.Assembler);
    }

    [Serializable]
    public class Settings
    {
        public Camera MainCamera;
        public AssemblyLine.Settings Assembler;
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