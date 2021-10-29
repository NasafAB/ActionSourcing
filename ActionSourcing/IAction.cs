// <copyright file="IAction.cs" company="Nasaf AB, Sweden">
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
    /// <summary>Represents an action that can be performed on T. Implementors must be immutable (preferably a record).</summary>
    /// <typeparam name="T">The type of IStateRoot on which the action is valid.</typeparam>
    public interface IAction<T>
        where T : IStateRoot
    {
        /// <summary>
        /// Applies the action to the specified stateRoot. This function must be pure with respect to state outside
        /// the stateRoot and the action in which it is defined. For example it cannot depend on DateTime.Now.
        /// However it can both read and write to the stateRoot object, and depend on properties defined in the action.
        /// </summary>
        /// <param name="stateRoot">The state root of type IStateRoot.</param>
        void Apply(T stateRoot);

        /// <summary>
        /// Validates if the action can be applied to stateRoot. This function must be pure except for reading the
        /// state of stateRoot and the properties of the action. It must not modify the state of stateRoot in any way.
        /// </summary>
        /// <param name="stateRoot">The state root of type IStateRoot.</param>
        /// <param name="validationResult">The validation result to add validation information to.</param>
        void Validate(T stateRoot, ValidationResult validationResult);
    }
}
