using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject {

    public TileBase tile;
    public BlockType type;
    public GameObject collideEffect;

    private GameManager gm;
    private Vector3Int blockPos;

    public void Activate(Vector3Int pos) {
        gm = GameManager.instance;
        blockPos = pos;
        if (gm == null)
            return;

        switch (type) {
            case BlockType.Default:
                Default();
                break;
            case BlockType.Red:
                Red();
                break;
            case BlockType.Spring:
                Spring();
                break;
            case BlockType.Powerup:
                Powerup();
                break;
            case BlockType.Spikes:
                Spike();
                break;

        }
    }

    private void Default() {
        // do nothing, trigger animation later
        Debug.Log("Nothing to see here");
    }

    private void Red() {
        // same
        Debug.Log("Nothing not red to see here");
    }

    private void Spring() {
        // affect Player's rigidbody
        Debug.Log("Boing");
        Rigidbody2D rigid = gm.player.GetComponent<Rigidbody2D>();
        Vector3 dir = (gm.player.transform.position - blockPos).normalized;
        rigid.AddForce(dir * 8, ForceMode2D.Impulse);
    }

    private void Powerup() {
        // affect Player's movement/health
        Debug.Log("Noice");
    }

    private void Spike() {
        // affect Player's health
        Debug.Log("OUch");
        Health player = gm.player.GetComponent<Health>();
        player.TakeDamage(10);
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        Vector3 dir = (player.transform.position - blockPos).normalized;
        rigid.AddForce(dir * 3, ForceMode2D.Impulse);
    }
}
