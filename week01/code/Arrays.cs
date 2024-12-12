using System;
using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}. Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Creates an array of size 'length' to store the results.
        double[] multiples = new double[length];

        // Step 2: Loops through from 0 to length - 1 to populate the array.
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculates the ith multiple of 'number' by multiplying 'number' by (i + 1).
            multiples[i] = number * (i + 1);
        }

        // Step 4: Returns the array of multiples.
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'. For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}. The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Determines the effective rotation by computing the amount and the list count.
        int count = data.Count;
        amount = amount % count;

        // Step 2: Checks if no rotation is needed (amount is 0) and returns early if so.
        if (amount == 0)
        {
            return;
        }

        // Step 3: Copies the last 'amount' elements into a temporary list.
        List<int> temp = data.GetRange(count - amount, amount);

        // Step 4: Shifts the remaining elements in the original list to the right by 'amount'.
        for (int i = count - 1; i >= amount; i--)
        {
            data[i] = data[i - amount];
        }

        // Step 5: Copies the elements from the temporary list back into the beginning of the original list.
        for (int i = 0; i < amount; i++)
        {
            data[i] = temp[i];
        }
    }
}
