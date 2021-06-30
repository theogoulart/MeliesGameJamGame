using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int[] matrixSize = new int[2] {4,4};
    // public int[,] floorMapping = new int[matrixSize[0], matrixSize[1]];
    // Start is called before the first frame update
    void Start()
    {
        // floorMapping = new int[matrixSize[0], matrixSize[1]];

        // for (var i = 0 ; i < matrixSize[0] ; i++) {
        //     for (var j = 0 ; j < matrixSize[1] ; j++) {
        //         floorMapping[i][j] = 0;
        //     }
        // }

        // Debug.Log(floorMapping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
