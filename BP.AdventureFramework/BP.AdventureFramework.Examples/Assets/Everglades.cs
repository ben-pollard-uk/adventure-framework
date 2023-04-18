﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Examples.Assets
{
    internal static class Everglades
    {
        private const string Knife = "Knife";
        private const string ConchShell = "Conch Shell";
        private const string Region = "Everglades";
        private const string ForestEntrance = "Forest Entrance";
        private const string ForestFloor = "Forest Floor";
        private const string CaveMouth = "Cave Mouth";
        private const string Cave = "Cave";
        private const string InnerCave = "Inner Cave";
        private const string GreatWesternOcean = "Great Western Ocean";
        private const string Outskirts = "Outskirts";
        private const string Overworld = "Unknown Place";

        internal static PlayableCharacter GeneratePC()
        {
            var player = new PlayableCharacter("Ben", "25 year old man", new Item(Knife, "A small pocket knife", true))
            {
                Interaction = (i, target) =>
                {
                    if (i != null && Knife.EqualsExaminable(i))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You slash wildly at your own throat. You are dead");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        internal static Overworld GenerateOverworld(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Region, "The starting place");

            var conchShell = new Item(ConchShell, "A pretty conch shell, it is about the size of a coconut", true)
            {
                Interaction = (item, target) =>
                {
                    switch (item.Identifier.IdentifiableName)
                    {
                        case Knife:
                            return new InteractionResult(InteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny pieces. Without the conch shell you are well and truly fucked");
                        default:
                            return new InteractionResult(InteractionEffect.NoEffect, item);
                    }
                }
            };

            var innerCave = new Room(InnerCave, string.Empty, new Exit(CardinalDirection.West), new Exit(CardinalDirection.North, true));

            InteractionCallback innerCaveInteraction = (i, target) =>
            {
                if (i != null && ConchShell.EqualsExaminable(i))
                {
                    innerCave[CardinalDirection.North].Unlock();
                    return new InteractionResult(InteractionEffect.ItemUsedUp, i, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces");
                }

                if (i != null && Knife.EqualsExaminable(i))
                    return new InteractionResult(InteractionEffect.NoEffect, i, "You slash wildly at the bats, but there are too many. Don't aggravate them!");

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            innerCave.Interaction = innerCaveInteraction;
            innerCave.SpecifyConditionalDescription(new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way", () => !innerCave[CardinalDirection.North].IsLocked));

            regionMaker[2, 0] = new Room(ForestEntrance, "You are standing on the edge of a beautiful forest. There is a parting in the trees to the north", new Exit(CardinalDirection.North));
            regionMaker[2, 1] = new Room(ForestFloor, "The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest", new Exit(CardinalDirection.North), new Exit(CardinalDirection.South));
            regionMaker[2, 2] = new Room(CaveMouth, "A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west", new Exit(CardinalDirection.North), new Exit(CardinalDirection.South), new Exit(CardinalDirection.West));
            regionMaker[1, 2] = new Room(GreatWesternOcean, "The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungrily. To the east is a small clearing", new[] { new Exit(CardinalDirection.East) }, conchShell);
            regionMaker[2, 3] = new Room(Cave, "The cave is so dark you struggling to see. A screeching noise is audible to the east", new Exit(CardinalDirection.South), new Exit(CardinalDirection.East));
            regionMaker[3, 3] = innerCave;
            regionMaker[3, 4] = new Room(Outskirts, "A vast chasm falls away before you", new Exit(CardinalDirection.South));
            
            var o = new Overworld(Overworld, "An unknown world");
            o.Regions.Add(regionMaker.Make());
            return o;
        }
    }
}
