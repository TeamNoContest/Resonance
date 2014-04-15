//Author Name: Carlis Moore
//Class Name: LibRevel.cs
//Class Purpose: A variety of utility and convenience methods I've found useful in multiple game development projects compiled together so that I don't have to perform copypasta surgery ever week.
using UnityEngine;
using System.Collections;

public static class LibRevel
{

    const float DAMPING = 7f;

    // User calls this method and passes the tag (as a string) they've applied to objects they wish to find. This will linearly search through all of them and pick the closest one with that tag. O(n).
    public static GameObject FindClosestGameObjectWithTagWhileAvoiding(GameObject performer, string tagToFind, GameObject ignoreThisGameObject)
    {
        GameObject result = null;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tagToFind);
        
        foreach (GameObject current in allObjects)
        {
            if (current != ignoreThisGameObject && current != performer)
            {
                if (result == null)
                {
                    result = current;
                } else
                {
                    //Only change if the newest object we're looking at is the closest.
                    if (Vector3.Distance(performer.transform.position, result.transform.position) > Vector3.Distance(performer.transform.position, current.transform.position))
                    {
                        result = current;
                        
                    }
                    
                }
                
            }
        }
        return result;
    }

    public static GameObject FindClosestGameObjectWithTag(GameObject performer, string tagToFind)
    {
        return FindClosestGameObjectWithTagWhileAvoiding(performer, tagToFind, null);
    }

    public static GameObject FindClosestGameObjectWithTag(string tagToFind)
    {
        return FindClosestGameObjectWithTag(null, tagToFind);
    }

    //This takes three pairs of min and max values and picks a random Vector3 of floats that are constrainted to those passed values.
    public static Vector3 RandomVector3InRange(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        float tempX = Random.Range(xMin, xMax);
        float tempY = Random.Range(yMin, yMax);
        float tempZ = Random.Range(zMin, zMax);

		Debug.Log(xMin + ":" + xMax + ", " + yMin + ":" + yMax + ", " + zMin + ":" + zMax);
		Debug.Log(tempX + ", " + tempY + ", " + tempZ);
        
        return new Vector3(tempX, tempY, tempZ);
    }

    public static void FlyTowardsGameObjectWithOnAxes(GameObject performer, GameObject destination, float movementSpeed, bool followX = true, bool followY = true, bool followZ = true)
    {
        //Precondition: You should only be calling this once per caller per FixedUpdate. - Moore
        if (destination != null)
        {

        //Make a new vector based on which Axes we need to follow by disregarding the target's unwanted axes, and then pass that anywhere destination is desired.
        
        Vector3 destinationPosition = destination.transform.position;
        if (!followX)
        {
            destinationPosition.x = performer.transform.position.x;
        }
        if (!followY)
        {
            destinationPosition.x = performer.transform.position.y;
        }
        if (!followZ)
        {
            destinationPosition.x = performer.transform.position.z;
        }
            //Make sure to check that the target is within a desirable distance. So there should be a maximum distance variable. - Moore            
            if (IsNotWithinDistanceThreshold(performer, destination, movementSpeed)) //WARNING: This movement speed is an assumption instead of a given threshold. Might be bad to have these two interconnected like this.
            { 

                if (performer.rigidbody == null)
                {
                    performer.transform.position = (Vector3.MoveTowards(performer.transform.position, destination.transform.position, 0.1f));
                } 

                else
                {               
                    performer.rigidbody.freezeRotation = true;
                    performer.rigidbody.velocity = Vector3.zero;



                    performer.rigidbody.AddForce(Vector3.MoveTowards(performer.transform.position, destination.transform.position, movementSpeed * Time.fixedDeltaTime));        
                }       
            }
        }
    }

    
    //Convenience method for the above.
    public static void FlyTowardsGameObject(GameObject performer, GameObject destination, float movementSpeed)
    {
        FlyTowardsGameObjectWithOnAxes(performer, destination, movementSpeed);
    }

    
    //Convenience method for the above.
    public static void FlyTowardsGameObjectIgnoringAxes(GameObject performer, GameObject destination, float movementSpeed, bool ignoreX = false, bool ignoreY = false, bool ignoreZ = false)
    {
        FlyTowardsGameObjectWithOnAxes(performer, destination, movementSpeed, !ignoreX, !ignoreY, !ignoreZ);
    }

    //Given two gameObjects and a float distance threshold, will check to see if the distance between the objects is within the threshold.
    public static bool IsWithinDistanceThreshold(Vector3 performer, Vector3 destination, float threshold)
    {
        bool result = false;
        
        if (Vector3.Distance(performer, destination) > threshold)
        { //If it is outside of the threshold...
            result = false; //Return false. - Moore
        } else
        { // Otherwise, return true. - Moore
            return true;
        }
        
        return result;
    }

    //Given two gameObjects and a float distance threshold, will check to see if the distance between the objects is within the threshold.
    public static bool IsWithinDistanceThreshold(GameObject performer, GameObject destination, float threshold)
    {
        return IsWithinDistanceThreshold(performer.transform.position, destination.transform.position, threshold);
    }

    //Convenience Negation Wrapper Method for the above when given Vector3s.
    public static bool IsNotWithinDistanceThreshold(Vector3 performer, Vector3 destination, float threshold)
    {
        return !IsWithinDistanceThreshold(performer, destination, threshold);
    }
    
    //Convenience Negation Wrapper Method for the above for gameObjects:
    public static bool IsNotWithinDistanceThreshold(GameObject performer, GameObject destination, float threshold)
    {
        return !IsWithinDistanceThreshold(performer, destination, threshold);
    }
}
