//
//  TextField.cs
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
using System.Collections;

using LunarPlugin;
using LunarPluginInternal;

namespace LunarEditor
{
    delegate void TextFieldDelegate(TextField field);
    delegate bool TextFieldKeyDelegate(TextField field, KeyCode code, bool pressed);

    class TextField : View
    {
        private string m_text;
        private bool m_firstKeyDown;

        public TextField(string text = "")
            : this(0, 0, text)
        {
        }

        public TextField(float x, float y, string text = "")
            : this(x, y, UISize.TextFieldWidth, UISize.TextFieldHeight, text)
        {
        }

        public TextField(float x, float y, float w, float h, string text = "")
            : base(x, y, w, h)
        {
            Text = text;
            m_firstKeyDown = true;

            this.IsFocusable = true;
            this.KeyUp = KeyUpHandler;
            this.KeyDown = KeyDownHandler;
        }

        private bool KeyDownHandler(View view, Event evt)
        {
            if (TextKeyDelegate == null)
            {
                return false;
            }

            if (m_firstKeyDown)
            {
                m_firstKeyDown = false;

                IsCtrlPressed = evt.control;
                IsShiftPressed = evt.shift;
                IsAltPressed = evt.alt;
                IsCmdPressed = evt.command;

                KeyCode key = GetKeyCode(evt);
                if (OnKeyPress(key))
                {
                    return true;
                }

                try
                {
                    if (TextKeyDelegate(this, key, true))
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Log.error(e);
                }
            }

            if (Event.current.keyCode == KeyCode.Tab || Event.current.character == '\t')
            {
                Event.current.Use();  // disable focus traversal on the tab key
            }

            return false;
        }

        private bool KeyUpHandler(View view, Event evt)
        {
            if (TextKeyDelegate == null)
            {
                return false;
            }

            m_firstKeyDown = true;

            IsCtrlPressed = Event.current.control;
            IsShiftPressed = Event.current.shift;
            IsAltPressed = Event.current.alt;
            IsCmdPressed = Event.current.command;

            KeyCode key = GetKeyCode(evt);
            if (OnKeyRelease(key))
            {
                return true;
            }

            try
            {
                if (TextKeyDelegate(this, key, false))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.error(e);
            }

            if (Event.current.keyCode == KeyCode.Tab || Event.current.character == '\t')
            {
                Event.current.Use(); // disable focus traversal on the tab key
            }

            return false;
        }

        protected override void DrawGUI()
        {
            string oldText = m_text;
            m_text = CreateTextField(oldText);

            if (oldText != m_text)
            {
                if (TextChangedDelegate != null)
                {
                    TextChangedDelegate(this);
                }
            }
        }

        protected virtual string CreateTextField(string text)
        {
            return GUI.TextField(Frame, text);
        }

        private bool OnKeyPress(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.A:
                {
                    if (IsCtrlPressed)
                    {
                        CaretPos = 0;
                        return true;
                    }
                    return false;
                }
                
                case KeyCode.E:
                {
                    if (IsCtrlPressed)
                    {
                        CaretPos = Text.Length > 0 ? Text.Length + 1 : 0;
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }

        private bool OnKeyRelease(KeyCode key)
        {
            return false;
        }

        private KeyCode GetKeyCode(Event evt)
        {
            KeyCode keyCode = evt.keyCode;
            if (keyCode != KeyCode.None)
            {
                return keyCode;
            }

            switch (evt.character)
            {
                case '\n': return KeyCode.KeypadEnter;
            }
            return KeyCode.None;
        }

        //////////////////////////////////////////////////////////////////////////////

        #region Properties

        public string Text 
        { 
            get { return m_text; }
            set 
            { 
                m_text = value;
                #if UNITY_5_1 || UNITY_5_0 || UNITY_4_6
                CaretPos = value != null ? value.Length + 1 : 0;
                #else
                TimerManager.ScheduleTimer(delegate() {
                    CaretPos = value != null ? value.Length + 1 : 0;
                });
                #endif
            }
        }

        public TextFieldDelegate TextChangedDelegate { get; set; }
        public TextFieldKeyDelegate TextKeyDelegate { get; set; }

        public bool IsCtrlPressed { get; private set; }
        public bool IsAltPressed { get; private set; }
        public bool IsCmdPressed { get; private set; }
        public bool IsShiftPressed { get; private set; }

        public int CaretPos
        {
            get 
            {
                TextEditor editor = (TextEditor) GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                #if UNITY_5_1 || UNITY_5_0 || UNITY_4_6
                return editor.pos;
                #else
                return editor.cursorIndex;
                #endif
            }
            set
            {
                TextEditor editor = (TextEditor) GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);

                int pos = Mathf.Max(0, Mathf.Min(value, Text.Length + 1));
                #if UNITY_5_1 || UNITY_5_0 || UNITY_4_6
                editor.selectPos = pos;
                editor.pos = pos;
                #else
                editor.selectIndex = pos;
                editor.cursorIndex = pos;
                #endif
            }
        }

        #endregion
    }
}
