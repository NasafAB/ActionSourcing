# Action Sourcing

Action sourcing is combining the concepts of event sourcing with purity.

Instead of generating events from the domain model as a result of applying unpure actions, we instead let the actions
be pure and we can both create and recreate the state in exactly the same way.

## License

This library is licensed under the GNU Lesser General Public License version 3 or later.

## Limitations

Action sourcing is only meant to be used for applications where it makes sense. Do not use it if you don't understand
how it works. Do not use it if you have lots and lots of state changes as this will become slow.

## How does it work?

First of all, you need to define a state that you want to manage using action sourcing. You do this by implementing
`IStateRoot`. There are special requirements that you need to keep in mind when doing this:

- IStateRoot and properties contained within can be implemented as both classes or records.
- IStateRoot and properties contained within can not in any way depend on state outside of IStateRoot.
- Outside users of this object/record must not in any way modify the state of the root or any sub-states directly, any
  modification has to be made through an Action (more on this soon).
- One way to do this is to make all publicly accessible properties/methods immutable and read-only (including lists,
  and objects within lists), while enabling state-modifying methods/properties internal. The actions will have to be
  defined in the same assembly (this might be a good thing since it gives control over available actions, albeit at
  the expense of extensibility).
- Implementation should contain an empty constructor which will set the inital state.

When you have definied your `IStateRoot`, the next step is to define Actions that can be used to manipulate the state.
Actions inherit from `IAction<T>` where `T` is the type of the `IStateRoot` that you just created. Actions should be
immutable (records).

Actions are free to manipulate the state inside `IStateRoot`, however it must do so without using state outside of the
`IStateRoot` and the Action itself. A bad action would for example use `DateTime.Now` which means it's going to behave
differently if applied now or one hour from now. A good action would have a property `DateTime Now { get; init; }` that
the client would set upon initialization of that action.

The final piece of the puzzle is the `IStateManager`. This keeps track of 1) the current state and 2) all actions that
have been applied to get to this state. A new `IStateManager` (created through `StateManagerFactory` or through the DI
if you use that additional package) always come with an initial state and no actions applied. Control the lifetime of
this instance as you see fit for your application.

## Action hashing

Hashing of actions provides a way to verify the integrity of a series of applied actions to an initial state. One usage
is to get them timestamped so you can prove that a certain state existed at a point in time, but there are certainly
other applications too for the creative.

To use action hashing, you need to use the additional package `NasafAB.ActionSourcing.ActionHashing`. Instead of basing
the actions on `IAction<T>` you need to base them on `IHashableAction<T>` and implement the hashing function yourself
for each action. It is important to avoid collisions between different valid states of the actions, the implementor is
responsible for that.

For now the hash used is SHA256 only. This might be extended in the future if there is need...

## Persistence

This is still a TODO, but the idea is to save the list of applied actions and replay them on load. Step 2 could be to
enable saving snapshots of the state for faster load (optionally, as this means one has to additionaly implement
serialization of `IStateRoot` in addition to each action).
