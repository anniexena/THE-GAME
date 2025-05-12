VAR questid = 1
VAR questItem = "Pine"
VAR questAmount = 10
VAR questActive = true
VAR turnedIn = false

-> main

=== main ===
Oh! I see that you have the wood that I asked for.
I can take that off your hands now.
    + [Sure, here you go]
        -> turn_in
    + [I think I'll keep it]
        -> leave

=== turn_in ===
Thanks so much for your help. The town will appreciate this.
~ turnedIn = true
    -> end

=== leave ===
I understand. Come back when you have it!
    -> end
    
=== end

-> END