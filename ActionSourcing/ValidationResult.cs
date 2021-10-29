// <copyright file="ValidationResult.cs" company="Nasaf AB, Sweden">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Contains the result of a validation of an action.</summary>
    public sealed record ValidationResult
    {
        private readonly List<ValidationError> errors = new ();
        private bool validationCompleted = false;

        /// <summary>Gets all validation errors.</summary>
        /// <value>The validation errors.</value>
        public IReadOnlyList<ValidationError> Errors => this.errors.AsReadOnly();

        /// <summary>Returns true if the ValidationResult contains no errors.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid() => !this.Errors.Any();

        /// <summary>Adds a validation error to the result.</summary>
        /// <param name="error">The error to add.</param>
        public void AddError(ValidationError error)
        {
            if (this.validationCompleted)
            {
                throw new InvalidOperationException("Errors cannot be added after validation has been completed.");
            }

            this.errors.Add(error);
        }

        /// <summary>Mark the validation as completed, and prevent new errors from being added essentially making this instance immutable.</summary>
        internal void CompleteValidation()
        {
            this.validationCompleted = true;
        }
    }
}
