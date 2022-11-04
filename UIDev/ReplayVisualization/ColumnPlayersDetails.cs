﻿using BossMod;
using ImGuiNET;
using System.Collections.Generic;

namespace UIDev
{
    public class ColumnPlayersDetails : Timeline.ColumnGroup
    {
        private StateMachineTree _tree;
        private List<int> _phaseBranches;
        private Replay _replay;
        private Replay.Encounter _encounter;
        private ColumnPlayerDetails?[] _columns;

        public ColumnPlayersDetails(Timeline timeline, StateMachineTree tree, List<int> phaseBranches, Replay replay, Replay.Encounter enc)
            : base(timeline)
        {
            _tree = tree;
            _phaseBranches = phaseBranches;
            _replay = replay;
            _encounter = enc;
            _columns = new ColumnPlayerDetails[enc.PartyMembers.Count];
        }

        public void DrawConfig(UITree tree)
        {
            for (int i = 0; i < _columns.Length; ++i)
            {
                var (p, c) = _encounter.PartyMembers[i];
                foreach (var n in tree.Node($"{c} {ReplayUtils.ParticipantString(p)}"))
                {
                    var col = _columns[i];
                    if (col != null)
                        col.DrawConfig(tree);
                    else if (ImGui.Button("Show details..."))
                        _columns[i] = Add(new ColumnPlayerDetails(Timeline, _tree, _phaseBranches, _replay, _encounter, p, c));
                }
            }
        }
    }
}