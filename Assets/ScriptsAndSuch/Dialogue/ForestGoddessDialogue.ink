VAR town_health = 0
VAR forest_health = 0
VAR sustainability_lvl = 0

-> main

=== main ===
Hello again young human! It is good to see you again...
Would you like to know how sustainable you have been?
    + [Yes]
        -> yes
    + [No]
        -> no

=== yes ===
Very well! Let me feel the life-force of Gaia and Man...
    -> town

=== town
Hmmmm... I sense for the town...
{town_health < 2:
    No! Your town is in grave danger! Houses are falling apart and people cry!
-else:
    {town_health < 4:
        Indeed, the town is becoming run down, finishing quests would be wise...
    -else:
        {town_health < 6:
            My, your town has become quite pretty! I sense few qualms...
            -else:
                My, what a beautiful and lively town!
        }
    }
}
    -> forest

=== forest
As for the forest, I sense...
{forest_health < 2:
    Oh no! Our forest is dying! There are very few trees and animals left!
-else:
    {forest_health < 4:
        Hmmm... yes the forest is slowly dying, planting seeds would be wise...
    -else:
        {forest_health < 6:
            Oh my, the forest has become quite beautiful!
            -else:
                Yes! Our forest is filled with life! I feel that smooth sun beautifully...
        }
    }
}
    -> sustainability

=== sustainability
{sustainability_lvl > 7:
    Yes, you've done an excellent job, dual worlds in harmony...
- else:
    {sustainability_lvl < 4:
        While there is good here, you still have work to do...
    - else:
        It seems you have a great many things to save!
    }
}
    -> end

=== no ===
Very well, I shall leave you be...
    -> end

=== end
I pray that you continue to return and save this world...

-> END