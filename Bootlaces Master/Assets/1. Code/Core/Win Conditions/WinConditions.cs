using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace BootlacesMaster
{
    [Serializable]
    public class WinConditions : IEquatable<WinConditions>
    {
        public List<PlacementCondition> PlacementConditions;
        public List<EntanglementCondition> EntanglementConditions;

        public static WinConditions Generate(IEnumerable<IConditionLace> laces)
        {
            WinConditions winConditions = new WinConditions();
            
            winConditions.PlacementConditions = new List<PlacementCondition>();
            winConditions.EntanglementConditions = new List<EntanglementCondition>();

            foreach (var lace in laces)
            {
                PlacementCondition placementCondition = new PlacementCondition();
                placementCondition.Color = lace.Color;
                placementCondition.FirstHole = lace.FirstHole;
                placementCondition.SecondHole = lace.SecondHole;
                
                winConditions.PlacementConditions.Add(placementCondition);
            }
            
            foreach (var (firstLace, secondLace) in laces.DistinctPairs((first, second) => (first, second)))
            {
                if (firstLace.Intersects(secondLace, out var intersections) == false)
                    continue;
                
                foreach (var intersection in intersections)
                {
                    var entanglement = intersection.FirstLacePoint.y > intersection.SecondLacePoint.y
                        ? Entanglement.Over
                        : Entanglement.Under;

                    EntanglementCondition entanglementCondition = new EntanglementCondition
                    {
                        First = firstLace.Color, Entanglement = entanglement, Second = secondLace.Color, Times = 1
                    };

                    var existedCondition = winConditions.EntanglementConditions.IndexOf(condition => 
                        condition.IsPremutationOf(entanglementCondition));

                    if (existedCondition == -1)
                        winConditions.EntanglementConditions.Add(entanglementCondition);
                    else
                    {
                        var existedEntanglementCondition = winConditions.EntanglementConditions[existedCondition];
                        existedEntanglementCondition.Times += 1;
                        winConditions.EntanglementConditions[existedCondition] = existedEntanglementCondition;
                    }
                }
            }

            return winConditions;
        }
        
        public bool Equals(WinConditions other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return PlacementConditions.PermutationEquals(other.PlacementConditions)
                && EntanglementConditions.PermutationEquals(other.EntanglementConditions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((WinConditions)obj);
        }
    }
}