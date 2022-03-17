﻿
using System;
using System.Collections.Generic;
using RogueElements;
using RogueEssence.Dungeon;
using PMDC.Dungeon;
using RogueEssence.LevelGen;
using RogueEssence.Dev;
using RogueEssence.Script;
using NLua;
using System.Linq;

namespace PMDC.LevelGen
{
    [Serializable]
    public class ScriptGenStep<T> : GenStep<T> where T : BaseMapGenContext
    {
        public string Script;
        [Multiline(0)]
        public string ArgTable;

        public ScriptGenStep() { Script = ""; ArgTable = "{}"; }

        public override void Apply(T map)
        {
            LuaFunction luafun = LuaEngine.Instance.LuaState.GetFunction("FLOOR_GEN_SCRIPT." + Script);

            if (luafun != null)
            {
                LuaTable args = LuaEngine.Instance.RunString("return " + ArgTable).First() as LuaTable;
                luafun.Call(new object[] { map, args });
            }
        }

    }
}
