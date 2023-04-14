﻿using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Frame;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class CommandsOn_Tests
    {
        [TestMethod]
        public void GivenNullFrameDrawer_WhenInvoke_ThenNoReaction()
        {
            var command = new CommandsOn(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenValidFrameDrawer_WhenInvoke_ThenReacted()
        {
            var command = new CommandsOn(new FrameDrawer());

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}