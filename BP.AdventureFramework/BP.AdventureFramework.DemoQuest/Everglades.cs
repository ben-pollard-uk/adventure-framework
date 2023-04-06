﻿using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.DemoQuest
{
    public static class Everglades
    {
        public static PlayableCharacter GeneratePC()
        {
            var player = new PlayableCharacter("Ben", "25 year old man", new Item("Knife", "A small pocket knife", true))
            {
                Interaction = (i, target) =>
                {
                    if (i != null && i.Name.ToUpper() == "KNIFE")
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You slash wildly at your own throat. You are dead");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        public static Overworld GenerateOverworld(PlayableCharacter pC)
        {
            var r = new Region("Everglades", "The starting place");
            r.CreateRoom(new Room("Forest Entrance", new Description("You are standing on the edge of a beautiful forest. There is a parting in the trees to the north"), new Exit(CardinalDirection.North)), 2, 0);
            r.CreateRoom(new Room("Forest Floor", new Description("The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest"), new Exit(CardinalDirection.North), new Exit(CardinalDirection.South)), 2, 1);
            r.CreateRoom(new Room("Cave Mouth", new Description("A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west"), new Exit(CardinalDirection.North), new Exit(CardinalDirection.South), new Exit(CardinalDirection.West)), 2, 2);

            var conchShell = new Item("Conch shell", "A pretty conch shell, it is about the size of a coconut", true)
            {
                Interaction = (item, target) =>
                {
                    switch (item.Name.ToUpper())
                    {
                        case "KNIFE":
                            return new InteractionResult(InteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny peices. Without the conch shell you are well and truly fucked");
                        default:
                            return new InteractionResult(InteractionEffect.NoEffect, item);
                    }
                }
            };

            r.CreateRoom(new Room(name: "Great Western Ocean", new Description("The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungerly. To the east is a small clearing"), new[] { new Exit(CardinalDirection.East) }, conchShell), 1, 2);
            r.CreateRoom(new Room("Cave", new Description("The cave is so dark you struggling to see. A screeching noise is audible to the east"), new Exit(CardinalDirection.South), new Exit(CardinalDirection.East)), 2, 3);

            var innerCave = new Room("Inner Cave", new Description(string.Empty), new Exit(CardinalDirection.West), new Exit(CardinalDirection.North, true));

            InteractionCallback innerCaveInteraction = (i, target) =>
            {
                if (i != null && i.Name.ToUpper() == "CONCH SHELL")
                {
                    innerCave[CardinalDirection.North].Unlock();
                    return new InteractionResult(InteractionEffect.ItemUsedUp, i, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces");
                }

                if (i != null && i.Name.ToUpper() == "KNIFE")
                    return new InteractionResult(InteractionEffect.NoEffect, i, "You slash wildy at the bats, but there are too many. Don't aggravate them!");

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            innerCave.Interaction = innerCaveInteraction;
            innerCave.SpecifyConditionalDescription(new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way", () => !innerCave[CardinalDirection.North].IsLocked));
            r.CreateRoom(innerCave, 3, 3);
            r.CreateRoom(new Room("Outskirts", new Description("A vast chasm falls away before you"), new Exit(CardinalDirection.South)), 3, 4);
            r.SetStartRoom(0);

            var o = new Overworld("A place", "Who knows");
            o.CreateRegion(r, 0, 0);
            return o;
        }
    }
}