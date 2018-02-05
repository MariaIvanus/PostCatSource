using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator {

    private int level;
    private int totalSections;
    private int[] lvlsMaxLenght;

    private float inititalFuel;
    public string dificulty = "easy";

    public LevelGenerator(int levelNumber) {

        this.level = levelNumber;

        lvlsMaxLenght = new[] { 2, 5, 7, 8, 11, 8, 9, 13 };

        GenerateLevel();
    }

    public float giveInitialFuel() {
        if (dificulty == "easy") {
            inititalFuel = totalSections * 30;
        }


        return inititalFuel;
    }

    public int maxSectionNumber {
        get {
            return totalSections;
        }
    }


    void GenerateLevel() {
        if (this.level < lvlsMaxLenght.Length) {
            totalSections = lvlsMaxLenght[level];
        } else {
            totalSections = (int)Mathf.Round(Random.Range(8, 16));

        }
    }
    
}
