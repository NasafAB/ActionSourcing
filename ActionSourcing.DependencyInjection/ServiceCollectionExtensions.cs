// <copyright file="ServiceCollectionExtensions.cs" company="Nasaf AB, Sweden">
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

namespace NasafAB.ActionSourcing.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>Extension methods for IServiceCollection.</summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>Adds the state manager of the specified IStateRoot T to the service collection.</summary>
        /// <typeparam name="TStateRoot">The type of <see cref="IStateRoot"/>.</typeparam>
        /// <typeparam name="TAction">The type of <see cref="IAction{TStateRoot}"/>.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="stateLifetime">The state lifetime.</param>
        public static void AddStateManager<TStateRoot, TAction>(
            this IServiceCollection services,
            ServiceLifetime stateLifetime = ServiceLifetime.Singleton)
            where TStateRoot : IStateRoot, new()
            where TAction : IAction<TStateRoot>
        {
            static IStateManager<TStateRoot, TAction> FactoryFunc(IServiceProvider sp) =>
                StateManagerFactory.CreateStateManager<TStateRoot, TAction>();
            switch (stateLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(FactoryFunc);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(FactoryFunc);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(FactoryFunc);
                    break;
                default:
                    break;
            }
        }
    }
}
