VAR houseBroken = 0
VAR houseCost = 0
VAR town_health = 0
VAR forest_health = 0
VAR houseType = 0

-> main

=== main ===
Hello there! It is good to run into you...
{town_health < 2:
    for the town is in grave danger! Houses are falling apart and our livelihood falls!
-else:
    {town_health < 4:
        for the town is becoming run down, if you could collect wood that would be best.
    -else:
        {town_health < 6:
            for the town has become quite pretty! Only a few houses require repairs.
            -else:
                for the town is quite incredible!
        }
    }
}
    -> forest

=== forest
{forest_health < 2:
    And the forest is dying! There are few trees and animals left in our world!
-else:
    {forest_health < 4:
        Also, the forest is slowly becoming dead, planting seeds for animals would be best.
    -else:
        {forest_health < 6:
            And I must say the forest has become quite beautiful!
            -else:
                for the forest is filled with life! I feel that smooth sun beautifully
        }
    }
}
    -> house

=== house
{houseBroken == 0:
    Finally, you've done good on your duties and helped maintain my house, many thanks!
- else:
    {houseBroken == 1:
        ~ temp msg = "You've even helped maintain my house, though I could definitely use " + houseCost + " " + houseType + " wood for repairs..."
        {msg}
    - else:
        ~ temp msg2 = "Lastly, I must plead you to give me the " + houseCost + " " + houseType + " wood I need for my house!"
        {msg2}
    }
}
    -> end

=== end

-> END