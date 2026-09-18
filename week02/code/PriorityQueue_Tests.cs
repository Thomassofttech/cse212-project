using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and verify the highest priority is dequeued first.
    // Expected Result: VeryHigh, High, Medium, Low
    // Defect(s) Found: 
    // - The Dequeue method removed items in FIFO order instead of by highest priority.
    // - The priority value was not being considered when selecting which item to remove.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("VeryHigh", 20);

        Assert.AreEqual("VeryHigh", priorityQueue.Dequeue());
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add items with the same priority and verify FIFO order among them.
    // Expected Result: First, Second, Third, Fourth
    // Defect(s) Found: 
    // - When multiple items had the same highest priority, the queue did not return the one
    //   closest to the front.
    // - The tie-breaker logic was missing or incorrect.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 10);
        priorityQueue.Enqueue("Third", 10);
        priorityQueue.Enqueue("Fourth", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Fourth", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add items with mixed priorities including ties.
    // Expected Result: B, D, A, C, E
    // Defect(s) Found: 
    // - The queue did not maintain FIFO order among items with equal priority when
    //   priorities were mixed.
    // - Items were being reordered incorrectly.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 10);
        priorityQueue.Enqueue("E", 1);

        Assert.AreEqual("B", priorityQueue.Dequeue());  // First with priority 10
        Assert.AreEqual("D", priorityQueue.Dequeue());  // Second with priority 10
        Assert.AreEqual("A", priorityQueue.Dequeue());  // First with priority 5
        Assert.AreEqual("C", priorityQueue.Dequeue());  // Second with priority 5
        Assert.AreEqual("E", priorityQueue.Dequeue());  // Priority 1
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: 
    // - The correct exception (InvalidOperationException with message "The queue is empty.")
    //   was not thrown.
    // - A different exception type or message was being used.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Add a single item and dequeue it.
    // Expected Result: The item is returned correctly.
    // Defect(s) Found: 
    // - No defects found in this simple case.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 42);

        Assert.AreEqual("Only", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add items with negative and zero priorities.
    // Expected Result: Positive, Zero, Negative
    // Defect(s) Found: 
    // - Negative priorities were not handled correctly.
    // - The comparison logic may have assumed all priorities are positive.
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Negative", -5);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Positive", 5);

        Assert.AreEqual("Positive", priorityQueue.Dequeue());
        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("Negative", priorityQueue.Dequeue());
    }
}