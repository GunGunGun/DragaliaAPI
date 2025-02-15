﻿using System.Collections.Immutable;
using System.Text.Json.Serialization;
using DragaliaAPI.Shared.Definitions.Enums;
using DragaliaAPI.Shared.Json;

namespace DragaliaAPI.Shared.MasterAsset.Models;

public record CharaData(
    Charas Id,
    WeaponTypes WeaponType,
    int Rarity,
    int MaxLimitBreakCount,
    UnitElement ElementalType,
    bool IsPlayable,
    int MinHp3,
    int MinHp4,
    int MinHp5,
    int MaxHp,
    int AddMaxHp1,
    int PlusHp0,
    int PlusHp1,
    int PlusHp2,
    int PlusHp3,
    int PlusHp4,
    int PlusHp5,
    int McFullBonusHp5,
    int MinAtk3,
    int MinAtk4,
    int MinAtk5,
    int MaxAtk,
    int AddMaxAtk1,
    int PlusAtk0,
    int PlusAtk1,
    int PlusAtk2,
    int PlusAtk3,
    int PlusAtk4,
    int PlusAtk5,
    int McFullBonusAtk5,
    int Skill1,
    int Skill2,
    int HoldEditSkillCost,
    int EditSkillId,
    int EditSkillLevelNum,
    int EditSkillCost,
    string ManaCircleName
)
{
    public bool HasManaSpiral => this.MaxLimitBreakCount > 4;

    public ManaNode GetManaNode(int num)
    {
        string MC_1 = this.ManaCircleName[3..];
        int MC_0 = int.Parse($"{MC_1}{num}");

        return MasterAsset.ManaNode.Get(MC_0);
    }

    public IEnumerable<ManaNode> GetManaNodes()
    {
        // There is a quirk in the DB where every mana circle has a node with index 0 and type 0, such that
        // characters always have 51/71 nodes instead of 50/70 as expected.
        // These are scattered through the data so it is easier to check in code than modify the auto-generated JSON.
        return MasterAsset.ManaNode.Enumerable.Where(
            x => x.ManaCircleName == this.ManaCircleName && x.ManaPieceType != ManaNodeTypes.None
        );
    }

    public CharaAvailabilities Availability =>
        AvailabilityMap.TryGetValue(this.Id, out CharaAvailabilities availability)
            ? availability
            : CharaAvailabilities.Default;

    private static readonly IReadOnlyDictionary<Charas, CharaAvailabilities> AvailabilityMap =
        new Dictionary<Charas, CharaAvailabilities>()
        {
            { Charas.ThePrince, CharaAvailabilities.Story },
            { Charas.Elisanne, CharaAvailabilities.Story },
            { Charas.Ranzal, CharaAvailabilities.Story },
            { Charas.Cleo, CharaAvailabilities.Story },
            { Charas.Luca, CharaAvailabilities.Story },
            { Charas.Alex, CharaAvailabilities.Story },
            { Charas.Laxi, CharaAvailabilities.Story },
            { Charas.Chelle, CharaAvailabilities.Story },
            { Charas.Zena, CharaAvailabilities.Story }
        };
}
