//
//  AbstractConsole.cs
//
//  Lunar Plugin for Unity: a command line solution for your game.
//  https://github.com/SpaceMadness/lunar-unity-plugin
//
//  Copyright 2016 Alex Lementuev, SpaceMadness.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//

﻿using UnityEngine;

using System.Collections.Generic;

using LunarPluginInternal;

namespace LunarEditor
{
    interface IConsoleDelegate
    {
        void OnConsoleEntryAdded(AbstractConsole console, ref ConsoleViewCellEntry entry);
        void OnConsoleCleared(AbstractConsole console);
    }

#if LUNAR_DEVELOPMENT
    public // looks ugly but works for Unit testing
#endif

    class AbstractConsole : IDestroyable, IConsoleDelegate
    {
        public AbstractConsole(int historySize)
        {
            Entries = new CycleArray<ConsoleViewCellEntry>(historySize);
            Delegate = this; // use null-object to avoid constant null reference checks
        }

        internal void Add(ConsoleViewCellEntry entry)
        {
            if (ThreadUtils.IsUnityThread())
            {
                Entries.Add(entry);
                Delegate.OnConsoleEntryAdded(this, ref entry);
            }
            else
            {
                TimerManager.ScheduleTimer(() =>
                {
                    Add(entry);
                });
            }
        }

        public void Clear()
        {
            Entries.Clear();
            Delegate.OnConsoleCleared(this);
        }

        //////////////////////////////////////////////////////////////////////////////
        
        #region IDestroyable
        
        public virtual void Destroy()
        {
        }
        
        #endregion

        //////////////////////////////////////////////////////////////////////////////

        #region IConsoleDelegate null implementation

        public void OnConsoleEntryAdded(AbstractConsole console, ref ConsoleViewCellEntry entry)
        {
        }

        public void OnConsoleCleared(AbstractConsole console)
        {
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////
        
        #region Properties
        
        internal CycleArray<ConsoleViewCellEntry> Entries { get; private set; }
        internal IConsoleDelegate Delegate { get; set; }

        public int Capacity
        {
            get { return Entries.Capacity; }
            set { Entries.Capacity = value; }
        }
        
        #endregion
    }

    struct AttributedString
    {
        public static readonly AttributedString Null = default(AttributedString);

        public string value;
        public Rect position;
        public int foregroundColor, backgroundColor;
        public int flags;

        public AttributedString(string value, float x = 0, float y = 0, float w = 0, float h = 0, int foregroundColor = 0, int backgroundColor = 0)
        {
            this.value = value != null ? value : "";
            this.position = new Rect(x, y, w, h);
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.flags = 0;
        }

        public override string ToString()
        {
            return string.Format("[AttributedString: value={0}, x={1}, y={2}, w={3}, h={4}]", value, x, y, w, h);
        }

        #region Properties

        public float x
        {
            get { return position.x; }
            set { position.x = value; }
        }

        public float y
        {
            get { return position.y; }
            set { position.y = value; }
        }

        public float w
        {
            get { return position.width; }
            set { position.width = value; }
        }

        public float h
        {
            get { return position.height; }
            set { position.height = value; }
        }

        #endregion
    }

    interface ITextMeasure
    {
        Vector2 CalcSize(string text);
        float CalcHeight(string text, float width);
        float LineHeight { get; }
    }
}