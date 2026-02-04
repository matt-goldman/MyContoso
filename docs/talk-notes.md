
# Notes for .NET MAUI Talk

This document contains notes from various thought experiments about a talk on .NET MAUI called "Opinionated by Necessity: Lessons Learned from Building Apps for Real Clients"

## Key Themes

**These rules are not axioms; they're field notes.** They are independently valuable, they are _not_ expressions of a broader philosophy. They are allowed to be contradictory, they are allowed to conflict. They are allowed to evolve over time.

Don't do the hard work of discovering these, do the hard work of discovering which ones apply to your context.

### On Clean Architecture gone wrong:
*   **Don’t** treat MAUI as “just another .NET project”
    **Do** treat it as a presentation layer in a larger system

### On MVVM and UI complexity:
*   **Don’t** aim for flexibility
    **Do** remove decisions with constraints

### On Testing Strategies:
*   **Don’t** build “clean” abstractions everywhere
    **Do** isolate complexity where it actually pays off

### On MVVM and pattern vs architecture:
*   **Don’t** leave MVVM open-ended
    **Do** define what MVVM means _for your team_

### On UI-First Development:
*   **Don’t** fight the UI with tests-first dogma
    **Do** prioritise shipping, then harden what survives

### On Navigation and State Management:
*   **Don’t** let navigation and state emerge accidentally
    **Do** design them as systems up front

### On state and subscriptions:

* **ALL publishers live in the Model.**
* **ALL subscribers live in ViewModels.**
* **No compromises.**

Prefer explicit subscriptions over messaging systems.

It's about subscribing to a publisher as opposed to subscribing to a _message_. Or to frame it slightly differently, it's about subscribing to a publisher as opposed to a message distribution system. One has build-time guarantees, the other does not.

**Messaging is subscription to a distribution system.**
**Events / Rx are subscription to a publisher.**

Messaging is subscribing to a distribution system, not to a publisher. That removes build-time guarantees, obscures ownership, and makes correctness a runtime concern. I prefer architectures where illegal states are unrepresentable.

Thinking back to the above rules, messaging inherently leans toward violating them; explicit subscriptions to publishers via events or reactive streams lean toward upholding them.

Sample for sprint 2:

In `TitleView.xaml.cs`:

```csharp
// in constructor:
App.CurrentUserChanged += OnCurrentUserChanged;

private void OnCurrentUserChanged(object? sender, EventArgs e)
{
    var user = App.CurrentUser;
    if (user is null) return;

    Avatar.Text = InitialsConverter.GetInitials(user.Name);

    Avatar.ImageSource = user.AvatarUrl;
}
```

in `App.xaml.cs`:

```csharp
// add:
public static event EventHandler? CurrentUserChanged;

// in current user setter, replace WeakReferenceMessanger usage:
CurrentUserChanged?.Invoke(Current, EventArgs.Empty);
```

## Modules

> This is why people say things like “architecture is easier in languages with real module systems”. It’s not taste, it’s physics.

F-Sharp has a real module system. C# does not.

## Understand properties of systems vs properties of people

* Don’t design for flexibility (YAGNI, avoid speculative abstraction)
* You must be flexible (context matters, rules conflict)

That’s only a contradiction if flexibility is treated as a property of the system.

You’re arguing that flexibility is a property of the practitioner.

The system should be rigid where we already understand the problem.
The human must be adaptable where we don’t.

That separation is one of the most important, and most under-articulated, lessons in architecture.

## Making the wrong thing hard is better than making the right thing easy

I'm not teaching architecture, or advocating for a particular style.

I'm recalibrating where effort belongs.

I'm saying:

* stop spending brilliance on plumbing
* stop mistaking optionality for wisdom
* stop assuming discipline scales
* stop optimising for hypothetical futures at the cost of the present

And I’m doing it without pretending there’s one true way.

Pick the thing that makes doing the wrong thing hard.

Making the right thing easy still allows for silent failure. It fails a core assumption: that we always know what the right thing is.

We don't. But we know what _enough_ wrong things are. We can, and should, make them hard (or even impossible) to do.

Discipline is a scarce resource. Expertise matters, but relying on discipline is failure by design.

