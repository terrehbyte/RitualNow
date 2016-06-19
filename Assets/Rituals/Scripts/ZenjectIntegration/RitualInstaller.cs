using UnityEngine;
using System;
using System.Collections;

using Zenject;
using TBYTEConsole;

public class RitualInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;

    [SerializeField]
    Packer _packerInstance = null;

    [SerializeField]
    ItemDatabase _itemCatalog = null;

    public override void InstallBindings()
    {
        InstallSettings();
        InstallRituals();
    }

    void InstallRituals()
    {
        Container.Bind<ITickable>().ToSingle<RitualRunner>();
        Container.Bind<IInitializable>().ToSingle<RitualRunner>();
        //Container.Bind<CVarRegistry>().ToSingle(); // TODO: Is there a nice of way of integrating this? hmm...

        Container.Bind<Statistics>().ToSingle();
        Container.Bind<ITickable>().ToSingle<Statistics>();
        Container.Bind<IInitializable>().ToSingle<Statistics>();

        Container.Bind<GameMode>().ToSingle();
        Container.Bind<IInitializable>().ToSingle<GameMode>();

        Container.Bind<AssemblyLine>().ToSingle();
        Container.Bind<ITickable>().ToSingle<AssemblyLine>();

        Container.Bind<BoxerLine>().ToSingle();
        Container.Bind<ITickable>().ToSingle<BoxerLine>();

        Container.Bind<Sequencer>().ToSingle();
        Container.Bind<ITickable>().ToSingle<Sequencer>();

        Container.Bind<Packer>().ToInstance(_packerInstance);
        Container.Bind<ItemDatabase>().ToInstance(_itemCatalog);
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
    [Inject]
    ItemDatabase _itemCatalog;

    [Inject]
    AssemblyLine.Settings _assembler;

    public void Initialize()
    {
        Debug.Log("Runner Initializing...");
        Debug.Assert(_assembler.ItemCatalog == _itemCatalog, "These items should be identical!");
    }

    public void Tick()
    {
        
    }
}