using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public string previous_side_ver = "S"; // {back,front} => {W,S}
    public string previous_side_hori = "S";// {right,left} => {D,A}

    public StateManager stateManager;
    public void SetManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void SetSide(string hori, string ver)
    {
        previous_side_hori = hori;
        previous_side_ver = ver;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public virtual void OnTriggerEnter() // Haven't use yet
    {

    }

    public virtual void OnCollision(Collider2D collider) // Haven't use yet
    {

    }

    public void setValueAndPlay(string nameOfState)  // Just use for Blend tree - Not inChamber
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (stateManager.spriteRenderer.flipX == true) stateManager.spriteRenderer.flipX = false;
            previous_side_hori = "D"; // Right
        }
        if (Input.GetKeyUp(KeyCode.D) && stateManager.isInChamber == true)
        {
            if (stateManager.spriteRenderer.flipX == true) stateManager.spriteRenderer.flipX = false;
            previous_side_hori = "S"; // Right
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stateManager.spriteRenderer.flipX == false) stateManager.spriteRenderer.flipX = true;
            previous_side_hori = "A"; // Leftt
        }
        if (Input.GetKeyUp(KeyCode.A) && stateManager.isInChamber == true)
        {
            if (stateManager.spriteRenderer.flipX == true) stateManager.spriteRenderer.flipX = false;
            previous_side_hori = "S"; // Right
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (!Input.GetKeyDown(KeyCode.D)) && !Input.GetKeyDown(KeyCode.A)) previous_side_hori = "S";

        if (Input.GetKeyDown(KeyCode.W)) previous_side_ver = "W";
        else if (Input.GetKeyDown(KeyCode.S)) previous_side_ver = "S";

        stateManager.animator.SetFloat("hori", ((previous_side_hori == "D" || previous_side_hori == "A") ? 1f : 0f));
        stateManager.animator.SetFloat("ver", (previous_side_ver == "W" ? 1f : 0f));
        stateManager.animator.Play(nameOfState);
    }

    public void SetValueAndPlayInChamber(string nameOfState)
    {
        bool right = stateManager.mousePos.x > stateManager.gameObject.transform.position.x + 0.5f;
        bool left = stateManager.mousePos.x < stateManager.gameObject.transform.position.x - 0.5f;
        bool up = stateManager.mousePos.y > stateManager.gameObject.transform.position.y + 0.5f;
        bool down = stateManager.mousePos.y < stateManager.gameObject.transform.position.y - 0.5f;
        if (right)
        {
            if (stateManager.spriteRenderer.flipX == true) stateManager.spriteRenderer.flipX = false;
            previous_side_hori = "D"; // Right
        }
        if (left)
        {
            if (stateManager.spriteRenderer.flipX == false) stateManager.spriteRenderer.flipX = true;
            previous_side_hori = "A"; // Leftt
        }

        if(up || down && (!right && !left)) previous_side_hori = "S";

        if (up) previous_side_ver = "W";
        else if (down) previous_side_ver = "S";

        stateManager.animator.SetFloat("hori", ((previous_side_hori == "D" || previous_side_hori == "A") ? 1f : 0f));
        stateManager.animator.SetFloat("ver", (previous_side_ver == "W" ? 1f : 0f));
        stateManager.animator.Play(nameOfState);
    }
}
