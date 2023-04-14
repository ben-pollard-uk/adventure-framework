﻿using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Global
{
    [TestClass]
    public class New_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNoReaction()
        {
            var command = new New(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenReacted()
        {
            var command = new New(new GameStructure.Game(string.Empty, string.Empty, null, null));

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}