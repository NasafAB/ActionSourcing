// <copyright file="IStateRoot.cs" company="Nasaf AB, Sweden">
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
    /// <summary>
    ///   <para>Represents a root of the state that should be action sourced. Implementors are free to layout the state
    ///   as they see fit, bearing in mind the following considerations:</para>
    ///   <list type="bullet">
    ///     <item>IStateRoot and properties contained within can be implemented as both classes or records.</item>
    ///     <item>IStateRoot and properties contained within can not in any way depend on state outside of IStateRoot.</item>
    ///     <item>Outside users of this object/record must not in any way modify the state of the root or any
    ///     sub-states directly, any modification has to be made through an <see cref="IAction{T}"/>.</item>
    ///     <item>One way to do this is to make all publicly accessible properties/methods immutable and read-only
    ///     (including lists, and objects within lists), while enabling state-modifying methods/properties internal.
    ///     The actions will have to be defined in the same assembly (this might be a good thing since it gives total
    ///     control over available actions, at the expense of extensibility).</item>
    ///     <item>Implementation should contain an empty constructor which will set the inital state.</item>
    ///   </list>
    /// </summary>
    public interface IStateRoot
    {
    }
}
