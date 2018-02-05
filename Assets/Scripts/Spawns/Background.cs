using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Скрипт взят отсюда: https://drive.google.com/file/d/1fae6lbbXsyRPlBa4iZfcpa_2n41I-t1U/view и немного допилен
public class Background : MonoBehaviour {


    public float Speed = 0;
    public SpriteRenderer[] sprites; //= new SpriteRenderer>();
    public Direction Dir = Direction.Right;


    private float heightCamera;
    private float widthCamera;

    private Vector3 PositionCam;
    private Camera cam;

    public bool reset = false;

    public void PlaceOnStart() {
        //Debug.Log("cool");
        sprites[0].transform.position = new Vector3(-15f, 0f, 0f);
       // Debug.Log("cool - "+ sprites[0].transform.position.x);
        for (int i = 1; i < sprites.Length; i++) {
            float positionX = sprites[i - 1].GetComponent<SpriteRenderer>().transform.position.x
                + sprites[i - 1].GetComponent<SpriteRenderer>().bounds.size.x / 2
                + sprites[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            //Debug.Log(sprites[i].GetComponent<SpriteRenderer>().size.x.ToString());
            //Debug.Log(positionX);
            sprites[i].transform.position = new Vector3(positionX, 0f, 0f);
            reset = false;

        }
    }

    // Use this for initialization
    void Awake() {
        cam = Camera.main;
        heightCamera = 2f * cam.orthographicSize;
        widthCamera = heightCamera * cam.aspect;

        PlaceOnStart();

    }
    // Update is called once per frame
    void Update() {
        if (!reset) {
            UpdateSpritesPos();
        } else {
            PlaceOnStart();
        }
    }

    void UpdateSpritesPos() {
        foreach (var item in sprites) {
            if (Dir == Direction.Left) {
                if (item.transform.position.x + item.bounds.size.x / 2 < cam.transform.position.x - widthCamera / 2) {
                    SpriteRenderer sprite = sprites[0];
                    foreach (var i in sprites) {
                        if (i.transform.position.x > sprite.transform.position.x)
                            //Debug.Log("notcool - " + sprites[0].transform.position.x);
                        sprite = i;
                    }

                    item.transform.position = new Vector2((sprite.transform.position.x + (sprite.bounds.size.x / 2) + (item.bounds.size.x / 2)), sprite.transform.position.y);
                }
            } else if (Dir == Direction.Right) {
                if (item.transform.position.x - item.bounds.size.x / 4 > cam.transform.position.x + widthCamera / 2) {
                    SpriteRenderer sprite = sprites[0];
                    foreach (var i in sprites) {
                        if (i.transform.position.x < sprite.transform.position.x)
                            sprite = i;
                    }

                    item.transform.position = new Vector2((sprite.transform.position.x - (sprite.bounds.size.x / 2) - (item.bounds.size.x / 2)), sprite.transform.position.y);
                }
            } else if (Dir == Direction.Down) {
                if (item.transform.position.y + item.bounds.size.y / 2 < cam.transform.position.y - heightCamera / 2) {
                    SpriteRenderer sprite = sprites[0];
                    foreach (var i in sprites) {
                        if (i.transform.position.y > sprite.transform.position.y)
                            sprite = i;
                    }

                    item.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y + (sprite.bounds.size.y / 2) + (item.bounds.size.y / 2)));
                }
            } else if (Dir == Direction.Up) {
                if (item.transform.position.y - item.bounds.size.y / 2 > cam.transform.position.y + heightCamera / 2) {
                    SpriteRenderer sprite = sprites[0];
                    foreach (var i in sprites) {
                        if (i.transform.position.y < sprite.transform.position.y)
                            sprite = i;
                    }

                    item.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y - (sprite.bounds.size.y / 2) - (item.bounds.size.y / 2)));
                }
            }


            if (Dir == Direction.Left)
                item.transform.Translate(new Vector2(Time.deltaTime * Speed * -1, 0));
            else if (Dir == Direction.Right)
                item.transform.Translate(new Vector2(Time.deltaTime * Speed, 0));
            else if (Dir == Direction.Down)
                item.transform.Translate(new Vector2(0, Time.deltaTime * Speed * -1));
            else if (Dir == Direction.Up)
                item.transform.Translate(new Vector2(0, Time.deltaTime * Speed));
        }
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}