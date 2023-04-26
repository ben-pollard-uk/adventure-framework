﻿using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    public class TitleFrameBuilder : ITitleFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TitleFrameBuilder class.
        /// </summary>
        /// <param name="frameDrawer">A drawer to use for the frame.</param>
        public TitleFrameBuilder(FrameDrawer frameDrawer)
        {
            FrameDrawer = frameDrawer;
        }

        #endregion

        #region Implementation of ITitleFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string title, string description, int width, int height)
        {
            var divider = FrameDrawer.ConstructDivider(width);
            var constructedScene = divider;
            constructedScene += FrameDrawer.ConstructWrappedPaddedString(title, width, true);
            constructedScene += divider;
            constructedScene += FrameDrawer.ConstructWrappedPaddedString(description, width, true);
            constructedScene += divider;
            constructedScene += FrameDrawer.ConstructPaddedArea(width, height / 2 - FrameDrawer.DetermineLinesInString(constructedScene));
            constructedScene += FrameDrawer.ConstructWrappedPaddedString("Press Enter to start", width, true);
            constructedScene += FrameDrawer.ConstructPaddedArea(width, height - FrameDrawer.DetermineLinesInString(constructedScene) - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}