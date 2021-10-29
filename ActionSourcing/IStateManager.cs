// <copyright file="IStateManager.cs" company="Nasaf AB, Sweden">
//     Copyright 2021 Nasaf AB, Sweden (info@nasaf.se).
//
//     This file is part of NasafAB.ActionSourcing.
//
//     NasafAB.ActionSourcing is free software: you can redistribute it and/or modify
//     it under the terms of the GNU Lesser General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
//
//     NasafAB.ActionSourcing is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU Lesser General Public License for more details.
//
//     You should have received a copy of the GNU Lesser General Public License
//     along with Foobar.  If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace NasafAB.ActionSourcing
{
    using System.Collections.Generic;

    /// <summary>
    /// Manages the state, in the sense that all actions have to be applied through it. It stores both the state and
    /// the history of actions that have been applied to reach the current state.
    /// </summary>
    /// <typeparam name="TStateRoot">Type of <see cref="IStateRoot"/> to manage.</typeparam>
    /// <typeparam name="TAction">Type of <see cref="IAction{TStateRoot}"/> to manage.</typeparam>
    public interface IStateManager<TStateRoot, TAction>
        where TStateRoot : IStateRoot, new()
        where TAction : IAction<TStateRoot>
    {
        /// <summary>Gets the state.</summary>
        /// <value>The state.</value>
        TStateRoot State { get; }

        /// <summary>Gets all actions that have been applied to reach the current state. The order of the list
        /// represents the order in which the actions have been applied.</summary>
        /// <value>The applied actions.</value>
        IReadOnlyList<TAction> AppliedActions { get; }

        /// <summary>Applies the action if it's valid.</summary>
        /// <param name="action">The action to apply.</param>
        /// <exception cref="InvalidActionException">The action is not valid.</exception>
        void ApplyAction(TAction action);

        /// <summary>Validates the action.</summary>
        /// <param name="action">The action.</param>
        /// <returns>The validation result.</returns>
        ValidationResult ValidateAction(TAction action);
    }
}
