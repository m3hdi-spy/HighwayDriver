using System.Collections;
using UnityEngine;

public class TrafficGeneratorFormul : TrafficGeneratorController
{
    
    private byte FreeLane;
    private byte FreeLaneRow;
    private byte AllCars;

    [SerializeField]
    private byte[] CarsInColumn = new byte[4];
    private bool[,] Lanes = new bool[4, 10];

   
    public void StartGenerate(byte aCars)
    {
        for (byte i = 0; i < 4; i++)
        {
            CarsInColumn[i] = 0;
            for (byte j = 0; j < 10; j++)
                Lanes[i, j] = false;
        }
        
        FreeLane = 0;
        AllCars = aCars;
        ChooseFreeLane();
    }


    void ChooseFreeLane()
    {
        if (LevelManager.Score < 10)
        {
            FreeLane = (byte)Random.Range(1, 3);
            //return;
        }

        switch (LastFreeLane)
        {
            case 0:
                {
                    FreeLane = AThirsRandom(1, 2);
                    break;
                }
            case 1:
                {
                    FreeLane = AThirsRandom(0, 2);
                    break;
                }
            case 2:
                {
                    FreeLane = AThirsRandom(3, 1);
                    break;
                }
            case 3:
                {
                    FreeLane = AThirsRandom(1, 2);
                    break;
                }
            default:
                {
                    LastFreeLane = 1;
                    FreeLane = 1;
                    Debug.LogError("ChooseFreeLane LastFreeLane is Default!");
                    break;
                }
        }
        LastFreeLane = FreeLane;
        SetOneInFree();

    }

    void SetOneInFree()
    {
        byte lessRow, maxRow;
        if(GameMode == 2 && (FreeLane == 2 || FreeLane == 3))
        {
            lessRow = 8;
            maxRow = 10;
        }
        else
        {
            lessRow = 3;
            maxRow = 7;
        }
        FreeLaneRow = (byte)Random.Range(lessRow, maxRow);
        Lanes[FreeLane, FreeLaneRow] = true;
        SetCountInColumn();
    }

    void SetCountInColumn()
    {
        switch (FreeLane)
        {
            case 0:
                {
                    SetColumnNumber(1, 3, 2);
                    break;
                }
            case 1:
                {
                    SetColumnNumber(0, 3, 2);
                    break;
                }
            case 2:
                {
                    SetColumnNumber(1, 0, 3);
                    break;
                }
            case 3:
                {
                    SetColumnNumber(2, 0, 1);
                    break;
                }
            default:
                {
                    SetColumnNumber(1, 2, 3);
                    Debug.Log("Che Vazeshe!");
                    break;
                }
        }

        for (byte r = 0; r < 4; r++)
        {
            if (r == FreeLane)
                continue;
            SetPlaceInRow(r);
        }

    }


    /// <summary>
        /// 
        /// </summary>
        /// <param name="l1"> Most Balance </param>
        /// <param name="l2">IDK </param>
        /// <param name="l3"> I Think Mos Massivly</param>
    void SetColumnNumber(byte l1, byte l2, byte l3)
    {
        CalculateHardnessCurve();
        
        byte a = (byte)Random.Range(MinCarsInColumn, MaxCarsInColumn + 1);
        byte b = (byte)Random.Range(MinCarsInColumn, MaxCarsInColumn + 1);

        if (a < 2)
            a = 2;
        if (a + b < 6)
        {
            if (a < 3)
            {
                a = 3;
                b--;
            }

        }

        if (a == MaxCarsInColumn && b >= MaxCarsInColumn - 1) // Don't make a && b High
        {
            a = (byte)(Instance.MaxCarsInColumn - 1);
            b = (byte)(MaxCarsInColumn - 2);
        }
        byte c = (byte)Random.Range(MinCarsInColumn, MaxCarsInColumn);
        if (c == MaxCarsInColumn)
            c--;

        // if (a <= MinCarsInColumn + 2 && b <= MinCarsInColumn + 1) // Don't make c High when a && b is low
        //    c = (byte)Random.Range(MinCarsInColumn, MaxCarsInColumn);

        CarsInColumn[l3] = c;
        CarsInColumn[l2] = b;
        CarsInColumn[l1] = a;

        if (GameMode == 2)
        {
            if (FreeLane == 1)
            {
                if (CarsInColumn[2] > 3)
                    CarsInColumn[2] = 3;
            }
            else if (FreeLane == 2)
            {
                if (CarsInColumn[3] > 3)
                    CarsInColumn[3] = 3;
                if (CarsInColumn[1] > 3)
                    CarsInColumn[1] = 3;
            }
            else if (FreeLane == 3)
            {
                if (CarsInColumn[2] > 3)
                    CarsInColumn[2] = 3;
            }
        }

        
    }

