//
//  Button.cs
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
using System.Collections;

namespace LunarEditor
{
    delegate void ButtonDelegate(Button button);

    class Button : AbstractButton
    {
        public Button(string title, ButtonDelegate buttonDelegate)
            : base(title)
        {
            ButtonDelegate = buttonDelegate;
        }

        public override void OnGUI()
        {
            if (GUI.Button(Frame, Content))
            {
                if (ButtonDelegate != null)
                    ButtonDelegate(this);
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        #region Properties

        public ButtonDelegate ButtonDelegate { get; private set; }

        #endregion
    }
}