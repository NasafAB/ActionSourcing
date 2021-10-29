// <copyright file="StateManagerExtensions.cs" company="Nasaf AB, Sweden">
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

namespace NasafAB.ActionSourcing.Hashing
{
    using System.Linq;
    using System.Security.Cryptography;

    /// <summary>Hash extensions for <see cref="IStateManager{TStateRoot, TAction}"/>.</summary>
    public static class StateManagerExtensions
    {
        /// <summary>Gets the computed chain hash from all applied actions to this
        /// <see cref="IStateManager{TStateRoot, TAction}"/>.</summary>
        /// <typeparam name="TStateRoot">The type of the state root.</typeparam>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="stateManager">The state manager.</param>
        /// <returns>The SHA256 chain hash.</returns>
        public static byte[] GetCurrentHash<TStateRoot, TAction>(this IStateManager<TStateRoot, TAction> stateManager)
            where TStateRoot : IStateRoot, new()
            where TAction : IHashableAction<TStateRoot>
        {
            var sha256 = SHA256.Create();
            var currentHash = new byte[32];
            foreach (var action in stateManager.AppliedActions)
            {
                currentHash = sha256.ComputeHash(currentHash.Concat(action.GetSha256()).ToArray());
            }

            return currentHash;
        }
    }
}
