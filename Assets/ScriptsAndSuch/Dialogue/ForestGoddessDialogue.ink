VAR sustainability_lvl = 0

-> main

=== main ===
Greetings! I am the Forest Goddess.
Would you like to know how sustainable you have been?
    + [Yes]
        -> yes
    + [No]
        -> no

=== yes ===
Great!
Hmm... Let's see...
{sustainability_lvl > 10:
    You are taking good care of the forest!
- else:
    {sustainability_lvl < 0:
        The forest is suffering!
    - else:
        You have a good balance!
    }
}
    -> end

=== no ===
I understand.
    -> end
    
=== end
Come back to me to see how you the forest is doing!

-> END