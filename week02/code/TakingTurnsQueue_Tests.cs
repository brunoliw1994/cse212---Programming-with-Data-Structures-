using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MyNamespace
{
    public class TakingTurnsQueue
    {
        public class Person
        {
            public string Name { get; set; }
            public int Turns { get; set; } // 0 or less means infinite turns

            public Person(string name, int turns)
            {
                Name = name;
                Turns = turns;
            }
        }

        // Queue for managing people taking turns
        private Queue<Person> queue = new Queue<Person>();

        // Property to get the number of people in the queue
        public int Length => queue.Count;

        public void AddPerson(string name, int turns)
        {
            // Adds a new person to the queue
            queue.Enqueue(new Person(name, turns));
        }

        public Person GetNextPerson()
        {
            // Throws an exception if the queue is empty
            if (queue.Count == 0)
            {
                throw new InvalidOperationException("No one in the queue.");
            }

            // Removes the next person from the queue
            var person = queue.Dequeue();

            // Re-enqueues the person if they have remaining turns or infinite turns
            if (person.Turns <= 0 || person.Turns > 1)
            {
                if (person.Turns > 0)
                {
                    person.Turns--; // Decrements the turns for finite-turn persons
                }
                queue.Enqueue(person); // Adds the person back to the queue
            }

            return person;
        }
    }
}


// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

namespace MyNamespace.Tests
{
    [TestClass]
    public class TakingTurnsQueueTests
    {
        [TestMethod]
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
        // run until the queue is empty
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        // Defect(s) Found: The queue does not cycle correctly through players based on turns.
        public void TestTakingTurnsQueue_FiniteRepetition()
        {
            var bob = new MyNamespace.TakingTurnsQueue.Person("Bob", 2);
            var tim = new MyNamespace.TakingTurnsQueue.Person("Tim", 5);
            var sue = new MyNamespace.TakingTurnsQueue.Person("Sue", 3);

            // Expected order of persons to be dequeued
            MyNamespace.TakingTurnsQueue.Person[] expectedResult =
            {
                bob, tim, sue, bob, tim, sue, tim, sue, tim, tim
            };

            var players = new MyNamespace.TakingTurnsQueue();
            players.AddPerson(bob.Name, bob.Turns);
            players.AddPerson(tim.Name, tim.Turns);
            players.AddPerson(sue.Name, sue.Turns);

            int i = 0;
            while (players.Length > 0)
            {
                if (i >= expectedResult.Length)
                {
                    Assert.Fail("Queue should have ran out of items by now.");
                }

                // Validates if dequeued person matches the expected order
                var person = players.GetNextPerson();
                Assert.AreEqual(expectedResult[i].Name, person.Name);
                i++;
            }
        }

        [TestMethod]
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
        // After running 5 times, add George with 3 turns.  Run until the queue is empty.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
        // Defect(s) Found: Adding a player midway does not integrate the new player correctly into the queue.
        public void TestTakingTurnsQueue_AddPlayerMidway()
        {
            var bob = new MyNamespace.TakingTurnsQueue.Person("Bob", 2);
            var tim = new MyNamespace.TakingTurnsQueue.Person("Tim", 5);
            var sue = new MyNamespace.TakingTurnsQueue.Person("Sue", 3);
            var george = new MyNamespace.TakingTurnsQueue.Person("George", 3);

            // Expected order of persons to be dequeued
            MyNamespace.TakingTurnsQueue.Person[] expectedResult =
            {
                bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george
            };

            var players = new MyNamespace.TakingTurnsQueue();
            players.AddPerson(bob.Name, bob.Turns);
            players.AddPerson(tim.Name, tim.Turns);
            players.AddPerson(sue.Name, sue.Turns);

            int i = 0;

            // Runs the first 5 cycles of dequeuing
            for (; i < 5; i++)
            {
                var person = players.GetNextPerson();
                Assert.AreEqual(expectedResult[i].Name, person.Name);
            }

            // Adds a new player to the queue
            players.AddPerson("George", 3);

            while (players.Length > 0)
            {
                if (i >= expectedResult.Length)
                {
                    Assert.Fail("Queue should have ran out of items by now.");
                }

                // Continues validating the sequence after the new player is added
                var person = players.GetNextPerson();
                Assert.AreEqual(expectedResult[i].Name, person.Name);

                i++;
            }
        }

        [TestMethod]
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
        // Run 10 times.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        // Defect(s) Found: Tim with infinite turns is not handled correctly; infinite is simulated as a large finite number.
        public void TestTakingTurnsQueue_ForeverZero()
        {
            var timTurns = 0;

            var bob = new MyNamespace.TakingTurnsQueue.Person("Bob", 2);
            var tim = new MyNamespace.TakingTurnsQueue.Person("Tim", timTurns);
            var sue = new MyNamespace.TakingTurnsQueue.Person("Sue", 3);

            // Expected order of persons to be dequeued
            MyNamespace.TakingTurnsQueue.Person[] expectedResult =
            {
                bob, tim, sue, bob, tim, sue, tim, sue, tim, tim
            };

            var players = new MyNamespace.TakingTurnsQueue();
            players.AddPerson(bob.Name, bob.Turns);
            players.AddPerson(tim.Name, tim.Turns);
            players.AddPerson(sue.Name, sue.Turns);

            for (int i = 0; i < 10; i++)
            {
                // Runs 10 cycles and validates the sequence
                var person = players.GetNextPerson();
                Assert.AreEqual(expectedResult[i].Name, person.Name);
            }

            // Verify that the people with infinite turns really do have infinite turns.
            var infinitePerson = players.GetNextPerson();
            Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
        }

        [TestMethod]
        // Scenario: Create a queue with the following people and turns: Tim (Forever), Sue (3)
        // Run 10 times.
        // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
        // Defect(s) Found: Negative values for infinite turns are not processed correctly, causing unexpected results.
        public void TestTakingTurnsQueue_ForeverNegative()
        {
            var timTurns = -3;
            var tim = new MyNamespace.TakingTurnsQueue.Person("Tim", timTurns);
            var sue = new MyNamespace.TakingTurnsQueue.Person("Sue", 3);

            // Expected order of persons to be dequeued (using proper array initializer)
            MyNamespace.TakingTurnsQueue.Person[] expectedResult =
            {
                tim, sue, tim, sue, tim, sue, tim, tim, tim, tim
            };

            var players = new MyNamespace.TakingTurnsQueue();
            players.AddPerson(tim.Name, tim.Turns);
            players.AddPerson(sue.Name, sue.Turns);

            for (int i = 0; i < 10; i++)
            {
                var person = players.GetNextPerson();
                Assert.AreEqual(expectedResult[i].Name, person.Name);
            }

            // Verify that the people with infinite turns really do have infinite turns.
            var infinitePerson = players.GetNextPerson();
            Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
        }

        [TestMethod]
        // Scenario: Try to get the next person from an empty queue
        // Expected Result: Exception should be thrown with appropriate error message.
        // Defect(s) Found: No exception is thrown when attempting to get a person from an empty queue.
        public void TestTakingTurnsQueue_Empty()
        {
            var players = new MyNamespace.TakingTurnsQueue();

            try
            {
                players.GetNextPerson();
                Assert.Fail("Exception should have been thrown.");
            }
            catch (InvalidOperationException e)
            {
                // Verifies the exception message matches the expected error
                Assert.AreEqual("No one in the queue.", e.Message);
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                // Fails the test if an unexpected exception type is thrown
                Assert.Fail(
                     string.Format("Unexpected exception of type {0} caught: {1}",
                                    e.GetType(), e.Message)
                );
            }
        }
    }
}
