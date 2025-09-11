using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.LogError(fieldName + " is empty and must be assigned a value in " + thisObject.name, thisObject);
            return true;
        }
        return false;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableToCheck)
    {
        bool error = false;
        int count = 0;
        foreach (var item in enumerableToCheck)
        {
            if (item == null)
            {
                Debug.LogError(fieldName + " has null value in object " + thisObject.name, thisObject);
                error =  true;
            }
            else
            {
                count++;
            }
        }
        if (count == 0)
        {
            Debug.LogError(fieldName + " has no values in object " + thisObject.name, thisObject);
            error = true;
        }
        return error;
    }
}
