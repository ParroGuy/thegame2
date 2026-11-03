using System;
using System.Threading;

namespace GetMeOutOfHere
{
    public class Program
    {
        static string subject;
        static int headteacherChance;
        static readonly object _consoleLock = new object();

        static void Main(string[] args)
        {
            Console.Clear();
            typewriter("Welcome to Get Me Out of Here! \n", ConsoleColor.Green);
            difficulty();

            typewriter($"You are stuck in a classroom doing {subject} work! \n", ConsoleColor.Green);
            Dialogue("Mcdonalds....", "YOU", ConsoleColor.Cyan);
            typewriter("You have 1 minute to get out! \n", ConsoleColor.Green);
            typewriter("Good luck... \n", ConsoleColor.Red, 70, false);

            Console.WriteLine("\nPress Enter to begin...");
            Console.ReadLine();

            Console.Clear();

            Thread timerThread = timer();
            room();
            teacherBattle();

            if (headteacherChance == 0)
            {
                headteacher();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"====================================================
                                            BUSTED BY THE HEADTEACHER! YOU LOSE!             
                                ====================================================");
                Console.ResetColor();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"====================================================
                                            CONGRATULATIONS! YOU ESCAPED!             
                                ====================================================");
                Console.ResetColor();
            }
            
            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        static Thread timer(double time = 1.0)
        {
            Thread timerThread = new Thread(() =>
            {
                int remaining = (int)(time * 60);

                while (remaining > 0)
                {
                    lock (_consoleLock)
                    {
                        int currentLeft = Console.CursorLeft;
                        int currentTop = Console.CursorTop;

                        Console.SetCursorPosition(0, 0);
                        Console.Write($"=== Time remaining: {remaining / 60:00}:{remaining % 60:00} ===           ");
                        Console.CursorVisible = true;

                        if (currentTop < 2) currentTop = 2;
                        Console.SetCursorPosition(currentLeft, currentTop);
                    }

                    Thread.Sleep(1000);
                    remaining--;
                }

                lock (_consoleLock)
                {
                    Console.Clear();
                    Console.WriteLine("Time's up! You lose.");
                    Environment.Exit(0);
                }
            });
            timerThread.IsBackground = true;
            timerThread.Start();
            return timerThread;
        }

        static void difficulty()
        {
            Random rand = new Random();
            var subjects = new[] { "Math", "Physics", "Engineering", "Computer Science" };

            int chosen = rand.Next(0, subjects.Length);
            headteacherChance = rand.Next(0, 4);
            subject = subjects[chosen];
        }

        static void room()
        {
            lock (_consoleLock)
            {
                Console.SetCursorPosition(0, 2);
                for (int i = 2; i < Console.WindowHeight; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, 2);
            }

            if (subject == "Math")
            {
                bool keyAcquired = false;

                Dialogue("I can't believe we have to do math again!", "Student", ConsoleColor.Cyan);
                Console.SetCursorPosition(0, 5);
                Console.WriteLine(@"
╔══════════════════════════════════════════════════╗
║                 MATHS CLASSROOM                  ║
╚══════════════════════════════════════════════════╝

    [WHITEBOARD]
    ┌──────────────────────┐
    │  f(x) = ax² + bx + c │
    └──────────────────────┘

            [TEACHER DESK]
          ┌──────────────────┐
          │    CHAIR   🔑    │
          └──────────────────┘

    [DESK]        [DESK]        [DESK]
     (o_o)         (o_o)         (o_o)

    [DESK]        [DESK]        [DESK]
     (o_o)         (o_o)         (o_o)

 ┌──────────┐
 │  [DOOR]  │
 └──────────┘
  YOU CAN INTERACT WITH THE OBJECTS WITH SQARE BRACKETS AROUND THEM
────────────────────────────────────────────────────
");

                Console.WriteLine("\nType where you want to go to first:");

                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine().ToLower().Trim();

                    switch (input)
                    {
                        case "door":
                            Console.WriteLine("You try to open the door.");
                            if (keyAcquired)
                            {
                                Console.WriteLine("You use the key to unlock the door and escape!");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("The door is locked! You need to find a key.");
                            }
                            break;
                        case "teacher desk":
                        case "desk":
                        case "teacher's desk":
                            Console.WriteLine("You sit in the teacher's chair.");
                            Dialogue("I'm the captain now", "YOU", ConsoleColor.Green);
                            Console.WriteLine("You look around the room and see a key on the teacher's desk.");
                            Dialogue("Wonder what this will open?", "Key Acquired!", ConsoleColor.Yellow);
                            keyAcquired = true;
                            break;
                        case "whiteboard":
                            Console.WriteLine("You walk over to the whiteboard and see a complex equation written on it.");
                            break;
                        default:
                            Console.WriteLine("You don't see that here.");
                            break;
                    }
                }
            }
            else if (subject == "Physics")
            {
                bool AcidAcquired = false;
                bool SteelBallsAcquired = false;

                Dialogue("What is going on with the physics teacher???", "Student", ConsoleColor.Cyan);
                Console.SetCursorPosition(0, 5);
                Console.WriteLine(@"
╔══════════════════════════════════════════════════╗
║                PHYSICS CLASSROOM                 ║
╚══════════════════════════════════════════════════╝

                        [WINDOW]
                     ┌────────────┐
                     │            │
                     └────────────┘

         ___                               WHITEBOARD
        / ! \                             ┌──────────────┐
       │ACID│                             │   F = ma     │
        \___/                             │   E = mc²    │
                                          └──────────────┘
        [LAB TABLE] 
     ┌──────────────────┐
     │  o---o---o---o   │  <-- NEWTON'S CRADLE
     └──────────────────┘

                        @ [YOU]

 ┌──────────┐
 │  [DOOR]  │  ---> WELDED SHUT!
 └──────────┘
 YOU CAN INTERACT WITH THE OBJECTS WITH SQARE BRACKETS AROUND THEM
────────────────────────────────────────────────────
");

                Console.WriteLine("\nType where you want to go to first:");

                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine().ToLower().Trim();

                    switch (input)
                    {
                        case "door":
                            Console.WriteLine("You run for the door.");
                            Dialogue("The door is welded shut! You need to find a way to open it.", "YOU", ConsoleColor.Cyan);
                            break;
                        case "acid":
                            Dialogue("You see a beaker of acid on the lab table. It might be useful.", "YOU", ConsoleColor.Cyan);
                            Dialogue("You acquire the acid!", "ACID ACQUIRED", ConsoleColor.Yellow);
                            AcidAcquired = true;
                            break;
                        case "lab table":
                        case "table":
                            Console.WriteLine("You see a newton's cradle on the lab table.");
                            Dialogue("You observe the newton's cradle in motion.", "YOU", ConsoleColor.Cyan);
                            Dialogue("The strings snap....", "ACTION", ConsoleColor.Cyan);
                            Dialogue("WHOOPS! The newton's cradle is broken!", "YOU", ConsoleColor.Cyan);
                            Dialogue("Steel Balls Acquired!", "STEEL BALLS ACQUIRED", ConsoleColor.Yellow);
                            SteelBallsAcquired = true;
                            break;
                        case "window":
                            Console.WriteLine("You look out the window and see a beautiful view of the campus.");
                            if (SteelBallsAcquired)
                            {
                                Dialogue("You throw the steel balls out the window and break it open!", "YOU", ConsoleColor.Cyan);
                                Dialogue("You escape through the window!", "YOU", ConsoleColor.Green);
                                return;
                            }
                            else if (AcidAcquired)
                            {
                                Dialogue("You pour the acid on the window and it melts through!", "YOU", ConsoleColor.Cyan);
                                Dialogue("You escape through the window!", "YOU", ConsoleColor.Green);
                                return;
                            }
                            else
                            {
                                Dialogue("The window is locked! You need to find a way to open it.", "YOU", ConsoleColor.Cyan);
                            }
                            break;
                        default:
                            Console.WriteLine("You don't see that here.");
                            break;
                    }
                }
            }
            else if (subject == "Engineering")
            {
                bool blueprintAcquired = false;
                bool toolAcquired = false;

                Dialogue("The engineering teachers have gone insane!!!", "Student", ConsoleColor.Cyan);
                Dialogue("We need to find a way to escape!", "Student", ConsoleColor.Cyan);
                Console.SetCursorPosition(0, 5);
                Console.WriteLine(@"
╔══════════════════════════════════════════════════╗
║              ENGINEERING CLASSROOM               ║
╚══════════════════════════════════════════════════╝

   [WORK BENCH]            [BLUEPRINT TABLE]
  ┌─────────────┐         ┌─────────────────┐
  │ 🔨 HAMMER   │         │ 📐 BLUEPRINT    │
  │ 🪛 SCREW    │         │ 🔋 DRILL        │
  └─────────────┘         └─────────────────┘

     DESK          DESK          DESK
     (o_o)         (o_o)         (o_o)

                                           WHITEBOARD
                                          ┌──────────────┐
                                          │ BRIDGE BUILD │
                                          └──────────────┘
                         @ [YOU]

 ┌──────────┐
 │  [DOOR]  │  ---> LOCKED BUT WEAK
 └──────────┘
YOU CAN INTERACT WITH THE OBJECTS WITH SQARE BRACKETS AROUND THEM
────────────────────────────────────────────────────
");

                Console.WriteLine("\nType where you want to go to first:");

                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine().ToLower().Trim();

                    switch (input)
                    {
                        case "door":
                            Console.WriteLine("You approach the door.");
                            if (toolAcquired)
                            {
                                Dialogue("You use the tool to force the door open!", "YOU", ConsoleColor.Cyan);
                                Dialogue("The door creaks open! You escape!", "YOU", ConsoleColor.Green);
                                return;
                            }
                            else
                            {
                                Dialogue("The door is locked but the wood is weak. You need a tool!", "YOU", ConsoleColor.Cyan);
                            }
                            break;
                        case "blueprint":
                        case "blueprint table":
                            Console.WriteLine("You examine the blueprint.");
                            Dialogue("The blueprint shows a complex bridge design.", "YOU", ConsoleColor.Cyan);
                            if (!blueprintAcquired)
                            {
                                Dialogue("You acquire the blueprint!", "BLUEPRINT ACQUIRED", ConsoleColor.Yellow);
                                blueprintAcquired = true;
                            }
                            break;
                        case "bench":
                        case "work bench":
                        case "workbench":
                            Console.WriteLine("You walk over to the workbench.");
                            Dialogue("You see a collection of tools and materials.", "YOU", ConsoleColor.Cyan);
                            Dialogue("You acquire a tool!", "TOOL ACQUIRED", ConsoleColor.Yellow);
                            toolAcquired = true;
                            break;
                        default:
                            Console.WriteLine("You don't see that here.");
                            break;
                    }
                }
            }
            else if (subject == "Computer Science")
            {
                bool pythonDefeated = false;
                bool notebookAcquired = false;

                Dialogue("OMG THERE ARE PYTHONS EVERYWHERE! WE NEED TO GET OUT OF HERE!", "Student", ConsoleColor.Cyan);
                Dialogue("I think we should run!", "Student", ConsoleColor.Cyan);
                Console.SetCursorPosition(0, 8);
                Console.WriteLine(@"
╔══════════════════════════════════════════════════╗
║            COMPUTER SCIENCE CLASSROOM            ║
╚══════════════════════════════════════════════════╝

   [COMPUTER]          [DESK]           [PYTHON]
   ┌──────────┐         (o_o)            ~~~~~> 🐍
   └──────────┘

                         @ [YOU]

 ┌──────────┐
 │  [DOOR]  │  ---> OPEN BUT GUARDED (PYTHONS)
 └──────────┘
YOU CAN INTERACT WITH THE OBJECTS WITH SQARE BRACKETS AROUND THEM
────────────────────────────────────────────────────
");

                Console.WriteLine("\nType where you want to go to first:");

                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine().ToLower().Trim();

                    switch (input)
                    {
                        case "door":
                            Console.WriteLine("You approach the door.");
                    
                            if (pythonDefeated)
                            {
                                Dialogue("The pythons are asleep! You slip through!", "YOU", ConsoleColor.Cyan);
                                return;
                            }
                            else
                            {
                                Dialogue("The door is open, but pythons are guarding it!", "YOU", ConsoleColor.Cyan);
                                Dialogue("You need to defeat the pythons first!", "YOU", ConsoleColor.Cyan);
                            }
                            break;
                        case "python":
                        case "pythons":
                            Console.WriteLine("The python snake strikes!");
                            Dialogue("The python hisses loudly!", "PYTHON", ConsoleColor.Red);
                            if (!pythonDefeated)
                            {
                                Dialogue("You write a Python script to sedate it!", "ACTION", ConsoleColor.Green);
                                Dialogue("The snake falls asleep!", "PYTHON", ConsoleColor.Cyan);
                                Dialogue("PYTHON DEFEATED!", "PYTHONEER", ConsoleColor.Yellow);
                                pythonDefeated = true;
                            }
                            else
                            {
                                Dialogue("The python is already asleep.", "PYTHON", ConsoleColor.Cyan);
                            }
                            break;
                        case "computer":
                            Console.WriteLine("You look at the computer.");
                            Dialogue("There's a terminal open with a Python REPL.", "YOU", ConsoleColor.Cyan);
                            if (!notebookAcquired)
                            {
                                Dialogue("You acquire a notebook from the desk!", "NOTEBOOK ACQUIRED", ConsoleColor.Yellow);
                                notebookAcquired = true;
                            }
                            break;
                        case "desk":
                        case "desks":
                            Console.WriteLine("You walk over to the desks.");
                            Dialogue("A python is sunbathing on the desk!", "PYTHON", ConsoleColor.Red);
                            break;
                        default:
                            Console.WriteLine("You don't see that here.");
                            break;
                    }
                }
            }
        }

        static void teacherBattle()
        {
            lock (_consoleLock)
            {
                Console.SetCursorPosition(0, 2);
                for (int i = 2; i < Console.WindowHeight; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, 2);
            }

            Dialogue($"You have to defeat the {subject} teacher to escape!", "Student", ConsoleColor.Red);

            bool battleWon = false;

            while (!battleWon)
            {
                lock (_consoleLock)
                {
                    Console.SetCursorPosition(0, 4);
                    for (int i = 4; i < Console.WindowHeight; i++)
                    {
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }
                    Console.SetCursorPosition(0, 4);
                }

                if (subject == "Math")
                {
                    Console.WriteLine("\nThe Math Teacher summons a giant quadratic equation from the blackboard!");
                    Dialogue("Solve this equation: x² + 5x + 6 = 0", "Math Teacher", ConsoleColor.Blue);

                    Console.WriteLine("\nChoose your weapon:");
                    Console.WriteLine("1. Factor the equation");
                    Console.WriteLine("2. Memorize pi");
                    Console.WriteLine("3. Ask for a hint");
                    Console.WriteLine("4. Run away!");

                    Console.Write("> ");
                    string choice = Console.ReadLine().ToLower().Trim();

                    if (choice == "1" || choice == "factor the equation")
                    {
                        Dialogue("You factor the equation: (x + 2)(x + 3) = 0! The teacher is defeated!", "ACTION", ConsoleColor.Green);
                        Dialogue("The teacher shrinks back in confusion...", "Math Teacher", ConsoleColor.Blue);
                        battleWon = true;
                    }
                    else
                    {
                        Dialogue("Haha! That won't save you here!", "Math Teacher", ConsoleColor.Red);
                        Dialogue("You failed the quadratic challenge! You have to try again.", "Math Teacher", ConsoleColor.Blue);
                    }
                }
                else if (subject == "Physics")
                {
                    Console.WriteLine("\nThe Physics Teacher activates a force field and throws a particle beam!");
                    Dialogue("Calculate: F = m × a, where m = 10kg and a = 5 m/s²", "Physics Teacher", ConsoleColor.Blue);

                    Console.WriteLine("\nChoose your weapon:");
                    Console.WriteLine("1. Say 50N");
                    Console.WriteLine("2. Split the atom");
                    Console.WriteLine("3. Ask for a hint");
                    Console.WriteLine("4. Teleport away");

                    Console.Write("> ");
                    string choice = Console.ReadLine().ToLower().Trim();

                    if (choice == "1" || choice == "50n" || choice == "50 n")
                    {
                        Dialogue("You calculate correctly! F = 10 × 5 = 50N! The force field drops!", "ACTION", ConsoleColor.Green);
                        Dialogue("The teacher's particles fly off in frustration...", "Physics Teacher", ConsoleColor.Blue);
                        battleWon = true;
                    }
                    else
                    {
                        Dialogue("The force field repels your attack!", "Physics Teacher", ConsoleColor.Red);
                        Dialogue("You failed the physics calculation! You have to try again.", "Physics Teacher", ConsoleColor.Blue);
                    }
                }
                else if (subject == "Engineering")
                {
                    Console.WriteLine("\nThe Engineering Teacher launches a giant robot at you!");
                    Dialogue("Program this robot to stop!", "Engineering Teacher", ConsoleColor.Blue);

                    Console.WriteLine("\nChoose your weapon:");
                    Console.WriteLine("1. Write a stop program");
                    Console.WriteLine("2. Bribe the robot with snacks");
                    Console.WriteLine("3. Ask for a hint");
                    Console.WriteLine("4. Run away");

                    Console.Write("> ");
                    string choice = Console.ReadLine().ToLower().Trim();

                    if (choice == "1" || choice == "write a stop program" || choice == "write a program")
                    {
                        Dialogue("Your code compiles! The robot powers down! You win!", "ACTION", ConsoleColor.Green);
                        Dialogue("The robot collapses and the teacher takes off running.", "Engineering Teacher", ConsoleColor.Blue);
                        battleWon = true;
                    }
                    else
                    {
                        Dialogue("The robot whirs angrily and charges up!", "Engineering Teacher", ConsoleColor.Red);
                        Dialogue("Your engineering solution failed! You have to try again.", "Engineering Teacher", ConsoleColor.Blue);
                    }
                }
                else if (subject == "Computer Science")
                {
                    Console.WriteLine("\nA giant python snake wrapped around the teacher's desk!");
                    Dialogue("Deal with this snake!", "Computer Science Teacher", ConsoleColor.Blue);

                    Console.WriteLine("\nChoose your weapon:");
                    Console.WriteLine("1. Write a Python script to sedate it");
                    Console.WriteLine("2. Use a net");
                    Console.WriteLine("3. Ask for a hint");
                    Console.WriteLine("4. Run away");

                    Console.Write("> ");
                    string choice = Console.ReadLine().ToLower().Trim();

                    if (choice == "1" || choice == "write a python script" || choice == "write a script" || choice == "write a python program")
                    {
                        Dialogue("You write the script! The snake falls asleep!", "ACTION", ConsoleColor.Green);
                        Dialogue("The teacher screams and drops the snake!", "Computer Science Teacher", ConsoleColor.Blue);
                        battleWon = true;
                    }
                    else
                    {
                        Dialogue("The snake hisses loudly!", "Computer Science Teacher", ConsoleColor.Red);
                        Dialogue("The snake is not impressed! You have to try again.", "Computer Science Teacher", ConsoleColor.Blue);
                    }
                }
                else
                {
                    Console.WriteLine("A teacher appears!");
                    Dialogue("Too slow! You're late for class!", "Teacher", ConsoleColor.Red);
                    battleWon = true;
                }
            }
        }

        static void headteacher()
        {
            Dialogue("You have defeated the subject teacher, but the headteacher is here!", "Headteacher", ConsoleColor.Red);
            Dialogue("You are not going anywhere!", "Headteacher", ConsoleColor.Red);
            Dialogue("Thanos Snaps YOU out of existence!", "ACTION", ConsoleColor.Magenta);
        }

        static void Dialogue(string diamsg, string name, ConsoleColor colour)
        {
            typewriter($"{name}: {diamsg}\n", colour, 20, true);
        }

        static void typewriter(string msg, ConsoleColor colour, int speed = 50, bool showEnter = true)
        {
            lock (_consoleLock)
            {
                if (Console.CursorTop < 2)
                {
                    Console.SetCursorPosition(0, 2);
                }

                Console.ForegroundColor = colour;

                for (int i = 0; i < msg.Length; i++)
                {
                    Console.Write(msg[i]);
                    Thread.Sleep(speed);
                }

                Console.ResetColor();
            }

            if (showEnter)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
    }
}