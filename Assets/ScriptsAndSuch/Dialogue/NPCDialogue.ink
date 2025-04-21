VAR houseBroken = false

-> main

=== main ===
Hello! I am NPC.
{houseBroken == 0:
    This world is beautiful, and my house is good
- else:
    {houseBroken == 1:
        My house is starting to get worse...
    -else:
        Please help, my house is falling apart!
    }
}
    -> end

=== end

-> END