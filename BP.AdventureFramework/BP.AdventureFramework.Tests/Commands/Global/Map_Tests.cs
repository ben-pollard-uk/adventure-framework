﻿using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands.Global;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exit = BP.AdventureFramework.Parsing.Commands.Global.Exit;

namespace BP.AdventureFramework.Tests.Commands.Global
{
    [TestClass]
    public class Map_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNone()
        {
            var command = new Map(null, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNullMapDrawer_WhenInvoke_ThenNone()
        {
            var command = new Map(new GameStructure.Game(string.Empty, string.Empty, null, null), null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenSelfContained()
        {
            var overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Locations.Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Locations.Exit(CardinalDirection.South)), 0, 1);
            overworld.Regions.Add(region);
            var command = new Map(new GameStructure.Game(string.Empty, string.Empty, new PlayableCharacter(Identifier.Empty, Description.Empty), overworld), new MapDrawer());

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.SelfContained, result.Result);
        }
    }
}
