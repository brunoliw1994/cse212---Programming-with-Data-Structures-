using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add an item with a certain priority and then dequeue it.
    // Expected Result: The item with the highest priority should be dequeued and its value returned.
    // Defect(s) Found: None.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Item1", result);
    }

    [TestMethod]
    // Scenario: Add multiple items with different priorities and dequeue them.
    // Expected Result: The item with the highest priority should always be dequeued first.
    // Defect(s) Found: None.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 3);
        priorityQueue.Enqueue("Item3", 2);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Item2", result); // Item2 has the highest priority (3)
    }

    [TestMethod]
    // Scenario: Add multiple items with the same priority and ensure the first one added is dequeued first.
    // Expected Result: The first item with the same highest priority should be dequeued.
    // Defect(s) Found: None.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 2);
        priorityQueue.Enqueue("Item2", 2);
        priorityQueue.Enqueue("Item3", 2);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Item1", result); // Item1 is the first added, so it should be dequeued first
    }

    [TestMethod]
    // Scenario: Dequeue when the queue is empty.
    // Expected Result: An exception should be thrown indicating the queue is empty.
    // Defect(s) Found: The empty queue does not throw an exception.
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Dequeue(); // This should throw an exception since the queue is empty
    }

    [TestMethod]
    // Scenario: Add an item to an empty queue, then dequeue it, and ensure the queue is empty afterward.
    // Expected Result: The dequeued item should be returned, and the queue should be empty afterward.
    // Defect(s) Found: None.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Item1", result);
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}