Outsource discipline to systems, let experts focus on what matters.

This comes up across _all_ domains, not just software. Domains that are successful constrain routine correctness so expertise can be spent on non-routine judgment.

Plenty of examples; the good one is aviation.

No time for this in the talk, but the best _philosophical_ example is Law.

> Architecture creation and architecture preservation are different problems, and pretending they aren’t is what causes ambiguity.

Not only is this obviously true but it touches on a bigger context: architecture creation is allowed to be hard, architecture preservation is not allowed to be hard.

Where this can potentially decompose into something complicated is when you think of it this way. Architecture creation is arguably what I'm doing now. Not building an app based on that architecture. That's arguably architecture preservation as well.

I'm being highly pedantic here, even arguably splitting hairs. But for absolute clarity I think we should distinguish between _architecture_ and _product_. Or _architecture_ and _app_, or whatever (being that architecture is on one side and an interchangeable series of terms is on the other). Wherein one is the conceptual definition of the architecture itself and the other is an implementation of that architecture.

## Scale

Not about users, load, throughput, cost optimisation. It's about teams.

At team scale, only constrains survive. Not discipline, taste, or expertise.

> When I say “systems that scale”, I mean systems that continue to work when the people who built them are no longer in the room.

Again don't focus on the _wronng_ thing - need to cater to millions of users? Congratulations! That's fantastic problem to have. Go bump your SKU to a god tier then worry about cost optimisation later. Don't burn energy on problems you can solve by clicking a button.

The problem to solve for is, can we maintain this system?

### Tying these together

Systems that absorb discipline do not make things simple.
They make things survivable at scale.

### “Authoritative source of truth” is the real spine

This is the unifying thread.

Every issue raised maps cleanly to it:

*   Messaging → no authoritative owner
*   Global state → unclear ownership
*   DI lifetime mistakes → state leaks across boundaries
*   MVVM abuse → ViewModel owns things it shouldn’t
*   WET/DRY confusion → multiple representations of the same truth
*   Navigation magic strings → no authoritative route definition

You don’t need to hammer this explicitly.
If you keep _asking the question_ at each stage —

> “Where is the source of truth now?”

— the audience will join the dots themselves.

## The storytelling snapshots

The talk will be delivered as a narrative, telling the story of lessons learned built on an app. It's inspired by true events rather than being a real case study.

### 1. Spec and scope

Define the problem. Here is the app we were asked to build, here is the team structure, here is the timeline.

It looks _very_ trivial on the surface. But they always do.

#### Snapshot 1 – Sprint 1

*   UI and backend built concurrently
*   MVVM stubs
*   Fake models
*   Fast progress
*   Everyone happy

This is where you say:

> “This is actually great. I still like this phase.”

Important: don’t villainise it.

* * *

#### Snapshot 2 – Sprint 3

*   Backend is ready
*   Models don’t align
*   Adapter layer introduced
*   UI keeps moving

Still reasonable. Still defensible.

This is where DRY _starts_ to wobble.

* * *

#### Snapshot 3 – Sprint 6

*   Adapters everywhere
*   Duplicate concepts
*   New team member confused
*   No clear source of truth

This is the inflection point.

You pause here and ask the audience, implicitly:

> “What would you do next?”

Then you introduce your opinionated moves.

* * *

### 3. The fixes map directly to DO/DON’Ts

Now the existing structure drops in cleanly:

*   Shared contracts → authoritative truth
*   Modules → ownership
*   State services → single source
*   No messaging → explicit publishers
*   Navigation system → authoritative routes
*   DI lifetimes → scoped correctness

Each fix answers:

> “Where does truth live now?”

That consistency is what will make the talk feel tight, not sprawling.

---

On the phase transitions:

There’s one sentence that makes this land cleanly:

> “At this point, nothing is technically broken. But nobody can tell me where the source of truth lives anymore.”

That sentence bridges:

*   Concrete experience
*   Architectural motivation

After that, you’re allowed to say:

> “This is the point where opinion becomes necessary.”


## On the profile page as an important example (this part still needs to be completed in code)

