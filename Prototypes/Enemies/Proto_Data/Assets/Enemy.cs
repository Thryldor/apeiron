using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

class Astar
{
  const int mapWidth = 20;
  const int mapHeight = 10;

  private class Tile
  {
    public int x = 0;
    public int y = 0;
    public int f = 0;
    public int g = 0;
    public int h = 0;
    public Tile previous = null;

    public Tile(int x, int y)
    {
      this.x = x;
      this.y = y;
    }
  }

  Tile[,] tiles;
  HashSet<Tile> open = new HashSet<Tile>();
  HashSet<Tile> close = new HashSet<Tile>();
  Tile start = null;
  Tile end = null;
  int[,] map;
  List<Jumpable> jumpables;

  public List<Vector3> getPath(int startX, int startY, int endX, int endY, int[,] map, List<Jumpable> jumpables)
  {
    //Debug.Log("StartX " + startX + " startY " + startY + " endX " + endX + " endY " + endY);
    end = new Tile(endX, endY);
    return getPath(startX, startY, map, jumpables);
  }

  private List<Vector3> getPath(int startX, int startY, int[,] map, List<Jumpable> jumpables)
  {
    tiles = new Tile[mapHeight, mapWidth];
    open.Clear();
    close.Clear();
    start = new Tile(startX, startY);
    this.map = map;
    this.jumpables = jumpables;
    tiles[startY, startX] = start;
    open.Add(start);

    while (open.Count > 0)
    {
      Tile current = getBestTile();

      if (current == null)
        return null;
      if (end != null && current.x == end.x && current.y == end.y)
        return buildPath(current);
      open.Remove(current);
      close.Add(current);
      addToOpen(current);
    }
    return null;
  }

  private List<Vector3> buildPath(Tile current)
  {
    List<Vector3> path = new List<Vector3>();

    while (current != start)
    {
      path.Insert(0, new Vector3((float)(current.x), (float)(current.y), 0.0f));
      current = current.previous;
    }
    path.Insert(0, new Vector3((float)(start.x), (float)(start.y), 0.0f));
    return path;
  }

  private void addToOpen(Tile current)
  {
    if (map[current.y, current.x] == 2) {
      Jumpable jumpable = jumpables.Find(jump => jump.x == current.x && jump.y == current.y);

      foreach (Vector2 jump in jumpable.jumps) {
        if (tiles[(int) jump.y, (int) jump.x] == null)
          computeOpen((int) jump.x, (int) jump.y, current, (int) Math.Abs(current.x - jump.x));
      }
    }

    if (current.x > 0 && tiles[current.y, current.x - 1] == null &&
        (current.y == mapHeight - 1 || map[current.y + 1, current.x - 1] == 1))
      computeOpen(current.x - 1, current.y, current, 0);
    if (current.x < mapWidth - 1 && tiles[current.y, current.x + 1] == null &&
        (current.y == mapHeight - 1 || map[current.y + 1, current.x + 1] == 1))
      computeOpen(current.x + 1, current.y, current, 0);
    /*if (current.x > 0 && tiles[current.y, current.x - 1] == null)
      computeOpen(current.x - 1, current.y, current);
    if (current.x < mapWidth - 1 && tiles[current.y, current.x + 1] == null)
      computeOpen(current.x + 1, current.y, current);
    if (current.y > 0 && tiles[current.y - 1, current.x] == null)
      computeOpen(current.x, current.y - 1, current);
    if (current.y < mapHeight - 1 && tiles[current.y + 1, current.x] == null)
      computeOpen(current.x, current.y + 1, current);*/
  }

  private void computeOpen(int x, int y, Tile previous, int jumpCost)
  {
    Tile tile = new Tile(x, y);

    tile.previous = previous;
    tile.g = previous.g + 1;
    tile.h = heuristic(tile);
    tile.f = tile.g + tile.h + jumpCost;
    open.Add(tile);
    tiles[y, x] = tile;
  }

  private Tile getBestTile()
  {
    Tile best = null;

    foreach (Tile current in open)
    {
      if (map[current.y, current.x] == 1)
        continue;
      if (best == null)
        best = current;
      if (current.f < best.f)
        best = current;
      else if (current.f == best.f)
        if (current.h < best.h)
          best = current;
    }
    return best;
  }