    void SetPlaceInRow(byte column)
    {
       
        if (CarsInColumn[column] < 3)
        {
            if (GameMode == 2 && (column == 2 || column == 3))
            {
                Lanes[column, Random.Range(5, 8)] = true;
                Lanes[column, Random.Range(8, 10)] = true;
            }
            else
            {
                Lanes[column, Random.Range(0, 5)] = true;
                Lanes[column, Random.Range(5, 10)] = true;
            }
        }
        else if (CarsInColumn[column] == 3)
        {
            if (GameMode == 2 && (column == 2 || column == 3))
            {
                byte ra = (byte)Random.Range(2, 5);
                Lanes[column, ra] = true;

                byte rb = (byte)Random.Range(ra + 1, ra + 3);
                Lanes[column, rb] = true;

                byte rc = (byte)Random.Range(rb + 1, 10);
                Lanes[column, rc] = true;
            }
            else
            {
                byte ra = (byte)Random.Range(0, 5);
                Lanes[column, ra] = true;

                byte rb = (byte)Random.Range(ra + 1, ra + 3);
                Lanes[column, rb] = true;

                byte rc = (byte)Random.Range(rb + 2, 10);
                Lanes[column, rc] = true;
            }
        }
        else if (CarsInColumn[column] == 4)
        {
            if (GameMode == 2 && (column == 2 || column == 3))
            {
                byte ra = (byte)Random.Range(2, 5);
                Lanes[column, ra] = true;

                byte rb = (byte)Random.Range(ra + 1, 7);
                Lanes[column, rb] = true;

                byte rc = (byte)Random.Range(rb + 1, 8);
                Lanes[column, rc] = true;

                byte rd = (byte)Random.Range(rc + 1, 10);
                Lanes[column, rd] = true;
            }
            else
            {
                byte ra = (byte)Random.Range(0, 4);
                Lanes[column, ra] = true;

                byte rb = (byte)Random.Range(ra + 1, 6);
                Lanes[column, rb] = true;

                byte rc = (byte)Random.Range(rb + 1, 8);
                Lanes[column, rc] = true;

                byte rd = (byte)Random.Range(rc + 1, 10);
                Lanes[column, rd] = true;
            }
        }
        else if (CarsInColumn[column] == 5)
        {
            byte ra = (byte)Random.Range(0, 3);
            Lanes[column, ra] = true;

            byte rb = (byte)Random.Range(ra + 1, 5);
            Lanes[column, rb] = true;

            byte rc = (byte)Random.Range(rb + 1, 7);
            Lanes[column, rc] = true;

            byte rd = (byte)Random.Range(rc + 1, 9);
            Lanes[column, rd] = true;

            byte re = 9;
            Lanes[column, re] = true;
        }
        if (column == 3)
            if (GameMode == 2)
                StartCoroutine(SpawnCars());
            else
                TrafficDensity();
    }

    IEnumerator SpawnCars()
    {
        yield return null;
        StartCoroutine(gameObject.GetComponent<TrafficGeneratorSpawn>().SpawnCarsInStreet(Lanes));

    }

    private void TrafficDensity()
    {
        for (byte i = 0; i < 10; i++)
        {
            if (Lanes[0, i] && Lanes[1, i] && Lanes[2, i] && Lanes[3, i])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
            }
            else if((i+1 < 10) && Lanes[0, i] && Lanes[1, i] && Lanes[2, i+1] && Lanes[3, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
            }
            else if((i+1 < 10) && Lanes[2,i] && Lanes[3,i] && Lanes[0, i+1] && Lanes[1, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
            }
            else if((i+1 < 10) && Lanes[0,i] && Lanes[3,i] && Lanes[1,i+1] && Lanes[2, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
            }
            //--- Zarbedari

            else if((i+3 < 10) && Lanes[0,i] && Lanes[1, i+1] && Lanes[2, i+2] && Lanes[3, i+3])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
                else if (Lanes[FreeLane, i + 2])
                    ChangePlaceInBadDensity((byte)(i + 2));
                else if (Lanes[FreeLane, i + 3])
                    ChangePlaceInBadDensity((byte)(i + 3));
            }
            else if((i+2 < 10) && Lanes[0,i] && Lanes[1, i+1] && Lanes[2, i+2] && Lanes[3, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
                else if (Lanes[FreeLane, i + 2])
                    ChangePlaceInBadDensity((byte)(i + 2));
            }
            else if((i+2 < 10) && Lanes[3,i] && Lanes[2, i + 1] && Lanes[1, i+2] && Lanes[0, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if (Lanes[FreeLane, i + 1])
                    ChangePlaceInBadDensity((byte)(i + 1));
                else if (Lanes[FreeLane, i + 2])
                    ChangePlaceInBadDensity((byte)(i + 2));
            }
            else if((i+1 < 10) && Lanes[0,i] && Lanes[3, i] && Lanes[1, i+1] && Lanes[2, i+1])
            {
                if (Lanes[FreeLane, i])
                    ChangePlaceInBadDensity(i);
                else if(Lanes[FreeLane, i+1])
                    ChangePlaceInBadDensity((byte)(i+1));
                
            }

            if (i == 9)
               StartCoroutine(SpawnCars());
        }

    }

    void ChangePlaceInBadDensity(byte row)
    {
        Lanes[FreeLane, row] = false;

        if (row + 2 < 10)
        {
            if (FreeLane + 1 < 4)
                if (Lanes[FreeLane + 1, row + 2])
                    Lanes[FreeLane + 1, row + 2] = false;
            Lanes[FreeLane, row + 2] = true;
        }
        else if (row - 2 > 10)
        {
            if (FreeLane + 1 < 4)
                if (Lanes[FreeLane + 1, row - 2])
                    Lanes[FreeLane + 1, row - 2] = false;

            Lanes[FreeLane, row - 2] = true;
        }
        else
            Debug.LogError(row + " This row is Not +2 and -2 Avalable! WTF!");
    }


    byte AThirsRandom(byte less, byte max)
    {
        byte a = (byte)Random.Range(0, 5);

        if (a % 2 == 0)
            return max;
        else return less;
        }
    }