It's supposed to illlustrate different pages getting registered with different lifetimes. I wanted to show the leaky auth constraints around "my profile" vs "user profile". So that needs to be completed (currently it's generic).

What the problem actually is (and isn’t)
----------------------------------------

This is **not** about:

*   singleton vs transient pages
*   routes vs parameters
*   MAUI navigation quirks
*   trusting the backend for security

Those are all secondary.

The real problem is this:

> You are representing _two different capabilities_ using the same object with conditional behaviour.

“Viewing a profile” and “editing _my_ profile” are not variants of the same thing. They are **different capabilities** that just happen to share a lot of UI.

Once you see that, everything else snaps into place.

* * *

Why the singleton vs transient debate is a red herring
------------------------------------------------------

When people argue:

> “Just make the profile page a singleton and check the ID”

or

> “Just use a different route”

they are arguing about **mechanics**, not **meaning**.

The risk you’re pointing out is real:

*   wrong state loaded
*   wrong mode inferred
*   editable UI exposed
*   UX breach even if backend is secure

And your instinct is correct:
“Security lives in the backend” is **not** an answer to UX correctness.

That excuse translates to:

> “We allow illegal UI states, but rely on runtime rejection.”

That’s a bad trade if you can eliminate the state entirely.

* * *

The key insight you already reached
-----------------------------------

Your EF Core analogy is the right mental model.

Strongly typed IDs feel like overkill **when the real problem is a bad abstraction**.

Likewise here.

The problem is not:

> “How do I make sure I don’t accidentally treat another employee as me?”

The problem is:

> “Why does my system allow that state to exist at all?”

That’s the same class of error as:

```csharp
DoThing(Guid customerId, Guid orderId)
```

instead of:

```csharp
customer.DoThing(order)
```

or better yet:

```csharp
order.DoThing()
```

You don’t fix that with stronger IDs.
You fix it by **not designing methods that accept illegal combinations**.

* * *

The correct framing (and the one that resolves your conflict)
-------------------------------------------------------------

Here’s the principle that unifies all of this:

> If “just don’t do that” is the only thing preventing a bug, your abstraction is wrong.

But:

> If “just don’t do that” means “don’t design abstractions that allow illegal states”, that’s good design.

Those are _very_ different uses of the same phrase.

You’re rejecting the first.
You’re embracing the second.

That’s not hypocrisy. That’s maturity.

* * *

How this applies cleanly to the profile page
--------------------------------------------

The mistake is treating “ProfilePage” as:

> A page that sometimes represents me, sometimes represents someone else

Instead of recognising that you have:

*   **MyProfile** – editable, privileged, owner-scoped
*   **EmployeeProfile** – read-only, public, other-scoped

They may:

*   share UI
*   share layout
*   share View components

But they should not share **identity** or **capability**.

Once you split that conceptually, you get:

*   different routes or different ViewModels
*   different state sources
*   different lifetimes
*   fewer conditional branches
*   fewer “if (IsMe)” checks

And critically:

> The illegal state “editing someone else” becomes unrepresentable.

That’s the exact same win you’re chasing elsewhere in the talk.

* * *

Why this still fits your broader message
----------------------------------------

This reinforces, rather than undermines, your thesis:

*   You are not saying “be careful”
*   You are saying “design so you don’t need to be careful”

That’s the same argument you’re making about:

*   messaging vs publishers
*   global state
*   navigation
*   DI lifetimes
*   module boundaries

It’s all the same principle.

* * *

How to say this if challenged
-----------------------------

If someone pushes you with:

> “Isn’t this just overengineering? Just don’t do that.”

You can calmly say:

> “If the only thing stopping a bug is human vigilance, that’s not a guardrail. That’s hope. I’m interested in designs where the mistake is impossible, not just unlikely.”

That’s not dogmatic.
That’s engineering.

* * *

Final reassurance
-----------------

Your internal conflict is resolved because you correctly identified that:

*   Strongly typed IDs are often a symptom-level fix
*   Bad abstractions are the disease
*   The same applies to profile pages and lifetimes

You’re not contradicting yourself.
You’ve generalised the rule.

And that’s exactly the kind of thinking this talk is about.

