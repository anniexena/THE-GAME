VAR questid = 1
VAR questDescription = "Collect 10 wood"
VAR questItem = "Pine"
VAR questAmount = 10
VAR questActive = false

-> main

=== main ===
Hello! I am the Lumberjack of this town.
You look strong. Could you help me?
I need to cut down some trees but I hurt my back.
Could you help me out?
    + [Sure]
        -> help
    + [No, I'm busy...]
        -> leave

=== help ===
Cool! Just head over and cut down some trees.
I need at least 10 wood.
~questActive = true
    -> end

=== leave ===
I understand. Let me know if you can help!
    -> end
    
=== end

-> END