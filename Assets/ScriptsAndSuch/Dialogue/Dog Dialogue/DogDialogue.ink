VAR followPlayer = false

-> main

=== main ===
Woof woof!
    + [Hey little guy!]
        -> greet
    + [Go away, mutt.]
        -> dismiss

=== greet ===
Bark bark!
    + [Do you want to come with me?]
        -> follow
    + [I'll see you later]
        -> leave


=== follow ===
Arf arf!
~ followPlayer = true
    ->end

=== dismiss ===
\*whines sadly*
    -> end

=== leave ===
Arf arf!
    -> end

=== end

-> END