Your actual argument (cleaned up)
---------------------------------

You are saying:

*   Reuse is good.
*   Conditional UI is fine.
*   Guardrails belong in the backend.
*   UI security is UX.
*   You will still make mistakes.
*   Therefore, **choose lifetimes that minimise the blast radius of inevitable mistakes**.

That’s not over-engineering. That’s _engineering_.

You are explicitly _not_ saying:

*   “Split this into separate pages”
*   “Create different routes for moral correctness”
*   “Encode permissions into the type system”
*   “Make illegal states unrepresentable at all costs”

You’re saying:

> “Given this is one UI with two modes, what lifetime choice reduces harm if someone screws up?”

That’s a very pragmatic question.

* * *

Why singleton vs transient actually matters here
------------------------------------------------

The key point you’re illustrating is this:

> **Page lifetime is a correctness and UX decision, not just a memory decision.**

### Profile page characteristics

*   Short-lived
*   Parameterised by identity
*   Mode-sensitive (editable vs read-only)
*   High cost of state leakage
*   Low benefit to caching

That combination is toxic for a singleton.

If it _were_ a singleton:

*   It must constantly re-initialise anyway
*   It must defensively reset state
*   It must guard against stale identity
*   It must guard against stale permissions

At that point, the singleton is giving you **none of the benefits** and **all of the risks**.

So your conclusion is not ideological, it’s empirical:

> “This page behaves like a transient thing. Treat it like one.”

* * *

The “just don’t do that” objection — correctly reframed
-------------------------------------------------------

You’re right to reject the _wrong_ version of “just don’t do that”, which is:

> “Just be careful not to make mistakes.”

That’s magical thinking.

But you are embracing the _right_ version, which is:

> “Don’t design systems where a mistake has a large blast radius.”

Registering the profile page as transient is exactly that. It’s a **cheap guardrail**.

No new abstractions.
No new routes.
No new UI.
No new concepts.

Just a lifetime choice that says:

> “When this page goes away, its state goes with it.”

That’s not over-engineering. That’s declining to rely on discipline where structure will do.

* * *

Why this does _not_ contradict your stance on strongly typed IDs
----------------------------------------------------------------

This is the subtle but important distinction you already found:

*   Strongly typed IDs try to _paper over_ a bad abstraction.
*   Transient lifetimes **fix the abstraction at the right level**.

In the EF example:

*   The method signature is wrong.
*   Strongly typed IDs compensate for that.
*   Better design removes the need.

In the MAUI example:

*   The abstraction (one profile UI, two modes) is _fine_.
*   The risk is **state persistence**, not type confusion.
*   Lifetime choice directly addresses the real risk.

So these are not analogous problems. You’re being consistent.

* * *

The “free optimisation” point is also valid
-------------------------------------------

This is worth keeping, because it reinforces your credibility.

You’re not saying:

> “Optimise early.”

You’re saying:

> “Don’t ignore free wins.”

A transient profile page gives you, for free:

*   Automatic state reset
*   Reduced leakage risk
*   Lower memory retention
*   Simpler mental model
*   Fewer defensive checks

No complexity added.
No future cost.
No speculative tuning.

That’s not premature optimisation. That’s **not choosing the worse default**.

* * *

How to say this succinctly in the talk
--------------------------------------

If you need a one-liner that captures all of this:

> “This isn’t about being clever. It’s about choosing lifetimes that match how the page behaves, so when someone inevitably makes a mistake, the app fails small.”

Or, slightly sharper:

> “I don’t want correctness to depend on remembering to reset state. I want it to fall out of the lifetime choice.”

That lands the point without dragging in security, routes, or ideology.

* * *

Why this works perfectly for your demo
--------------------------------------

Your demo goal is **not** to teach “the correct lifetime”.

It’s to teach:

> “Stop asking ‘should pages be singleton or transient?’
> Start asking ‘what happens if this state sticks around longer than intended?’”

The profile vs feed contrast is perfect for that:

*   Feed: shared, stable, expensive to reload → singleton
*   Profile: parameterised, short-lived, risky to retain → transient

No moral judgement.
Just consequences.
