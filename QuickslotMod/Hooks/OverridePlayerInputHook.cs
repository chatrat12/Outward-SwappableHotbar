using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SwappableHotbar
{
    [HarmonyPatch(typeof(LocalCharacterControl), "UpdateQuickSlots")]
    public class OverridePlayerInputHook
    {
        // Disable OG button checks

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var matcher = new CodeMatcher(instructions, generator);

            matcher.MatchForward(false, new CodeMatch(i =>
               i.ToString() == "call static System.Boolean ControlsInput::QuickSlotInstant1(System.Int32 _playerID)"
            ));
            matcher.MatchBack(false, new CodeMatch(i => i.opcode == OpCodes.Ldloc_0));
            var ret = new CodeInstruction(OpCodes.Ret);

            var fld = typeof(LocalCharacterControl).GetField("m_character", BindingFlags.Instance | BindingFlags.NonPublic);
            var mthd = typeof(Input).GetMethod("UpdateInput", BindingFlags.Static | BindingFlags.Public);

            matcher.Insert(new CodeInstruction[]
            { 
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, fld),
                new CodeInstruction(OpCodes.Call, mthd),
                new CodeInstruction(OpCodes.Ret) 
            });
            var retPos = matcher.Pos - 1;
            matcher.MatchBack(true, new CodeMatch(i => i.opcode == OpCodes.Brtrue));
            matcher.RemoveInstruction();
            matcher.InsertBranch(OpCodes.Brtrue, retPos);

            return matcher.Instructions();
        }
    }
}
