// <copyright file="StateManagerFactory.cs" company="Nasaf AB, Sweden">
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
    /// <summary>Factory for creating instances of <see cref="IStateManager{TStateRoot, TAction}"/>.</summary>
    public static class StateManagerFactory
    {
        /// <summary>Creates a new <see cref="IStateManager{TStateRoot, TAction}"/>.</summary>
        /// <typeparam name="TStateRoot">The type of <see cref="IStateRoot"/>.</typeparam>
        /// <typeparam name="TAction">The type of <see cref="IAction{TStateRoot}"/>.</typeparam>
        /// <returns>A new <see cref="IStateManager{TStateRoot, TAction}"/>. State will always be initial for the
        /// specified <see cref="IStateRoot"/>.</returns>
        public static IStateManager<TStateRoot, TAction> CreateStateManager<TStateRoot, TAction>()
            where TStateRoot : IStateRoot, new()
            where TAction : IAction<TStateRoot>
        {
            return new StateManager<TStateRoot, TAction>();
        }
    }
}
