﻿using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Represents any object that can act as an interpreter for input.
    /// </summary>
    internal interface IInterpreter
    {
        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        InterpretationResult Interpret(string input, Game game);
    }
}
