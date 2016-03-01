//
//  CCommandOptionAttribute.cs
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

﻿using System;

namespace LunarPlugin
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CCommandOptionAttribute : Attribute
    {
        public CCommandOptionAttribute()
        {
            Required = false;
        }

        //////////////////////////////////////////////////////////////////////////////

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }

        public string ShortName { get; set; }
        public bool Required { get; set; }

        public string Values { get; set; }

        #endregion
    }
}