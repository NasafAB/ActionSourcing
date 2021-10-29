// <copyright file="StateManager.cs" company="Nasaf AB, Sweden">
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

    /// <summary>Implementation of <see cref="IStateManager{TStateRoot, TAction}"/>.</summary>
    /// <typeparam name="TStateRoot">Type of <see cref="IStateRoot"/> to manage.</typeparam>
    /// <typeparam name="TAction">Type of <see cref="IAction{TStateRoot}"/> to manage.</typeparam>
    public class StateManager<TStateRoot, TAction> : IStateManager<TStateRoot, TAction>
        where TStateRoot : IStateRoot, new()
        where TAction : IAction<TStateRoot>
    {
        private readonly TStateRoot state = new ();
        private readonly List<TAction> appliedActions = new ();
        private object applyValidateLock = new object();

        /// <summary>Gets the state.</summary>
        /// <value>The state.</value>
        public TStateRoot State => this.state;

        /// <summary>Gets all actions that have been applied to reach the current state. The order of the list
        /// represents the order in which the actions have been applied.</summary>
        /// <value>The applied actions.</value>
        public virtual IReadOnlyList<TAction> AppliedActions => this.appliedActions.AsReadOnly();

        /// <summary>Applies the action if it's valid. If unvalid, throws an InvalidActionException.</summary>
        /// <param name="action">The action to apply.</param>
        /// <exception cref="InvalidActionException">The action is not valid.</exception>
        public virtual void ApplyAction(TAction action)
        {
            lock (this.applyValidateLock)
            {
                var validationResult = this.ValidateAction(action);
                if (!validationResult.IsValid())
                {
                    throw new InvalidActionException(validationResult);
                }

                action.Apply(this.state);
                this.appliedActions.Add(action);
            }
        }

        /// <summary>Validates the action.</summary>
        /// <param name="action">The action.</param>
        /// <returns>The validation result.</returns>
        public virtual ValidationResult ValidateAction(TAction action)
        {
            lock (this.applyValidateLock)
            {
                var validationResult = new ValidationResult();
                action.Validate(this.State, validationResult);
                validationResult.CompleteValidation();
                return validationResult;
            }
        }
    }
}