  private int heuristic(Tile current)
  {
    if (end == null)
      return 0;

    int dx = System.Math.Abs(current.x - end.x);
    int dy = System.Math.Abs(current.y - end.y);

    return 1 * (dx + dy);
  }
}

public class Jumpable
{
  public int x;
  public int y;
  public List<Vector2> jumps = new List<Vector2>();

  public Jumpable(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public void addJump(int x, int y)
  {
    jumps.Add(new Vector2(x, y));
  }
}

public class Enemy : MonoBehaviour
{
  const int mapWidth = 20;
  const int mapHeight = 10;

  public float speed = 5f;
  public float jumpForce = 7.0f;

  List<Jumpable> jumpables = new List<Jumpable>();
  List<Vector3> path = null;
  private Astar astar = new Astar();
  int jumpTimer = 0;
  int playerMapX = -1;
  int playerMapY = -1;
  int[,] map = new int[mapHeight, mapWidth] {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
                                             {0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                                             {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 0, 0},
                                             {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1, 2, 0, 1, 0, 2, 0}};

  void Start() {
    Jumpable jumpable = new Jumpable(4, 7);
    jumpable.addJump(5, 9);
    jumpable.addJump(9, 6);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(9, 6);
    jumpable.addJump(4, 7);
    jumpable.addJump(8, 9);
    jumpable.addJump(10, 9);
    jumpable.addJump(13, 6);
    jumpable.addJump(16, 8);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(13, 6);
    jumpable.addJump(12, 9);
    jumpable.addJump(14, 9);
    jumpable.addJump(9, 6);
    jumpable.addJump(16, 8);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(16, 8);
    jumpable.addJump(15, 9);
    jumpable.addJump(17, 9);
    jumpable.addJump(13, 6);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(6, 9);
    jumpable.addJump(4, 7);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(14, 9);
    jumpable.addJump(16, 8);
    jumpables.Add(jumpable);
    jumpable = new Jumpable(18, 9);
    jumpable.addJump(16, 8);
    jumpables.Add(jumpable);

    /*foreach (Vector3 point in path) {
      Debug.Log(point);
    }*/

    gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
  }

  bool reachedTarget(float x, float y, float targetX, float targetY) {
    return (x >= targetX - 0f && x <= targetX + 0f &&
            y >= targetY - 0.08f && y <= targetY + 0.08f);
  }

  void moveEnemy() {
    if (jumpTimer == 1)
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    if (jumpTimer > 0)
      jumpTimer--;

    if (jumpTimer > 0)
      return;

    float x = (float)(path[0].x - 9.5f);
    float y = (float)(path[0].y * -1 + 9 - 4.5);

    if (reachedTarget(transform.position.x, transform.position.y, x, y)) {
      if (map[(int)path[0].y, (int)path[0].x] == 2) {
        if (path.Count > 1 && Math.Abs(x - (path[1].x - 9.5f)) > 1)
          jumpTimer = 3;
      }
      path.RemoveAt(0);
    }
    Vector3 p = transform.position;

    if (transform.position.x < x) {
      p.x += speed * Time.deltaTime;
      if (p.x > x)
        p.x = x;
    } else {
      p.x -= speed * Time.deltaTime;
      if (p.x < x)
        p.x = x;
    }
    transform.position = p;
  }

  void Update() {
    GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

    if (Input.GetKeyDown(KeyCode.K)) {
      path = null;
      jumpTimer = 0;
      playerMapX = -1;
      playerMapY = -1;
      transform.position = new Vector3(-8.5f, -2.5f, 0);
    }

    if (playerMapX != System.Convert.ToInt32(player.transform.position.x + 9.5) ||
        playerMapY != System.Convert.ToInt32(player.transform.position.y * -1 + 4.5)) {
      int mapX = path != null && path.Count > 0 ? (int) path[0].x : System.Convert.ToInt32(transform.position.x + 9.5);
      int mapY = path != null && path.Count > 0 ? (int) path[0].y : System.Convert.ToInt32(transform.position.y * -1 + 4.5);

      playerMapX = System.Convert.ToInt32(player.transform.position.x + 9.5);
      playerMapY = System.Convert.ToInt32(player.transform.position.y * -1 + 4.5);

      List<Vector3> tmpPath = astar.getPath(mapX, mapY, playerMapX, playerMapY, map, jumpables);
      if (tmpPath != null && tmpPath.Count > 0)
        path = tmpPath;
    }

    if (path != null && path.Count > 0)
      moveEnemy();
  }
}
