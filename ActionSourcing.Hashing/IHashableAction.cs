// <copyright file="IHashableAction.cs" company="Nasaf AB, Sweden">
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
    /// <summary>A type of <see cref="IAction{T}"/> that can be hashed.</summary>
    /// <typeparam name="T">The type of IStateRoot.</typeparam>
    public interface IHashableAction<T> : IAction<T>
        where T : IStateRoot
    {
        /// <summary>
        /// Gets the SHA256 hash of this action.
        /// Implementors are free to choose their hashing method, but must make sure that no hash collision can happen between actions with different values (for example: an action with two strings as parameters where the hash is calculated based on just the concatenation of the strings would be bad, as "aa" + "b" would collide with "a" + "ab").
        /// </summary>
        /// <returns>The 32 byte long SHA256 hash value.</returns>
        byte[] GetSha256();
    }
}
