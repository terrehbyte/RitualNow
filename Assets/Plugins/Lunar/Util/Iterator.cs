//
//  Iterator.cs
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

using System;
using System.Collections.Generic;

namespace LunarPluginInternal
{
    class Iterator<T>
    {
        private IList<T> m_target;
        private int m_current;

        public Iterator(IList<T> target)
        {
            Init(target);
        }

        public void Init(IList<T> target)
        {
            if (target == null)
            {
                throw new NullReferenceException("target is null");
            }

            m_target = target;
            m_current = -1;
        }

        public bool HasNext(int items = 1)
        {
            return m_current + items < Count;
        }

        public bool HasPrev(int items = 1)
        {
            return m_current - items >= 0;
        }

        public void Skip(int items = 1)
        {
            m_current += items;
        }

        public bool TrySkip(int items = 1)
        {
            if (HasNext(items))
            {
                Skip(items);
                return true;
            }

            return false;
        }

        public T Current()
        {
            return m_target[m_current];
        }

        public T Next()
        {
            ++m_current;
            return Current();
        }

        public T Prev()
        {
            --m_current;
            return Current();
        }

        public void Reset()
        {
            m_current = -1;
        }

        public int Count
        {
            get { return m_target.Count; }
        }

        public int Position 
        { 
            get { return m_current; }
            set 
            { 
                if (value < -1 || value >= Count)
                {
                    throw new IndexOutOfRangeException("Invalid position: " + value);
                }
                m_current = value; 
            }
        }
    }
}