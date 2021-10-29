// <copyright file="InvalidActionException.cs" company="Nasaf AB, Sweden">
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
    using System;

    /// <summary>Thrown when an operation can't be performed because an action is invalid.</summary>
    public class InvalidActionException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidActionException" /> class.</summary>
        /// <param name="validationResult">The validation result which caused the exception to be thrown.</param>
        public InvalidActionException(ValidationResult validationResult)
            : base()
        {
            this.ValidationResult = validationResult;
        }

        /// <summary>Gets the validation result which caused the exception to be thrown.</summary>
        /// <value>The validation result.</value>
        public ValidationResult ValidationResult { get; }
    }
}
