using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.LogWarning(fieldName + " is empty and must be assigned a value in " + thisObject.name, thisObject);
            return true;
        }
        return false;
    }

    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            Debug.Log(fieldName + " is null and must be assigned a value in " + thisObject.name, thisObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableToCheck)
    {
        bool error = false;
        int count = 0;
        
        if (enumerableToCheck == null)
        {
            Debug.LogWarning(fieldName + " is null in object " + thisObject.name, thisObject);
            return true;
        }
        
        foreach (var item in enumerableToCheck)
        {
            if (item == null)
            {
                Debug.LogWarning(fieldName + " has null value in object " + thisObject.name, thisObject);
                error =  true;
            }
            else
            {
                count++;
            }
        }
        if (count == 0)
        {
            Debug.LogWarning(fieldName + " has no values in object " + thisObject.name, thisObject);
            error = true;
        }
        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, int valueToCheck,
        bool isZeroAllowed = false)
    {
        bool error = false;
        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.Log(fieldName + " is less than zero in object " + thisObject.name, thisObject);
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(fieldName + " is less than or equal to zero in object " + thisObject.name, thisObject);
                error = true;
            }
        }

        return error;
    }
}
