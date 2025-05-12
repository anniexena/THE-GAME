VAR followPlayer = true
VAR petting = false

-> main

=== main ===
Bark bark!
    + [Pet Jeff]
        -> pet
    + [Wait here]
        -> wait

=== pet ===
Woof!
~ petting = true
    -> end

=== wait ===
\*whines sadly*
~ followPlayer = false
    -> end

=== end
    -> END
    